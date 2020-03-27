using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using MiniEyes.Geometry;
using MiniEyes.WpfHelperTools;
using MiniMvvm;
using MiniViewer3D.Models;

namespace MiniViewer3D.ViewModels
{
    /// <summary>
    /// Light 탭 뷰모델
    /// </summary>
    public class LightsTabViewModel : TabViewModel
    {
        private IDialogService _dialogService;
        private string _lastEditComponent;

        private LightAttribute _lightUnit1;
        private LightAttribute _lightUnit2;
        private LightAttribute _lightUnit3;
        private LightAttribute _lightUnit4;
        private LightAttribute _lightUnit5;
        private LightAttribute _lightUnit6;

        private LightAttribute _lastEditAttribute;

        public override string Name { get; }

        public override event RequestSceneUpdateHandler RequestSceneUpdate;

        public LightAttribute LightUnit1
        {
            get => _lightUnit1;
            set => SetProperty(ref _lightUnit1, value);
        }

        public LightAttribute LightUnit2
        {
            get => _lightUnit2;
            set => SetProperty(ref _lightUnit2, value);
        }

        public LightAttribute LightUnit3
        {
            get => _lightUnit3;
            set => SetProperty(ref _lightUnit3, value);
        }

        public LightAttribute LightUnit4
        {
            get => _lightUnit4;
            set => SetProperty(ref _lightUnit4, value);
        }

        public LightAttribute LightUnit5
        {
            get => _lightUnit5;
            set => SetProperty(ref _lightUnit5, value);
        }

        public LightAttribute LightUnit6
        {
            get => _lightUnit6;
            set => SetProperty(ref _lightUnit6, value);
        }

        public ICommand ShowColorPickerCommand { get; }

        public LightsTabViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            ShowColorPickerCommand = new DelegateCommand<object>(ShowColorPicker);
        }

        public override void Initialize(SceneAttribute node)
        {
            var lights = node.Scene.Lights.GetLights();

            LightUnit1 = CreateLightUnit(lights[0]);
            LightUnit2 = CreateLightUnit(lights[1]);
            LightUnit3 = CreateLightUnit(lights[2]);
            LightUnit4 = CreateLightUnit(lights[3]);
            LightUnit5 = CreateLightUnit(lights[4]);
            LightUnit6 = CreateLightUnit(lights[5]);
        }

        public override void Cleanup()
        {
            foreach(var light in GetLights())
            {
                if (light != null)
                {
                    light.PropertyChanged -= OnLightAttributePropertyChanged;
                }
            }

            LightUnit1 = null;
            LightUnit2 = null;
            LightUnit3 = null;
            LightUnit4 = null;
            LightUnit5 = null;
            LightUnit6 = null;

            _lastEditComponent = string.Empty;
            _lastEditAttribute = null;
        }

        private void ShowColorPicker(object arguments)
        {
            var items = arguments as object[];
            var component = items[0] as string;
            var activeLight = items[1] as LightAttribute;

            SolidColorBrush brush = null;

            if (component == "Ambient")
            {
                brush = activeLight.Ambient;
            }
            else if (component == "Diffuse")
            {
                brush = activeLight.Diffuse;
            }
            else if (component == "Specular")
            {
                brush = activeLight.Specular;
            }

            var viewModel = new ColorAttributeViewModel(brush.Color);
            viewModel.AttributeSaved += OnAttributeSaved;

            _dialogService.ShowDialogAsync<ColorPickerPopup>(viewModel, () =>
            {
                viewModel.AttributeSaved -= OnAttributeSaved;
            });

            _lastEditAttribute = activeLight;
            _lastEditComponent = component;
        }

        private void OnAttributeSaved(object sender, EventArgs e)
        {
            var viewModel = sender as ColorAttributeViewModel;

            if (_lastEditComponent == "Ambient")
            {
                _lastEditAttribute.Ambient = viewModel.Brush;
            }
            else if (_lastEditComponent == "Diffuse")
            {
                _lastEditAttribute.Diffuse = viewModel.Brush;
            }
            else if (_lastEditComponent == "Specular")
            {
                _lastEditAttribute.Specular = viewModel.Brush;
            }
        }

        private LightAttribute CreateLightUnit(Light light)
        {
            LightAttribute lightAttribute = null;

            if (light is PointLight pointLight)
            {
                lightAttribute = new PointLightAttribute(pointLight);
            }
            else if(light is DirectionalLight directionalLight)
            {
                lightAttribute = new DirLightAttribute(directionalLight);
            }

            lightAttribute.PropertyChanged += OnLightAttributePropertyChanged;

            return lightAttribute;
        }

        private void OnLightAttributePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RequestSceneUpdate?.Invoke();
        }

        private IEnumerable<LightAttribute> GetLights()
        {
            return new LightAttribute[]
            {
                LightUnit1,
                LightUnit2,
                LightUnit3,
                LightUnit4,
                LightUnit5,
                LightUnit6
            };
        }
    }
}