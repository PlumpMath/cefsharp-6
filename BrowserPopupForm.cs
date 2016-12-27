using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

using CefSharp;
using CefSharp.WinForms;

namespace Caesar
{
    public partial class BrowserPopupForm : Form
    {
        [DllImport("User32.dll", EntryPoint = "SetWindowPos")]
        static extern bool SetWindowPos(
            int hWnd,
            int hWndInsertAfter,
            int X,
            int Y,
            int cx,
            int cy,
            uint uFlags
        );
        [DllImport("User32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public void Minimize()
        {
            ShowWindow(this.Handle, 7);
        }

        public bool dragging = false;
        public Point dragCursorPoint;
        public Point dragFormPoint;

        public void ShowNoActive() {
            ShowWindow(this.Handle, 4);
        }

        public void SetTopNoActive(bool showWnd = true)
        {
            int SW_SHOWNOACTIVATE = 4; // Displays a window in its most recent size and position. This value is similar to SW_SHOWNORMAL, except that the window is not activated.

            if (showWnd) ShowWindow(this.Handle, SW_SHOWNOACTIVATE);

            int HWND_BOTTOM = 1;        // Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
            int HWND_NOTOPMOST = -2;    // Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.
            int HWND_TOP = 0;           // Places the window at the top of the Z order.
            int HWND_TOPMOST = -1;      // Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.   

            UInt16 SWP_NOACTIVATE = 0x0010; // Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).

            SetWindowPos(this.Handle.ToInt32(), HWND_TOPMOST, this.Left, this.Top, this.Width, this.Height, SWP_NOACTIVATE);
        }



        public IWebBrowser Browser { get; private set; }
        private bool menuBound = false;
        private bool CloseContextMenu = true;
        private string windowId;
        public string targetUrl;
        private string CBW_LOAD_START_SCRIPT = @"
            window.open = function(){
                bound.openCBW(arguments[0]);
            };";
        private string TRB_LOAD_START_SCRIPT = @"
            if (window.location.href.includes('/login.jsp')) bound.onLogout();
            if (window.location.href.endsWith('/web-sso/')) bound.onLogin();";
        private string CBW_LOAD_END_SCRIPT = "window.__TC = true;";
        private string TRB_LOAD_END_SCRIPT = @"
            window.open = function(){
                if(typeof arguments[1] == 'object') {
                    bound.openWindow(arguments[0], Ext.encode(arguments[1]))
                } else {
                    bound.openWindow(arguments[0])
                }
            }
            var els = document.getElementsByClassName('login');
            if (els.length > 0) {
                els[0].style.top = 'auto';
                els[0].style.left = 'auto';
                els[0].style.marginTop = 'auto';
                els[0].style.marginLeft = 'auto';
                bound.setCurrentWindowSize(els[0].getBoundingClientRect().width + 37, els[0].getBoundingClientRect().height + 50);
            }


    
            window.addEventListener('resize', function(){ 
                if (window.location.href.endsWith('#statusBar'))  {
                    if (typeof Ext == 'object') {
                        var el = Ext.ComponentQuery.query('StatusBarCounters container');
                        if (el.length > 0) {
                            var isCounters = el[1].isVisible(),
                                alerts = Ext.ComponentQuery.query('alertDetails')[0],
                                isAlerts = alerts.isVisible();

                            window.bound.setStatusBarSize(isCounters, isAlerts);
                            //if (isExpanded) {
                            //    window.bound.setCurrentWindowSize(720, 117);
                            //} else {
                            //   window.bound.setCurrentWindowSize(720, 94);
                            //}
                        }
                    }
                }

            });
            if (window.location.href.endsWith('#statusBar')) {
                window.bound.setStatusBarSize(false, false);   
            }
                    
            if (typeof Ext == 'object') {
                Ext.onReady(function(){
                    $('body').css('padding', 0);
                    if (window.location.href.endsWith('web-sso/')) {
                        var items = [];
                        Ext.getStore('SettingMenuStore').each(function(item){
                            items.push([item.get('text'), item.get('path'), item.get('disable'), item.get('appName'), item.get('appUrl')]);
                        });
                        bound.fillContextMenu(items);
                    }
                });
            }";

        public static int GetTraderBookCount()
        {
            return Program.Windows.Items.Count(item => item.Value.WindowId.StartsWith("traderBook"));
        }

        public static string ParseWindowId(string url)
        {
            int pos = url.IndexOf('#') + 1;
            string windowId = "traderBook";

            if (pos > 0)
            {
                windowId = url.Substring(pos);

                // if there are parameters passed after hash - windowId will be just a hash without paramenters
                int pos2 = windowId.IndexOf('?');
                if (pos2 > 0) windowId = windowId.Substring(0, pos2);

                
            }
            else if (url.Contains("/admin-ui/"))
            {
                windowId = "admin-ui";
            }
            else if (url.Contains("/secmaster-ui/"))
            {
                windowId = "secmaster-ui";
            }
            else if (url.EndsWith("web-sso/"))
            {
                windowId = "landingPage";
            }
            
            if (windowId == "traderBook") windowId += "." + GetTraderBookCount();

            return windowId;
        }

        public String WindowId
        {
            get { return this.windowId; }
            set { this.windowId = ParseWindowId(value); }
        }

        public WindowTypes WindowType;
        public bool IsLandingPage { get { return (this.WindowType == WindowTypes.LandingPage); } }
        public bool IsStatusBar { get { return (this.WindowType == WindowTypes.StatusBar); } }


        public BrowserPopupForm(string targetFrameName, string targetUrl, WindowTypes windowType)
        {

            string ON_LOAD_END = windowType == WindowTypes.CBW ? CBW_LOAD_END_SCRIPT: TRB_LOAD_END_SCRIPT;
            string ON_LOAD_START = windowType == WindowTypes.CBW ? CBW_LOAD_START_SCRIPT: TRB_LOAD_START_SCRIPT;

           

            this.Init(targetFrameName, targetUrl, windowType);
            
            this.Browser.RegisterAsyncJsObject("bound", new BoundObject() { form = this });

            this.Browser.FrameLoadEnd += delegate {
                this.Browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync(ON_LOAD_END);
            };

            this.Browser.FrameLoadStart += delegate {this.Browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync(ON_LOAD_START);};

        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);



        public BrowserPopupForm(string targetFrameName, ChromiumWebBrowser browser, WindowTypes windowType = WindowTypes.TraderBook)
        {
            this.Init(targetFrameName, browser, windowType);
        }

        
        private void Init(string targetFrameName, ChromiumWebBrowser browser, WindowTypes windowType)
        {
            InitializeComponent();

            this.Browser = browser;

            this.Browser.LoadError += ((e, o) => {
                CefSharp.CefErrorCode errCode = o.ErrorCode;
                string errText = o.ErrorText;
                //browser.Load("custom://cefsharp/Error.html#statusBar");
            });

            this.Browser.LifeSpanHandler = new Handlers.LifeSpanHandler();
            this.Browser.DownloadHandler = new Handlers.DownloadHandler { downloadDirectory = Program.DownloadDirectoryPath };

            this.targetUrl = this.Browser.Address;
            this.WindowType = windowType;
            this.WindowId = this.targetUrl;
            this.Text = targetFrameName;


            this.notifyIcon1.Visible = this.IsLandingPage;
            this.contextMenuStrip1.Items.Find("devToolStripMenuItem", true)[0].Visible = (Program.AppMode == AppModes.Dev);

            FormLayout layout = Program.Layouts.GetLayout(this.WindowId);

            if (!this.IsLandingPage)  {
                browser.FrameLoadEnd += (obj, e) => {
                    IWebBrowser IW = (IWebBrowser)obj;
                    IW.SetZoomLevel(Program.Layouts.ZoomLevel);
                };
                
            }

            
            this.StartPosition = FormStartPosition.Manual;
            this.WindowState = (FormWindowState)layout.WindowState;
            this.SetBounds(layout.X, layout.Y, layout.Width, layout.Height);
            
            

            browser.IsBrowserInitializedChanged += (obj, e) => {

                if (e.IsBrowserInitialized) {
                   
                } 
            };

            if (Program.Windows.Items.Count == 0) Program.Windows.Changed += (o, e) => this.updateDevtoolItems();
            Program.Windows.RegisterWindow(this);
           

            this.Controls.Add(browser);
        }

        private void Init(string targetFrameName, string targetUrl, WindowTypes windowType)
        {
            this.Init(targetFrameName, new ChromiumWebBrowser(targetUrl), windowType);
        }

        private void updateDevtoolItems()
        {
            ToolStripMenuItem root = (ToolStripMenuItem)this.contextMenuStrip1.Items.Find("devtoolsToolStripMenuItem", true)[0];
            root.DropDownItems.Clear();

            foreach (BrowserPopupForm f in Program.Windows.Items.Values)
            {
                string title = f.IsLandingPage ? "Landing Page" : f.IsStatusBar ? "Status Bar" : f.Text;
                root.DropDownItems.Add(title, null, (sender, e) => {
                    f.Browser.GetBrowser().GetHost().ShowDevTools();
                });
               
            }
        }

        public void hideInTaskbar()
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Minimized;
                //this.ShowInTaskbar = false;
                this.Hide();
            }
        }

        public void showInTaskbar()
        {
            this.WindowState = FormWindowState.Normal;
            //this.ShowInTaskbar = true;
            this.Show();
        }

        private void ShowDevToolsMenuItemClick(object sender, EventArgs e)
        {
            this.Browser.GetBrowser().GetHost().ShowDevTools();
        }

        private void CloseDevToolsMenuItemClick(object sender, EventArgs e)
        {
            this.Browser.GetBrowser().GetHost().CloseDevTools();

        }

        

        public void RunNotificationEmu()
        {

        }

        private void OnCloseAll(object sender, EventArgs e)
        {
            Program.CloseAllPopups();
        }

        private void OnAppExit(object sender, EventArgs e)
        {

            CefSharp.Cef.GetGlobalCookieManager().DeleteCookiesAsync("", "").Wait();
            Program.CloseAllOpened();
            CefSharp.Cef.Shutdown();
            Application.Exit();
        }

        private void OnSaveLayout(object sender, EventArgs e)
        {
            Program.UpdateLayouts();
            Program.Layouts.Save();
        }

        private void OnResetLayouts(object sender, EventArgs e)
        {
            Program.Layouts.ResetWindowLayouts();
        }

        private void PopupForm_Activated(object sender, EventArgs e)
        {

        }

        private void launchDesktopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (FormLayout layout in Program.Layouts.Items.Values)
            {
                if (layout.inDesktop)
                {
                    JObject args = JObject.FromObject(layout);
                    BoundObject.openWindow(layout.targetUrl, args.ToString());
                }
            }
        }

        private void onOpenWindowClick(string path)
        {
            string js = string.Format(@"
                var btn = Ext.ComponentQuery.query('dynamictoolbar button[path=\'{0}\']')[0];
                btn.fireEvent('click', btn);
            ", path);
            this.Browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync(js);
        }

        public void fillMeritItems(ToolStripMenuItem root)
        {
            root.DropDownItems.Add("Bid Monitor (All)", null, (sender, e) => BoundObject.openCBW("bidMonitorAll", "Bid Monitor (All)"));
            root.DropDownItems.Add("Retail Bids", null, (sender, e) => BoundObject.openCBW("bidMonitorRet", "Retail Bids"));
            root.DropDownItems.Add("Institutional Bids", null, (sender, e) => BoundObject.openCBW("bidMonitorInst", "Institutional Bids"));
            root.DropDownItems.Add("Street Bids", null, (sender, e) => BoundObject.openCBW("bidMonitorStreet", "Street Bids"));
            root.DropDownItems.Add("History", null, (sender, e) => BoundObject.openCBW("history", "History"));
            root.DropDownItems.Add("Audit", null, (sender, e) => BoundObject.openCBW("audit", "Audit"));
            root.DropDownItems.Add("Re-Offerings", null, (sender, e) => BoundObject.openCBW("roBlotter", "Re-Offerings"));
            root.DropDownItems.Add("Aggregate View", null, (sender, e) => BoundObject.openCBW("aggregateView", "Aggregate View"));
            root.DropDownItems.Add("Bid List", null, (sender, e) => BoundObject.openCBW("bidList", "Bid List"));
            root.DropDownItems.Add("Itemized View", null, (sender, e) => BoundObject.openCBW("itemizedView", "Itemized View"));

            root.DropDownItems.Add(new ToolStripSeparator());

            root.DropDownItems.Add("Close All", null, (sender, e) => Program.CloseAllCBW());

        }

        public void fillContextMenu(List<object> records)
        {
            if (this.menuBound) return;
            int pos = 0;
            foreach (var record in records)
            {
                var props = (record as IEnumerable<object>).ToArray();

                ToolStripItem item = new ToolStripMenuItem(props[0].ToString());
                
                item.Tag = props[1].ToString();
                if (props[3].ToString() == "cbw")
                {
                    item.Click += (sender, e) => BoundObject.openCBW(props[4].ToString() + props[1].ToString(), props[0].ToString());
                } else
                {
                    item.Click += (sender, e) => onOpenWindowClick(props[1].ToString());
                }
                
                item.Enabled = !Convert.ToBoolean(props[2]);
                item.Name = "navMenu_" + props[1].ToString().Replace("#", "");

                this.contextMenuStrip1.Items.Insert(pos++, item);
            }

            //ToolStripMenuItem sbItemCBW = new ToolStripMenuItem("Merit CBW");
            //sbItemCBW.Name = "navMenu_CBW";
            //fillMeritItems(sbItemCBW);
            //this.contextMenuStrip1.Items.Insert(0, sbItemCBW);


            // adding a Status Bar option
            ToolStripItem sbItemSB = new ToolStripMenuItem("Status Bar");
            sbItemSB.Name = "navMenu_StatusBar";
            sbItemSB.Click += (sender, e) => Program.resetStatusBar();
            this.contextMenuStrip1.Items.Insert(0, sbItemSB);

            this.contextMenuStrip1.Items["wndMenu_Root"].Visible = true;
            this.contextMenuStrip1.Items["contextMenuSeparator"].Visible = true;
            this.menuBound = true;

        }


        public void clearContextMenu()
        {
            List<string> navItems = new List<string>();
            foreach (ToolStripItem item in this.contextMenuStrip1.Items)
            {
                if (item.Name.StartsWith("navMenu_")) navItems.Add(item.Name);

            }
            foreach (string navItem in navItems)
            {
                this.contextMenuStrip1.Items.RemoveByKey(navItem);
            }
            this.contextMenuStrip1.Items["wndMenu_Root"].Visible = false;
            this.contextMenuStrip1.Items["contextMenuSeparator"].Visible = false;
            this.menuBound = false;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm appSettingsForm = new SettingsForm();
            appSettingsForm.Show();
        }

        public string resolveWindowTitle(string path)
        {
            if (path.EndsWith("/tradersbook-ui/")) {
                int c = GetTraderBookCount();
                return "Trader's Book" + ((c > 0) ? " - " + c.ToString() : "");
            } else if (path.Contains("#")) {
                string itemName = path.Substring(path.IndexOf("#") + 1);
                ToolStripItem[] items = this.contextMenuStrip1.Items.Find("navMenu_" + itemName, true);
                if (items.Length > 0) {
                    return ((ToolStripMenuItem)items[0]).Text;
                } else {
                    return "Trader's Book";
                }
            } else if (path.EndsWith("/secmaster-ui/")) {
                return "Security Information";
            } else if (path.EndsWith("/admin-ui/")) {
                return "Administration";
            } else {
                return "Trader's Book";
            }
        }

        private void openNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string targetUrl = Microsoft.VisualBasic.Interaction.InputBox("Title", "Prompt", "Default", 0, 0);
            string targetUrl = "http://localhost:8080";
            var popup = new BrowserPopupForm("dev window", targetUrl, WindowTypes.TraderBook);
            popup.Show();
        }

        private void openStatusBarDevTool_Click(object sender, EventArgs e)
        {

        }

        private void openTraderBookDevTool_Click(object sender, EventArgs e)
        {

        }



        private void BrowserPopupForm_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void traderbook(object sender, EventArgs e)
        {

        }

        private void downloadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Program.DownloadDirectoryPath);
        }


        private List<int> getElementDimension(IFrame frame, string className)
        {
            var dims = new List<int>();
            //var frame = Program.Windows.Items["landingPage"].Browser.GetMainFrame();
            var task = frame.EvaluateScriptAsync(
                "(function(){var el = document.getElementsByClassName('" + className + "')[0];return [el.offsetWidth, el.offsetHeight];})();"
            );
            task.Wait();
            if (task.Result.Success)
            {
                var list = task.Result.Result as System.Collections.IEnumerable;
                foreach (object item in list) dims.Add(Convert.ToInt16(item));
            }
            return dims;
        }

        private void doZoom(BrowserPopupForm form, List<int> dims, string zoom)
        {
            double dZoom = Convert.ToDouble(zoom);
            double k = dZoom < 2 ? 5.5 : 4.7;

            double zoomX = 1 + dZoom / k;
            double zoomY = 1 + dZoom / k;


            form.Width = Convert.ToInt16(dims[0] * zoomX);
            form.Height = Convert.ToInt16((dims[1] + 24) * zoomY);
        }

        private void adjustWindowSizes(string zoom)
        {
            var forms = new List<BrowserPopupForm>();
            if (Program.Windows.Items.ContainsKey("landingPage")) {
                var form = Program.Windows.Items["landingPage"];
                var dims = getElementDimension(form.Browser.GetMainFrame(), "login");
                if (dims.Count > 0) doZoom(form, dims, zoom);
            };
            if (Program.Windows.Items.ContainsKey("statusBar")) {
                var form = Program.Windows.Items["statusBar"];
                var dims = getElementDimension(form.Browser.GetMainFrame(), "statusbar-counter-container");
                dims[0] = 720;
                if (dims.Count > 0) doZoom(form, dims, zoom);
            };
            
        }



        private void regularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var browser = Program.Windows.Items["statusBar"].Browser;
            //var task = browser.GetZoomLevelAsync();
            //task.Wait();
            //var zoomLevel = Math.Round(task.Result, 2) + 0.1;

            Program.Layouts.ZoomLevel = Convert.ToDouble(((ToolStripMenuItem)sender).Tag);

            foreach(KeyValuePair<string, BrowserPopupForm> form in Program.Windows.Items)
            {
                form.Value.Browser.SetZoomLevel(Program.Layouts.ZoomLevel);
            }
            //this.adjustWindowSizes(zoom);
        }

        private void landingPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Windows.Items["landingPage"].Browser.ShowDevTools();
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Windows.Items["statusBar"].Browser.ShowDevTools();
        }

        private void BrowserPopupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Browser.Dispose();
            Program.Windows.Remove(this.windowId);
        }
    }
}
