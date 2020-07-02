using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using Tesseract;

namespace Ocr1
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> answers = new Queue<int>();
            Queue<int> question = new Queue<int>();
            PicConverter converter = new PicConverter();
            Clicker clicker = new Clicker();

            Console.WriteLine("Нажмите клавишу для старта");
            Console.ReadLine();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            try
            {
                //берём картинку и делаем чёрно-белой
                Bitmap image = new Bitmap(@"C:\Users\gvozd\source\repos\Ocr1\Ocr1\NewFolder1\image.png");
                Bitmap imageBW = converter.BitmapToBlackWhite2(image,0.9);   
               
                //Bitmap image = new Bitmap(b1, new Size(450, 150));  //если нужно ресайзим
                imageBW.Save(@"D:\image.png");

                //вырезаем вопрос и ответы
                Bitmap questionImg = converter.CutImgFromImg(20, 8, 120, 38, imageBW, "question.png");   
                Bitmap stackAns= converter.StackImg(imageBW);


                //ОКР
                var ocr = new TesseractEngine(@"./tessdata", "eng", EngineMode.TesseractAndCube);
                var text = ocr.Process(questionImg);              
                Console.WriteLine(text.GetText());

                char[] numbrs = text.GetText().ToCharArray();
                int x = 0;
                //если число, то добавляем его в лист
                foreach (var c in numbrs)
                {
                    if (int.TryParse(c.ToString(), out x))
                    {
                        question.Enqueue(Convert.ToInt32(c-'0'));
                    }
                    else
                    {
                        //не число
                    }
                }

                int correctQuest = 0;
                for (int i=0;i<2;i++)
                {
                    correctQuest += question.Dequeue();
                }

                // НОВАЯ ОКР
                text.Dispose();
                text = ocr.Process(stackAns);
                Console.WriteLine(text.GetText());

                char[] numbrsAns = text.GetText().ToCharArray();
                int x1 = 0;
                foreach (var c in numbrsAns)
                {
                    if (int.TryParse(c.ToString(), out x1))
                    {
                        answers.Enqueue(Convert.ToInt32(c - '0'));
                    }
                    else
                    {
                        //не число
                    }
                }                
                sw.Stop();
                Console.WriteLine((sw.ElapsedMilliseconds / 1000.0).ToString()+ " секунд");
                clicker.DoCorrectClick(answers, correctQuest);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            Console.ReadKey();

        }
    }
}
