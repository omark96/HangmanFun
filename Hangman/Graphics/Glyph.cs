namespace HangmanFun.Graphics;

internal class Glyph : IEquatable<Glyph>
{
    public Color Foreground { get; set; }
    public Color Background { get; set; }
    public char Character { get; set; }

    public Glyph() : this(new Color(255, 255, 255), new Color(0, 0, 0), ' ') { }
    public Glyph(Color foreground, Color background, char character)
    {
        Foreground = foreground;
        Background = background;
        Character = character;
    }

    public bool Equals(Glyph? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (GetType() != other.GetType())
        {
            return false;
        }
        return Character == other.Character && Foreground == other.Foreground && Background == other.Background;
    }

    public override int GetHashCode() => (Foreground, Background, Character).GetHashCode();

    public static bool operator ==(Glyph lhs, Glyph rhs)
    {
        if (lhs is null)
        {
            if (rhs is null)
            {
                return true;
            }

            // Only the left side is null.
            return false;
        }
        // Equals handles case of null on right side.
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Glyph lhs, Glyph rhs) => !(lhs == rhs);

    public override bool Equals(object? obj) => Equals(obj as Glyph);
}
