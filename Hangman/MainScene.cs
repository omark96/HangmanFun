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
        _player = new(_world, 15, 9);
        _ground = new(_world, 0, 13);
        _letterBoxes = new();
        //_letterBox = new(_world, 16, 6, 'A');
        for (int i = 0; i < 10; i++)
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

    public override void Draw(GlyphBuffer textBuffer)
    {
        _ground.Draw(textBuffer);
        foreach (LetterBox letterBox in _letterBoxes)
        {
            letterBox.Draw(textBuffer);
        }
        _player.Draw(textBuffer);
    }
}
