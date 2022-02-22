using System;
using System.Windows.Input;
using WPFAppTest.Infrastructure.Commands.Base;
using WPFAppTest.ViewModels.Base;

namespace WPFAppTest.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public ICommand CreateChildCommand { get; set; }

        public MainViewModel()
        {
            CreateChildCommand = new SimpleCommand(CreateChild);
        }

        private void CreateChild()
        {
            var child = new DemoViewModel()
            {
                Title = "Дочернее окно",
                Date = DateTime.Now
            };
            Show(child);
        }
    }
}
