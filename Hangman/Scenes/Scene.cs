using Hangman;
using HangmanFun.Graphics;

namespace HangmanFun.Scenes;

internal abstract class Scene
{
    public GameData Data { get; protected set; }
    public Renderer Renderer { get; protected set; }

    protected Scene(GameData data, Renderer renderer)
    {
        Data = data;
        Renderer = renderer;
    }

    internal abstract GameScene Update(ConsoleKeyInfo input);
    internal abstract void Draw();

}

internal enum GameScene
{
    None,
    TitleScene,
    MainScene,
    GameOverScene
}