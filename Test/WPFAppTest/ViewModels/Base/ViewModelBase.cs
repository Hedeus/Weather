using System.Windows;
using WPFAppTest.Views.Windows;

namespace WPFAppTest.ViewModels.Base
{
    internal class ViewModelBase : DependencyObject
    {
        ///
        /// Окно в котором показывается текущий ViewModel
        ///
        private ChildWindow _wnd = null;

        /// <summary>
        /// Заголовок Окна
        /// </summary>        
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string),
            typeof(ViewModelBase), new PropertyMetadata(""));

        ///
        /// Метод вызываемый окном при закрытии
        ///
        protected virtual void Closed()
        {

        }

        /// <summary>
        /// Метод вызываемый для закрытия окна связанного с ViewModel
        /// </summary>  
        public bool Close()
        {
            var result = false;
            if (_wnd != null)
            {
                _wnd.Close();
                _wnd = null;
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Метод показа ViewModel в окне
        /// </summary>  
        protected void Show(ViewModelBase viewModel)
        {
            viewModel._wnd = new ChildWindow();
            viewModel._wnd.DataContext = viewModel;
            viewModel._wnd.Closed += (sender, e) => Closed();
            viewModel._wnd.Show();
        }
    }
}
