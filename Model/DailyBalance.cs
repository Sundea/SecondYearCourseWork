using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace CourseWork.Model
{

    [Serializable()]
    class Balance : IObservable, ISpreadsheet, IBalanceIndexator
    {
        List<Sale> balance; //список операцій

        public delegate bool Compare(Sale sale, object obj);

        public delegate Sale Statistics();
        protected event NotifyObserver send;

        /// <summary>
        /// Ініціалізує новий об'єкт DailyBalance
        /// </summary>
        public Balance()
        {
            balance = new List<Sale>();
        }

        public event NotifyObserver Send
        {
            add
            {
                if (value != null)
                {
                    send += value;
                }
            }

            remove
            {
                if (value != null)
                {
                    send -= value;
                }
            }
        }


        /// <summary>
        /// Повертає кількість операцій в об'єкті
        /// </summary>
        public int Length
        {
            get
            {
                return balance.Count;
            }
        }

        public double TotalCash
        {
            get
            {
                double totalCash = 0.0;
                foreach (Sale sale in balance)
                {
                    totalCash += sale.TotalPrice;
                }
                return totalCash;
            }
        }

        #region Файли

        /// <summary>
        /// Записує даний об'єкт у бінарний файл
        /// </summary>
        /// <exception cref="ArgumentNullException">Викидується якщо ім'я файлу null або \"\"</exception>
        /// <param name="fileName">Ім'я файлу</param>
        /// <returns><code>true</code>, якщо запис успішний</returns>
        public void WriteToBinaryFile(string fileName)
        {
            Stream stream = null;
            BinaryFormatter writer;

            try
            {
                stream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                writer = new BinaryFormatter();
                writer.Serialize(stream, balance);
            }
            finally
            {
                if (stream != null)
                    { stream.Close(); }
            }
        }

        /// <summary>
        /// Зчитує об'єкт з бінарного файлу
        /// </summary>
        /// <param name="fileName">Ім'я файлу</param>
        /// <returns>Зчитаний об'єкт</returns>
        public static List<Sale> ReadFromBinaryFile(string fileName)
        {
            List<Sale> readInformation;
            Stream stream = null;
            try
            {
                stream = File.Open(fileName, FileMode.Open);
                BinaryFormatter bFormatter = new BinaryFormatter();
                readInformation = (List<Sale>)bFormatter.Deserialize(stream);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            return readInformation;
        }

        /// <summary>
        /// Зчитує об'єкт з текстового файлу і повертає його
        /// </summary>
        /// <exception cref="ArgumentNullException">Викидується, коли ім'я файлу некоректне</exception>
        /// <param name="fileName">Ім'я файлу з якого буде проводитись зчитування</param>
        /// <returns>Зчитаний об'єкт</returns>
        public static List<Sale> ReadFromTextFile(string fileName)
        {
            StreamReader reader = null;
            List<Sale> readInformation = null;
            Sale record;
            string [] inputFields;
            try
            {
                reader = File.OpenText(fileName);
                readInformation = new List<Sale>();

                while (reader.Peek() != -1) // поки не кінець файлу
                {
                    inputFields = reader.ReadLine().Split(' '); 
                    
                    record = new Sale(inputFields[0], inputFields[1],
                        inputFields[2], inputFields[3], inputFields[4],
                        inputFields[5], inputFields[6], inputFields[7],
                        inputFields[8], inputFields[9], inputFields[10],
                        inputFields[11]);

                    readInformation.Add(record);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return readInformation;    
        }

        /// <summary>
        /// Записує об'єкт у текстовий файл
        /// </summary>
        /// <exception cref="ArgumentNullException">Якщо ім'я файлу некоректне</exception>
        /// <param name="fileName">Ім'я файлу</param>
        public void WriteToTextFile(string fileName)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(fileName);
                foreach (Sale element in balance)
                {
                    writer.WriteLine(element.ToString());
                }
            }
            finally
            {
                if (writer != null)
                    { writer.Close(); }
            }
        }

        #endregion

        /// <summary>
        /// Повертає посилання на операцію продажу за індексом
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">Якщо індекс поза діапазоном</exception>
        /// <param name="index">Індекс елемента</param>
        /// <returns>Операцію продажу по індексу</returns>
        public Sale this[int index]
        {
            get
            {
                if (index >= 0 && index < Length)
                   { return balance[index]; }
                else
                   { throw new IndexOutOfRangeException("Індекс колекції за межами діапазону"); }
            }
        }

        /// <summary>
        /// Додає операцію продажу відсортовану по даті
        /// </summary>
        /// <exception cref="ArgumentNullException">Якщо <paramref name="sale"/> = <c>null</c></exception>
        /// <param name="sale">Операція продажу</param>
        public void Add(Sale sale)
        {
            if (sale != null)
            {
                if (balance.Contains(sale))
                    return;

                int i = 0;
                while (i < balance.Count && balance[i].SaleDate < sale.SaleDate)
                {
                    i++;
                }
                
                balance.Insert(i, sale);
                Notify();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Розсилає зміну стану об'єкту
        /// </summary>
        /// <see cref="IObservable"/>
        /// <remarks>
        /// Реалізує інтерфейс <see cref="IObservable"/>
        /// </remarks>
        public void Notify()
        {
            if (send != null)
                send();
        }

        #region Пошук

        /// <summary>
        /// Метод пошуку
        /// </summary>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Balance Search(Compare del, object obj)
        {
            Balance sales = new Balance();
            foreach (Sale element in balance)
            {
                if (del(element, obj))
                {
                    sales.Add(element);
                }
            }

            return sales;
        }

        /// <summary>
        /// Метод для порівнювання за ім'ям
        /// </summary>
        /// <param name="sale"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CompareByName(Sale sale, object name)
        {
            string field = (name as string).ToUpper();
            return (sale.GoodsTransaction.Name.ToUpper().Equals(field));
        }

        /// <summary>
        /// Методі для порівнювання за ціною
        /// </summary>
        /// <param name="sale"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public bool CompareByPrice(Sale sale, object price)
        {
            
            return (sale.GoodsTransaction.Price.Equals(price));
        }

        #endregion

        public Sale Max()
        {
            Sale max;
            try
            {
                max = balance[0];
            }
            catch
            {
                throw;
            }
            foreach (Sale sale in balance)
            {
                if (sale.TotalPrice > max.TotalPrice)
                    max = sale;
            }
            return max;
        }

        public Sale Min()
        {
            Sale min;
            try
            {
                min = balance[0];
            }
            catch
            {
                throw;
            }
            foreach (Sale sale in balance)
            {
                if (sale.TotalPrice < min.TotalPrice)
                    min = sale;
            }
            return min;
        }

        public string ToTableRow()
        {
            StringBuilder rows = new StringBuilder();
            foreach (Sale element in balance)
            {
                rows.Append(element.ToTableRow());
            }
            return rows.ToString();
        }

    }
}
