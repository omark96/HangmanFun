using HangmanFun.Graphics;
using HangmanFun.Physics;
using System.Numerics;

namespace HangmanFun.Entities
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
            get { return (int)Math.Round(_body.Position.Y - 0.5); }
        }
        public Lava(World world, int x, int y)
        {
            int width = 200;
            int height = 20;
            Vector2 position = new(x, y);
            Vector2 size = new(width, height);
            _body = new Body(BodyType.Static, position, size, "lava", this);
            _body.Speed = new Vector2(0, -0.01f);
            world.AddBody(_body);

            _texture = new();
            _texture.LoadTGA(@".\Assets\lava4.tga");
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
            renderer.DrawImage(X, Y, _texture);
        }
    }
}