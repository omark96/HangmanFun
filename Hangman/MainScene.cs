
namespace Hangman;

internal class MainScene : Scene
{
    Player _player;
    World _world;
    Ground _ground;
    List<LetterBox> _letterBoxes;
    Sprite _maskedWordSprite;

    public override GameData Data { get; set; }
    public MainScene(GameData data)
    {
        _world = new();
        _player = new(_world, 15, 11);
        _ground = new(_world, -20, 14);
        _letterBoxes = new();
        Data = data;
        _maskedWordSprite = NewSprite();
        //LetterBox letterBox = new(_world, 0, 0, 'A');
        //_letterBoxes.Add(letterBox);
        for (int i = 0; i < 5; i++)
        {
            char c = (char)(i + 65);
            LetterBox letterBox = new(_world, i * 6, 6, c);
            _letterBoxes.Add(letterBox);
        }
        for (int i = 0; i < 10; i++)
            Console.WriteLine(i);
        if (3 < 5)
            Console.WriteLine("Why?");
    }

    private Sprite NewSprite()
    {
        string secretWord = Data.SecretWord;
        char[] maskedWord = Data.MaskedWord;
        return new Sprite(0, 0, $"▛{new string('▀', secretWord.Length)}▜\n" +
            $"▌{new string(maskedWord)}▐\n" +
            $"▙{new string('▄', secretWord.Length)}▟", new Color(0, 0, 0), new Color(255, 255, 255));
    }

    public override GameScene Update(ConsoleKey? input)
    {
        string secretWord = Data.SecretWord;
        char[] maskedWord = Data.MaskedWord;
        _player.Update(input);
        _world.Update();
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
                        _maskedWordSprite = NewSprite();
                    }
                }
                if (box.State == BoxState.Selected)
                {
                    box.State = BoxState.Wrong;
                }
            }
        }
        if (new string(maskedWord).ToUpper() == secretWord.ToUpper())
        {
            return GameScene.GameOverScene;
        }
        else
        {
            return GameScene.None;
        }
    }


    public override void Draw(Renderer renderer)
    {
        renderer.CameraX = _player.X - renderer.Buffer.Width / 2;
        renderer.CameraY = _player.Y - 12;
        _ground.Draw(renderer);
        foreach (LetterBox letterBox in _letterBoxes)
        {
            letterBox.Draw(renderer);
        }
        _player.Draw(renderer);
        renderer.CameraX = 0;
        renderer.CameraY = 0;
        //renderer.DrawRectangle(0, 0, 10, 5, new Color(255, 255, 255));
        //renderer.DrawRectangleOutline(0, 0, 10, 5, new Color(255, 0, 0));
        //renderer.DrawText(2, 2, "Test", new Color(0, 255, 0));
        for (int i = 0; i < 10; i++)
        {
            renderer.DrawTextBox(2 + i * 7, 2, "Test", new Color((byte)(i * 20), (byte)(i * 20), (byte)(i * 20)), new Color(255, 255, 255));
        }
        //renderer.DrawTextBox(2, 2, "Test", new Color(90, 90, 90), new Color(255, 255, 255));
        //renderer.DrawTextBox(9, 2, "Test", new Color(110, 110, 110), new Color(255, 255, 255));
        //renderer.DrawTextBox(16, 2, "Test", new Color(150, 150, 150), new Color(255, 255, 255));
        //renderer.DrawSprite(_maskedWordSprite, renderer.Buffer.Width / 2, 1);
    }
}
