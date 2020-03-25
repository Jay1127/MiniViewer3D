using MiniEyes.Geometry;

namespace MiniViewer3D.Models
{
    /// <summary>
    /// Directional Light 맵핑 클래스
    /// </summary>
    public class DirLightAttribute : LightAttribute
    {
        /// <summary>
        /// 빛 방향
        /// </summary>
        public MutableVector Direction { get; set; }

        /// <summary>
        /// Directional Light를 생성함.
        /// </summary>
        /// <param name="light"></param>
        public DirLightAttribute(DirectionalLight light) : base(light)
        {
            Name = "Directional Light";
            Direction = new MutableVector(light.Direction);
        }
    }
}