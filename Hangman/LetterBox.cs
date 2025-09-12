namespace Hangman;

internal class LetterBox
{
    Sprite _sprite;
    public int X { get; set; }
    public int Y { get; set; }

    public LetterBox(int x, int y, char letter)
    {
        X = x;
        Y = y;
        _sprite = new Sprite(0, 0, letter.ToString());
    }

    public void Draw(GlyphBuffer buffer)
    {
        _sprite.Draw(buffer, X, Y);
    }
}
