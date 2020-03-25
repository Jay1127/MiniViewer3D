using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using MiniEyes.Geometry;
using MiniMvvm;

namespace MiniViewer3D.Models
{
    public class MeshAttribute : PropertyChangedBase
    {
        private Mesh _spatial { get; }

        public string Name
        {
            get => _spatial.Name;
        }

        public int VertexCount
        {
            get => _spatial.Vertices.Length;
        }

        public int FaceCount
        {
            get => _spatial.Faces.Length;
        }      
        
        public string MaterialName
        {
            get => _spatial.MaterialName;
        }

        public MeshAttribute(Mesh spatial)
        {
            _spatial = spatial;
        }
    }
}