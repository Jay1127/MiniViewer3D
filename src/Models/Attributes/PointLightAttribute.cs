using MiniEyes.Geometry;

namespace MiniViewer3D.Models
{
    /// <summary>
    /// PointLight 맵핑 클래스
    /// </summary>
    public class PointLightAttribute : LightAttribute
    {
        /// <summary>
        /// 조명 위치
        /// </summary>
        public MutableVector Position { get; set; }

        /// <summary>
        /// 상수 감쇄
        /// </summary>
        public double ConstantAttenuation { get; set; }

        /// <summary>
        /// 선형 감쇄
        /// </summary>
        public double LinearAttenuation { get; set; }

        /// <summary>
        /// 비선형 감쇄
        /// </summary>
        public double QuadraticAttenuation { get; set; }

        /// <summary>
        /// PointLight를 생성함.
        /// </summary>
        /// <param name="pointLight"></param>
        public PointLightAttribute(PointLight pointLight)
            : base(pointLight)
        {
            Name = "Point Light";
            Position = new MutableVector(pointLight.Position);
            ConstantAttenuation = pointLight.ConstantAttenuation;
            LinearAttenuation = pointLight.LinearAttenuation;
            QuadraticAttenuation = pointLight.QuadraticAttenuation;
        }
    }
}