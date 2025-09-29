
using Hangman;
using HangmanFun.Graphics;

namespace HangmanFun.Scenes;

internal class Title : Scene
{
    internal override GameData Data { get; set; }

    public Title(GameData data)
    {
        Data = data;
    }

    internal override void Draw(Renderer renderer)
    {
        renderer.CenterCameraX();
        string askForName = "What's your name?";
        renderer.DrawText(-askForName.Length / 2, 10, askForName, Color.White);
        renderer.DrawBox(-askForName.Length / 2 + 1, 12, 15, 3, Color.Black, Color.White);
        renderer.DrawText(-5, 13, Data.Name, Color.White);
        if ((Data.Tick / 10) % 2 == 0)
        {
            renderer.DrawText(-5 + Data.Name.Length, 13, "_", Color.White);
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