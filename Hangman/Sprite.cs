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

    //string CharToGlyph(char c)
    //{
    //    return $"\x1b[48;2;{BgColor.Item1:D3};{BgColor.Item2:D3};{BgColor.Item3:D3}m\x1b[38;2;{FgColor.Item1:D3};{FgColor.Item2:D3};{FgColor.Item3:D3}m{c}";
    //}

    public int Width => Texture.Split("\n").Max(line => line.Length);
    public int Height => Texture.Split("\n").Length;
}
