using Hangman;
using HangmanFun.Graphics;

namespace HangmanFun.Scenes;

internal class GameOver : Scene
{
    internal override GameData Data { get; set; }

    public GameOver(GameData data)
    {
        Data = data;
    }

    internal override void Draw(Renderer renderer)
    {
        string winLoseText;
        if (Data.Won)
        {
            winLoseText = $"{Data.Name}, you won!";
        }
        else
        {
            winLoseText = $"{Data.Name}, you lost!";
        }
        string guesses = $"You made a total of {Data.TotalGuesses} guesses!";
        winLoseText = winLoseText.PadLeft(winLoseText.Length + (guesses.Length - winLoseText.Length) / 2);
        //winLoseText = String.Concat(winLoseText, "\n\n", guesses);
        winLoseText = $"{winLoseText}\n\n{guesses}";
        int width = renderer.TextWidth(winLoseText);
        renderer.CenterCameraX();
        renderer.DrawText(-width / 2, 8, winLoseText, new Color(255, 255, 255));
    }

    //internal string CenterAlignText() { }

    internal override GameScene Update(ConsoleKeyInfo input)
    {
        if (input.Key == ConsoleKey.None)
        {
            return GameScene.None;
        }
        else
        {
            Data.Reset("B");
            return GameScene.MainScene;
        }
    }
}