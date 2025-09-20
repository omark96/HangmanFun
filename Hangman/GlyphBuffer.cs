namespace Hangman;

internal class GlyphBuffer
{

    public int Width { get; set; }
    public int Height { get; set; }
    public Glyph[] Glyphs { get; set; }
    //public Glyph[] PrevBuffer { get; set; }

    public GlyphBuffer(int width, int height)
    {
        Width = width;
        Height = height;
        Glyphs = new Glyph[Width * Height];
        //PrevBuffer = new Glyph[width * height];
        Clear();
    }

    public void Clear()
    {
        for (int i = 0; i < Glyphs.Length; i++)
        {
            Glyphs[i] = new Glyph();
        }
    }
}
