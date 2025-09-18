namespace Hangman;

internal class GlyphBuffer
{

    public int Width { get; set; }
    public int Height { get; set; }
    public char[] Buffer { get; set; }
    public int GlyphWidth = 39;
    public int LineWidth
    {
        get { return Width * GlyphWidth + 1; }
    }
    //public Glyph[] PrevBuffer { get; set; }

    public GlyphBuffer(int width, int height)
    {
        Width = width;
        Height = height;
        Buffer = new char[LineWidth * Height];
        //PrevBuffer = new Glyph[width * height];
        Clear();
    }

    public void Clear()
    {

        string blank = "\x1b[48;2;000;000;000m\x1b[38;2;000;000;000m ";
        char[] blankArray = blank.ToArray();
        char[] blankRow = new char[LineWidth];
        for (int i = 0; i < Width; i++)
        {
            Array.Copy(blankArray, 0, blankRow, i * blankArray.Length, blankArray.Length);
        }
        blankRow[blankRow.Length - 1] = '\n';
        for (int i = 0; i < Height; i++)
        {
            Array.Copy(blankRow, 0, Buffer, i * blankRow.Length, blankRow.Length);
        }
    }
}
