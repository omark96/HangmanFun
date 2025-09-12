namespace Hangman;

internal class Sprite
{
    public int X { get; set; }
    public int Y { get; set; }
    public string Texture { get; set; }

    public Sprite(int x, int y, string? texture)
    {
        X = x;
        Y = y;
        Texture = texture ?? "";
    }

    public void Draw(GlyphBuffer buffer, int x, int y)
    {
        int row = y + Y;
        foreach (string line in Texture.Split('\n'))
        {
            int col = x + X;
            foreach (char c in line)
            {
                if (c != '\0')
                {
                    int pos = row * buffer.Width + col;
                    buffer.Buffer[pos].Character = c;
                }
                col++;
            }
            row++;
        }
    }

    public int Width => Texture.Split("\n").Max(line => line.Length);
    public int Height => Texture.Split("\n").Length;
}
