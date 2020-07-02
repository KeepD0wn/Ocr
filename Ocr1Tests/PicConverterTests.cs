using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocr1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Ocr1.Tests
{
    [TestClass()]
    public class PicConverterTests
    {
        [TestMethod()]
        public void BitmapToBlackWhite2Test()
        {
            var pic = new Bitmap(@"C:\Users\gvozd\source\repos\Ocr1\Ocr1\num\un1.png");
            PicConverter pc = new PicConverter();
            Bitmap image = pc.BitmapToBlackWhite2(pic,0.85);
            image.Save(@"D:\image.png");
        }

        [TestMethod()]
        public void StuckImgTest()
        {
            int imgPx = 15;

            Image img1 = Bitmap.FromFile(@"C:\Users\gvozd\source\repos\Ocr1\Ocr1\NewFolder1\ans1.jpg");
            Image img2 = Bitmap.FromFile(@"C:\Users\gvozd\source\repos\Ocr1\Ocr1\NewFolder1\ans2.jpg");
            Image img3 = Bitmap.FromFile(@"C:\Users\gvozd\source\repos\Ocr1\Ocr1\NewFolder1\ans3.jpg");

            Bitmap res = new Bitmap(imgPx * 3, imgPx);

            Graphics g = Graphics.FromImage(res);
            g.DrawImage(img1, 0, 0);
            g.DrawImage(img2, imgPx, 0);
            g.DrawImage(img3, imgPx*2, 0);

            res.Save(@"D:\imagestack.png");
        }
    }
}