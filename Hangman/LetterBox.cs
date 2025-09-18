using System.Numerics;

namespace Hangman;

internal class LetterBox : ICollidable
{
    Sprite _sprite;
    Body _body;
    char Letter { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public LetterBox(World world, int x, int y, char letter)
    {
        Random rand = new();
        X = x;
        Y = y;
        Letter = letter;
        Vector2 size = new(5, 3);
        Vector2 position = new(x, y);
        (byte, byte, byte) fgColor = ((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256));
        (byte, byte, byte) bgColor = ((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256));
        _sprite = new Sprite(0, 0, $"▛▀▀▀▜\n▌ {letter.ToString()} ▐\n▙▄▄▄▟", bgColor, fgColor);
        _body = new(BodyType.Static, position, size, "letterbox", this);
        world.AddBody(_body);
    }

    public void Draw(GlyphBuffer buffer)
    {
        _sprite.Draw(buffer, X, Y);
    }

    void ICollidable.OnCollision(Body other)
    {
        if (other.Name == "player")
        {

        }
    }
}