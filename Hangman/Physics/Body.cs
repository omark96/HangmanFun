using System.Numerics;

namespace HangmanFun.Physics;

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
            collidable2.OnCollision(this);
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
