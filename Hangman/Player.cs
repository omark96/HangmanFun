using System.Numerics;

namespace Hangman;

internal class Player : ICollidable
{
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

    public bool IsAlive { get; internal set; }

    public Player(World world, int x, int y)
    {
        Vector2 position = new((float)x, (float)y);
        Vector2 size = new(1, 3);
        _speed = new Vector2(0, 0);
        _inAir = false;
        _body = new Body(BodyType.Kinematic, position, size, "player", this);
        IsAlive = true;
        world.AddBody(_body);
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

    public void Update(ConsoleKeyInfo? input)
    {
        if (!_inAir && input != null)
        {
            if (input.Value.Key == ConsoleKey.LeftArrow)
            {
                Move(-1);
            }
            else if (input.Value.Key == ConsoleKey.RightArrow)
            {
                Move(1);
            }
            else if (input.Value.Key == ConsoleKey.UpArrow)
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
        if (other.Name == "lava")
        {
            IsAlive = false;
        }
    }

    public void Draw(Renderer renderer)
    {
        // \0o\n/|\\\n/\0\\
        renderer.DrawText(X - 1, Y, "\0o\n/|\\\n/\0\\", Color.White);
    }
}
