using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MiniEyes.WpfHelperTools;
using MiniMvvm;

namespace MiniViewer3D.ViewModels
{
    public class ScreenShotResultViewModel : PropertyChangedBase, IDialogModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public ImageSource ImageSource { get; set; }

        public double Width
        {
            get => ImageSource.Width;
        }

        public double Height
        {
            get => ImageSource.Height + 30; // caption height;
        }

        public ICommand SaveCommand { get; }
        public bool IsShown { get; set; }
        public bool IsModal { get; set; }

        public ScreenShotResultViewModel(Bitmap bitmap)
        {
            Title = "ScreenShot";
            Message = string.Empty;
            
            ImageSource = BitmapToImageSource(bitmap);

            SaveCommand = new DelegateCommand(SaveImage);
            IsModal = false;
        }

        public event EventHandler RequestClose;

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            BitmapImage bitmapimage = new BitmapImage();
            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Bmp);
                ms.Position = 0;

                bitmapimage.BeginInit();
                bitmapimage.StreamSource = ms;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
            }

            bitmapimage.Freeze();

            return bitmapimage;
        }

        private void SaveImage()
        {
            string filePath = string.Empty;

            using (var dialog = new SaveFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = dialog.FileName;
                }
            }

            PngBitmapEncoder encorder = new PngBitmapEncoder();
            encorder.Interlace = PngInterlaceOption.Default;

            encorder.Frames.Add(BitmapFrame.Create(ImageSource as BitmapSource));

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                encorder.Save(fs);
            }
        }
    }
}