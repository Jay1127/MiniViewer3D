using System.Windows.Media;
using MiniEyes.Geometry;
using MiniMvvm;

namespace MiniViewer3D.Models
{
    /// <summary>
    /// 화면에 보여질 조명 맵핑 클래스
    /// </summary>
    public class LightAttribute : PropertyChangedBase, IAttribute 
    {
        /// <summary>
        /// 조명
        /// </summary>
        protected Light _light { get; set; }

        /// <summary>
        /// 조명 이름(=조명 타입)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ambient
        /// </summary>
        public SolidColorBrush Ambient
        {
            get => MiniEyesHelper.ToBrush(_light.Ambient);
            set
            {
                _light.Ambient = MiniEyesHelper.ToColorF(value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Diffuse
        /// </summary>
        public SolidColorBrush Diffuse
        {
            get => MiniEyesHelper.ToBrush(_light.Diffuse);
            set
            {
                _light.Diffuse = MiniEyesHelper.ToColorF(value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Specular
        /// </summary>
        public SolidColorBrush Specular
        {
            get => MiniEyesHelper.ToBrush(_light.Specular);
            set
            {
                _light.Specular = MiniEyesHelper.ToColorF(value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 조명 활성 여부
        /// </summary>
        public bool IsCast
        {
            get => _light.IsCast;
            set
            {
                _light.IsCast = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 조명을 생성함.
        /// </summary>
        /// <param name="light"></param>
        public LightAttribute(Light light)
        {
            _light = light;
        }
    }
}