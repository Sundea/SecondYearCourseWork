using System;
 
namespace CourseWork.Model
{
    //Операція Продажу { Дата (продажу), Продавець, Товар, Кількість товару };
    [Serializable()]
    class Sale : ISpreadsheet
    {
        private double amount = 1; // кількість товару
        private Date saleDate;
        private Goods goodsTransaction;
        private Seller seller;


        /// <summary>
        /// Ініціалізує операцію продажу заданими значеннями
        /// </summary>
        /// <see cref="Amount"/>
        /// <param name="seller">Продавець</param>
        /// <param name="goods">Товар</param>
        /// <param name="saleDate">Дата продажу</param>
        /// <param name="amount">Кількість</param>
        public Sale(Seller seller, Goods goods, Date saleDate, double amount)
        {
            SellerTransaction = seller;
            GoodsTransaction = goods;
            SaleDate = saleDate;
            Amount = amount;
        }

        /// <summary>
        /// Ініціалізує об'єкт string параметрами, попередньо конвертуючи їх в потрібний тип
        /// </summary>
        /// <exception cref="FormatException">Викидається при конвертації данних, коли дані є невірного типу</exception>
        /// <see cref="Date"/>
        /// <see cref="Seller"/>
        /// <see cref="Goods"/>
        /// <see cref="GoodsType"/>
        /// <see cref="Amount"/>
        /// <param name="saleDay">День продажу</param>
        /// <param name="saleMonth">Місяць продажу</param>
        /// <param name="sellerEmployDay">День прийняття на роботу продавця</param>
        /// <param name="sellerEmployMonth">Місяць прийняття на роботу продавця</param>
        /// <param name="sellerName">Ім'я продавця</param>
        /// <param name="sellerSurname">Прізвище продавця</param>
        /// <param name="goodsReceiptDay">День отримання товару</param>
        /// <param name="goodsReceiptMonth">Місяць отримання товару</param>
        /// <param name="goodsName">Назва товару</param>
        /// <param name="goodsPrice">Ціна товару</param>
        /// <param name="goodsType">Тип товару</param>
        /// <param name="amount">Кількість проданого товару</param>
        public Sale(string saleDay, string saleMonth, string sellerEmployDay,
            string sellerEmployMonth, string sellerName, string sellerSurname,
            string goodsReceiptDay, string goodsReceiptMonth,
            string goodsName, string goodsPrice, string goodsType,
            string amount)
        {
            SellerTransaction = new Seller(sellerEmployDay, sellerEmployMonth,
                sellerName, sellerSurname);

            goodsTransaction = new Goods(goodsReceiptDay, goodsReceiptMonth,
                goodsName, goodsPrice, goodsType);

            SaleDate = new Date(saleDay, saleMonth);
            Amount = Double.Parse(amount);
        }
        


        /// <summary>
        /// Повертає та встановлює продавця даної операції продажу
        /// </summary>
        internal Seller SellerTransaction
        {
            get
            {
                return seller;
            }

            private set
            {
                if (value == null)
                    throw new ArgumentNullException(SupportText.Goods,
                        SupportText.ArgumentNull);
                seller = value;
            }
        }

        /// <summary>
        /// Повертає/встановлює товар даної операції продажу
        /// </summary>
        internal Goods GoodsTransaction
        {
            get
            {
                return goodsTransaction;
            }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException(SupportText.Goods,
                        SupportText.ArgumentNull);
                goodsTransaction = new Goods(value);
            }
            
        }

        internal Date SaleDate
        {
            get
            {
                return saleDate;
            }

            private set
            {
                if (value == null)
                    throw new ArgumentNullException(SupportText.Date,
                        SupportText.ArgumentNull);
                saleDate = value;
            }
        }

        /// <summary>
        /// Повертає та встановлює кількість проданого товару
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Викидається, коли намагаються присвоїти значення менше\рівне 0</exception>
        public double Amount
        {
            get
            {
                return amount;
            }

            set
            {
                if (value > 0.0)
                    { amount = value; }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        SupportText.SaleAmount, SupportText.ArgumentOutOfRange
                        + ": 0.0 <");
                }
            }
        }

        public double TotalPrice
        {
            get
            {
                return amount * GoodsTransaction.Price;
            }
        }

        

        public override string ToString()
        {
            return SaleDate.ToString() + ' ' + SellerTransaction.ToString() + ' ' + GoodsTransaction.ToString() + ' ' + Amount;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Sale sale = obj as Sale;

            if (sale == null)
                return false;

            if (sale == this)
            {
                return true;
            }

            return (amount == sale.amount && saleDate.Equals(sale.saleDate)
                && SellerTransaction.Equals(sale.SellerTransaction)
                && GoodsTransaction.Equals(sale.GoodsTransaction));
        }

        /// <summary>
        /// Повертає об'єкт классу представлений у вигляді рядка таблиці
        /// </summary>
        /// <returns>
        /// Рядок з строковими представленнями об'єкту у вигляді рядка таблиці
        /// </returns>
        public string ToTableRow()
        {
            return '\u2502' + saleDate.ToTableRow() +  GoodsTransaction.ToTableRow() + String.Format("{0, 7: 0.0}\u2502{1, 14:0.0}\u2502", amount, TotalPrice) + SellerTransaction.ToTableRow();
        }
    }
}
