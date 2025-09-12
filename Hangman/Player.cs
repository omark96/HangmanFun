using System.Numerics;

namespace Hangman;

internal class Player
{
    Sprite _sprite;
    Body _body;
    bool _inAir;
    public int X { get; }
    public int Y { get; }

    public Player(int x, int y)
    {
        Vector2 position = new(0, 0);
        Vector2 size = new(3, 0);
        _inAir = false;
        _sprite = new Sprite(0, 0, "\0o\n/|\\\n/\0\\");
        _body = new Body(BodyType.Kinematic, position, size);
    }

    public void Draw(GlyphBuffer buffer)
    {
        _sprite.Draw(buffer, X, Y);
    }

    public void Move(int x)
    {
        if (!_inAir)
        {
            _body.Speed = new(Convert.ToSingle(x), 0f);
        }
    }
    public void Jump()
    {
        _body.Speed = new(0, 3);
    }

}
