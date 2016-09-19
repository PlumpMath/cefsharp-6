using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace Caesar.Handlers
{
    public class LifeSpanHandler : ILifeSpanHandler
    {
        bool ILifeSpanHandler.OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;

            ChromiumWebBrowser chromiumBrowser = null;

            var windowX = windowInfo.X;
            var windowY = windowInfo.Y;
            var windowWidth = (windowInfo.Width == int.MinValue) ? 600 : windowInfo.Width;
            var windowHeight = (windowInfo.Height == int.MinValue) ? 800 : windowInfo.Height;



            chromiumWebBrowser.Invoke(new Action(() =>
            {
                var owner = chromiumWebBrowser.FindForm();

                chromiumBrowser = new ChromiumWebBrowser(targetUrl)
                {
                    LifeSpanHandler = this
                };
                chromiumBrowser.SetAsPopup();


                ContextMenuStrip ctxMnu = new ContextMenuStrip();
                ctxMnu.Items.Add("Show DevTool", null, (object sender, EventArgs e) => {
                    chromiumBrowser.ShowDevTools();
                });

                var popup = new BrowserPopupForm(targetFrameName, chromiumBrowser);
                owner.AddOwnedForm(popup);
                popup.Show();
            }));

            newBrowser = chromiumBrowser;

            return false;
        }



        void ILifeSpanHandler.OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {


        }

        bool ILifeSpanHandler.DoClose(IWebBrowser browserControl, IBrowser browser)
        {
            return false;
        }

        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
        {

        }
    }
}

