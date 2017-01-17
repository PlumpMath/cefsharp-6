using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CefSharp.WinForms;
using System.Drawing;
using System.Windows.Forms;
using CefSharp;
using Newtonsoft.Json.Linq;



namespace Caesar
{
    public class BoundObject
    {
        public BrowserPopupForm form;
        
        public static bool openWindow(string url, string args = null)
        {
            string windowId = BrowserPopupForm.ParseWindowId(url);
            int maxTrbNum = Program.Settings.MaximumNumberOfTRBScreens;
            int trbCnt = BrowserPopupForm.GetTraderBookCount();
            string title = null;
            bool single = false;

            if (!String.IsNullOrEmpty(args)) {
                JObject options = JObject.Parse(args);
                title = options["title"].ToString();
                single = bool.Parse((options["single"] ?? "false").ToString());

            }
            

            
            if (windowId.StartsWith("traderBook") && trbCnt >= maxTrbNum) {
                (new Thread(() => MessageBox.Show(
                        "Number of opened Trader Book's screens are limited by " + maxTrbNum.ToString(),
                        "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                ))).Start();
                return false;
            }
            
            ChromiumWebBrowser browser = (ChromiumWebBrowser)Program.Windows.Items["landingPage"].Browser;
            
            string targetUrl = (url.StartsWith("http")) ? url : Program.Settings.BaseURL + url.Replace("../", "");

            browser.Invoke(new Action(() => {
                if (Program.Windows.Items.ContainsKey(windowId)) {
                    if (single) {
                        Program.Windows.Items[windowId].SetTopNoActive();
                    } else {
                        Program.Windows.Items[windowId].ShowNoActive();
                    }
                } else {
                    var windowTitle = String.IsNullOrEmpty(title) ? Program.Windows.Items["landingPage"].resolveWindowTitle(url) : title;
                    var popup = new BrowserPopupForm(windowTitle, targetUrl, WindowTypes.TraderBook);
                    popup.Show();
                    if (single) popup.SetTopNoActive();
                }
            }));
            return true;
        }
            


        public bool OpenWindow(string url, string option = null)
        {
            return openWindow(UrlRewrite(url), option );
        }

        public static void openCBW(string url, string title)
        {
            //string url = Program.Settings.CBW_URL + "#" + hash;
            string windowId = BrowserPopupForm.ParseWindowId(url);
            if (Program.Windows.Items.ContainsKey(windowId)) Program.Windows.Items[windowId].ShowNoActive();
            else
            {
                var popup = new BrowserPopupForm(title, url, WindowTypes.CBW);
                popup.Show();
            }
        }

        public void ShowAndReloadCBW(string url, string title)
        {
            this.form.Invoke(new Action(() => {
                string windowId = BrowserPopupForm.ParseWindowId(url);
                if (Program.Windows.Items.ContainsKey(windowId))
                {
                    var w = Program.Windows.Items[windowId];
                    w.ShowNoActive();
                    w.Browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync("window.location.reload();");
                }
                else
                {
                    var popup = new BrowserPopupForm(title, url, WindowTypes.CBW);
                    popup.Show();
                }  
            }));

        }

        public bool OpenCBW(string url)
        {
            this.form.Invoke(new Action(() => {
                openCBW(url, "Merit CBW");
            }));
            return true;
        }

        public static string UrlRewrite(string url)
        {
            if (false && Program.AppMode == AppModes.Dev && url.EndsWith("/tradersbook-ui/")) return "http://localhost:8080/dev.app#traderBook";
            else return url;

        }

        public int ShowWindow(String windowId)
        {
            return showWindow(windowId);
        }

        public static int showWindow(String windowId)
        {
            if (!Program.Windows.Items.ContainsKey(windowId))
            {
                return 0;
            }
            else
            {
                BrowserPopupForm form = Program.Windows.Items[windowId];
                if (form.InvokeRequired)
                {
                    form.Invoke(new Action(() => {
                        form.SetTopNoActive();
                    }));
                }
                else
                {
                    form.SetTopNoActive();
                }

                return 1;
            }
        }



        public void onLogin()
        {

            form.Invoke(new Action(() => {
                form.hideInTaskbar();
                Program.openStatusBar();
            }));
        }

        public void SetCurrentWindowSize(double width, double height)
        {
            form.Invoke(new Action(() => {

                var task = form.Browser.GetZoomLevelAsync();
                task.Wait();

                
                var zoom = Math.Round(task.Result, 2);
                double k = zoom < 2 ? 5.5 : 4.7;

                //form.Width = Convert.ToInt16(width * (1 + zoom/5.5));
                //form.Height = Convert.ToInt16((height + 2) * (1 + zoom/5.5));

                form.Width = Convert.ToInt16(width * (1 + zoom/k));
                form.Height = Convert.ToInt16((height + 2) * (1 + zoom/k));

                //form.Width = Convert.ToInt16(width + 15);
                //form.Height = Convert.ToInt16((height + 17));


            }));
        }

        public void SetStatusBarSize(bool expanded, bool alerts)
        {
            form.Invoke(new Action(() => {
                var task = form.Browser.GetZoomLevelAsync();
                task.Wait();
                var zoom = Math.Round(task.Result, 2);
                int[] dims;

                if      (zoom == 0) dims = new int[]    { 701, alerts ? 672 : (expanded ? 96 : 72) };
                else if (zoom == 0.25) dims = new int[] { 734, alerts ? 692 : (expanded ? 100 : 75) };
                else if (zoom == 0.50) dims = new int[] { 768, alerts ? 705 : (expanded ? 104 : 78) };
                else if (zoom == 0.80) dims = new int[] { 811, alerts ? 722 : (expanded ? 112 : 84) };
                else if (zoom == 1.8) dims = new int[]  { 973, alerts ? 772 : (expanded ? 132 : 99) };
                else dims = new int[] { 701, 71 };

                form.Width = dims[0];
                form.Height = dims[1];

            }));
            

        }

        public void SetCurrentWindowTitle(string text)
        {
            form.Invoke(new Action(() => {
                form.Text = text;
            }));
        }

        public void ShowNoActive()
        {
            form.Invoke(new Action(() => {
                form.ShowNoActive();
            }));
        }

        /***
         * Moving window without titles proxying mouse events from javascript
         * Chrome is eating a mousemove event making impossible to handle it in parent window 
         */
        public void onBrowserMouseDown()
        {
            form.Invoke(new Action(() => {
                form.dragging = true;
                form.dragCursorPoint = Cursor.Position;
                form.dragFormPoint = form.Location;
            }));
        }

        public void onWindowDrag(int x, int y)
        {
            form.Invoke(new Action(() => {
                Point dif = Point.Subtract(Cursor.Position, new Size(form.dragCursorPoint));
                form.Location = Point.Add(form.dragFormPoint, new Size(dif));
            }));
        }
        /*
         ***/

        public string GetWindowId()
        {
            return this.form?.WindowId;
        }

        public void CloseAllCBW()
        {
            List<Form> forms = new List<Form>();
            foreach (KeyValuePair<string, BrowserPopupForm> form in Program.Windows.Items.Where(p => p.Value.WindowType == WindowTypes.CBW).ToArray()) forms.Add(form.Value);
            
            foreach (BrowserPopupForm f in forms) f.Browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync("window.close()");
        }


        public void fillContextMenu(List<object> items)
        {
            Program.Windows.Items["landingPage"].Invoke(new Action(() => {
                form.fillContextMenu(items);
            }));
        }

        public void onLogout()
        {
            form.Invoke(new Action(() => {
                Program.CloseAllButLandingPage();
                form.clearContextMenu();
                form.SetTopNoActive();
                CefSharp.Cef.GetGlobalCookieManager().DeleteCookiesAsync("", "").Wait();
            }));

        }

        public void startIdleMonitor(double idleTimeoutSeconds, int timerIntervalSeconds)
        {
            form.Invoke(new Action(() => {
                IdleMonitor.Start(idleTimeoutSeconds, timerIntervalSeconds);
            }));
        }

        public string getStoredWS()
        {
            FormLayout layout = Program.Layouts.GetLayout(this.form?.WindowId);
            return layout.selectedWS;
        }


    }
}
