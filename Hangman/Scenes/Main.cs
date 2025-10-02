
using Hangman;
using HangmanFun.Graphics;

namespace HangmanFun.Scenes;

internal class Main : Scene
{
    World _world;
    Player _player;
    Ground _ground;
    List<LetterBox> _letterBoxes;
    Lava _lava;
    Image _background;

    internal override GameData Data { get; set; }
    public Main(GameData data)
    {
        _world = new();
        _player = new(_world, 35, 11);
        _ground = new(_world, 0, 14);
        _letterBoxes = new();
        _lava = new Lava(_world, 0, 25);
        Data = data;
        //LetterBox letterBox = new(_world, 0, 0, 'A');
        //_letterBoxes.Add(letterBox);
        for (int i = 0; i < 26; i++)
        {
            char c = (char)(i + 65);
            LetterBox letterBox = new(_world, 20 + i * 6, 6, c);
            _letterBoxes.Add(letterBox);
        }
        _background = new();
        _background.LoadImage(@".\Assets\volcano.tga");
    }

    internal override GameScene Update(ConsoleKeyInfo input)
    {
        string secretWord = Data.SecretWord;
        char[] maskedWord = Data.MaskedWord;
        _player.Update(input);
        bool wrongGuess = false;
        foreach (LetterBox box in _letterBoxes)
        {
            if (box.State == BoxState.Selected)
            {
                Data.TotalGuesses++;
                char guess = char.ToUpper(box.Letter);
                for (int i = 0; i < secretWord.Length; i++)
                {
                    if (guess == char.ToUpper(secretWord[i]))
                    {
                        maskedWord[i] = guess;
                        box.State = BoxState.Correct;
                    }
                }
                if (box.State == BoxState.Selected)
                {
                    box.State = BoxState.Wrong;
                    wrongGuess = true;
                }
            }
        }
        _lava.Update(wrongGuess);
        _world.Update();
        Data.Won = new string(maskedWord).ToUpper() == secretWord.ToUpper();
        if (Data.Won || !_player.IsAlive)
        {
            return GameScene.GameOverScene;
        }
        else
        {
            return GameScene.None;
        }
    }


    internal override void Draw(Renderer renderer)
    {
        renderer.CameraY = _player.Y - 12;
        renderer.DrawImage(renderer.CameraX, -5, _background);
        //int playerX = _player.X - renderer.CameraX;
        if (_player.X < renderer.CameraX + 30)
        {
            renderer.CameraX--;
        }
        else if (_player.X > renderer.CameraX + 50)
        {
            renderer.CameraX++;
        }
        //if (_player.X < renderer.CameraX + 20 || _player.X > renderer.CameraX + 60)
        //{
        //    renderer.CameraTargetX = _player.X - renderer.Buffer.Width / 2;
        //}
        //renderer.UpdateCamera();
        if (renderer.CameraX < 0) { renderer.CameraX = 0; }
        if (renderer.CameraX > 200 - renderer.Buffer.Width) { renderer.CameraX = 200 - renderer.Buffer.Width; }
        _ground.Draw(renderer);
        foreach (LetterBox letterBox in _letterBoxes)
        {
            letterBox.Draw(renderer);
        }
        _player.Draw(renderer);
        _lava.Draw(renderer);
        //renderer.CameraX = 0;
        //renderer.CameraY = 0;
        DrawMaskedWordBox(renderer);

        //renderer.DrawRectangle(0, 0, 10, 5, new Color(255, 255, 255));
        //renderer.DrawRectangleOutline(0, 0, 10, 5, new Color(255, 0, 0));
        //renderer.DrawText(2, 2, "Test", new Color(0, 255, 0));
        //renderer.DrawTextBox(2, 2, "Test", new Color(90, 90, 90), new Color(255, 255, 255));
        //renderer.DrawTextBox(9, 2, "Test", new Color(110, 110, 110), new Color(255, 255, 255));
        //renderer.DrawTextBox(16, 2, "Test", new Color(150, 150, 150), new Color(255, 255, 255));
        //renderer.DrawSprite(_maskedWordSprite, renderer.Buffer.Width / 2, 1);
    }

    private void DrawMaskedWordBox(Renderer renderer)
    {
        int xPos = renderer.CameraX + (renderer.Buffer.Width - Data.MaskedWord.Length - 1) / 2;
        int yPos = renderer.CameraY + 2;
        renderer.DrawTextBox(xPos, yPos, new string(Data.MaskedWord), new Color(0, 0, 0), new Color(255, 255, 255));
    }
}
