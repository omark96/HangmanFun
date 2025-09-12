using System.Numerics;

namespace Hangman;

internal class Game
{
    Scene _activeScene;
    GlyphBuffer _glyphBuffer;
    int _tick;

    public Game()
    {
        _activeScene = new TitleScreen();
        _glyphBuffer = new GlyphBuffer(50, 20);

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

            _activeScene.Update(_tick);
            _activeScene.Draw(_glyphBuffer);
            Draw();
            _glyphBuffer.Clear();
            _tick++;
            //Console.Beep(300, 200);
            //Console.Beep();
            System.Threading.Thread.Sleep(100);
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
                _dynamicBodies.Add(body);
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
        if (bodyA.Position.X + bodyA.Size.X < bodyB.Position.X &&
            bodyA.Position.X < bodyB.Position.X + bodyB.Size.X &&
            bodyA.Position.Y + bodyA.Size.Y < bodyB.Position.Y &&
            bodyA.Position.Y < bodyB.Position.Y + bodyB.Size.Y)
        {
            return new Collision(bodyA, bodyB);
        }
        return null;

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
    public BodyType Type { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public Vector2 Speed { get; set; }

    public Body(BodyType type, Vector2 position, Vector2 size)
    {
        Type = type;
        Position = position;
        Size = size;
        Speed = Vector2.Zero;
    }

    public void Move()
    {
        Position += Speed;
        if (Type == BodyType.Kinematic)
        {
            Speed = Vector2.Zero;
        }
    }
}