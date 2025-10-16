
using Hangman;
using HangmanFun.Graphics;
using HangmanFun.Sound;

namespace HangmanFun.Scenes;

internal class Title : Scene
{

    public Title(GameData data, AudioPlayer audioPlayer, Renderer renderer) : base(data, audioPlayer, renderer) { }

    internal override void Draw()
    {
        Renderer.CenterCameraX();
        string askForName = "What's your name?";
        Renderer.DrawText(-askForName.Length / 2, 10, askForName, Color.White);
        Renderer.DrawBox(-askForName.Length / 2 + 1, 12, 15, 3, Color.Black, Color.White);
        Renderer.DrawText(-5, 13, Data.Name, Color.White);
        if ((Data.Tick / 10) % 2 == 0)
        {
            Renderer.DrawText(-5 + Data.Name.Length, 13, "_", Color.White);
        }
    }

    internal override GameScene Update(ConsoleKeyInfo input)
    {

        char c = input.KeyChar;
        if (char.IsLetterOrDigit(c))
        {
            Data.Name += c;
        }
        if (input.Key == ConsoleKey.Backspace && Data.Name.Length > 0)
        {
            Data.Name = Data.Name.Substring(0, Data.Name.Length - 1);
        }
        if (input.Key == ConsoleKey.Enter && Data.Name.Length >= 2)
        {
            return GameScene.MainScene;
        }
        return GameScene.None;
    }
}