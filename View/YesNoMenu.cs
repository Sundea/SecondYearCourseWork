using System;

namespace CourseWork.View
{
    class YesNoMenu : Menu
    {

        public YesNoMenu(ConsoleColor font, ConsoleColor background)
            : 
            base(font, background, StringConstants.MenuYes,
                  StringConstants.MenuNo) { }

        public new bool Choice
        {
            get
            {
                if (base.Choice == 0)
                    return true;
                return false;
            }
        }

        public new bool DoMenuAction(string question)
        {
            base.DoMenuAction(question);
            return Choice;
        }
    }
}
