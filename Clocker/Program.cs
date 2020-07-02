using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Clocker
{
    class Program
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

        public void button1_Click(object sender, EventArgs e)
        {
            //и использование - клик левой примерно в центре экрана
            //(подробнее о координатах, передаваемых в mouse_event см. в MSDN): 
            const int x = 32000;
            const int y = 32000;

            mouse_event(MouseFlags.Absolute | MouseFlags.Move, x, y, 0, UIntPtr.Zero);
            mouse_event(MouseFlags.Absolute | MouseFlags.LeftDown, x, y, 0, UIntPtr.Zero);
            mouse_event(MouseFlags.Absolute | MouseFlags.LeftUp, x, y, 0, UIntPtr.Zero);
        }

        static void Main(string[] args)
        {
            Program pr = new Program();
            pr.button1_Click(null,null);

            Console.ReadKey();
        }
    }
}
