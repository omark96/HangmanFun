using Hangman;
using HangmanFun.Graphics;
using HangmanFun.Sound;

namespace HangmanFun.Scenes;
internal class Test : Scene
{
    Image _img;

    public Test(GameData data, AudioPlayer audioPlayer, Renderer renderer) : base(data, audioPlayer, renderer)
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
        //_img.LoadImage(@".\Assets\lava3.tga");
    }

    internal override void Draw()
    {
        Renderer.DrawImage(0, 0, _img);
    }

    internal override GameScene Update(ConsoleKeyInfo input)
    {

        if (input.Key == ConsoleKey.Spacebar)
        {
            return GameScene.TitleScene;
        }
        return GameScene.None;
    }
}
