using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using MiniMvvm;
using MiniViewer3D.Models;

namespace MiniViewer3D.ViewModels
{
    public class MaterialTabViewModel : TabViewModel
    {
        private MaterialAttribute _activeMaterial;
        private bool _isAttributeHide;

        public override string Name => "Materials";

        public override event RequestSceneUpdateHandler RequestSceneUpdate;

        public ObservableCollection<MaterialAttribute> Materials { get; }

        public MaterialAttribute ActiveMaterial
        {
            get => _activeMaterial;
            set => SetProperty(ref _activeMaterial, value);
        }

        public bool IsAttributeHide
        {
            get => _isAttributeHide;
            set => SetProperty(ref _isAttributeHide, value);
        }

        public ICommand FixTextureCommand { get; }

        public MaterialTabViewModel()
        {
            Materials = new ObservableCollection<MaterialAttribute>();
            IsAttributeHide = true;
            FixTextureCommand = new DelegateCommand(FixTexture);
        }

        private void FixTexture()
        {
            string filePath = string.Empty;

            using (var dialog = new OpenFileDialog())
            {
                if(dialog.ShowDialog()== DialogResult.OK)
                {
                    filePath = dialog.FileName;
                }
            }

            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            ActiveMaterial.FixTexture(filePath);

            NotifyPropertyChanged(nameof(ActiveMaterial.TextureAttribute));
        }

        public override void Cleanup()
        {
            ActiveMaterial = null;
            Materials.Clear();
        }

        public override void Initialize(SceneAttribute scene)
        {   
            foreach(var material in scene.GetMaterialAttributes().Values)
            {
                Materials.Add(material);
            }
        }
    }
}