using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using MiniEyes.Geometry;
using MiniEyes.WpfHelperTools;
using MiniMvvm;
using MiniViewer3D.Models;

namespace MiniViewer3D.ViewModels
{
    /// <summary>
    /// ColorPickerPopup에 바인딩 뷰모델
    /// </summary>
    public class ColorAttributeViewModel : PropertyChangedBase, IDialogModel, ITransactionUnit
    {
        private Color _source;  // 원본
        private Color _color;
        private byte _componentR;
        private byte _componentG;
        private byte _componentB;
        private bool _canChangeColor;   // 색상 업데이트 시 이벤트 발생 여부

        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsShown { get; set; }
        public bool IsModal { get; set; }

        /// <summary>
        /// 현재 선택된 색상
        /// </summary>
        public SolidColorBrush Brush { get; set; }

        /// <summary>
        /// 현재 선택된 색상
        /// </summary>
        public Color Color
        {
            get => _color;
            set
            {
                SetProperty(ref _color, value);
                Brush = new SolidColorBrush(_color);
                NotifyPropertyChanged(nameof(Brush));

                _canChangeColor = false;
                ComponentR = _color.R;
                ComponentG = _color.G;
                ComponentB = _color.B;
                _canChangeColor = true;
            }
        }

        /// <summary>
        /// RGB 성분 R
        /// </summary>
        public byte ComponentR
        {
            get => _componentR;
            set
            {
                SetProperty(ref _componentR, value);
                if (_canChangeColor)
                {
                    Color = Color.FromRgb(_componentR, _componentG, _componentB);
                }
            }
        }

        /// <summary>
        /// RGB 성분 G
        /// </summary>
        public byte ComponentG
        {
            get => _componentG;
            set
            {
                SetProperty(ref _componentG, value);
                if (_canChangeColor)
                {
                    Color = Color.FromRgb(_componentR, _componentG, _componentB);
                }
            }
        }

        /// <summary>
        /// RGB 성분 B
        /// </summary>
        public byte ComponentB
        {
            get => _componentB;
            set
            {
                SetProperty(ref _componentB, value);
                if (_canChangeColor)
                {
                    Color = Color.FromRgb(_componentR, _componentG, _componentB);
                }
            }
        }

        /// <summary>
        /// 색상값이 저장되면 호출됨.
        /// </summary>
        public event EventHandler AttributeSaved;

        /// <summary>
        /// Popup을 닫도록 요청함.
        /// </summary>
        public event EventHandler RequestClose;

        public ICommand SaveCommand { get; }

        public ICommand ResetCommand { get; }

        public ColorAttributeViewModel(Color color)
        {
            _source = color;
            Color = Color.FromArgb(color.A, color.R, color.G, color.B);
            IsModal = false;

            SaveCommand = new DelegateCommand(SaveChanges);
        }

        public void Rollback()
        {
            //Brush = MiniEyesHelper.ToBrush(_source);
        }

        /// <summary>
        /// 현재 상태를 저장함.
        /// </summary>
        public void SaveChanges()
        {
            //var newColor = MiniEyesHelper.ToColorF(Brush);
            _source.A = Color.A;
            _source.R = Color.R;
            _source.G = Color.G;
            _source.B = Color.B;

            AttributeSaved?.Invoke(this, EventArgs.Empty);
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}