namespace Hangman;

internal abstract class Scene
{
    //public abstract void Init();
    public abstract void Update(ConsoleKey? input);
    public abstract void Draw(Renderer renderer);
}
