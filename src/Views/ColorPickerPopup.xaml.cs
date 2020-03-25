using System;
using System.Windows;
using MiniEyes.WpfHelperTools;

namespace MiniViewer3D
{
    /// <summary>
    /// ColorPickerPopup.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ColorPickerPopup : Window, IDialogWindow
    {
        public IDialogModel Model
        {
            get => (IDialogModel)DataContext;
            set => DataContext = value;
        }

        public ColorPickerPopup()
        {
            InitializeComponent();

            Owner = App.Current.MainWindow;
        }

        private void OnCloseBtnClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
