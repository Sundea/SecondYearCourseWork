using System;
using System.Text;

namespace CourseWork.View
{
    class ViewComponent
    {
        public readonly static ConsoleColor Background = ConsoleColor.Black;
        public readonly static ConsoleColor Font = ConsoleColor.Gray;
        public readonly static ConsoleColor StatusBar = ConsoleColor.DarkGreen;

        private static readonly string CleanString = new string(' ', Console.WindowWidth);
        private readonly ClassicMenu mainMenu;
        private readonly ClassicMenu searchChoice;
        private readonly YesNoMenu yesNo;


        internal ClassicMenu MainMenu
        {
            get
            {
                return mainMenu;
            }
        }
        
        internal YesNoMenu YesNo
        {
            get
            {
                return yesNo;
            }
        }

        internal ClassicMenu SearchChoice
        {
            get
            {
                return searchChoice;
            }
        }

        internal ClassicMenu StatisticChoice
        {
            get; private set;
        }

        public ViewComponent()
        {
            mainMenu = new ClassicMenu(LastItem.Exit, Font, Background, StringConstants.InputData,
                StringConstants.PrintTableData, StringConstants.WriteTextFile,
                StringConstants.WriteBinaryFile, StringConstants.ReadTextFile,
                StringConstants.ReadBinaryFile, StringConstants.SearchByField,
                StringConstants.StatisticFunc);

            searchChoice = new ClassicMenu(LastItem.Back, Font,
                Background, StringConstants.SearchByGoodsName,
                StringConstants.SearchByPrice);

            yesNo = new YesNoMenu(Font, Background);

            StatisticChoice = new ClassicMenu(LastItem.Back, Font, Background,
                StringConstants.StatisticFuncMax, StringConstants.StatisticFuncMin);
        }
        

        public static void ClearStringTo(int top, int left)
        {
            Console.CursorTop = top;
            Console.CursorLeft = 0;
            Console.Write(new string(' ', left));
            Console.CursorLeft = 0;
        }

        /// <summary>
        /// Очищує рядок вперед від заданих координат
        /// </summary>
        /// <param name="top">Номер рядка від верхнього боку вікна</param>
        /// <param name="left">Номер стовпця від лівого боку вікна</param>
        public static void ClearStringFrom(int top, int left)
        {
            Console.CursorTop = top;
            Console.CursorLeft = left;
            Console.Write(new string(' ', Console.WindowWidth - left));
            Console.CursorTop--;
            Console.CursorLeft = left;
        }

        /// <summary>
        /// Очищує рядок
        /// </summary>
        /// <param name="top">Номер рядка від верхнього боку вікна</param>
        public static void ClearString(int top)
        {
            ClearStringFrom(top, 0);
        }
        
        public static void ClearTo(int top, int left)
        {
            int prevTop = Console.CursorTop;
            int prevLeft = Console.CursorLeft;


            if (Console.CursorTop == top)
            {
                if (Console.CursorLeft == left)
                {
                    return;
                }
                else if (Console.CursorLeft > left)
                {
                    Console.CursorLeft = left;
                    left = prevLeft;
                }
            }
            else if (Console.CursorTop > top)
            {
                Console.CursorTop = top;
                Console.CursorLeft = left;
                top = prevTop;
                left = prevLeft;
                
            }

            ClearStringFrom(Console.CursorTop, Console.CursorLeft);
            
            for (Console.CursorTop++; Console.CursorTop < top; ++Console.CursorTop)
            {
                ClearString(Console.CursorTop);
            }

            ClearStringTo(Console.CursorTop, left);
        }

        public static void ClearWithotFirst()
        {
            Console.CursorTop = 1;
            
            for (int i = 1; i < Console.WindowHeight - 1; i++)
            {
                Console.Write(CleanString);
            }
            Console.CursorTop = 1;
        }

    }
}
