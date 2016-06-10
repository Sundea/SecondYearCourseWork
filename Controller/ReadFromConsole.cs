using CourseWork.View;
using System;
using System.Text.RegularExpressions;

namespace CourseWork.Controller
{
    static class ReadFromConsole
    {
        static int left;
        static int top;
        static int count;

        static Match matcher;

        public static double ReadDouble(string pattern)
        {
            double num = 0;
            bool IsParseAccepted = false;
            do
            {
                try
                {
                    num = Double.Parse(ReadStringAndReaction(pattern, StringConstants.DoubleParseError, RegexOptions.IgnoreCase));
                    IsParseAccepted = true;
                }
                catch
                {
                    WriteError(StringConstants.DoubleParseError);
                }
               
            } while (!IsParseAccepted);
            return num;
        }


        public static void WriteError(string message)
        {
            Console.Write(message);
            Console.ReadKey(true);
            ViewComponent.ClearTo(top, left);
            Console.CursorTop = top;
            Console.CursorLeft = left;
        }

        private static string ReadString()
        {
            string line;
            left = Console.CursorLeft;
            top = Console.CursorTop;

            line = Console.ReadLine();
            count = line.Length;
         
            return line;
        }
     
        public static string ReadStringAndReaction(string pattern, string errorMessage, RegexOptions options)
        {
            string line;
            do
            {
                line = ReadString();
                matcher = Regex.Match(line, pattern, options);

                if (!matcher.Success)
                {
                    WriteError(errorMessage);
                }
            } while (!matcher.Success);
            return line;
        }

        
    }
}
