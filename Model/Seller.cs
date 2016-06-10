using System;
 
namespace CourseWork.Model
{
    //Продавець {Прізвище, Ім’я, Дата (прийняття на  роботу)};

    [Serializable()]
    class Seller: ISpreadsheet
    {
        private string name; 
        private string surname;
        private Date recruited;


        /// <summary>
        /// Ініціалізує об'єкт значеннями
        /// </summary>
        /// <param name="name">Ім'я продавця</param>
        /// <param name="surname">Прізвище продавця</param>
        /// <param name="date">Дата прийняття на роботу</param>
        public Seller(string name, string surname, Date date)
        {
            Name = name;
            Surname = surname;
            recruited = new Date(date);
        }

        /// <summary>
        /// Ініціалізує об'єкт string параметрами, попередньо конвертуючи їх в потрібний тип
        /// </summary>
        /// <param name="day">День прийняття на роботу</param>
        /// <param name="month">Місяць прийняття на роботу</param>
        /// <param name="name">Ім'я продавця</param>
        /// <param name="surname">Прізвище продавця</param>
        public Seller(string day, string month, string name, string surname)
        {
            Name = name;
            Surname = surname;
            recruited = new Date(day, month);
        }

        /// <summary>
        /// Повертає\встановлює ім'я продавця
        /// </summary>
        /// <exception cref="ArgumentNullException">Викидуєтья, якщо <paramref name="value"/> <c>null</c> або <c>""</c></exception>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(SupportText.SellerName,
                        SupportText.ArgumentNull);
                }
                else
                {
                    name = value;
                }
            }
        }

        /// <summary>
        /// Повертає\встановлює прізвище продавця
        /// </summary>
        /// <exception cref="ArgumentNullException">Викидуєтья, якщо <paramref name="value"/> <c>null</c> або <c>""</c></exception>
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(SupportText.SellerSurname,
                        SupportText.ArgumentNull);
                }
                else 
                    { surname = value; }

            }
        }

        internal Date Recruited
        {
            get
            {
                return recruited;
            }

            private set
            {
                if (value == null)
                    throw new ArgumentNullException(SupportText.Date,
                        SupportText.ArgumentNull);
                recruited = value;
            }
        }

        public override string ToString()
        {
            return recruited.ToString() + ' ' + Name + ' ' + Surname;
        }

        public new bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Seller seller = obj as Seller;

            if (seller == null)
                { return false; }

            return (Name.Equals(seller.Name) && Surname.Equals(seller.Surname)
                && recruited.Equals(seller.recruited));
        }

        /// <summary>
        /// Повертає об'єкт классу представлений у вигляді рядка таблиці
        /// </summary>
        /// <returns>
        /// Рядок з строковими представленнями об'єкту у вигляді рядка таблиці
        /// </returns>
        public string ToTableRow()
        {
            const string Format = "{0, 14}\u2502";
            return string.Format(Format, Surname);
        }
    }
}
