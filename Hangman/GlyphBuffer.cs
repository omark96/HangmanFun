namespace Hangman;

internal class GlyphBuffer
{

    public int Width { get; set; }
    public int Height { get; set; }
    public Glyph[] Buffer { get; set; }
    public Glyph[] PrevBuffer { get; set; }

    public GlyphBuffer(int width, int height)
    {
        Width = width;
        Height = height;
        Buffer = new Glyph[width * height];
        PrevBuffer = new Glyph[width * height];
        for (int i = 0; i < Buffer.Length; i++)
        {
            Buffer[i] = new Glyph();
            PrevBuffer[i] = new Glyph();
        }
    }

    public void Clear()
    {
        var temp = Buffer;
        Buffer = PrevBuffer;
        PrevBuffer = temp;
        for (int i = 0; i < Buffer.Length; i++)
        {
            Buffer[i].Character = ' ';
            Buffer[i].Background = ConsoleColor.Black;
            Buffer[i].Foreground = ConsoleColor.White;
        }
    }
}
