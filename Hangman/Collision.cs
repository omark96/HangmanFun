namespace Hangman;

internal interface ICollidable
{
    public void OnCollision(Body other);
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
