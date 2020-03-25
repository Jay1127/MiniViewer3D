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
        public SolidColorBrush Ambient { get; set; }
        
        /// <summary>
        /// Diffuse
        /// </summary>
        public SolidColorBrush Diffuse { get; set; }

        /// <summary>
        /// Specular
        /// </summary>
        public SolidColorBrush Specular { get; set; }

        /// <summary>
        /// 조명 활성 여부
        /// </summary>
        public bool IsCast { get; set; }

        /// <summary>
        /// 조명을 생성함.
        /// </summary>
        /// <param name="light"></param>
        public LightAttribute(Light light)
        {
            _light = light;
            Ambient = MiniEyesHelper.ToBrush(_light.Ambient);
            Diffuse = MiniEyesHelper.ToBrush(_light.Diffuse);
            Specular = MiniEyesHelper.ToBrush(_light.Specular);
        }
    }
}