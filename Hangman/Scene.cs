namespace Hangman;

internal abstract class Scene
{
    //public abstract void Init();
    public abstract void Update(int tick);
    public abstract void Draw(GlyphBuffer textBuffer);
}
