using System.Text;

namespace HangmanFun.Graphics;
internal class Image
{
    public int Height { get; private set; }
    public int Width { get; private set; }
    public byte[]? PixelData { get; private set; }
    public Image()
    {
    }

    public Image(byte[] pixelData, int width, int height)
    {
        Height = height;
        Width = width;
        PixelData = pixelData;
    }

    public void LoadTGA(string path)
    {
        using (var stream = File.Open(path, FileMode.Open))
        {
            using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
            {
                byte identSize = reader.ReadByte();
                byte colourMapType = reader.ReadByte();
                byte imageType = reader.ReadByte();

                Int16 colourMapStart = reader.ReadInt16();
                Int16 colourMapLength = reader.ReadInt16();
                byte colourMapBits = reader.ReadByte();
                Int16 xStart = reader.ReadInt16();
                Int16 yStart = reader.ReadInt16();
                Width = reader.ReadInt16();
                Height = reader.ReadInt16();
                byte bits = reader.ReadByte();
                byte descriptor = reader.ReadByte();

                PixelData = new byte[Width * Height * 4];
                for (int i = 0; i < PixelData.Length; i++)
                {
                    PixelData[i] = reader.ReadByte();
                }
                //Console.WriteLine("{0}, {1}, {2}", PixelData[0], PixelData[1], PixelData[2]);
                //Console.WriteLine($"Width: {width}, Height: {height}");
            }
        }
    }
}
