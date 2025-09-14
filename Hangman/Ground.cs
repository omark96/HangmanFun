using System.Numerics;

namespace Hangman;

internal class Ground
{
    Sprite _sprite;
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
        int width = 40;
        Vector2 position = new((float)x, (float)y);
        Vector2 size = new(width, 5);
        string texture = new string('‾', width);
        _sprite = new Sprite(0, 0, texture);
        _body = new Body(BodyType.Kinematic, position, size, "ground");
        world.AddBody(_body);
    }

    public void Draw(GlyphBuffer buffer)
    {
        _sprite.Draw(buffer, X, Y);
    }
}
