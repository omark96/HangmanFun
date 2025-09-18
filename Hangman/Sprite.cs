namespace Hangman;

internal class Sprite
{
    public int X { get; set; }
    public int Y { get; set; }
    public string Texture { get; set; }
    public (byte, byte, byte) FgColor { get; set; }
    public (byte, byte, byte) BgColor { get; set; }

    public Sprite(int x, int y, string? texture, (byte, byte, byte) bgColor, (byte, byte, byte) fgColor)
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
        Texture = texture ?? "";
        FgColor = (255, 255, 255);
        BgColor = (0, 0, 0);

    }

    public void Draw(GlyphBuffer buffer, int x, int y)
    {
        int row = y + Y;
        //foreach (string line in Texture.Split('\n'))
        //{
        //    int col = x + X;
        //    foreach (char c in line)
        //    {
        //        if (c != '\0')
        //        {
        //            char[] glyph = [.. CharToGlyph(c)];
        //            Array.Copy(glyph, 0, buffer, )
        //        }
        //        col++;
        //    }
        //    row++;
        //    //int col = x + X;
        //    //foreach (char c in line)
        //    //{
        //    //    if (c != '\0')
        //    //    {
        //    //        int pos = row * buffer.Width + col;
        //    //        buffer.Buffer[pos].Character = c;
        //    //    }
        //    //    col++;
        //    //}
        //    //row++;

        //}
        string[] lines = Texture.Split('\n');
        for (int j = 0; j < lines.Length; j++)
        {
            row = y + Y + j;
            for (int i = 0; i < lines[j].Length; i++)
            {
                int col = x + X + i;
                int pos = col * buffer.GlyphWidth + row * buffer.LineWidth;
                char c = lines[j][i];
                if (c != '\0')
                {
                    char[] glyph = [.. CharToGlyph(c)];
                    Array.Copy(glyph, 0, buffer.Buffer, pos, glyph.Length);
                }
            }
        }
    }

    string CharToGlyph(char c)
    {
        return $"\x1b[48;2;{BgColor.Item1:D3};{BgColor.Item2:D3};{BgColor.Item3:D3}m\x1b[38;2;{FgColor.Item1:D3};{FgColor.Item2:D3};{FgColor.Item3:D3}m{c}";
    }

    public int Width => Texture.Split("\n").Max(line => line.Length);
    public int Height => Texture.Split("\n").Length;
}
