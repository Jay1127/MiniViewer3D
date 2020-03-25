using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using MiniEyes.Geometry;
using MiniEyes.WpfHelperTools;
using MiniMvvm;
using MiniViewer3D.Models;

namespace MiniViewer3D.ViewModels
{
    public class NodeTreeTabViewModel : TabViewModel
    {
        private IDialogService _dialogService;
        private MeshAttribute _activeNode;
        private string _lastEditComponent;
        private Dictionary<string, MaterialAttribute> _materials;

        public override event RequestSceneUpdateHandler RequestSceneUpdate;

        public override string Name => "Nodes";

        public ObservableCollection<MeshAttribute> MeshNodes { get; set; }

        public MeshAttribute ActiveNode
        {
            get => _activeNode;
            set
            {
                SetProperty(ref _activeNode, value);
                NotifyPropertyChanged(nameof(ActiveMaterial));
            }
        }

        public MaterialAttribute ActiveMaterial
        {
            get
            {
                if(_activeNode == null)
                {
                    return null;
                }

                return _materials[ActiveNode.MaterialName];
            }
        }

        public ICommand ShowColorPickerCommand { get; }

        public NodeTreeTabViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            MeshNodes = new ObservableCollection<MeshAttribute>();
            ShowColorPickerCommand = new DelegateCommand<string>(ShowColorPicker);
        }

        public void ChangeTreeRoot(SceneAttribute node)
        {
            _materials = node.Scene.Materials
                .Select(m => new MaterialAttribute(m))
                .ToDictionary(m => m.Name);

            var meshAttrs = node.Scene.Spatials
                .Select(spatial => new MeshAttribute(spatial as Mesh));

            foreach(var meshAttr in meshAttrs)
            {
                MeshNodes.Add(meshAttr);
            }

            //foreach(var meshNode in node.Nodes)
            //{
            //    MeshNodes.Add(meshNode as MeshNode);
            //}
        }

        public override void Initialize(SceneAttribute sceneNode)
        {
            ChangeTreeRoot(sceneNode);
        }

        public override void Cleanup()
        {
            ActiveNode = null;
            MeshNodes.Clear();
        }

        private void ShowColorPicker(string component)
        {
            SolidColorBrush brush = null;

            if (component == "Ambient")
            {
                brush = ActiveMaterial.Ambient;
            }
            else if (component == "Diffuse")
            {
                brush = ActiveMaterial.Diffuse;
            }
            else if (component == "Specular")
            {
                brush = ActiveMaterial.Specular;
            }            

            var viewModel = new ColorAttributeViewModel(brush.Color);
            viewModel.AttributeSaved += OnAttributeSaved;

            _dialogService.ShowDialogAsync<ColorPickerPopup>(viewModel, () =>
            {
                viewModel.AttributeSaved -= OnAttributeSaved;
            });

            _lastEditComponent = component;
        }

        private void OnAttributeSaved(object sender, EventArgs e)
        {
            var viewModel = sender as ColorAttributeViewModel;

            if (_lastEditComponent == "Ambient")
            {
                ActiveMaterial.Ambient = viewModel.Brush;
            }
            else if(_lastEditComponent == "Diffuse")
            {
                ActiveMaterial.Diffuse = viewModel.Brush;
            }
            else if(_lastEditComponent == "Specular")
            {
                ActiveMaterial.Specular = viewModel.Brush;
            }

            RequestSceneUpdate?.Invoke();
        }
    }
}