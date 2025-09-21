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