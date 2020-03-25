using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using MiniEyes;
using MiniEyes.Documents;
using MiniEyes.Geometry;
using MiniMvvm;
using static MiniEyes.SceneConfiguration;

namespace MiniViewer3D.Models
{
    /// <summary>
    /// Scene 표현 노드
    /// </summary>
    public class SceneAttribute : PropertyChangedBase, IDisposable
    {
        private Scene _scene;
        private string _filePath;

        public Guid Id
        {
            get => Scene.Id;
        }

        public string Name
        {
            get
            {
                return string.IsNullOrEmpty(FilePath) ? null : Path.GetFileNameWithoutExtension(FilePath);
            }
        }

        public string FilePath
        {
            get => _filePath;
            private set
            {
                SetProperty(ref _filePath, value);
                NotifyPropertyChanged(nameof(Name));
            }
        }

        public RenderMode RenderMode
        {
            get => Scene.Configuration.RenderingMode;
            set
            {
                Scene.Configuration.RenderingMode = value;
                NotifyPropertyChanged();
                Scene.ReDraw();
            }
        }

        public bool IsOriginVisible
        {
            get => Scene.Configuration.Origin.IsVisible;
            set
            {
                Scene.Configuration.Origin.IsVisible = value;
                NotifyPropertyChanged();
                Scene.ReDraw();
            }
        }

        public Scene Scene
        {
            get => _scene;
            set => SetProperty(ref _scene, value);
        }

        public SceneAttribute()
        {
            New();
        }

        public void New()
        {
            Dispose();

            FilePath = string.Empty;

            Scene = new Scene();
            Scene.Configuration.Background = ColorF.FromColor(Color.FromArgb(221, 221, 221));
        }

        public void AddDocumentToScene(Doc doc)
        {
            FilePath = doc.FilePath;

            AddDocument(doc);

            Scene.Reset();
            Scene.ReDraw();
        }

        public void Dispose()
        {
            Scene?.ClearResources();
            Scene?.Dispose();

            Scene = null;
        }

        public void AddDocument(Doc doc)
        {
            if (doc is DocObj docObj)
            {
                Scene.Materials.AddRange(docObj.Materials);
            }

            for (int i = 0; i < doc.Spatials.Count; i++)
            {
                doc.Spatials[i].Name = $"Object-{i + 1}";       // temporary naming(to global setting)
            }

            Scene.Spatials.AddRange(doc.Spatials);
        }

        public IEnumerable<MeshAttribute> GetMeshAttributes()
        {
            return Scene.Spatials
                .Select(spatial => new MeshAttribute(spatial as Mesh));
        }

        public Dictionary<string, MaterialAttribute> GetMaterialAttributes()
        {
            return Scene.Materials
                   .Select(m => new MaterialAttribute(m))
                   .ToDictionary(m => m.Name);
        }
    }
}