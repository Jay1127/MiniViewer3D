using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiniViewer3D
{
    /// <summary>
    /// ColorAttributeControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ColorAttributeControl : Control
    {
        public SolidColorBrush Color
        {
            get { return (SolidColorBrush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(SolidColorBrush), typeof(ColorAttributeControl));

        public ICommand ClickSettingCommand
        {
            get { return (ICommand)GetValue(ClickSettingCommandProperty); }
            set { SetValue(ClickSettingCommandProperty, value); }
        }
        
        public static readonly DependencyProperty ClickSettingCommandProperty =
            DependencyProperty.Register("ClickSettingCommand", typeof(ICommand), typeof(ColorAttributeControl));

        public object ClickSettingCommandParameter
        {
            get { return (object)GetValue(ClickSettingCommandParameterProperty); }
            set { SetValue(ClickSettingCommandParameterProperty, value); }
        }

        public static readonly DependencyProperty ClickSettingCommandParameterProperty =
            DependencyProperty.Register("ClickSettingCommandParameter", typeof(object), typeof(ColorAttributeControl));

        public ColorAttributeControl()
        {
            InitializeComponent();
        }
    }
}
