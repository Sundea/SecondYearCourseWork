using System;

namespace CourseWork.View
{
    enum LastItem { Back, Exit}

    class ClassicMenu : Menu
    {
        /// <summary>
        /// Ініціалізує новий об'єкт меню
        /// </summary>
        /// <param name="line">Тип виходу з даного меню</param>
        /// <param name="menu">Пункти меню</param>
        public ClassicMenu(LastItem line, ConsoleColor font, ConsoleColor background, params string [] menu)
            :base(font, background, menu)
        {
            this.menu[this.menu.Length - 1] = (line == LastItem.Back) 
                ? StringConstants.MenuLastItemBack 
                : StringConstants.MenuLastItemExit;
        }

        /// <summary>
        /// Вивід меню на екран, вибір 
        /// </summary>
        /// <returns></returns>
        public int DoMenuAction()
        {
            return base.DoMenuAction(new string(' ', 1));
        }

        public int DoMenuActionMessage(string msg)
        {
            return base.DoMenuAction(msg);
        }
    }
}
