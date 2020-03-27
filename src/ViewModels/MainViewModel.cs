using MiniEyes;
using MiniEyes.WpfHelperTools;
using MiniMvvm;
using MiniViewer3D.Models;

namespace MiniViewer3D.ViewModels
{
    public class MainViewModel : PropertyChangedBase
    {
        /// <summary>
        /// Scene의 레이아웃을 관리
        /// </summary>
        public SceneLayoutViewModel SceneLayoutViewModel { get; }

        /// <summary>
        /// 상단의 명령어 버튼을 관리
        /// </summary>
        public CommandViewModel CommandViewModel { get; }

        public MainViewModel(IDialogService dialogService)
        {
            CommandViewModel = new CommandViewModel();
            SceneLayoutViewModel = new SceneLayoutViewModel(dialogService);

            CommandViewModel.NewCommand = new DelegateCommand(SceneLayoutViewModel.New, SceneLayoutViewModel.HasActiveScene);
            CommandViewModel.NewTabCommand = new DelegateCommand(SceneLayoutViewModel.AddScene);
            CommandViewModel.CloseTabCommand = new DelegateCommand(SceneLayoutViewModel.CloseActiveScene, SceneLayoutViewModel.HasActiveScene);
            //CommandViewModel.AddCommand = new DelegateCommand(SceneLayoutViewModel.AddModel, SceneLayoutViewModel.HasActiveScene);
            CommandViewModel.ImportCommand = new DelegateCommand(SceneLayoutViewModel.ImportFile);
            CommandViewModel.ExitCommand = new DelegateCommand(Shutdown);

            CommandViewModel.SetProjectionModeCommand = new DelegateCommand<ProjectionMode>(SceneLayoutViewModel.SetProjectionMode);
            CommandViewModel.FitCommand = new DelegateCommand(SceneLayoutViewModel.Fit, SceneLayoutViewModel.HasActiveScene);
            CommandViewModel.ResetViewCommand = new DelegateCommand(SceneLayoutViewModel.Reset, SceneLayoutViewModel.HasActiveScene);
            CommandViewModel.SetViewCommand = new DelegateCommand<ViewMode>(SceneLayoutViewModel.SetView, (o) => SceneLayoutViewModel.HasActiveScene());

            CommandViewModel.ScreenShotCommand = new DelegateCommand(SceneLayoutViewModel.ScreenShot);
            CommandViewModel.CopyClipboardCommand = new DelegateCommand(SceneLayoutViewModel.CopyClipBoard);
        }

        private void Shutdown()
        {
            App.Current.Shutdown();
        }
    }
}