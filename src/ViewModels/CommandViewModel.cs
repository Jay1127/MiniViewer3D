using System.Windows.Input;
using MiniMvvm;

namespace MiniViewer3D.ViewModels
{
    public class CommandViewModel : PropertyChangedBase
    {
        #region file command

        /// <summary>
        /// 현재 탭의 정보를 초기화함.
        /// </summary>
        public ICommand NewCommand { get; set; }

        /// <summary>
        /// 새로운 탭을 추가함.
        /// </summary>
        public ICommand NewTabCommand { get; set; }

        /// <summary>
        /// 현재 탭을 닫음.
        /// </summary>
        public ICommand CloseTabCommand { get; set; }

        /// <summary>
        /// 현재 탭에 파일을 읽어 모델 정보를 가져옴.
        /// </summary>
        public ICommand ImportCommand { get; set; }

        /// <summary>
        /// 현재 탭에 모델정보를 내보냄.
        /// </summary>
        public ICommand ExportCommand { get; set; }

        /// <summary>
        /// 프로그램 종료.
        /// </summary>
        public ICommand ExitCommand { get; set; }

        #endregion

        #region view command

        /// <summary>
        /// 투영모드를 변경함.
        /// </summary>
        public ICommand SetProjectionModeCommand { get; set; }

        /// <summary>
        /// 초기 카메라 상태로 되돌림.
        /// </summary>
        public ICommand ResetViewCommand { get; set; }

        /// <summary>
        /// 카메라를 화면의 모델에 맞춰 확대/축소함.
        /// </summary>
        public ICommand FitCommand { get; set; }

        /// <summary>
        /// 주어진 특정 위치로 카메라를 이동함.
        /// </summary>
        public ICommand SetViewCommand { get; set; }

        #endregion

        #region scene command

        /// <summary>
        /// Scene의 배경색을 수정함.
        /// </summary>
        public ICommand SetBackgroundCommand { get; set; }

        public ICommand ShowSettingCommand { get; set; }
        public ICommand ShowComponentCommand { get; set; }

        #endregion

        #region tool command

        /// <summary>
        /// 현재 Scene을 캡쳐함.
        /// </summary>
        public ICommand ScreenShotCommand { get; set; }

        /// <summary>
        /// 현재 Scene을 클립보드에 저장함.
        /// </summary>
        public ICommand CopyClipboardCommand { get; set; }

        #endregion

        #region help command
        #endregion

        public CommandViewModel()
        {

        }
    }
}