using System;
using System.Windows;
using System.Windows.Input;
using WPFAppTest.Infrastructure.Commands.Base;
using WPFAppTest.ViewModels.Base;

namespace WPFAppTest.ViewModels
{
    internal class DemoViewModel : ViewModelBase
    {
        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        // Using a DependencyProperty as the backing store for Date.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register("Date", typeof(DateTime), typeof(DemoViewModel), new PropertyMetadata(null));

        public ICommand CloseCommand
        {
            get => (ICommand)GetValue(CloseCommandProperty);
            set => SetValue(CloseCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for CloseCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseCommandProperty =
            DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(DemoViewModel), new PropertyMetadata(null));

        public DemoViewModel()
        {
            CloseCommand = new SimpleCommand(() => Close());
        }
    }
}
