using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tesseract;

namespace Ocr1
{
    class Program
    {
        public static void PageToNumbers(Page text1, ref Queue<int> queue)
        {
            char[] numbrs = text1.GetText().ToCharArray();
            int x = 0;
            //если число, то добавляем его в лист
            foreach (var c in numbrs)
            {
                if (int.TryParse(c.ToString(), out x))
                {
                    queue.Enqueue(Convert.ToInt32(c - '0'));
                }
                else
                {
                    //не число
                }
            }
        }

        static void Main(string[] args)
        {
            Queue<int> answers = new Queue<int>();
            Queue<int> question = new Queue<int>();            
            PicConverter converter = new PicConverter();
            Clicker clicker = new Clicker();
            Stopwatch sw = new Stopwatch();
            var ocr1 = new TesseractEngine(@"./tessdata", "eng", EngineMode.TesseractAndCube);
            var ocr2 = new TesseractEngine(@"./tessdata", "eng", EngineMode.TesseractAndCube);
            int correctAnswer = 0;

            Console.WriteLine("Введите что-нибудь для старта");
            Console.ReadLine();
            sw.Start();

            try
            {
                //берём картинку и делаем чёрно-белой
                // метод CutImgFromScreen для скрина экрана
                Bitmap image = new Bitmap(@"C:\Users\gvozd\source\repos\Ocr1\Ocr1\NewFolder1\image.png");
                Bitmap imageBW = converter.BitmapToBlackWhite2(image,0.9);   
               
                //Bitmap image = new Bitmap(b1, new Size(450, 150));  //если нужно ресайзим
                imageBW.Save(@"D:\image.png");

                //вырезаем вопрос и ответы
                Bitmap questionImg = converter.CutImgFromImg(20, 8, 120, 38, imageBW, "question.png");   
                Bitmap stackAns= converter.StackImg(imageBW);
                               
                var task1 = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("таск 1 начался");                    
                    var text1 = ocr1.Process(questionImg);
                    Console.WriteLine(text1.GetText());

                    PageToNumbers(text1, ref question);
                                                            
                    for (int i = 0; i < 2; i++)
                    {
                        correctAnswer += question.Dequeue();
                    }
                    ocr1.Dispose();
                    Console.WriteLine("таск 1 закончился");
                });
               
                var task2 = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("таск 2 начался");
                    var text2 = ocr2.Process(stackAns);
                    Console.WriteLine(text2.GetText());
                                       
                    PageToNumbers(text2, ref answers);                                     

                    ocr2.Dispose();
                    Console.WriteLine("таск 2 закончился");
                });

                Task.WaitAll(task1,task2);
                sw.Stop();
                Console.WriteLine((sw.ElapsedMilliseconds / 1000.0).ToString()+ " секунд");
                clicker.DoCorrectClick(answers, correctAnswer);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            Console.ReadKey();

        }
    }
}
