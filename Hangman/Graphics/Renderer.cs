using System.Text;

namespace HangmanFun.Graphics;

internal class Renderer
{
    public GlyphBuffer Buffer { get; set; }
    public int CameraX { get; set; }
    public int CameraTargetX { get; set; }
    public int CameraY { get; set; }
    public Renderer(int width, int height)
    {
        Buffer = new GlyphBuffer(width, height);
        Buffer.Clear();
        CameraX = 0;
        CameraY = 0;
    }

    internal void CenterCameraX()
    {
        CameraX = -Buffer.Width / 2;
    }
    internal void CenterCameraY()
    {
        CameraX = -Buffer.Height / 2;
    }

    internal void UpdateCamera()
    {
        if (CameraTargetX > CameraX)
        {
            CameraX++;
        }
        else if (CameraTargetX < CameraX)
        {
            CameraX--;
        }
    }

    internal int TextWidth(string text)
    {
        string[] lines = text.Split('\n');
        int width = 0;
        foreach (string line in lines)
        {
            width = Math.Max(width, line.Length);
        }
        return width;
    }

    internal void DrawText(int x, int y, string text, Color color)
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

    internal void DrawCenteredText(int y, string text, Color color)
    {
        int x = (Buffer.Width - text.Length) / 2;
        DrawText(x, y, text, color);
    }

    internal void DrawRectangle(int x, int y, int width, int height, Color color)
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
    internal void DrawBox(int x, int y, int width, int height, Color bgColor, Color fgColor)
    {
        DrawRectangle(x, y, width, height, bgColor);
        DrawRectangleOutline(x, y, width, height, fgColor);
    }
    internal void DrawTextBox(int x, int y, string text, Color bgColor, Color fgColor, Color borderColor)
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
        DrawBox(x, y, width, height, bgColor, borderColor);
        DrawText(x + 1, y + 1, text, fgColor);
    }
    internal void DrawHorizontalLine(int x, int y, int width, Color lineColor)
    {
        string line = new string('─', width);
        DrawText(x, y, line, lineColor);
    }
    internal void DrawRectangleOutline(int x, int y, int width, int height, Color color)
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
    internal void Draw()
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

    internal void DrawImage(int x, int y, Image img)
    {
        //int i = x < CameraX ? CameraX - x : 0;
        //int j = y - CameraX > 0 ? y : 0;
        //int width = x + img.Width < CameraX + Buffer.Width ? img.Width : Buffer.Width;
        //int height = y + img.Height < CameraY + Buffer.Height ? img.Height : Buffer.Height;
        for (int j = 0; j < img.Height; j++)
        {
            int yPos = y + j - CameraY;
            if (yPos < 0 || yPos >= Buffer.Height)
            {
                continue;
            }
            for (int i = 0; i < img.Width; i++)
            {
                int xPos = x + i - CameraX;
                if (xPos < 0 || xPos >= Buffer.Width)
                {
                    continue;
                }
                int pos = xPos + yPos * Buffer.Width;
                Glyph glyph = Buffer.Glyphs[pos];
                int imgPos = 4 * (i + j * img.Width);
                Color color = new Color(img.PixelData![imgPos + 2], img.PixelData[imgPos + 1], img.PixelData[imgPos]);
                glyph.Character = ' ';
                glyph.Background = color;
            }
        }
    }
}