using CourseWork.Model;
using CourseWork.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CourseWork.Controller
{
    class ControllerComponent : IObserver
    {
        // public delegate void Statistcs();

        private ViewComponent view;
        private Balance model;


        /// <summary>
        /// Ініціалізує Controller компонент паттерну MVC
        /// </summary>
        /// <param name="view">View компонент паттерну MVC</param>
        /// <param name="model">Model компонент паттерну MVC</param>
        public ControllerComponent(ViewComponent view, Balance model)
        {
            this.model = model;
            this.view = view;
        }


        /// <summary>
        /// Починає роботу програми, викликає основне меню
        /// </summary>
        public void DoActions()
        {
            model.Send += Update;
            model.Notify();
           
            while (view.MainMenu.DoMenuAction() != Menu.ExitCode)
            {
                switch (view.MainMenu.Choice)
                {
                    case 0: ConsoleReadData(); break;
                    case 1: PrintTableModel(model); break;
                    case 2: WriteTextFile(); break;
                    case 3: WriteBinaryFile(); break;
                    case 4: ReadFromTextFile(); break;
                    case 5: ReadFromBinaryFile(); break;
                    case 6: Search(); break;
                    case 7: StatisticFunc(); break;
                }
            }
        }

        #region Пошук

        private void Search()
        {
            if (model.Length > 0)
            {
                view.SearchChoice.DoMenuAction();
                switch (view.SearchChoice.Choice)
                {
                    case 0: SearchByName(); break;
                    case 1: SearchByPrice(); break;
                }
            }
            else
            {
                Console.WriteLine(StringConstants.EmptyList);
                Console.ReadKey();
            }
        }

        private void SearchByPrice()
        {
            Console.Write(StringConstants.GoodsPrice);
            Balance searched = model.Search(model.CompareByPrice,
               ReadFromConsole.ReadDouble(StringConstants.PositiveNumRegex));
            PrintResults(searched);
        }

        private void SearchByName()
        {
            Console.Write(StringConstants.GoodsName);
            Balance searched = null;
            try
            {
                searched = (model.Search(model.CompareByName,
                ReadFromConsole.ReadStringAndReaction(
                    StringConstants.GoodsNameRegex,
                    StringConstants.OnlyCharactersAndNumbersError,
                    RegexOptions.IgnoreCase)));
            }
            finally
            {
                PrintResults(searched);
            }
        }

        private void PrintResults(Balance searched)
        {
            if (searched == null || searched.Length > 0)
            { PrintTableModel(searched); }
            else
            {
                Console.WriteLine(StringConstants.SearchNoFound);
                Console.ReadKey();
            }
        }
        
        #endregion

        #region Робота з файлами

        /// <summary>
        /// Виводить скільки зчитаних елементів було проігноровано,
        /// тому що вони вже є в системі
        /// </summary>
        /// <param name="lenghtBefore">Кількість елементів в системі до читання з файлу</param>
        /// <param name="count">Кількість елементів в файлі</param>
        private void PrintSimilarsCount(int lenghtBefore, int count)
        {
            if (lenghtBefore > 0 && (lenghtBefore + count) > model.Length)
            {
                Console.WriteLine(StringConstants.SimilarSales + (lenghtBefore + count - model.Length).ToString());
            }
        }

        /// <summary>
        /// Зчитує список операцій з текстового файлу
        /// і виводить повідомлення про стан процесу
        /// </summary>
        private void ReadFromTextFile()
        {
            List<Sale> balance;
            bool IsWasException = true;
            try
            {
                balance = Balance.ReadFromTextFile(StringConstants.DefaultTextFileName);
                int count = model.Length;
                foreach (Sale element in balance)
                {
                    model.Add(element);
                }
                Console.WriteLine(StringConstants.ReadAccept + StringConstants.DefaultTextFileName);
                PrintSimilarsCount(count, balance.Count);
                PrintTableModel(model);
                IsWasException = false;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(StringConstants.FileNotFound);
            }
            catch(FormatException)
            {
                Console.WriteLine(StringConstants.FormatError);
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine(StringConstants.ArgumentOutError);
            }
            finally
            {
                if (IsWasException)
                {
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Зчитує список операцій з бінарного файлу
        /// і виводить повідомлення про стан процесу
        /// </summary>
        private void ReadFromBinaryFile()
        {
            List<Sale> read;
           
            try
            {
                read = Balance.ReadFromBinaryFile(StringConstants.DefaultBinaryFileName);
                int count = model.Length;
                foreach(Sale element in read)
                {
                    model.Add(element);
                }

                Console.WriteLine(StringConstants.ReadAccept + StringConstants.DefaultBinaryFileName);
                PrintSimilarsCount(count, read.Count);
                PrintTableModel(model);
            }
            catch (FileNotFoundException)
            {
                ReadFromConsole.WriteError(StringConstants.FileNotFound);
            }
            catch (IOException)
            {
                ReadFromConsole.WriteError(StringConstants.FileIOError);
            }
        }

        /// <summary>
        /// Записує об'єкт у бінарний файл
        /// і виводить повідомлення про стан процесу
        /// </summary>
        private void WriteBinaryFile()
        {
            if (model.Length > 0)
            {
                try
                {
                    model.WriteToBinaryFile(StringConstants.DefaultBinaryFileName);
                    Console.WriteLine(StringConstants.WriteAccept + StringConstants.DefaultBinaryFileName);
                }
                catch (IOException)
                {
                    ReadFromConsole.WriteError(StringConstants.FileIOErrorOpen);
                }
                catch (UnauthorizedAccessException)
                {
                    ReadFromConsole.WriteError(StringConstants.AccessException);
                }
            }
            else
            { Console.WriteLine(StringConstants.EmptyList); }
            
            Console.ReadKey();
        }

        /// <summary>
        /// Записує об'єкт у текстовий файл
        /// і виводить повідомлення про стан процесу
        /// </summary>
        private void WriteTextFile()
        {
            if (model.Length > 0)
            {
                try
                {
                    model.WriteToTextFile(StringConstants.DefaultTextFileName);
                    Console.WriteLine(StringConstants.WriteAccept 
                        + StringConstants.DefaultTextFileName);
                    Console.ReadKey();
                }
                catch (IOException)
                {
                    ReadFromConsole.WriteError(StringConstants.FileIOError);
                }
                catch(UnauthorizedAccessException)
                {
                    ReadFromConsole.WriteError(StringConstants.AccessException);
                }
            }
            else
            {
                ReadFromConsole.WriteError(StringConstants.EmptyList);
            }

        }

        #endregion

        #region Введення даних

        /// <summary>
        /// Введення даних з консолі
        /// </summary>
        public void ConsoleReadData()
        {
            do
            {
                model.Add(Input());
            } while (view.YesNo.DoMenuAction(StringConstants.QuestionAddSell));
        }

        /// <summary>
        /// Забезпечує безпечне ввдееня даних операції продажу користувачем
        /// </summary>
        /// <returns>Ініціалзований користувацькими даними об'єкт</returns>
        public Sale Input()
        {
            Seller seller = InputSeller();
            Date saleDate = InputDate(StringConstants.DaySell, StringConstants.MonthSell);
            Goods goods = InputGoods();

            Console.Write(StringConstants.Amount);
            double amount = ReadFromConsole.ReadDouble(StringConstants.PositiveNumRegex);

            return new Sale(seller, goods, saleDate, amount);
        }

        /// <summary>
        /// Забезпечує безпечне введення даних товару користувачем
        /// </summary>
        /// <returns>Ініціалізований користувачем об'єкт</returns>
        public Goods InputGoods()
        {
            Date receiving = InputDate(StringConstants.GoodsReceiptDay, StringConstants.GoodsReceiptMonth);

            Console.Write(StringConstants.GoodsName);
            string name = ReadFromConsole.ReadStringAndReaction(
                StringConstants.GoodsNameRegex, 
                StringConstants.OnlyCharactersAndNumbersError,
                RegexOptions.IgnoreCase);
            
            Console.Write(StringConstants.GoodsPrice);
            double price = ReadFromConsole.ReadDouble(StringConstants.PositiveNumRegex);

            Console.Write(StringConstants.GoodsType);
            GoodsType type = UpDownChoice();

            return new Goods(name, price, receiving, type);
        }

        /// <summary>
        /// Забезпечує безпечне введеня даних продавця користувачем
        /// </summary>
        /// <returns>ініціалізований об'єкт введеними користувачем значеннями</returns>
        public Seller InputSeller()
        {
            Date recruited = InputDate(StringConstants.SellerEmployeDay, StringConstants.SellerEmployeMonth);

            Console.Write(StringConstants.SellerName);
            string name = ReadFromConsole.ReadStringAndReaction(
                StringConstants.NameSurnameRegex,
                StringConstants.OnlyCharactersError,
                RegexOptions.IgnoreCase);
            
            Console.Write(StringConstants.SellerSurname);
            string surname = ReadFromConsole.ReadStringAndReaction(
                StringConstants.NameSurnameRegex,
                StringConstants.OnlyCharactersError,
                RegexOptions.IgnoreCase);

            return new Seller(name, surname, recruited);
        }

        /// <summary>
        /// Забезпечує правильне введення дати, шляхом безпечного вводу стрілками
        /// </summary>
        /// <param name="dayMessage">Повідомлення про введення дня</param>
        /// <param name="monthMessge">Повідомлення про введення місяця</param>
        /// <returns>Ініціалізований користувачем об'єкт</returns>
        public Date InputDate(string dayMessage, string monthMessge)
        {
            int day;
            int month;

            Console.Write(monthMessge);
            month = UpDownIntInput(Date.MinMonth, Date.MaxMonth);

            Console.Write(dayMessage);
            day = UpDownIntInput(Date.MinDay, Date.MaxDayMonth(month));

            return new Date(day, month);
        }

        /// <summary>
        /// Забезпечує введення цілочисельних значень за допомогою клавіш UpArrow i DownArrow
        /// </summary>
        /// <param name="min">Мінімально допустиме значення</param>
        /// <param name="max">Максимально допустиме значення</param>
        /// <returns>Введене число</returns>
        public int UpDownIntInput(int min, int max)
        {
            if (max <= min)
            { throw new ArgumentOutOfRangeException(); }

            int currentNum = min;

            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            int numbers = 0;
            for (int i = max; i > 0; i /= 10)
            { numbers++; }

            string format = "{0," + numbers + ":" + new string('0', numbers) + '}';


            Console.CursorVisible = false;
            Console.Write(format, min);

            ConsoleKeyInfo pressedKey;
            do
            {
                pressedKey = Console.ReadKey(true);
                switch (pressedKey.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (currentNum > min)
                        { currentNum--; }
                        else
                        { currentNum = max; }
                        break;

                    case ConsoleKey.UpArrow:
                        if (currentNum < max)
                        { currentNum++; }
                        else
                        { currentNum = min; }
                        break;
                }

                Console.CursorLeft -= numbers;
                Console.Write(format, currentNum);

            } while (pressedKey.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return currentNum;
        }

        /// <summary>
        /// Дозволяє вибрати стрілками тип товару
        /// </summary>
        /// <returns>вибраний тип</returns>
        public GoodsType UpDownChoice()
        {
            ConsoleKeyInfo pressed;
            GoodsType choice = GoodsType.Unpacked;

            Console.Write(StringConstants.GoodsTypeUnpacked);

            do
            {
                pressed = Console.ReadKey(true);
                if (pressed.Key == ConsoleKey.UpArrow || pressed.Key == ConsoleKey.DownArrow)
                {
                    if (choice == GoodsType.Unpacked)
                    {
                        choice = GoodsType.WeighedOut;
                        ViewComponent.ClearStringFrom(Console.CursorTop,
                            Console.CursorLeft - StringConstants.GoodsTypeUnpacked.Length);
                        Console.Write(StringConstants.GoodsTypeWeighedOut);
                    }
                    else
                    {
                        choice = GoodsType.Unpacked;
                        ViewComponent.ClearStringFrom(Console.CursorTop,
                                Console.CursorLeft - StringConstants.GoodsTypeWeighedOut.Length);
                        Console.Write(StringConstants.GoodsTypeUnpacked);
                    }
                }
            } while (pressed.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return choice;
        }

        #endregion


        /// <summary>
        /// Виводить об'єкт <paramref name="balance"/> на еркан у вигляді таблиці
        /// </summary>
        /// <param name="balance">Об'єк, що потрібно вивести на екран</param>
        public void PrintTableModel(Balance balance)
        {
            if (balance.Length == 0)
            {
                Console.WriteLine(StringConstants.EmptyList);
                Console.ReadKey();
                return;
            }

            Console.Write(StringConstants.TopLine);
            Console.Write(StringConstants.HeaderFormat, 
                StringConstants.HeaderDate, StringConstants.HeaderGoodsName,
                StringConstants.HeaderGoodsType, StringConstants.HeaderPrice,
                StringConstants.HeaderAmount, StringConstants.HeaderTotalPrice,
                StringConstants.HeaderSurname);
            Console.Write(StringConstants.CenterLine);

            for (int i = 0; i < balance.Length - 1; i++)
            {
                Console.Write(balance[i].ToTableRow());
                Console.Write(StringConstants.CenterLine);
            }
            Console.Write(balance[balance.Length - 1].ToTableRow());

            Console.Write(StringConstants.BottomLine);
            Console.ReadKey();

        }

        public void StatisticFunc()
        {
            view.StatisticChoice.DoMenuAction();
            switch (view.StatisticChoice.Choice)
            {
                case 0: PrintStatistic(model.Max); break;
                case 1: PrintStatistic(model.Min); break;
            }
        }

        public void PrintStatistic(Balance.Statistics statistic)
        {
            try
            {
                Console.Write(StringConstants.TopLine);
                Console.Write(StringConstants.HeaderFormat,
                    StringConstants.HeaderDate, StringConstants.HeaderGoodsName,
                    StringConstants.HeaderGoodsType, StringConstants.HeaderPrice,
                    StringConstants.HeaderAmount, StringConstants.HeaderTotalPrice,
                    StringConstants.HeaderSurname);
                Console.Write(StringConstants.CenterLine);
                Console.Write(statistic().ToTableRow());
                Console.Write(StringConstants.BottomLine);

            }
            catch(Exception)
            {
                ViewComponent.ClearWithotFirst();
                Console.WriteLine(StringConstants.EmptyList);
            }
            finally
            {
                Console.ReadKey();
            }
        }

        public void Update()
        {
            Console.CursorTop = 0;
            Console.BackgroundColor = ViewComponent.StatusBar;
            Console.Write(StringConstants.StatusBarSellsFormat, StringConstants.StatusBarSells, model.Length);
            Console.Write(StringConstants.StatusBarCashFormat, StringConstants.StatusBarCash, model.TotalCash);
            Console.WriteLine();
            Console.BackgroundColor = ViewComponent.Background;
        }
    }
}
