using Hangman;
using HangmanFun.Graphics;
using HangmanFun.Sound;

namespace HangmanFun.Scenes;
internal class Test : Scene
{
    Image _img;
    AudioPlayer _audioPlayer;
    bool _playing = true;
    internal override GameData Data { get; set; }

    public Test(GameData data)
    {
        Data = data;
        byte[] buffer = new byte[256 * 4];
        for (int i = 0; i < 255; i++)
        {
            buffer[4 * i] = (byte)i;
            buffer[4 * i + 1] = 0;
            buffer[4 * i + 2] = 0;
        }
        _img = new(buffer, 64, 4);
        _audioPlayer = new("./Assets/bg.pd");
        _audioPlayer.Play();
        //_img.LoadImage(@".\Assets\lava3.tga");
    }

    internal override void Draw(Renderer renderer)
    {
        renderer.DrawImage(0, 0, _img);
    }

    internal override GameScene Update(ConsoleKeyInfo input)
    {
        if (input.Key == ConsoleKey.P)
        {
            if (_playing)
            {
                _audioPlayer.Pause();
            }
            else
            {
                _audioPlayer.Play();
            }
            _playing = !_playing;
        }
        else if (input.Key == ConsoleKey.Spacebar)
        {
            return GameScene.TitleScene;
        }
        return GameScene.None;
    }
}
