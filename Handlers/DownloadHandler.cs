using CefSharp;
namespace Caesar.Handlers
{
    public class DownloadHandler : IDownloadHandler
    {
        public string downloadDirectory;
        public void OnBeforeDownload(IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            if (!callback.IsDisposed)
            {
                using (callback)
                {

                    string filePath = System.IO.Path.Combine(this.downloadDirectory, downloadItem.SuggestedFileName);
                    callback.Continue(filePath, showDialog: true);
                }
            }
        }

        public void OnDownloadUpdated(IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {

        }
    }
}
