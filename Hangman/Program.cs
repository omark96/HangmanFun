


namespace Hangman;

internal class Program
{
    static void Main(string[] args)
    {
        var game = new Game();
        game.Run();
    }

}

internal class Game
{
    private Scene _activeScene;
    private TextBuffer _textBuffer;
    private int _tick;

    public Game()
    {
        _activeScene = new TitleScreen();
        _textBuffer = new TextBuffer(50, 20);
        _tick = 0;
    }
    internal void Run()
    {
        Console.Clear();
        System.Threading.Thread.Sleep(100);
        Console.CursorVisible = false;
        while (true)
        {
            _activeScene.Update(_tick);
            _activeScene.Draw(_textBuffer);
            Console.Clear();
            Draw();
            _textBuffer.Clear();
            _tick++;
            //Console.Beep(300, 200);
            //Console.Beep();
            System.Threading.Thread.Sleep(200);
        }
    }

    internal void Draw()
    {

        for (int row = 0; row < _textBuffer.Height; row++)
        {
            for (int col = 0; col < _textBuffer.Width; col++)
            {
                char symbol = _textBuffer.Buffer[row * _textBuffer.Width + col];
                if (symbol != '\0')
                {
                    Console.SetCursorPosition(col, row);
                    Console.Write(symbol);
                }
            }
        }
    }
}

internal class TitleScreen : Scene
{
    Player _player;
    public TitleScreen()
    {
        _player = new Player(4, 4);
    }
    //public override void Init()
    //{
    //    throw new NotImplementedException();
    //}
    public override void Update(int tick)
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(false).Key;
            while (Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }
            if (key == ConsoleKey.LeftArrow)
            {
                _player.Move(-1, 0);
            }
            else if (key == ConsoleKey.RightArrow)
            {
                _player.Move(1, 0);
            }
            else if (key == ConsoleKey.UpArrow)
            {
                _player.Move(0, -1);
            }
            else if (key == ConsoleKey.DownArrow)
            {
                _player.Move(0, 1);
            }
            Console.In.Close();
        }
        //if (tick % 20 == 0)
        //{
        //    if (_player.X > 15 || _player.X < 3)
        //    {
        //        _player.Speed *= -1;
        //    }
        //    _player.Move(_player.Speed, 0);
        //}
    }

    public override void Draw(TextBuffer textBuffer)
    {
        _player.Draw(textBuffer);
    }
}

internal class Sprite
{
    private int _x;
    private int _y;
    private string _texture;

    public int X
    {
        get => _x;
        set => _x = value;
    }
    public int Y
    {
        get => _y;
        set => _y = value;
    }
    public string Texture
    {
        get => _texture;
        set => _texture = value;
    }

    public Sprite(int x, int y, string? texture)
    {
        _x = x;
        _y = y;
        _texture = texture ?? "";
    }

    public void Draw(TextBuffer buffer, int x, int y)
    {
        int row = y + _y;
        foreach (string line in _texture.Split('\n'))
        {
            int col = x + _x;
            foreach (char c in line)
            {
                if (c != '\0')
                {
                    int pos = row * buffer.Width + col;
                    buffer.Buffer[pos] = c;
                }
                col++;
            }
            row++;
        }
    }

    public int Width => _texture.Split("\n").Max(line => line.Length);
    public int Height => _texture.Split("\n").Length;
}

internal abstract class Scene
{
    //public abstract void Init();
    public abstract void Update(int tick);
    public abstract void Draw(TextBuffer textBuffer);
}

internal class Player
{
    private int _x;
    private int _y;
    private Sprite _sprite;
    private int _speed;

    public int Speed
    {
        get => _speed;
        set => _speed = value;
    }
    public int X
    {
        get => _x;
    }

    public int Y
    {
        get => _y;
    }

    public Player(int x, int y)
    {
        _x = x;
        _y = y;
        _sprite = new Sprite(0, 0, "\0o\n/|\\\n/\0\\");
        _speed = 1;
    }

    public void Draw(TextBuffer buffer)
    {
        _sprite.Draw(buffer, _x, _y);
    }

    public void Move(int x, int y)
    {
        _x += x;
        _y += y;
    }
}
internal class TextBuffer
{
    private int _width;
    private int _height;
    private char[] _buffer;
    public int Width
    {
        get => _width;
        set => _width = value;
    }
    public int Height
    {
        get => _height;
        set => _height = value;
    }
    public char[] Buffer
    {
        get => _buffer;
    }

    public TextBuffer(int width, int height)
    {
        _width = width;
        _height = height;
        _buffer = new char[_width * _height];
    }

    public void Clear()
    {
        Array.Clear(_buffer, 0, _buffer.Length);
    }
}