using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniEyes;
using MiniEyes.Documents;
using MiniEyes.Geometry;
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
            string filePath = OpenFileDialog();

            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            var dialogData = new BusyIndicatorModel("Load File", "Loading...");
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
                    if (!string.IsNullOrEmpty(ActiveSceneModel.FilePath))
                    {
                        New();
                    }

                    ActiveSceneModel.AddDocumentToScene(document);

                    SetUpTabLayout();
                    //NodeTree.ChangeTreeRoot(_activeSceneModel);
                });
            });

            dialogData.IsShown = false;
        }

        public async void ExportFile()
        {
            string filePath = OpenSaveDialog();

            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            var dialogData = new BusyIndicatorModel("Export File", "Writing...");
            _dialogService.ShowDialogAsync<MiniBusyIndicatorPopup>(dialogData);

            await Task.Run(() =>
            {
                WriteFile(filePath);
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

        public bool IsActiveSceneLoaded()
        {
            return ActiveSceneModel != null
                && !string.IsNullOrEmpty(ActiveSceneModel.FilePath);
        }

        public bool CanCloseActiveScene()
        {
            return SceneModels.Count != 1;
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

        private void WriteFile(string filePath)
        {
            string format = Path.GetExtension(filePath);

            if (format is ".stl")
            {
                var stream = new DocStlStream();
                stream.Write(new DocStl(filePath, ActiveSceneModel.Scene.Spatials.ToList()));
            }
            else if (format is ".obj")
            {
                var stream = new DocObjStream();

                var materials = new List<Material>(ActiveSceneModel.Scene.Materials);
                if(materials.Count == 0)
                {
                    materials.Add(ActiveSceneModel.Scene.Materials.Default);
                }

                stream.Write(new DocObj(filePath, ActiveSceneModel.Scene.Spatials.ToList(), materials));
            }
            else
            {
                throw new ArgumentException($"{format} 확장자는 지원하지 않음.");
            }
        }

        private string OpenFileDialog()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "3d files(*.stl; *obj)|*.stl; *obj";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.FileName;
                }
            }

            return string.Empty;
        }

        private string OpenSaveDialog()
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "stl files (*.stl)|*.stl|obj files (*.obj)|*.obj";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.FileName;
                }
            }

            return string.Empty;
        }

        private void SetUpTabLayout()
        {
            ActiveTabLayoutViewModel.Cleanup();
            ActiveTabLayoutViewModel.Initialize(_activeSceneModel);
        }
    }
}