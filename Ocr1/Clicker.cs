using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ocr1
{
    class Clicker
    {
        [DllImport("User32.dll")]
        static extern void mouse_event(MouseFlags dwFlags, int dx, int dy, int dwData, UIntPtr dwExtraInfo);

        //для удобства использования создаем перечисление с необходимыми флагами (константами), которые определяют действия мыши: 
        [Flags]
        enum MouseFlags
        {
            Move = 0x0001, LeftDown = 0x0002, LeftUp = 0x0004, RightDown = 0x0008,
            RightUp = 0x0010, Absolute = 0x8000
        };

         void LeftClick(int x=32000,int y=32000)
         {
            mouse_event(MouseFlags.Absolute | MouseFlags.Move, x, y, 0, UIntPtr.Zero);
            mouse_event(MouseFlags.Absolute | MouseFlags.LeftDown, x, y, 0, UIntPtr.Zero);
            mouse_event(MouseFlags.Absolute | MouseFlags.LeftUp, x, y, 0, UIntPtr.Zero);
         }

        public void DoCorrectClick(IEnumerable<int> answers, int correctQuest)
        {
            int[] arr = answers.ToArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (correctQuest == arr[i])
                {
                    //тут кликаем в зависимости от i 
                    Console.WriteLine($"{correctQuest} = {arr[i]}");

                    switch (i)
                    {
                        case 0:
                            Console.WriteLine("кликаем в первый ответ");
                            LeftClick();
                            break;
                        case 1:
                            Console.WriteLine("кликаем во второй ответ");
                            LeftClick();
                            break;
                        case 2:
                            Console.WriteLine("кликаем в третий ответ");
                            LeftClick();
                            break;
                    }
                }
            }
        }
    }
}
