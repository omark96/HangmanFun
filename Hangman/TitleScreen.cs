namespace Hangman;

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

    public override void Draw(GlyphBuffer textBuffer)
    {
        _player.Draw(textBuffer);
    }
}
