using Hangman;
using HangmanFun.Graphics;
using HangmanFun.Sound;

namespace HangmanFun.Scenes;

internal class GameOver : Scene
{

    public GameOver(GameData data, AudioPlayer audioPlayer, Renderer renderer) : base(data, audioPlayer, renderer) { }

    internal override void Draw()
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
        int width = Renderer.TextWidth(winLoseText);
        Renderer.CenterCameraX();
        Renderer.DrawText(-width / 2, 8, winLoseText, new Color(255, 255, 255));
    }

    //internal string CenterAlignText() { }

    internal override GameScene Update(ConsoleKeyInfo input)
    {
        if (input.Key == ConsoleKey.Enter)
        {
            return GameScene.MainScene;
        }
        return GameScene.None;
    }
}