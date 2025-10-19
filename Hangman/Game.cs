
using HangmanFun.Graphics;
using HangmanFun.Scenes;

namespace Hangman;
internal class Game
{
    Scene _activeScene;
    Renderer _renderer;
    GameData _gameData;
    int _gameWidth;
    int _gameHeight;

    public Game()
    {
        _gameWidth = 80;
        _gameHeight = 30;
        _gameData = new();
        _renderer = new Renderer(_gameWidth, _gameHeight);
        _activeScene = new Options(_gameData, _renderer);
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
            _activeScene.Draw();
            _renderer.Draw();
            _renderer.Buffer.Clear();
            _gameData.Tick += 1;
            _activeScene = SwitchScene(newScene) ?? _activeScene;
            Thread.Sleep(50);
            if (newScene == GameScene.GameOverScene)
            {
                Thread.Sleep(500);
            }
        }
    }

    internal Scene? SwitchScene(GameScene scene)
    {
        switch (scene)
        {
            case GameScene.TitleScene:
                return new Title(_gameData, _renderer);

            case GameScene.GameOverScene:
                return new GameOver(_gameData, _renderer);

            case GameScene.MainScene:
                _gameData.NewRound();
                return new Main(_gameData, _renderer);

            default:
                return null;
        }
    }

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


