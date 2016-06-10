using System;
 
namespace CourseWork.Model
{
    //Товар {Назва, Дата (надходження), Тип (Ваговий, розпакований), Ціна} 

    /// <summary>
    /// Перелічування стану товарів
    /// </summary>
    public enum GoodsType { Unpacked, WeighedOut }

    [Serializable()]
    class Goods : ISpreadsheet
    {
        private string name; // Ім'я товару
        private double price; // Ціна товару
        private Date receiving;



        
        /// <summary>
        /// Ініціалізує об'єкт товару значеннями 
        /// </summary>
        /// /// <see cref="Name"/>
        /// <see cref="Price"/>
        /// <see cref="Date.Day"/>
        /// <see cref="Date.Month"/>
        /// <see cref="GoodsType"/>
        /// <see cref="Date"/>
        /// <param name="name">Ім'я товару</param>
        /// <param name="price">Ціна товару</param>
        /// <param name="date">Дата надходження товару</param>
        /// <param name="type">Тип товару</param>
        public Goods(string name, double price, Date date, GoodsType type)
        {
            Name = name;
            Price = price;
            Receiving = date;
            Type = type;
        }

        /// <summary>
        /// Ініціалізує об'єкт за допомогою рядкових параметрів, попередньо приводячи їх до потрібного типу
        /// </summary>
        /// <exception cref="FormatException">Викидається, якщо неможливо виконати метод <code>.Parse</code></exception>
        /// <param name="day">Строкове представлення дня місяця надходження товару</param>
        /// <param name="month">Строкове представлення місяця надходження товару</param>
        /// <param name="name">Ім'я товару</param>
        /// <param name="price">Строкове представлення ціни товару</param>
        /// <param name="Type">Строкове представлення типу товару</param>
        public Goods(string day, string month, string name, string price, string type)
        {
            Name = name;
            Price = Double.Parse(price);
            Type = (GoodsType)Enum.Parse(typeof(GoodsType), type);
            Receiving = new Date(day, month);
        }

        public Goods(Goods goods)
        {
            this.name = goods.name;
            this.price = goods.price;
            receiving = new Date(goods.Receiving);
        }
        


        /// <summary>
        /// Повертає\встановлює тип товару
        /// </summary>
        /// <see cref="GoodsType"/>
        public GoodsType Type
        {
            get; protected set;
        }

        /// <summary>
        /// Повертає та встановлює ціну товару
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Викидається, якщо
        /// <paramref name="value"/> менше рівне 0</exception>
        /// <value>Значення має бути більше\рівне 0</value>
        public double Price
        {
            get
                { return price; }

            protected set
            {
                if (value > 0.0)
                    { price = value; }
                else
                    { throw new ArgumentOutOfRangeException(
                        SupportText.Price, SupportText.ArgumentOutOfRange 
                        + ":  0.0 <"); }
            }
        }

        /// <summary>
        /// Повертає та встановлює назву товару
        /// </summary>
        /// <exception cref="ArgumentNullException">Викидуєтья, якщо <paramref name="value"/> <c>null</c> або <c>""</c></exception>
        public string Name
        {
            get
            {
                return name;
            }
            protected set
            {
                if (String.IsNullOrEmpty(value)) // якщо null або ""
                {
                    throw new ArgumentNullException(SupportText.GoodsName,
                        SupportText.ArgumentNull);
                }
                else
                    { name = value; }
            }
        }

        /// <summary>
        /// Повертає та встановлє посилання на дату прийняття товару
        /// </summary>
        public Date Receiving
        {
            get
            {
                return receiving;
            }

            private set
            {
                if (value == null)
                    throw new ArgumentNullException(SupportText.Date,
                        SupportText.ArgumentNull);
                receiving = value;
            }
        }

        public override string ToString()
        {
            return receiving.ToString() + ' ' + name + ' ' + price + ' ' + Enum.GetName(typeof(GoodsType), Type);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Goods goods = obj as Goods;

            if (goods == null)
                return false;

            return (receiving.Equals(goods.receiving) && price == goods.price
                && Name.Equals(goods.Name) && Type == goods.Type);
        }

        /// <summary>
        /// Повертає об'єкт классу представлений у вигляді рядка
        /// </summary>
        /// <returns>
        /// Рядок з строковими представленнями об'єкту у вигляді рядка таблиці
        /// </returns>
        public string ToTableRow()
        {
            const string Format = "{0, 14}\u2502{1, 11}\u2502{2,7:##0.0}\u2502";
            return String.Format(Format, name, (Type == GoodsType.Unpacked) 
                ? SupportText.GoodsTypeUnpacked 
                : SupportText.GoodsTypeWeighedOut, price);
        }
    }
}
