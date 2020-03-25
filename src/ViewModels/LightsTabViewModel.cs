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

        public override string Name { get; }

        public override event RequestSceneUpdateHandler RequestSceneUpdate;

        public LightAttribute LightUnit1 { get; set; }
        public LightAttribute LightUnit2 { get; set; }
        public LightAttribute LightUnit3 { get; set; }
        public LightAttribute LightUnit4 { get; set; }
        public LightAttribute LightUnit5 { get; set; }
        public LightAttribute LightUnit6 { get; set; }

        public LightsTabViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
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
            LightUnit1 = null;
            LightUnit2 = null;
            LightUnit3 = null;
            LightUnit4 = null;
            LightUnit5 = null;
            LightUnit6 = null;
        }

        private void ShowColorPickerDialog()
        {
            //_dialogService.ShowDialogAsync<ColorPickerPopup>(new ColorAttributeViewModel(null));
        }

        private LightAttribute CreateLightUnit(Light light)
        {
            if(light is PointLight pointLight)
            {
                return new PointLightAttribute(pointLight);
            }
            else if(light is DirectionalLight directionalLight)
            {
                return new DirLightAttribute(directionalLight);
            }

            return null;
        }
    }
}