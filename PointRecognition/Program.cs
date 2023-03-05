using System;
using System.IO;
using System.Drawing;

// Решение написал Тимофеев Лавр для Sitronics KT
namespace PointRecognition
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles(@"D:\Test\", "*.png");
            ImageProcessor imageProcessor = new ImageProcessor();
            string answer = imageProcessor.ProcessImages(files);
            Console.WriteLine(answer);
        }
    }

    public class ImageProcessor
    {
        public string ProcessImages(string[] files)
        {
            string answer = "";

            foreach (string file in files)
            {
                using (Image image = Image.FromFile(file))
                using (Bitmap bitmap = new Bitmap(image))
                {
                    Point point = FindWhitePixel(bitmap);

                    if (point != Point.Empty)
                    {
                        int middleX = point.X + GetWhitePixelCountX(bitmap, point.X, point.Y) / 2;
                        int middleY = point.Y + GetWhitePixelCountY(bitmap, point.X, point.Y) / 2;

                        string fileName = Path.GetFileNameWithoutExtension(file);
                        answer += $"{fileName},{middleX},{middleY};";
                    }
                }
            }

            return answer;
        }

        private Point FindWhitePixel(Bitmap bitmap)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    if (pixelColor.R == Color.White.R && pixelColor.G == Color.White.G && pixelColor.B == Color.White.B)
                    {
                        return new Point(x, y);
                    }
                }
            }

            return Point.Empty;
        }

        private int GetWhitePixelCountX(Bitmap bitmap, int x, int y)
        {
            int count = 0;
            Color pixelColor;

            while (x < bitmap.Width)
            {
                pixelColor = bitmap.GetPixel(x, y);

                if (pixelColor.R == Color.White.R && pixelColor.G == Color.White.G && pixelColor.B == Color.White.B)
                {
                    count++;
                }
                else
                {
                    break;
                }

                x++;
            }

            return count;
        }

        private int GetWhitePixelCountY(Bitmap bitmap, int x, int y)
        {
            int count = 0;
            Color pixelColor;

            while (y < bitmap.Height)
            {
                pixelColor = bitmap.GetPixel(x, y);

                if (pixelColor.R == Color.White.R && pixelColor.G == Color.White.G && pixelColor.B == Color.White.B)
                {
                    count++;
                }
                else
                {
                    break;
                }

                y++;
            }

            return count;
        }
    }

}
