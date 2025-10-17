namespace HangmanFun.Graphics;

internal record struct Color
{
    public byte R { get; }
    public byte G { get; }
    public byte B { get; }
    public Color(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
    }

    public Color(byte v)
    {
        R = v;
        G = v;
        B = v;
    }

    public static readonly Color Black = new Color(0);
    public static readonly Color White = new Color(255);
    public static readonly Color LightGray = new Color(180);
    public static readonly Color DarkGray = new Color(80);
    public static readonly Color Red = new Color(255, 0, 0);
    public static readonly Color Green = new Color(0, 255, 0);
    public static readonly Color Blue = new Color(0, 0, 255);
    public static readonly Color Yellow = new Color(200, 200, 0);

}