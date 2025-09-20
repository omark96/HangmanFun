namespace Hangman;

internal class MainScene : Scene
{
    Player _player;
    World _world;
    Ground _ground;
    List<LetterBox> _letterBoxes;
    public MainScene()
    {
        _world = new();
        _player = new(_world, 15, 11);
        _ground = new(_world, 0, 14);
        _letterBoxes = new();
        //LetterBox letterBox = new(_world, 0, 0, 'A');
        //_letterBoxes.Add(letterBox);
        for (int i = 0; i < 5; i++)
        {
            char c = (char)(i + 65);
            LetterBox letterBox = new(_world, 10 + i * 6, 6, c);
            _letterBoxes.Add(letterBox);
        }
    }
    //public override void Init()
    //{
    //    throw new NotImplementedException();
    //}
    public override void Update(ConsoleKey? input)
    {

        _player.Update(input);
        _world.Tick();
        _world.HandleCollisions();
        _world.ResetSpeedOfKinematicBodies();
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

    public override void Draw(Renderer renderer)
    {
        renderer.DrawSprite(_ground.Sprite, _ground.X, _ground.Y);
        foreach (LetterBox letterBox in _letterBoxes)
        {
            renderer.DrawSprite(letterBox.Sprite, letterBox.X, letterBox.Y);
        }
        renderer.DrawSprite(_player.Sprite, _player.X, _player.Y);
    }
}
