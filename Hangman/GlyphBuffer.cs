using System.Text;

namespace Hangman;

internal class GlyphBuffer
{

    public int Width { get; set; }
    public int Height { get; set; }
    public Glyph[] Buffer { get; set; }
    //public Glyph[] PrevBuffer { get; set; }

    public GlyphBuffer(int width, int height)
    {
        Width = width;
        Height = height;
        Buffer = new Glyph[Width * Height];
        //PrevBuffer = new Glyph[width * height];
        Clear();
    }

    public void Clear()
    {
        for (int i = 0; i < Buffer.Length; i++)
        {
            Buffer[i] = new Glyph();
        }
    }

    internal void Draw()
    {
        Console.SetCursorPosition(0, 0);
        StringBuilder sb = new(Width * Height * 39 + Height);

        sb.AppendFormat($"\x1b[48;2;{Buffer[0].Background.R};{Buffer[0].Background.G};{Buffer[0].Background.B}m");
        sb.AppendFormat($"\u001b[38;2;{Buffer[0].Foreground.R};{Buffer[0].Foreground.G};{Buffer[0].Foreground.B}m");
        sb.Append(Buffer[0].Character);

        for (int i = 1; i < Buffer.Length; i++)
        {
            if (i % Width == 0)
            {
                sb.Append('\n');
            }
            Glyph glyph = Buffer[i];
            Glyph prevGlyph = Buffer[i - 1];
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
        //for (int row = 0; row < Height; row++)
        //{
        //    for (int col = 0; col < Width; col++)
        //    {
        //        int pos = row * Width + col;
        //Glyph glyph = Buffer[row * Width + col];
        //Glyph prevGlyph = Buffer[row * Width + col - 1];
        //if (glyph.Background != prevGlyph.Background)
        //{
        //    sb.AppendFormat($"\x1b[48;2;{glyph.Background.R};{glyph.Background.G};{glyph.Background.B}m");
        //}
        //if (glyph.Foreground != prevGlyph.Foreground)
        //{
        //    sb.AppendFormat($"\u001b[38;2;{glyph.Foreground.R};{glyph.Foreground.G};{glyph.Foreground.B}m");
        //}
        //sb.Append(glyph.Character);
        //    }
        //    sb.Append('\n');
        //}
        Console.Write(sb.ToString());
    }
}
