using System.Numerics;

namespace Hangman;

internal class Ground
{
    Body _body;
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

    public Ground(World world, int x, int y)
    {
        int width = 200;
        Vector2 position = new((float)x, (float)y);
        Vector2 size = new(width, 10);
        _body = new Body(BodyType.Static, position, size, "ground", this);
        world.AddBody(_body);
    }

    internal void Draw(Renderer renderer)
    {
        renderer.DrawRectangle(X, Y, (int)_body.Size.X, (int)_body.Size.Y, Color.White);
    }
}
