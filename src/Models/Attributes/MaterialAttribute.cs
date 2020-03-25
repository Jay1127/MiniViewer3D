using System.ComponentModel;
using System.Drawing;
using System.Windows.Media;
using MiniEyes.Geometry;
using MiniMvvm;

namespace MiniViewer3D.Models
{
    /// <summary>
    /// 화면에 보여질 재질 맵핑 클래스
    /// </summary>
    public class MaterialAttribute : PropertyChangedBase, IAttribute
    {
        private TextureAttribute _textureAttribute;

        /// <summary>
        /// 재질
        /// </summary>
        private Material _material { get; }

        /// <summary>
        /// 재질명
        /// </summary>
        public string Name
        {
            get => _material.Name;
        }

        /// <summary>
        /// Ambient
        /// </summary>
        public SolidColorBrush Ambient
        {
            get => MiniEyesHelper.ToBrush(_material.Ambient);
            set
            {
                _material.Ambient = MiniEyesHelper.ToColorF(value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Diffuse
        /// </summary>
        public SolidColorBrush Diffuse
        {
            get => MiniEyesHelper.ToBrush(_material.Diffuse);
            set
            {
                _material.Diffuse = MiniEyesHelper.ToColorF(value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Specular
        /// </summary>
        public SolidColorBrush Specular
        {
            get => MiniEyesHelper.ToBrush(_material.Specular);
            set
            {
                _material.Specular = MiniEyesHelper.ToColorF(value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 투명도
        /// </summary>
        public double Opacity
        {
            get => _material.Opacity;
        }

        /// <summary>
        /// 빛남 정도
        /// </summary>
        public double Shininess
        {
            get => _material.Shininess;
        }

        public bool HasTexture
        {
            get => TextureAttribute != null;
        }

        public TextureAttribute TextureAttribute
        {
            get => _textureAttribute;
            private set => SetProperty(ref _textureAttribute, value);
        }

        /// <summary>
        /// 재질 모델을 생성함.
        /// </summary>
        /// <param name="material"></param>
        public MaterialAttribute(Material material)
        {
            _material = material;
            TextureAttribute = material.DiffuseMap != null ? new TextureAttribute(material.DiffuseMap) : null;
        }

        public void FixTexture(string texturePath)
        {
            _material.DiffuseMap = new Texture(texturePath);

            TextureAttribute.Bitmap?.Dispose();

            TextureAttribute = new TextureAttribute(_material.DiffuseMap);
        }
    }

    public class TextureAttribute : PropertyChangedBase, IAttribute
    {
        private Texture _texture;

        public string Name => "Texture";

        public string FilePath { get => _texture.FilePath; }

        public Bitmap Bitmap { get => _texture.Map; }

        public bool IsValidated
        {
            get => _texture.Map != null;
        }

        public TextureAttribute(Texture texture)
        {
            _texture = texture;
        }

        public void Fix()
        {
            
        }
    }
}