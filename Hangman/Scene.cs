namespace Hangman;

internal abstract class Scene
{
    public required GameData Data { get; set; }
    public abstract void Update(ConsoleKey? input);
    public abstract void Draw(Renderer renderer);
}
