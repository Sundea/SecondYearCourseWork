using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.View
{
    class StringConstants
    {
        #region Date string

        public const string DaySell = "День продажу: ";
        public const string MonthSell = "Мiсяць продажу: ";

        #endregion


        #region Sellers string

        public const string SellerName = "Iм'я продавця: ";
        public const string SellerSurname = "Прiзвище продавця: ";
        public const string SellerEmployeDay = "День прийому на роботу: ";
        public const string SellerEmployeMonth = "Мiсяць прийому на роботу: ";

        #endregion


        #region Goods string

        public const string GoodsName = "Найменування товару: ";
        public const string GoodsPrice = "Цiна товару за одиницю: ";
        public const string GoodsReceiptDay = "День прийому товару: ";
        public const string GoodsReceiptMonth = "Мiсяць прийому товару: ";
        public const string GoodsType = "Тип товару: ";

        #endregion

        public const string Amount = "Кiлькiсть проданого товару: ";

        public const string EmptyList = "\nВ системi ще немає операцiй.\nДодайте данi та спробуйте знову.";

        #region таблиця

        public const string TopLine =    "┌─────┬──────────────┬───────────┬───────┬───────┬──────────────┬──────────────┐";
        public const string HeaderFormat = "│{0,5}│{1, 14}│{2, 11}│{3, 7:0.0}│{4, 7:0.0}│{5, 14:0.0}│{6, 14}│";
        public const string CenterLine = "├─────┼──────────────┼───────────┼───────┼───────┼──────────────┼──────────────┤";
        public const string BottomLine = "└─────┴──────────────┴───────────┴───────┴───────┴──────────────┴──────────────┘";

        #endregion

        #region Файли

        public const string DefaultBinaryFileName = "DailyBalance.bin";
        public const string DefaultTextFileName = "DailyBalance.txt";
        public const string FileIOError = "Помилка вводу/виводу";
        public const string AccessException = "Вiдмовлено в доступi";
        public const string WriteAccept = "Успiшно записано в ";
        public const string ReadAccept = "Успiшно зчитано з ";
        public const string FileNotFound = "Файл не знайдено";
        public const string FormatError = "Зчитанi данi невiрного формату.";
        public const string ArgumentOutError = "Зчитанi данi некоректнi.";
        public const string FileIOErrorOpen = "Невiрне iм'я файлу.";
        public const string SimilarSales = "Однакових об'єктiв: ";

        #endregion

        #region Menu

        public const string InputData = "Введення даних";
        public const string PrintTableData = "Виведення даних на екран в табличному виглядi";
        public const string WriteTextFile = "Запис у текстовий файл";
        public const string WriteBinaryFile = "Запис у бiнарний файл";
        public const string ReadTextFile = "Зчитування з текстового файлу";
        public const string ReadBinaryFile = "Зчитування з бiнарного файлу";
        public const string SearchByField = "Пошук за полем";
        public const string StatisticFunc = "Статистичнi функцiї";

        #endregion

        public const string StatisticFuncMax = "Операцiя продажу з масимальною сумою";
        public const string StatisticFuncMin = "Операцiя продажу з мiнiмальною сумою";
        

        #region Search Menu
        public const string SearchByGoodsName = "Шукати за найменуванням товару";
        public const string SearchByPrice = "Шукати за цiною";
        #endregion

        #region YesNoMenu

        public const string MenuYes = "Так";
        public const string MenuNo = "Нi";

        public const string QuestionAddSell = "Додати ще одну операцiю покупки?";


        #endregion

        public const string MenuLastItemBack = "Назад";
        public const string MenuLastItemExit = "Вихiд";


        public const string MenuNavigation = "Для навiгацiї по меню використовуйте стрiлки \u2191 та \u2193";

        #region Header
        public const string HeaderDate = "Дата";
        public const string HeaderGoodsName = "Товар";
        public const string HeaderGoodsType = "Тип";
        public const string HeaderPrice = "Цiна шт";
        public const string HeaderSurname = "Прiзвище";
        public const string HeaderAmount = "Кiльк";
        public const string HeaderTotalPrice = "Цiна";

        #endregion

        public const string SearchNoFound = "Не знайдено вiдповiдних елементiв";

        #region Status bar

        public const string StatusBarCash = "Продано на суму";
        public const string StatusBarCashFormat = "    {0}: {1,10:0000000.0}      ";
        public const string StatusBarSells = "Кiлькiсть продаж";
        public const string StatusBarSellsFormat = "         {0}: {1,10:000}      ";

        #endregion

        #region
        public const string GoodsTypeUnpacked = "Розпакований";
        public const string GoodsTypeWeighedOut = "Ваговий";
        #endregion

        public const string DoubleParseError = "Невiрний тип данних";


        public const string OnlyCharactersError = "Допустимi лише лiтери";
        public const string OnlyCharactersAndNumbersError = "Допустимi лише лiтери та цифри";
        public const string NameSurnameRegex = "^[ієа-яa-z]+";
        public const string GoodsNameRegex = "^[ієа-яa-z1-9]+";
        public const string PositiveNumRegex = "^[+]?[0-9]+";
    }

    
}
