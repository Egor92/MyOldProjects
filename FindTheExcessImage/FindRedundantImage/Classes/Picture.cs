using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace FindTheExcessImage
{
    internal class Picture : INotifyPropertyChanged
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        private Brush _brush = new SolidColorBrush(Colors.Transparent);
        private Visibility _loadingRingVisibility = Visibility.Visible;
        private readonly BitmapImage _originalImage = new BitmapImage();
        private readonly BitmapImage _previewImage = new BitmapImage();
        private bool _isOriginalImageLoaded;
        private bool _isPreloaded;

        public BitmapImage Image
        {
            get { return _isOriginalImageLoaded ? _originalImage : _previewImage; }
        }

        private readonly string _originalPictureUri;
        private readonly string _previewPictureUri;
        public Word Word { get; set; }
        public Brush Brush
        {
            get { return _brush; }
            set { _brush = value; NotifyPropertyChanged(); }
        }

        public Visibility LoadingRingVisibility
        {
            get { return _loadingRingVisibility; }
            set { _loadingRingVisibility = value; NotifyPropertyChanged(); }
        }

        public Picture(Word word, string originalPictureUri, string previewPictureUri)
        {
            Word = word;
            _originalPictureUri = originalPictureUri;
            _previewPictureUri = previewPictureUri;
        }

        #region Реализация INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        public Picture Clone()
        {
            return new Picture(Word, _originalPictureUri, _previewPictureUri);
        }

        public async Task Preload()
        {
            if (_isPreloaded)
                return;

            _isPreloaded = true;

            byte[] previewPictureBytes = await HttpClient.GetByteArrayAsync(_previewPictureUri);
            await LoadImageFromBytes(_previewImage, previewPictureBytes);

            DownloadOriginalPicture();

            LoadingRingVisibility = Visibility.Collapsed;
        }

        private async void DownloadOriginalPicture()
        {
            try
            {
                var bytes = await HttpClient.GetByteArrayAsync(_originalPictureUri);
                await LoadImageFromBytes(_originalImage, bytes);
                _isOriginalImageLoaded = true;
                NotifyPropertyChanged("Image");
                Debug.WriteLine("Word: " + Word + ". Image " + _originalPictureUri + " preloaded. ");
            }
            catch (Exception)
            {
            }
            
        }

        private async Task LoadImageFromBytes(BitmapImage image, byte[] bytes)
        {
            using (var ms = new InMemoryRandomAccessStream())
            {
                using (var dw = new DataWriter(ms))
                {
                    dw.WriteBytes(bytes);
                    await dw.StoreAsync();
                    ms.Seek(0);
                    await image.SetSourceAsync(ms);

                    if (image.PixelWidth == 0)
                        throw new ArgumentException("Массив bytes не является допустимым изображением");
                }
            }
        }
    }
}
