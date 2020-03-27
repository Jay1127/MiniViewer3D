using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniEyes;
using MiniEyes.Documents;
using MiniEyes.WpfHelperTools;
using MiniMvvm;
using MiniViewer3D.Models;
using Application = System.Windows.Application;
using Clipboard = System.Windows.Forms.Clipboard;

namespace MiniViewer3D.ViewModels
{
    public class SceneLayoutViewModel : PropertyChangedBase
    {
        private SceneAttribute _activeSceneModel;

        private IDialogService _dialogService;

        public ObservableCollection<SceneAttribute> SceneModels { get; }

        /// <summary>
        /// 현재 Active된 탭
        /// </summary>
        public TabLayoutViewModel ActiveTabLayoutViewModel { get; set; }

        /// <summary>
        /// 현재 선택된 Scene
        /// </summary>
        public SceneAttribute ActiveSceneModel
        {
            get => _activeSceneModel;
            set
            {
                SetProperty(ref _activeSceneModel, value);

                SetUpTabLayout();

                NotifyPropertyChanged(nameof(_activeSceneModel.Scene));
            }
        }

        public SceneLayoutViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            SceneModels = new ObservableCollection<SceneAttribute>();
            ActiveTabLayoutViewModel = new TabLayoutViewModel(dialogService);

            AddScene();
        }

        public void AddScene()
        {
            var sceneNode = new SceneAttribute();

            SceneModels.Add(sceneNode);

            ActiveSceneModel = sceneNode;
        }

        public void CloseActiveScene()
        {
            RemoveScene(ActiveSceneModel);
        }

        public void RemoveScene(SceneAttribute sceneNode)
        {
            SceneModels.Remove(sceneNode);

            if (ActiveSceneModel == sceneNode)
            {
                ActiveSceneModel = SceneModels.LastOrDefault();
            }

            sceneNode.Dispose();
        }

        public async void ImportFile()
        {
            string filePath = OpenDialog();

            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            var dialogData = new BusyIndicatorModel("Loading...", "Loading...");
            _dialogService.ShowDialogAsync<MiniBusyIndicatorPopup>(dialogData);

            // read -> new 순서로 실행(파일이 읽어진 경우만 new 실행)   
            await Task.Run(() =>
            {
                var document = ReadFile(filePath);

                if (ActiveSceneModel == null)
                {
                    AddScene();
                }

                Application.Current.Dispatcher.Invoke(() =>
                {
                    //ActiveSceneModel.New();
                    New();

                    ActiveSceneModel.AddDocumentToScene(document);

                    SetUpTabLayout();
                    //NodeTree.ChangeTreeRoot(_activeSceneModel);
                });
            });

            dialogData.IsShown = false;
        }

        public void New()
        {
            // tabs
            ActiveTabLayoutViewModel.Cleanup();

            // scene
            ActiveSceneModel.New();
            ActiveSceneModel.Scene.ReDraw();;
        }

        public void SetProjectionMode(ProjectionMode projectionMode)
        {
            ActiveSceneModel.Scene.Camera.ProjectionMode = projectionMode;
            ActiveSceneModel.Scene.Fit();
            ActiveSceneModel.Scene.ReDraw();
        }

        public void Fit()
        {
            ActiveSceneModel.Scene.Fit();
            ActiveSceneModel.Scene.ReDraw();
        }

        public void Reset()
        {
            ActiveSceneModel.Scene.Reset();
            ActiveSceneModel.Scene.ReDraw();
        }

        public void SetView(ViewMode viewMode)
        {
            ActiveSceneModel.Scene.SetView(viewMode);
            ActiveSceneModel.Scene.ReDraw();
        }

        public void ScreenShot()
        {
            using (var bitmap = ActiveSceneModel.Scene.CaptureScene())
            {
                var vm = new ScreenShotResultViewModel(bitmap);
                _dialogService.ShowDialogAsync<ScreenShotResultPopup>(vm);
            }
        }

        public void CopyClipBoard()
        {
            using (var bitmap = ActiveSceneModel.Scene.CaptureScene())
            {
                Clipboard.SetImage(bitmap);
            }
        }

        public bool HasActiveScene()
        {
            return ActiveSceneModel != null;
        }

        private Doc ReadFile(string filePath)
        {
            string format = Path.GetExtension(filePath);

            if (format is ".stl")
            {
                var stream = new DocStlStream();
                return stream.Read(filePath);
            }
            else if (format is ".obj")
            {
                var stream = new DocObjStream();
                return stream.Read(filePath);
            }

            throw new ArgumentException($"{format} 확장자는 지원하지 않음.");
        }

        private string OpenDialog()
        {
            string filePath = string.Empty;

            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = dialog.FileName;
                }
            }

            return filePath;
        }

        private void SetUpTabLayout()
        {
            ActiveTabLayoutViewModel.Cleanup();
            ActiveTabLayoutViewModel.Initialize(_activeSceneModel);
        }
    }
}