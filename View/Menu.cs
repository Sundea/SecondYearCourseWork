using System;

namespace CourseWork.View
{
    /// <summary>
    /// Клас, що є основою для консольного меню
    /// <para>Має методи для виводу меню на екран, зміни стану пункту меню та
    /// отримання вибраного значення.</para>
    /// </summary>
    abstract class Menu
    {
        private int previousPos;
        private int currentPos;
        private int min;
        private int max;
        protected string[] menu;
        public const int ExitCode = -1;



        /// <summary>
        /// iнiцiалiзує новий об'єкт 
        /// </summary>
        /// <exception cref="ArgumentNullException">Викидується, коли массив
        /// рiвний <c>null</c></exception>
        /// <exception cref="ArgumentOutOfRangeException">Викидується, коли
        /// один з елементiв масиву є пустим рядком або null</exception>
        /// <param name="menu">Массив пунктiв меню</param>
        /// <param name="background">Колір фону меню</param>
        /// <param name="font">Колір тексту меню</param>
        public Menu(ConsoleColor font, ConsoleColor background, params string [] menu)
            :this(font, background)
        {
            if (menu != null)
            {
                this.menu = new string[menu.Length + 1];
                for (int i = 0; i < menu.Length; i++)
                {
                    if (String.IsNullOrEmpty(menu[i]))
                        { throw new ArgumentOutOfRangeException(); }
                    else
                        { this.menu[i] = menu[i]; }
                }
            }
            else
            {
                throw new ArgumentNullException();
            }

        }

        /// <summary>
        /// Ініціалізує новий об'єкт без пунктів меню
        /// </summary>
        /// <param name="font">Колір тексту</param>
        /// <param name="background">Колір фону</param>
        protected Menu(ConsoleColor font, ConsoleColor background)
        {
            Font = font;
            Background = background;
        }



        /// <summary>
        /// Колір тексту
        /// </summary>
        public ConsoleColor Font
        {
            get; protected set;
        }

        /// <summary>
        /// Колір фону
        /// </summary>
        public ConsoleColor Background
        {
            get; protected set;
        }

        /// <summary>
        /// Індекс поточного пункту меню
        /// </summary>
        private int Position
        {
            get
            {
                return currentPos;
            }

            set
            {
                previousPos = currentPos;
                if (value < min)
                    currentPos = max;
                else if (value > max)
                    currentPos = min;
                else
                    currentPos = value;
            }
        }

        /// <summary>
        /// Повертає номер вибраного пункту меню 
        /// </summary>
        public int Choice
        {
            get
            {
                if (currentPos - min == menu.Length - 1)
                    return ExitCode;
                return currentPos - min;
            }
        }



        /// <summary>
        /// Друк меню на екран
        /// </summary>
        private void PrintMenu()
        {
            for (int i = 0; i < menu.Length; i++)
            {
                Console.WriteLine(menu[i]);
            }
        }

        /// <summary>
        /// Змінює кольори фону і тексту
        /// </summary>
        /// <param name="background">Колір фону</param>
        /// <param name="foreground">Уолір тексту</param>
        private void ChangeColors(ConsoleColor background, ConsoleColor foreground)
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
        }

        /// <summary>
        /// Реєструє натиск клавіш стрілок і змінює активний пункт меню
        /// </summary>
        private void SetActiveMenuItem()
        {
            Console.CursorVisible = false;
            ChangeRowColors(currentPos, Background, Font);
            ConsoleKeyInfo pressedKey;
            
            do
            {
//                previousPos = currentPos;
                pressedKey = Console.ReadKey(true);
                switch (pressedKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        { Position--; }
                        break;
                    case ConsoleKey.DownArrow:
                        { Position++; }
                        break;
                }

                ChangeRowColors(previousPos, Font, Background);
                ChangeRowColors(currentPos, Background, Font);

            } while (pressedKey.Key != ConsoleKey.Enter);
            Console.CursorVisible = true;
        }

        /// <summary>
        /// Змінює фон і текст пункту меню
        /// </summary>
        /// <param name="cursorTop">Позиція потрібного пункту меню від верхнього краю вікна</param>
        /// <param name="foreground">Колір тексту</param>
        /// <param name="background">Колір фону</param>
        private void ChangeRowColors(int cursorTop, ConsoleColor foreground, ConsoleColor background)
        {
            if (cursorTop >= min && cursorTop <= max)
            {
                ChangeColors(background, foreground);
                ViewComponent.ClearString(cursorTop);
                Console.WriteLine(menu[cursorTop - min]);
            }
        }
        
        /// <summary>
        /// Викликає меню
        /// </summary>
        /// <param name="msg">Повідомлення, яке виводиться перед пунктами меню</param>
        /// <returns>Вибраний пункт меню</returns>
        protected int DoMenuAction(string msg)
        {
            ViewComponent.ClearWithotFirst();
            ChangeColors(Background, Font);
            Console.WriteLine(StringConstants.MenuNavigation);
            Console.WriteLine(msg);
            currentPos = Console.CursorTop;
            min = currentPos;
            max = menu.Length + min - 1;
            PrintMenu();
            SetActiveMenuItem();
            ChangeColors(Background, Font);
            ViewComponent.ClearWithotFirst();
            return Choice;
        }

    }
}
