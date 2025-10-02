using HangmanFun.Graphics;
using System.Numerics;

namespace Hangman
{
    internal class Lava
    {
        Body _body;
        Image _texture;
        public int X
        {
            get { return (int)Math.Round(_body.Position.X); }
        }
        public int Y
        {
            get { return (int)Math.Round(_body.Position.Y); }
        }
        public Lava(World world, int x, int y)
        {
            int width = 200;
            int height = 20;
            Vector2 position = new((float)x, (float)y);
            Vector2 size = new(width, height);
            _body = new Body(BodyType.Static, position, size, "lava", this);
            _body.Speed = new Vector2(0, -0.02f);
            world.AddBody(_body);

            _texture = new();
            _texture.LoadImage(@".\Assets\lava4.tga");
        }

        public void Update(bool wrongGuess)
        {
            if (wrongGuess)
            {
                _body.Position = new Vector2(_body.Position.X, _body.Position.Y - 1);
            }
            _body.Move();
        }

        internal void Draw(Renderer renderer)
        {
            //renderer.DrawRectangle(X, Y - 1, (int)_body.Size.X, (int)_body.Size.Y, Color.Red);
            renderer.DrawImage(X, Y - 1, _texture);
        }
    }
}