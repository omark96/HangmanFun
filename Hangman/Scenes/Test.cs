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
        _img = new();
        _img.LoadImage(@".\Assets\lava3.tga");
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
