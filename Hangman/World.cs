
namespace Hangman;

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

    internal void Update()
    {
        Tick();
        HandleCollisions();
        ResetSpeedOfKinematicBodies();
    }
}
