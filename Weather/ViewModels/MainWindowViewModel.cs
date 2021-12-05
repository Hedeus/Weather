﻿using Weather.Infrastructure.Commands.Base;
using Weather.ViewModels.Base;
using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Weather.Models.Decanat;
using System.Linq;
using Weather.Data;
using System.Data;

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

        private DataTable _Table;

        public DataTable Table
        {
            get => _Table;
            set => Set(ref _Table, value);
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

        #endregion

        /*------------------------------------------------------------------------------------*/

        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

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

            WorkWithDataBase.OpenConnection("server=localhost;uid=root;pwd=1h9e8d7;database=weather;");
            // string response = WorkWithDataBase.ExecuteQuery("SELECT temperature FROM weather2021 WHERE date='2021-01-13';");
            //TextWeather = response;
            //string sql = "SELECT temperature FROM weather2021 WHERE date='2021-01-13';";
            //string sql = "SELECT * FROM weather2021;";            
            string sql = "SELECT day(t.date) as `День`," +
                         "month(t.date) as `Місяць`," +
                         "temperature as `Температура`," +
                         "pressure as `Тиск`," +
                         "precipitation as `Опади`" +
                         "from weather2021 t;";
            Table = WorkWithDataBase.ExecuteQuery(sql);


            WorkWithDataBase.CloseConnection();

        }
    }
}
