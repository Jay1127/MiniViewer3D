using System.Windows;
using MiniViewer3D.ViewModels;

namespace MiniViewer3D
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLogoDoubleClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                App.Current.Shutdown();
            }
        }

        private void OnMaximizeBtnClicked(object sender, RoutedEventArgs e)
        {
            if(WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void OnMinimizeBtnClicked(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void OnCloseBtnClicked(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
