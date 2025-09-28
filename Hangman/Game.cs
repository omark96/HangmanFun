
using HangmanFun.Scenes;

namespace Hangman;
internal class Game
{
    Scene _activeScene;
    Renderer _renderer;
    GameData _data;
    int _gameWidth;
    int _gameHeight;

    public Game()
    {
        _gameWidth = 80;
        _gameHeight = 30;
        _data = new GameData("C");
        _activeScene = new Title(_data);
        _renderer = new Renderer(_gameWidth, _gameHeight);
    }

    internal void Run()
    {
        Console.Clear();
        System.Threading.Thread.Sleep(500);
        Console.CursorVisible = false;
        Console.Clear();
        while (true)
        {
            GameScene newScene = _activeScene.Update(Input());
            _activeScene.Draw(_renderer);
            _renderer.Draw();
            _renderer.Buffer.Clear();
            _data.Tick += 1;
            //Console.SetCursorPosition(0, 0);
            //Console.WriteLine(_activeScene._player._speed);
            //Console.Beep(300, 200);
            //Console.Beep();
            //if (newScene == GameScene.GameOverScene)
            //{
            //    Console.Clear();
            //    Console.WriteLine("You won!");
            //    Console.WriteLine($"Number of guesses: {_data.TotalGuesses}");
            //    while (true)
            //    {
            //    }
            //}
            _activeScene = SwitchScene(newScene) ?? _activeScene;
            System.Threading.Thread.Sleep(50);
        }
    }

    internal Scene? SwitchScene(GameScene scene) => scene switch
    {
        GameScene.TitleScene => new Title(_data),
        GameScene.GameOverScene => new GameOver(_data),
        GameScene.MainScene => new Main(_data),
        _ => null,
    };

    //internal void Draw()
    //{
    //    //Console.SetCursorPosition(0, 0);
    //    //for (int row = 0; row < _glyphBuffer.Height; row++)
    //    //{

    //    //}
    //    //for (int row = 0; row < _glyphBuffer.Height; row++)
    //    //{
    //    //    Console.WriteLine();
    //    //}
    //}

    internal ConsoleKeyInfo Input()
    {
        ConsoleKeyInfo input = new((char)0, ConsoleKey.None, false, false, false);
        if (Console.KeyAvailable)
        {
            //input = Console.ReadKey(false).Key;
            while (Console.KeyAvailable)
            {
                input = Console.ReadKey(true);
            }
            Console.In.Close();
        }
        return input;
    }
}


