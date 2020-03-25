using MiniEyes.Geometry;

namespace MiniViewer3D.Models
{
    /// <summary>
    /// 읽고 쓰기 가능한 벡터(포인트 역활도 함.)
    /// </summary>
    /// <remarks>기존의 Point3D는 Mutable이므로 UI에서 읽고 쓰기 힘듬.</remarks>
    public class MutableVector
    {
        /// <summary>
        /// 성분 X
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// 성분 Y
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// 성분 Z
        /// </summary>
        public double Z { get; set; }

        public MutableVector(Point3D point)
            : this(point.X, point.Y, point.Z)
        {
        }

        public MutableVector(Vector3D vector)
            : this(vector.X, vector.Y, vector.Z)
        {
        }

        public MutableVector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}