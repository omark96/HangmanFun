
namespace Hangman;

internal class MainScene : Scene
{
    Player _player;
    World _world;
    Ground _ground;
    List<LetterBox> _letterBoxes;
    string _secretWord;
    char[] _maskedWord;
    int _totalGuesses;

    Sprite _maskedWordSprite;
    public GameData Data { get; set; };
    public MainScene()
    {
        _world = new();
        _player = new(_world, 15, 11);
        _ground = new(_world, -20, 14);
        _letterBoxes = new();
        _secretWord = "ABC";
        _maskedWord = new char[_secretWord.Length];
        Array.Fill(_maskedWord, '-');
        _totalGuesses = 0;
        _maskedWordSprite = NewSprite();
        //LetterBox letterBox = new(_world, 0, 0, 'A');
        //_letterBoxes.Add(letterBox);
        for (int i = 0; i < 5; i++)
        {
            char c = (char)(i + 65);
            LetterBox letterBox = new(_world, i * 6, 6, c);
            _letterBoxes.Add(letterBox);
        }
    }

    private Sprite NewSprite()
    {
        return new Sprite(0, 0, $"▛{new string('▀', _secretWord.Length)}▜\n" +
            $"▌{new string(_maskedWord)}▐\n" +
            $"▙{new string('▄', _secretWord.Length)}▟", new Color(0, 0, 0), new Color(255, 255, 255));
    }

    public override void Update(ConsoleKey? input)
    {

        _player.Update(input);
        _world.Update();
        foreach (LetterBox box in _letterBoxes)
        {
            if (box.State == BoxState.Selected)
            {
                char guess = char.ToUpper(box.Letter);
                for (int i = 0; i < _secretWord.Length; i++)
                {
                    if (guess == char.ToUpper(_secretWord[i]))
                    {
                        _maskedWord[i] = guess;
                        box.State = BoxState.Correct;
                        box.Sprite.FgColor = box.StateColor();
                        _maskedWordSprite = NewSprite();
                    }
                }
                if (box.State == BoxState.Selected)
                {
                    box.State = BoxState.Wrong;
                    box.Sprite.FgColor = box.StateColor();
                }
            }
        }
    }


    public override void Draw(Renderer renderer)
    {
        renderer.CameraX = _player.X - renderer.Buffer.Width / 2;
        renderer.CameraY = _player.Y - 12;
        renderer.DrawSprite(_ground.Sprite, _ground.X, _ground.Y);
        foreach (LetterBox letterBox in _letterBoxes)
        {
            renderer.DrawSprite(letterBox.Sprite, letterBox.X, letterBox.Y);
        }
        renderer.DrawSprite(_player.Sprite, _player.X, _player.Y);
        renderer.CameraX = 0;
        renderer.CameraY = 0;
        renderer.DrawSprite(_maskedWordSprite, renderer.Buffer.Width / 2, 1);
    }
}
