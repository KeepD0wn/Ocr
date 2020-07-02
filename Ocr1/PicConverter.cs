using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocr1
{
    public class PicConverter
    {
        public Bitmap BitmapToBlackWhite2(Bitmap src, double treshold)
        {
            Bitmap dst = new Bitmap(src.Width, src.Height);

            for (int i = 0; i < src.Width; i++)
            {
                for (int j = 0; j < src.Height; j++)
                {
                    dst.SetPixel(i, j, src.GetPixel(i, j).GetBrightness() < treshold ? Color.Black : Color.White);
                }
            }

            return dst;
        }

        /// <summary>
        /// Вырезает каждую картинку с ответом и сохраняет их. Дальше 3 картинки скрепляются в ряд
        /// </summary>
        /// <param name="imageBW"></param>
        /// <returns></returns>
        public Bitmap StackImg(Bitmap imageBW)
        {
            int imgPx = 15;

            Bitmap ans1 = CutImgFromImg(70, 74, 15, 15, imageBW, "ans1.png");
            Bitmap ans2 = CutImgFromImg(70, 123, 15, 15, imageBW, "ans2.png");
            Bitmap ans3 = CutImgFromImg(70, 171, 15, 15, imageBW, "ans3.png");

            Bitmap res = new Bitmap(imgPx * 3, imgPx);

            Graphics g = Graphics.FromImage(res);
            g.DrawImage(ans1, 0, 0);
            g.DrawImage(ans2, imgPx, 0);
            g.DrawImage(ans3, imgPx * 2, 0);

            res.Save(@"D:\imagestack.png");
            return res;
        }
                
        public Bitmap CutImgFromImg(int imgX, int imgY,int imgIndentX,int imgIndentY, Bitmap imgSource, string imgName)
        {
            Rectangle rectangle = new Rectangle(imgX, imgY, imgIndentX, imgIndentY);            
            Bitmap newimg = imgSource.Clone(rectangle, PixelFormat.Format8bppIndexed);
            newimg.Save($@"D:\{imgName}");
            return newimg;
        }

        public Bitmap CutImgFromScreen()
        {
            Bitmap screen = new Bitmap(@"C:\Users\gvozd\source\repos\Ocr1\Ocr1\NewFolder1\image.png");
            using (Graphics g = Graphics.FromImage(screen))
            {
                g.CopyFromScreen(32, 11, 0, 0, screen.Size);
                screen.Save(@"C:\image.png");
            }

            return screen;
        }

    }
}
