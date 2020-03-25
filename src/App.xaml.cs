using System.Windows;
using Autofac;
using MiniEyes.WpfHelperTools;
using MiniViewer3D.Models;
using MiniViewer3D.ViewModels;

namespace MiniViewer3D
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ContainerBuilder();
;
            builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf().SingleInstance();            

            var container = builder.Build();
            
            using (var scope = container.BeginLifetimeScope())
            {
                var main = scope.Resolve<MainWindow>();
                main.DataContext = scope.Resolve<MainViewModel>();
                main.Show();
            }
        }
    }
}
