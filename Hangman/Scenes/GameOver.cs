using Hangman;
using HangmanFun.Graphics;
using HangmanFun.Sound;

namespace HangmanFun.Scenes;

internal class GameOver : Scene
{

    public GameOver(GameData data, AudioPlayer audioPlayer, Renderer renderer) : base(data, audioPlayer, renderer) { }

    internal override void Draw()
    {
        Renderer.CameraX = 0;
        string winLoseText;
        if (Data.Won)
        {
            winLoseText = $"Congratulations {Data.Name}, you won!";
        }
        else
        {
            winLoseText = $"Too bad, {Data.Name}, you lost!";
        }
        int winLoseY = 6;
        Renderer.DrawCenteredText(winLoseY, winLoseText, Color.White);

        string guessesText = $"You made a total of {Data.TotalGuesses} guesses!";
        int guessesY = winLoseY + 2;
        Renderer.DrawCenteredText(guessesY, guessesText, Color.White);

        string wordText = $"The secret word was: {Data.SecretWord.ToUpper()}";
        int wordY = guessesY + 2;
        Renderer.DrawCenteredText(wordY, wordText, Color.White);

        string restartText = "Press ENTER to play again!";
        int restartY = wordY + 4;
        Renderer.DrawCenteredText(restartY, restartText, Color.Yellow);




        //winLoseText = winLoseText.PadLeft(winLoseText.Length + (guesses.Length - winLoseText.Length) / 2);
        ////winLoseText = String.Concat(winLoseText, "\n\n", guesses);
        //winLoseText = $"{winLoseText}\n\n{guesses}";
        //int width = Renderer.TextWidth(winLoseText);
        //Renderer.CenterCameraX();
        //Renderer.DrawText(-width / 2, 8, winLoseText, new Color(255, 255, 255));
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