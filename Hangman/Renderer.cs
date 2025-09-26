using System.Text;

namespace Hangman;

internal class Renderer
{
    public GlyphBuffer Buffer { get; set; }
    public int CameraX { get; set; }
    public int CameraY { get; set; }
    public Renderer(int width, int height)
    {
        Buffer = new GlyphBuffer(width, height);
        Buffer.Clear();
        CameraX = 5;
        CameraY = 0;
    }

    public void DrawSprite(Sprite sprite, int x, int y)
    {
        string[] lines = sprite.Texture.Split('\n');
        for (int j = 0; j < lines.Length; j++)
        {
            int yPos = sprite.Y + y + j - CameraY;
            if (yPos < 0 || yPos >= Buffer.Height)
            {
                continue;
            }
            for (int i = 0; i < lines[j].Length; i++)
            {
                char c = lines[j][i];
                if (c != '\0')
                {
                    int xPos = sprite.X + x - CameraX + i;
                    if (xPos < 0 || xPos >= Buffer.Width)
                    {
                        continue;
                    }
                    int pos = xPos + yPos * Buffer.Width;
                    Glyph glyph = Buffer.Glyphs[pos];
                    glyph.Character = c;
                    if (sprite.BgColor != null)
                    {
                        glyph.Background = (Color)sprite.BgColor;
                    }
                    if (sprite.FgColor != null)
                    {
                        glyph.Foreground = (Color)sprite.FgColor;
                    }
                }
            }
        }
    }

    public void DrawText(int x, int y, string text, Color color)
    {
        string[] lines = text.Split('\n');
        for (int j = 0; j < lines.Length; j++)
        {
            int yPos = y + j - CameraY;
            if (yPos < 0 || yPos >= Buffer.Height)
            {
                continue;
            }
            for (int i = 0; i < lines[j].Length; i++)
            {
                char c = lines[j][i];
                if (c != '\0')
                {
                    int xPos = x - CameraX + i;
                    if (xPos < 0 || xPos >= Buffer.Width)
                    {
                        continue;
                    }
                    int pos = xPos + yPos * Buffer.Width;
                    Glyph glyph = Buffer.Glyphs[pos];
                    glyph.Character = c;

                    glyph.Foreground = color;
                }
            }
        }
    }

    public void DrawRectangle(int x, int y, int width, int height, Color color)
    {
        for (int j = 0; j < height; j++)
        {
            int yPos = y + j - CameraY;
            if (yPos < 0 || yPos >= Buffer.Height)
            {
                continue;
            }
            for (int i = 0; i < width; i++)
            {
                int xPos = x + i - CameraX;
                if (xPos < 0 || xPos >= Buffer.Width)
                {
                    continue;
                }
                int pos = xPos + yPos * Buffer.Width;
                Glyph glyph = Buffer.Glyphs[pos];
                glyph.Character = ' ';
                glyph.Background = color;
            }
        }
    }
    public void DrawBox(int x, int y, int width, int height, Color bgColor, Color fgColor)
    {
        DrawRectangle(x, y, width, height, bgColor);
        DrawRectangleOutline(x, y, width, height, fgColor);
    }
    public void DrawTextBox(int x, int y, string text, Color bgColor, Color fgColor)
    {
        string[] lines = text.Split('\n');
        int height = lines.Length + 2;
        int width = 0;
        foreach (string line in lines)
        {
            if (line.Length > width)
            {
                width = line.Length;
            }
        }
        width += 2;
        DrawBox(x, y, width, height, bgColor, fgColor);
        DrawText(x + 1, y + 1, text, fgColor);
    }
    public void DrawRectangleOutline(int x, int y, int width, int height, Color color)
    {
        //int xPos = x - CameraX;
        //int yPos = y - CameraY;
        //int topLeft = xPos + (yPos) * Buffer.Width;
        //int topRight = topLeft + width - 1;
        //int bottomLeft = xPos + (yPos + height - 1) * Buffer.Width;
        //int bottomRight = bottomLeft + width - 1;
        //Buffer.Glyphs[topLeft].Character = '▛';
        //Buffer.Glyphs[topLeft].Foreground = color;
        //Buffer.Glyphs[topRight].Character = '▜';
        //Buffer.Glyphs[topRight].Foreground = color;
        //Buffer.Glyphs[bottomLeft].Character = '▙';
        //Buffer.Glyphs[bottomLeft].Foreground = color;
        //Buffer.Glyphs[bottomRight].Character = '▟';
        //Buffer.Glyphs[bottomRight].Foreground = color;
        //for (int i = 1; i < width - 1; i++)
        //{
        //    Buffer.Glyphs[topLeft + i].Character = '▀';
        //    Buffer.Glyphs[topLeft + i].Foreground = color;
        //    Buffer.Glyphs[bottomLeft + i].Character = '▄';
        //    Buffer.Glyphs[bottomLeft + i].Foreground = color;
        //}
        //for (int i = 1; i < height - 1; i++)
        //{
        //    Buffer.Glyphs[topLeft + i * Buffer.Width].Character = '▌';
        //    Buffer.Glyphs[topLeft + i * Buffer.Width].Foreground = color;
        //    Buffer.Glyphs[topRight + i * Buffer.Width].Character = '▐';
        //    Buffer.Glyphs[topRight + i * Buffer.Width].Foreground = color;
        //}
        StringBuilder sb = new();
        sb.Append('▛');
        sb.Append('▀', width - 2);
        sb.Append('▜');
        sb.Append('\n');
        StringBuilder midSb = new();
        midSb.Append('▌');
        midSb.Append(' ', width - 2);
        midSb.Append('▐');
        midSb.Append('\n');
        for (int i = 0; i < height - 2; i++)
        {
            sb.Append(midSb);
        }
        sb.Append('▙');
        sb.Append('▄', width - 2);
        sb.Append('▟');
        string box = sb.ToString();
        DrawText(x, y, box, color);
    }
    public void Draw()
    {
        Console.SetCursorPosition(0, 0);
        StringBuilder sb = new(Buffer.Width * Buffer.Height * 39 + Buffer.Height);

        sb.AppendFormat($"\x1b[48;2;{Buffer.Glyphs[0].Background.R};{Buffer.Glyphs[0].Background.G};{Buffer.Glyphs[0].Background.B}m");
        sb.AppendFormat($"\u001b[38;2;{Buffer.Glyphs[0].Foreground.R};{Buffer.Glyphs[0].Foreground.G};{Buffer.Glyphs[0].Foreground.B}m");
        sb.Append(Buffer.Glyphs[0].Character);

        for (int i = 1; i < Buffer.Glyphs.Length; i++)
        {
            if (i % Buffer.Width == 0)
            {
                sb.Append('\n');
            }
            Glyph glyph = Buffer.Glyphs[i];
            Glyph prevGlyph = Buffer.Glyphs[i - 1];
            if (glyph.Background != prevGlyph.Background)
            {
                sb.AppendFormat($"\x1b[48;2;{glyph.Background.R};{glyph.Background.G};{glyph.Background.B}m");
            }
            if (glyph.Foreground != prevGlyph.Foreground)
            {
                sb.AppendFormat($"\u001b[38;2;{glyph.Foreground.R};{glyph.Foreground.G};{glyph.Foreground.B}m");
            }
            sb.Append(glyph.Character);
        }
        string finalResult = sb.ToString();
        Console.Write(sb.ToString());
    }
}