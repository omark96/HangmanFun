namespace Hangman;

internal abstract class Scene
{
    public abstract GameData Data { get; set; }
    public abstract GameScene Update(ConsoleKey? input);
    public abstract void Draw(Renderer renderer);
}

internal enum GameScene
{
    None,
    TitleScene,
    MainScene,
    GameOverScene
}