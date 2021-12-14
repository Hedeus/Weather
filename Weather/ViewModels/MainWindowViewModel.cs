using Weather.Infrastructure.Commands.Base;
using Weather.ViewModels.Base;
using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Weather.Models.Decanat;
using System.Linq;
using Weather.Data;
using System.Data;
using Weather.Models.Weather;

namespace Weather.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        /*------------------------------------------------------------------------------------*/

        public ObservableCollection<Group> Groups { get; }

        #region SelectedGroup : Group - Выбранная группа

        /// <summary>Выбранная группа

        private Group _SelectedGroup;
        /// </summary>Выбранная группа
        public Group SelectedGroup 
            { 
                get => _SelectedGroup;
                set => Set(ref _SelectedGroup, value);
            }
        #endregion

        #region Заголовок окна
        private string _Title = "Курсовая работа по C#";

        /// <summary>Заголовок окна</summary>

        public string Title
        {
            get => _Title;
            //set
            //{
            //    //if (Equals(_Title, value)) return;
            //    //_Title = value;
            //    //OnPropertyChanged();
            //
            //    Set(ref _Title, value);
            //}
            set => Set(ref _Title, value);
        }

        #endregion

        #region Status : String - Статус программы

        /// <summary>Статус программы</summary>
        private string _Status = "Готов!";

        /// <summary>Статус программы</summary>
        public string Status 
        {
            get => _Status;
            set => Set(ref _Status, value);
        }

        #endregion

        #region Weather

        private string _TextWeather = "Не відомо";

        public string TextWeather
        {
            get => _TextWeather;
            set => Set(ref _TextWeather, value);
        }


        /// <summary>
        /// Результат запита SELECT
        /// </summary>
        private DataTable _Table;
        public DataTable Table
        {
            get => _Table;
            set => Set(ref _Table, value);
        }

        /// <summary>
        /// Обраний день в таблиці
        /// </summary>
        private DayWeather _SelectedDay;
        public DayWeather SelectedDay
        {
            get => _SelectedDay;
            set => Set(ref _SelectedDay, value);
        }

        /// <summary>
        /// Параметри пошуку
        /// </summary>
        private DesiredDay _desiredDay;
        public DesiredDay desiredDay
        {
            get => _desiredDay;
            set => Set(ref _desiredDay, value);
        }

        #endregion

        /*------------------------------------------------------------------------------------*/

        #region Методы

        internal void WeatherSearch()
        {
            int year = 2021;
            string SearchStr = "SELECT day(t.date) as `День`," +
                             "month(t.date) as `Місяць`," +
                             "temperature as `Температура`," +
                             "pressure as `Тиск`," +
                             "precipitation as `Опади`" +
                             "FROM weather2021 t ";                             
            string StartDateStr = "";
            string EndDateStr = "";
            string StartTempStr = "";
            string EndTempStr = "";
            string StartPresStr = "";
            string EndPresStr = "";
            bool twoDates = false;
            bool IsWhere = false;

            //WorkWithDataBase.OpenConnection("server=localhost;uid=root;pwd=1h9e8d7;database=weather;");

            #region Перевірка не необхідність пошуку, або вивід всіх даних з бази

            if (desiredDay.IsDate)
            {
                if (desiredDay.EndMonth > 12)
                    desiredDay.EndMonth = 12;
                if (desiredDay.StartMonth > 12)
                    desiredDay.StartMonth = 12;

                if (desiredDay.StartDay < 0)
                    desiredDay.StartDay = 0;
                if (desiredDay.EndDay < 0)
                    desiredDay.EndDay = 0;
            }

            if ((desiredDay.IsDate && ((desiredDay.StartMonth != 0) ||
                (desiredDay.StartDay != 0) ||
                (desiredDay.EndMonth != 0) ||
                (desiredDay.EndDay != 0))) ||
                desiredDay.IsTemperature ||
                desiredDay.IsPress ||
                desiredDay.IsPrecipitation())
            {
                SearchStr += "WHERE ";
                IsWhere = true;
            }

            //if (!IsWhere)
            else
            {
                SearchStr += ";";
                Table = WorkWithDataBase.ExecuteQuery(SearchStr);
                return;
            }


            #endregion

            #region Додавання пошуку за датою

            if (desiredDay.IsDate)
            {
                if (desiredDay.StartDay > 0)
                {
                    if (desiredDay.StartMonth == 0)
                        desiredDay.StartMonth = 1;
                    if (desiredDay.EndMonth == 0)
                        desiredDay.EndMonth = 12;
                }
                if ((desiredDay.EndDay > 0) && (desiredDay.EndMonth == 0))
                    desiredDay.EndMonth = 12;

                if (desiredDay.StartMonth > 0)
                {
                    if (desiredDay.StartMonth < desiredDay.EndMonth)
                    {
                        StartDateStr += desiredDay.StartMonth.ToString();
                        EndDateStr += desiredDay.EndMonth.ToString();
                        twoDates = true;
                    }
                    else
                    {
                        StartDateStr += desiredDay.StartMonth.ToString();
                        desiredDay.EndMonth = desiredDay.StartMonth;
                        EndDateStr += desiredDay.EndMonth.ToString();
                    }
                }
                else
                {
                    if (desiredDay.EndMonth >= 0)
                    {
                        desiredDay.StartMonth = 1;
                        StartDateStr += desiredDay.StartMonth.ToString();
                        if (desiredDay.EndMonth == 0)
                            desiredDay.EndMonth = 12;
                        EndDateStr += desiredDay.EndMonth.ToString();
                        twoDates = true;
                    }
                }

                if (desiredDay.StartDay > DateTime.DaysInMonth(year, desiredDay.StartMonth))
                    desiredDay.StartDay = DateTime.DaysInMonth(year, desiredDay.StartMonth);
                if (desiredDay.EndDay > DateTime.DaysInMonth(year, desiredDay.EndMonth))
                    desiredDay.EndDay = DateTime.DaysInMonth(year, desiredDay.EndMonth);

                if (desiredDay.StartDay > 0)
                {
                    if (desiredDay.StartDay < desiredDay.EndDay)
                    {
                        StartDateStr += "," + desiredDay.StartDay.ToString();
                        EndDateStr += "," + desiredDay.EndDay.ToString();
                        twoDates = true;
                    }
                    else
                    {
                        StartDateStr += "," + desiredDay.StartDay.ToString();
                        EndDateStr += "," + desiredDay.StartDay.ToString();
                    }
                }
                else
                {
                    if (desiredDay.EndMonth >= 0)
                    {
                        StartDateStr += ",1";
                        if (desiredDay.EndDay == 0)
                            desiredDay.EndDay = DateTime.DaysInMonth(year, desiredDay.EndMonth);
                        EndDateStr += "," + desiredDay.EndDay.ToString();
                        twoDates = true;
                    }
                }

                StartDateStr += "," + year.ToString();
                EndDateStr += "," + year.ToString();

                if (!twoDates)
                {
                    SearchStr += "`date`=STR_TO_DATE( '" + StartDateStr + "', '%m,%d,%Y' ) ";
                }
                else
                    SearchStr += "`date` >= STR_TO_DATE( '" + StartDateStr + "', '%m,%d,%Y' ) && " +
                                 "`date` <= STR_TO_DATE( '" + EndDateStr + "', '%m,%d,%Y' ) ";
            }

            #endregion

            #region Пошук за температурою

            if (desiredDay.IsTemperature)
            {
                if (desiredDay.IsDate)
                    SearchStr += "&& ";
                SearchStr += "temperature >= " + desiredDay.StartTemperature.ToString() + " &&" +
                             " temperature <= " + desiredDay.EndTemperature.ToString() + " "; 
            }
            #endregion

            #region Пошук за тиском

            if (desiredDay.IsPress)
            {
                if (desiredDay.IsTemperature || (desiredDay.IsDate && !desiredDay.IsTemperature))
                    SearchStr += "&& ";
                SearchStr += "pressure >= " + desiredDay.StartPressure.ToString() + " &&" +
                            " pressure <= " + desiredDay.EndPressure.ToString() + " ";
            }

            #endregion

            #region Пошук за погодними умовами

            if (desiredDay.PresipitationToInt() > 0)
            {
                if (!(!desiredDay.IsDate && !desiredDay.IsTemperature && !desiredDay.IsPress))
                    SearchStr += "&& ";
                SearchStr += "precipitation & " + desiredDay.PresipitationToInt(); 
            }

            #endregion


            //if (desiredDay.IsDate)
            //{
            //    SearchStr += "month(t.date) >= " + desiredDay.StartMonth.ToString() +
            //        " && month(t.date) <= " + desiredDay.EndMonth.ToString() +
            //        " && day(t.date) >= " + desiredDay.StartDay.ToString() +
            //        " && day(t.date) <= " + desiredDay.EndDay.ToString();
            //}
            SearchStr += ";";

            Table = WorkWithDataBase.ExecuteQuery(SearchStr);

            //WorkWithDataBase.CloseConnection();
        }

        #endregion

        /*------------------------------------------------------------------------------------*/

        #region Команды

        #region  CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region Пошук

        public ICommand SearchCommand { get; }

        private bool CanSearchCommandExecute(object p) => true;

        private void OnSearchCommandExecuted(object p)
        {
            WeatherSearch();
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------*/

        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            SearchCommand = new LambdaCommand(OnSearchCommandExecuted, CanSearchCommandExecute);

            #endregion

            var student_index = 1;

            var students = Enumerable.Range(1, 10).Select(i => new Student
            {
                Name = $"Name {student_index}",
                Surname = $"Surname {student_index}",
                Patronymic = $"PAtronymic {student_index++}",
                Birthday = DateTime.Now,
                Rating = 0
            });

            var groups = Enumerable.Range(1, 20).Select(i => new Group
            {
                Name = $"Группа { i }",
                Students = new ObservableCollection<Student>(students)
            });
            Groups = new ObservableCollection<Group>(groups);

            //WorkWithDataBase.OpenConnection("server=localhost;uid=root;pwd=1h9e8d7;database=weather;");
            //// string response = WorkWithDataBase.ExecuteQuery("SELECT temperature FROM weather2021 WHERE date='2021-01-13';");
            ////TextWeather = response;
            ////string sql = "SELECT temperature FROM weather2021 WHERE date='2021-01-13';";
            ////string sql = "SELECT * FROM weather2021;";            
            //string sql = "SELECT day(t.date) as `День`," +
            //             "month(t.date) as `Місяць`," +
            //             "temperature as `Температура`," +
            //             "pressure as `Тиск`," +
            //             "precipitation as `Опади`" +
            //             "from weather2021 t;";
            //Table = WorkWithDataBase.ExecuteQuery(sql);

            desiredDay = new DesiredDay();
            desiredDay.StartDay = 20;
            desiredDay.StartMonth = 3;
            desiredDay.EndDay = 29;
            desiredDay.EndMonth = 8;
            //desiredDay.StartDay = 0;
            //desiredDay.StartMonth = 0;
            //desiredDay.EndDay = 0;
            //desiredDay.EndMonth = 0;
            //Table = new DataTable();

            WorkWithDataBase.OpenConnection("server=localhost;uid=root;pwd=1h9e8d7;database=weather;");


            //WorkWithDataBase.CloseConnection();

        }
    }
}
