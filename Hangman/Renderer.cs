namespace Hangman;

internal class Renderer
{
    GlyphBuffer _buffer;
    public Renderer(int width, int height)
    {
        _buffer = new GlyphBuffer(width, height);
        _buffer.Clear();
    }

    public void Draw(Sprite sprite) { }
}