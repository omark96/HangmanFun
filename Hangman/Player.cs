using System.Numerics;

namespace Hangman;

internal class Player : ICollidable
{
    Sprite _sprite;
    Body _body;
    bool _inAir;
    Vector2 _speed;
    public int X
    {
        get
        {
            return (int)Math.Round(_body.Position.X);
        }
    }
    public int Y
    {
        get
        {
            return (int)Math.Round(_body.Position.Y);
        }
    }

    public Player(World world, int x, int y)
    {
        Vector2 position = new((float)x, (float)y);
        Vector2 size = new(1, 3);
        _speed = new Vector2(0, 0);
        _inAir = false;
        _sprite = new Sprite(-1, 0, "\0o\n/|\\\n/\0\\");
        _body = new Body(BodyType.Kinematic, position, size, "player", this);
        world.AddBody(_body);
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
        else
        {
            _body.Speed = _speed;
        }
    }
    public void Jump()
    {
        _inAir = true;
        _speed = new(0, -0.7f);
    }

    public void Update(ConsoleKey? input)
    {
        if (!_inAir)
        {
            if (input == ConsoleKey.LeftArrow)
            {
                Move(-1);
            }
            else if (input == ConsoleKey.RightArrow)
            {
                Move(1);
            }
            else if (input == ConsoleKey.UpArrow)
            {
                Jump();
            }
        }
        else
        {
            _speed += new Vector2(0, 0.07f);
            _body.Speed = _speed;
        }
    }

    public void OnCollision(Body other)
    {
        if (other.Name == "ground")
        {
            _inAir = false;
        }
    }
}
