namespace Hangman;

internal class MainScene : Scene
{
    Player _player;
    World _world;
    Ground _ground;
    public MainScene()
    {
        _world = new();
        _player = new(_world, 15, 10);
        _ground = new(_world, 0, 13);
    }
    //public override void Init()
    //{
    //    throw new NotImplementedException();
    //}
    public override void Update(ConsoleKey? input)
    {

        _player.Update(input);
        _world.Tick();
        //else if (input == ConsoleKey.DownArrow)
        //{
        //    _player.Move(0, 1);
        //}
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
        _ground.Draw(textBuffer);
    }
}
