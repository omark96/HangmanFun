using System.Numerics;

namespace Hangman;

internal class Game
{
    MainScene _activeScene;
    GlyphBuffer _glyphBuffer;
    int _tick;

    public Game()
    {
        _activeScene = new MainScene();
        _glyphBuffer = new GlyphBuffer(150, 20);

        _tick = 0;
    }
    internal void Run()
    {
        Console.Clear();
        System.Threading.Thread.Sleep(500);
        Console.CursorVisible = false;
        Console.Clear();
        while (true)
        {
            _activeScene.Update(Input());
            _activeScene.Draw(_glyphBuffer);
            Draw();
            _glyphBuffer.Clear();
            _tick++;
            //Console.SetCursorPosition(0, 0);
            //Console.WriteLine(_activeScene._player._speed);
            //Console.Beep(300, 200);
            //Console.Beep();
            System.Threading.Thread.Sleep(50);
        }
    }

    internal void Draw()
    {

        for (int row = 0; row < _glyphBuffer.Height; row++)
        {
            for (int col = 0; col < _glyphBuffer.Width; col++)
            {
                int pos = row * _glyphBuffer.Width + col;
                char symbol = _glyphBuffer.Buffer[pos].Character;
                char prevSymbol = _glyphBuffer.PrevBuffer[pos].Character;
                if (symbol != prevSymbol)
                {
                    Console.SetCursorPosition(col, row);
                    Console.Write(symbol);
                }
            }
        }
    }

    internal ConsoleKey? Input()
    {
        ConsoleKey? input = null;
        if (Console.KeyAvailable)
        {
            //input = Console.ReadKey(false).Key;
            while (Console.KeyAvailable)
            {
                input = Console.ReadKey(false).Key;
            }
            Console.In.Close();
        }
        return input;
    }
}
public enum BodyType
{
    Static,
    //Dynamic, 
    Kinematic
}

internal class World
{
    List<Body> _staticBodies;
    List<Body> _kinematicBodies;
    List<Body> _dynamicBodies;


    public World()
    {
        _staticBodies = new List<Body>();
        _kinematicBodies = new List<Body>();
        _dynamicBodies = new List<Body>();

    }

    public void AddBody(Body body)
    {
        switch (body.Type)
        {
            case BodyType.Static:
                _staticBodies.Add(body);
                break;
            //case BodyType.Dynamic:
            //    _dynamicBodies.Add(body);
            //    break;
            case BodyType.Kinematic:
                _kinematicBodies.Add(body);
                break;
            default:
                break;
        }
    }

    public void HandleCollisions()
    {

        var collisions = DetectCollisions();
        foreach (var collision in collisions)
        {
            var bodyA = collision.BodyA;
            var bodyB = collision.BodyB;
            bodyA.OnCollision(bodyB);
        }
        return;
    }

    public List<Collision> DetectCollisions()
    {
        List<Collision> collisions = new();
        for (int i = 0; i < _kinematicBodies.Count; i++)
        {
            Body bodyA = _kinematicBodies[i];
            if (i < _kinematicBodies.Count)
            {
                for (int j = i + 1; j < _kinematicBodies.Count; j++)
                {
                    Body bodyB = _kinematicBodies[j];
                    Collision? collision = CheckForCollision(bodyA, bodyB);
                    if (collision != null)
                    {
                        collisions.Add(collision);
                    }
                }
            }
            foreach (Body bodyB in _staticBodies)
            {
                Collision? collision = CheckForCollision(bodyA, bodyB);
                if (collision != null)
                {
                    collisions.Add(collision);
                }
            }
        }

        return collisions;
    }
    public Collision? CheckForCollision(Body bodyA, Body bodyB)
    {
        if (bodyA.Position.X + bodyA.Size.X > bodyB.Position.X &&
            bodyA.Position.X < bodyB.Position.X + bodyB.Size.X &&
            bodyA.Position.Y + bodyA.Size.Y > bodyB.Position.Y &&
            bodyA.Position.Y < bodyB.Position.Y + bodyB.Size.Y)
        {
            return new Collision(bodyA, bodyB);
        }
        return null;

    }

    internal void Tick()
    {
        foreach (Body body in _kinematicBodies)
        {
            body.Move();
        }
    }

    internal void ResetSpeedOfKinematicBodies()
    {
        foreach (Body body in _kinematicBodies)
        {
            body.Speed = new(0, 0);
        }
    }
}

internal class Collision
{
    public Body BodyA { get; set; }
    public Body BodyB { get; set; }
    public Collision(Body a, Body b)
    {
        BodyA = a;
        BodyB = b;
    }
}

internal class Body
{
    public string Name { get; set; }
    public BodyType Type { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public Vector2 Speed { get; set; }
    public object Entity { get; set; }

    public Body(BodyType type, Vector2 position, Vector2 size, string name, object entity)
    {
        Type = type;
        Position = position;
        Size = size;
        Speed = Vector2.Zero;
        Name = name;
        Entity = entity;
    }

    public void Move()
    {
        Position += Speed;
    }

    public void OnCollision(Body other)
    {
        // TODO: Add support for other types of collision than Static-Kinematic
        if (Type == BodyType.Kinematic)
        {
            HandleKinematicCollision(other);
        }
        else if (Type == BodyType.Static)
        {
            HandleStaticCollision(other);
        }
        if (Entity is ICollidable collidable)
        {
            collidable.OnCollision(other);
        }
        if (other.Entity is ICollidable collidable2)
        {
            collidable2.OnCollision(other);
        }
    }

    private void HandleStaticCollision(Body other)
    {
        if (other.Type == BodyType.Kinematic)
        {
            other.HandleKinematicCollision(this);
        }
    }

    private void HandleKinematicCollision(Body other)
    {
        if (other.Type != BodyType.Static)
        {
            return;
        }
        MTV(other);
    }

    private void MTV(Body other)
    {
        if (Speed.Y > 0)
        {
            Position = new(Position.X, other.Position.Y - Size.Y);
        }
        else if (Speed.Y < 0)
        {
            Position = new(Position.X, other.Position.Y + other.Size.Y);
        }
        else if (Speed.X > 0)
        {
            Position = new(other.Position.X - Size.X, Position.Y);
        }
        else if (Speed.X < 0)
        {
            Position = new(other.Position.X + other.Size.X, Position.Y);
        }
    }
}

internal interface ICollidable
{
    public void OnCollision(Body other);
}