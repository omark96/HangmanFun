using Hangman;
using HangmanFun.Graphics;
using HangmanFun.Sound;

namespace HangmanFun.Scenes;

internal abstract class Scene
{
    public GameData Data { get; protected set; }
    public AudioPlayer AudioPlayer { get; protected set; }
    public Renderer Renderer { get; protected set; }

    protected Scene(GameData data, AudioPlayer audioPlayer, Renderer renderer)
    {
        Data = data;
        AudioPlayer = audioPlayer;
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