namespace Hangman;

internal class Sprite
{
    public int X { get; set; }
    public int Y { get; set; }
    public string Texture { get; set; }
    public Color? FgColor { get; set; }
    public Color? BgColor { get; set; }

    public Sprite(int x, int y, string? texture, Color? bgColor, Color? fgColor)
    {
        X = x;
        Y = y;
        Texture = texture ?? "";
        FgColor = fgColor;
        BgColor = bgColor;
    }
    public Sprite(int x, int y, string? texture)
    {
        X = x;
        Y = y;
        Texture = texture ?? " ";
        FgColor = new Color(255, 255, 255);
        BgColor = new Color(0, 0, 0);

    }

    public void Draw(GlyphBuffer buffer, int x, int y)
    {
        string[] lines = Texture.Split('\n');
        for (int j = 0; j < lines.Length; j++)
        {
            int yPos = Y + y + j;
            for (int i = 0; i < lines[j].Length; i++)
            {
                char c = lines[j][i];
                if (c != '\0')
                {
                    int xPos = X + x + i;
                    int pos = xPos + yPos * buffer.Width;
                    Glyph glyph = buffer.Buffer[pos];
                    glyph.Character = c;
                    if (BgColor != null)
                    {
                        glyph.Background = (Color)BgColor;
                    }
                    if (FgColor != null)
                    {
                        glyph.Foreground = (Color)FgColor;
                    }
                }
            }
        }
    }

    //string CharToGlyph(char c)
    //{
    //    return $"\x1b[48;2;{BgColor.Item1:D3};{BgColor.Item2:D3};{BgColor.Item3:D3}m\x1b[38;2;{FgColor.Item1:D3};{FgColor.Item2:D3};{FgColor.Item3:D3}m{c}";
    //}

    public int Width => Texture.Split("\n").Max(line => line.Length);
    public int Height => Texture.Split("\n").Length;
}
