using System.Numerics;

namespace Hangman;

internal class LetterBox : ICollidable
{
    Body _body;
    public char Letter { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public Sprite Sprite { get; set; }
    public BoxState State { get; set; }

    public LetterBox(World world, int x, int y, char letter)
    {
        Random rand = new();
        X = x;
        Y = y;
        Letter = letter;
        Vector2 size = new(5, 3);
        Vector2 position = new(x, y);
        State = BoxState.Default;
        Color fgColor = StateColor();
        Color bgColor = new Color(0, 0, 0);
        Sprite = new Sprite(0, 0, $"▛▀▀▀▜\n▌ {letter.ToString()} ▐\n▙▄▄▄▟", bgColor, fgColor);
        _body = new(BodyType.Static, position, size, "letterbox", this);
        world.AddBody(_body);
    }
    void ICollidable.OnCollision(Body other)
    {
        //State = BoxState.Selected;
        if (other.Name == "player")
        {
            if (State == BoxState.Default)
            {
                State = BoxState.Selected;
                Sprite.FgColor = StateColor();
            }
        }
    }
    public Color StateColor()
    {
        switch (State)
        {
            case BoxState.Default:
                return new Color(255, 255, 255);
            case BoxState.Selected:
                return new Color(200, 200, 0);
            case BoxState.Correct:
                return new Color(0, 230, 0);
            case BoxState.Wrong:
                return new Color(255, 0, 0);
            default:
                return new Color(255, 255, 255);
        }
    }
}

public enum BoxState
{
    Default,
    Selected,
    Correct,
    Wrong
}