using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using MiniEyes.WpfHelperTools;
using MiniMvvm;
using MiniViewer3D.Models;

namespace MiniViewer3D.ViewModels
{
    public class SceneTabViewModel : TabViewModel
    {
        private SolidColorBrush _background;

        private IDialogService _dialogService;

        public override event RequestSceneUpdateHandler RequestSceneUpdate;

        public override string Name => "Scene";

        public SceneAttribute SceneNode { get; private set; }

        public string Count
        {
            get
            {
                return string.IsNullOrEmpty(SceneNode.Name) ? 
                    string.Empty : SceneNode.Scene.Spatials.Count.ToString();
            }
        }

        public SolidColorBrush Background
        {
            get => _background;
            set
            {
                SetProperty(ref _background, value);

                SceneNode.Scene.Configuration.Background = MiniEyesHelper.ToColorF(_background);
            }
        }

        public ICommand ShowColorPickerCommand { get; }

        public SceneTabViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            ShowColorPickerCommand = new DelegateCommand(ShowColorPicker);
        }

        private void ShowColorPicker()
        {
            var viewModel = new ColorAttributeViewModel(Background.Color);
            viewModel.AttributeSaved += OnAttributeSaved;  
            
            _dialogService.ShowDialogAsync<ColorPickerPopup>(viewModel, () =>
            {
                viewModel.AttributeSaved -= OnAttributeSaved;
            });
        }

        private void OnAttributeSaved(object sender, EventArgs e)
        {
            var viewModel = sender as ColorAttributeViewModel;

            Background = viewModel.Brush;
            SceneNode.Scene.ReDraw();
        }

        public override void Cleanup()
        {
        }

        public override void Initialize(SceneAttribute sceneNode)
        {
            SceneNode = sceneNode;

            Background = MiniEyesHelper.ToBrush(sceneNode.Scene.Configuration.Background);

            NotifyPropertyChanged(nameof(Count));
            NotifyPropertyChanged(nameof(SceneNode));
        }
    }
}