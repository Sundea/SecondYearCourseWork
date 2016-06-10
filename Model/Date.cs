using System; 

namespace CourseWork.Model
{
    //Дата {Місяць, День};

    [Serializable()] 
    class Date: ISpreadsheet, ICloneable
    {
        public const int MinDay = 1;
        public const int MinMonth = 1;
        public const int MaxMonth = 12;

        private int day;
        private int month = MinMonth; // 1 - 12, Jan - Dec



        /// <summary>
        /// Ініціалізує об'єкт значеннями <paramref name="day"/>
        /// та <paramref name="month"/> попередно конвертуючи їх в
        /// цілочисленний формат
        /// </summary>
        /// <see cref="Day"/>
        /// <see cref="Month"/>
        /// <exception cref="FormatException">Викидується, коли неможливо
        /// конвертувати <paramref name="day"/> і <paramref name="month"/>
        /// в </exception>
        /// <param name="day">день від 1 до <code>MaxDayMonth</code></param>
        /// <param name="month">місяць від 1 до 12</param>
        public Date(string day, string month)
            : this(Int32.Parse(day), Int32.Parse(month))
        {
        }

        /// <summary>
        /// Ініціалізує об'єкт значеннями <paramref name="day"/> 
        /// та <paramref name="month"/>
        /// </summary>
        /// <param name="day">
        /// День місяця від 1 до <see cref="MaxDayMonth(Months)"/>
        /// </param>
        /// <param name="month">Місяць</param>
        public Date(int day, int month)
        {
            Day = day;
            Month = month;
        }

        /// <summary>
        /// Конструктор копіювання
        /// </summary>
        /// <param name="date">джерело для створення копіїї</param>
        public Date(Date date)
        {
            this.day = date.day;
            this.month = date.month;
        }

        

        /// <summary>
        /// Установлює та повертає день місяця
        /// </summary>
        /// <exception cref = "ArgumentOutOfRangeException" >
        /// Викидується, коли <code>value</code> менше 1, або більше
        /// <see cref="MaxDayMonth(Months)"/></exception>
        public int Day
        {
            get
            {
                return day;
            }

            protected set
            {
                if (value <= MaxDayMonth(month) && value >= MinDay)
                    { day = value; }
                else
                {
                    throw new ArgumentOutOfRangeException(SupportText.DateDay,
                      SupportText.ArgumentOutOfRange + ": " + MinDay + '-' 
                      + MaxDayMonth(month).ToString());
                }
            }
        }

        /// <summary>
        /// Повертає та встановлює місяць типу <c>int</c>
        /// </summary>
        /// <exception cref = "ArgumentOutOfRangeException" >
        /// Викидується, коли <code>value</code> менше <see cref="MinMonth"/>,
        /// або більше <see cref="MaxMonth"/></exception>
        public int Month
        {
            get
            {
                return month;
            }
            protected set
            {
                if (value >= MinMonth && value <= MaxMonth)
                    { month = value; }
                else
                {
                    throw new ArgumentOutOfRangeException(SupportText.DateMonth,
                        SupportText.ArgumentOutOfRange + ": " + MinMonth + '-'
                        + MaxMonth);
                }
            }
        }


        
        /// <summary>
        /// Повертає максимальну кількість днів в <paramref name="month"/>
        /// </summary>
        /// <param name="month">Місяць</param>
        /// <returns>Максимальну кількість днів в місяці</returns>
        public static int MaxDayMonth(int month)
        {
            if (month > MaxMonth || month < MinMonth)
            {
                throw new ArgumentOutOfRangeException(
                    SupportText.DateMonth,
                    SupportText.ArgumentOutOfRange);
            }
                int max;
                switch (month)
                {
                    case 4:
                    case 6:
                    case 9:
                    case 11: max = 30; break;
                    case 2: max = 28; break;
                    default: max = 31; break;
                }
            
            return max;
        }

        public static bool operator <(Date obj1, Date obj2)
        {
            if (obj1 == null)
                return true;
            else if (obj2 == null)
                return false;

            return (obj1.month < obj2.month || (obj1.month == obj2.month && obj1.day < obj2.day));
        }

        public static bool operator >(Date obj1, Date obj2)
        {
            if (obj1 == null)
                return false;
            else if (obj2 == null)
                return true;

            return (obj1.month > obj2.month || (obj1.month == obj2.month && obj1.day > obj2.day));
        }



        /// <summary>
        /// Повертає об'єкт классу представлений у вигляді рядка
        /// </summary>
        /// <returns>
        /// Рядок з строковими представленнями об'єкту у вигляді рядка таблиці
        /// </returns>
        public string ToTableRow()
        {
            const string Format = "{0, 2:00}.{1, 2:00}\u2502";
            return string.Format(Format, day, month);
        }

        public override string ToString()
        {
            return day + ' '.ToString() + month;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Date date = obj as Date;
            if (date == null)
                { return false; }

            return (date.day == day && Month == date.month);

        }

        public object Clone()
        {
            return new Date(this.day, this.month);
        }
    }
}
