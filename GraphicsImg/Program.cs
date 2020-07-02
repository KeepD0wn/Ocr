using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsImg
{
    class Program
    {
        static void Main(string[] args)
        {
            // Это для скриншота с экрана

            //Bitmap screen = new Bitmap(@"C:\Users\gvozd\source\repos\Ocr1\Ocr1\NewFolder1\image.png");
            //using (Graphics g = Graphics.FromImage(screen))
            //{
            //    g.CopyFromScreen(32,11, 0, 0, screen.Size);
            //    screen.Save(@"D:\image1.png");
            //}


            // Это для обрезания картинки
            Rectangle rectangle = new Rectangle(20, 8, 120, 38);
            var pic = new Bitmap(@"C:\Users\gvozd\source\repos\Ocr1\Ocr1\NewFolder1\image.png");
            Bitmap newimg = pic.Clone(rectangle, PixelFormat.Format16bppRgb555);
            newimg.Save(@"D:\image1.png");

            Console.ReadKey();
        }
    }
}
