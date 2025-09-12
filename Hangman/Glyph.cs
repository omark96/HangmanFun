namespace Hangman;

internal class Glyph : IEquatable<Glyph>
{
    public ConsoleColor Foreground { get; set; }
    public ConsoleColor Background { get; set; }
    public char Character { get; set; }

    public Glyph() : this(ConsoleColor.White, ConsoleColor.Black, ' ') { }
    public Glyph(ConsoleColor foreground, ConsoleColor background, char character)
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

        if (Object.ReferenceEquals(this, other))
        {
            return true;
        }

        if (this.GetType() != other.GetType())
        {
            return false;
        }
        return (this.Character == other.Character) && (this.Foreground == other.Foreground) && (this.Background == other.Background);
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