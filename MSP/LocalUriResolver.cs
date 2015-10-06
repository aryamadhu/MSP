using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Foundation;
using Windows.Web;
using Windows.Security.Cryptography;


namespace MSP
{
    class LocalUriResolver : IUriToStreamResolver
    {
        public IAsyncOperation<IInputStream> UriToStreamAsync(Uri uri)
        {
            if (uri == null)
            {
                throw new Exception();
            }
            string path = uri.AbsolutePath;
            return GetContent(path).AsAsyncOperation();
        }

        private async Task<IInputStream> GetContent(string uriPath)
        {
            try
            {
                Uri localUri = new Uri("ms-appx://" + uriPath);
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(localUri);
                IRandomAccessStream stream = await file.OpenReadAsync();
                return stream.GetInputStreamAt(0);
            }
            catch (Exception)
            {
                throw new Exception("Invalid path");
            }
        }
    }
}
