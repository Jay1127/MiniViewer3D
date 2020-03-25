using System;
using System.Windows;
using MiniEyes.WpfHelperTools;

namespace MiniViewer3D
{
    /// <summary>
    /// ScreenShotResultPopup.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ScreenShotResultPopup : Window, IDialogWindow
    {
        public IDialogModel Model
        {
            get => (IDialogModel)DataContext;
            set => DataContext = value;
        }

        public ScreenShotResultPopup()
        {
            InitializeComponent();

            Owner = App.Current.MainWindow;
            this.Closed += OnClosed;
        }

        private void OnClosed(object sender, EventArgs e)
        {
            image.Source = null;
            UpdateLayout();
            GC.Collect();
        }

        private void OnCloseBtnClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
