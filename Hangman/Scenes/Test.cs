using Hangman;
using HangmanFun.Graphics;

namespace HangmanFun.Scenes;
internal class Test : Scene
{
    Image _img;
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
        //_img.LoadImage(@".\Assets\lava3.tga");
    }

    internal override void Draw(Renderer renderer)
    {
        renderer.DrawImage(0, 0, _img);
    }

    internal override GameScene Update(ConsoleKeyInfo input)
    {
        return GameScene.None;
    }
}
