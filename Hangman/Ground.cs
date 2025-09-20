using System.Numerics;

namespace Hangman;

internal class Ground
{
    public Sprite Sprite { get; set; }
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
        int width = 80;
        Vector2 position = new((float)x, (float)y);
        Vector2 size = new(width, 10);
        string texture = new string('T', width);
        Sprite = new Sprite(0, 0, texture);
        _body = new Body(BodyType.Static, position, size, "ground", this);
        world.AddBody(_body);
    }
}
