namespace Hangman;

internal abstract class Scene
{
    internal abstract GameData Data { get; set; }
    internal abstract GameScene Update(ConsoleKeyInfo input);
    internal abstract void Draw(Renderer renderer);
}

internal enum GameScene
{
    None,
    TitleScene,
    MainScene,
    GameOverScene
}