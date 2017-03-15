using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using CefSharp;


namespace Caesar
{
    public enum AppModes : int { Dev, Prod };
    public enum WindowTypes : int { StatusBar, LandingPage, Admin, TraderBook, SecMaster, CBW };


    public class Program
    {
        static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");
        [STAThread]
        public static int Main(string[] args)
        {
            if (!mutex.WaitOne(TimeSpan.Zero, true)) {
                MessageBox.Show("Only one instance of Caesar application is allowed.");
                return 0;
            }

            Program.UserDataDirectory = findUserDataPath();
            Program.Settings = UserSettings.Load();
            Program.Layouts = Layouts.Load();


            // Set the unhandled exception mode to force all Windows Forms errors to go through our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            CefBrowserSettings.Init(Program.UserDataDirectory);

            AppMode = Program.Settings.AppMode.Equals("dev") ? AppModes.Dev : AppModes.Prod;

            Run();
            mutex.ReleaseMutex();

            return 0;
        }

        public static AppModes AppMode;

        //public static CometDProxy cometd = new CometDProxy();

        public static UserSettings Settings;

        public static Layouts Layouts;

        public static double ZoomLevel = 0;

        public static ChildWindows Windows = new ChildWindows() { Items = new Dictionary<String, BrowserPopupForm>() };


        public static string UserDataDirectory;

        public static void Run()
        {
            var popup = new BrowserPopupForm("Caesar", Program.Settings.BaseURL + "web-sso/", WindowTypes.LandingPage);
            popup.ControlBox = false;
            popup.MinimizeBox = false;
            popup.MaximizeBox = false;

            Application.Run(popup);
        }

        public static string DownloadDirectoryPath
        {
            get
            {
                string dDir = Program.UserDataDirectory + "\\downloads";
                if (!Directory.Exists(dDir)) { Directory.CreateDirectory(dDir); }
                return dDir;
            }
        }

        public static void UpdateLayouts()
        {
            
            var task = Program.Windows.Items["landingPage"].Browser.GetZoomLevelAsync();
            task.Wait();
            double zoomLevel = Math.Round(task.Result, 2);
            //task.ContinueWith(previous => {
            //    zoomLevel = Math.Round(previous.Result, 2);
            //});

            Program.Layouts.ZoomLevel = zoomLevel;

            //foreach (FormLayout layout in Program.Layouts.Items.Values) layout.inDesktop = false;
            Program.Layouts.Items.Clear();
            foreach (BrowserPopupForm form in Windows.Items.Values)
            {
                //Program.Layouts.Items[form.WindowId] = new FormLayout
                FormLayout layout = new FormLayout
                {
                    X = form.Bounds.X,
                    Y = form.Bounds.Y,
                    Width = form.Bounds.Width,
                    Height = form.Bounds.Height,
                    targetUrl = form.targetUrl,
                    title = form.Text,
                    inDesktop = (form.IsStatusBar || form.IsLandingPage) ? false : true,
                    WindowState = Convert.ToInt32(form.WindowState),
                    selectedWS = form.getSelectedWorkspaceId(),
                    WindowType = form.WindowType 
                };
                string itemId = form.IsStatusBar ? "statusBar" : Program.Layouts.Items.Count.ToString();
                Program.Layouts.Items[itemId] = form.layout = layout;
            }
        }


        public static void CloseAllCBW()
        {
            List<Form> forms = new List<Form>();
            foreach (KeyValuePair<string, BrowserPopupForm> form in Windows.Items.Where(p => p.Value.WindowType == WindowTypes.CBW).ToArray()) forms.Add(form.Value);
            CloseWindows(forms);

        }

        public static bool CloseAllPopups(bool showConfirmation = false)
        {
            bool result = true;
            List<Form> forms = new List<Form>();
            foreach (KeyValuePair<string, BrowserPopupForm> form in Windows.Items.Where(p => !p.Value.IsLandingPage && !p.Value.IsStatusBar).ToArray()) {
                forms.Add(form.Value);
            }
            if (forms.Count > 0 && showConfirmation) {
                DialogResult response = MessageBox.Show(
                    forms[0], 
                    "Currently opened windows will be closed. Do you want to proceed?", 
                    "You have opened windows", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question
                );
                result = (response == DialogResult.Yes);
                if (response == DialogResult.Yes) CloseWindows(forms);
            } else {
                CloseWindows(forms);
            }
            return result;
        }

        public static void CloseAllButLandingPage()
        {
            List<Form> forms = new List<Form>();
            foreach (KeyValuePair<string, BrowserPopupForm> form in Windows.Items.Where(p => !p.Value.IsLandingPage).ToArray())
            {
                forms.Add(form.Value);
            }
            CloseWindows(forms);
        }

        public static void CloseAllOpened()
        {
            List<Form> forms = new List<Form>();
            foreach (Form form in Application.OpenForms) forms.Add(form);
            CloseWindows(forms);
        }

        public static void CloseWindows(List<Form> forms)
        {
            foreach (Form form in forms)
            {
                if (form.InvokeRequired)
                {
                    form.Invoke(new Action(() => {
                        form.Close();
                    }));
                }
                else
                {
                    form.Close();
                }
            };
        }
        public static void resetStatusBar()
        {
            BrowserPopupForm wnd = Program.Windows.Items["statusBar"];
            wnd.Left = 40;
            wnd.Top = 40;
        }

        public static void openStatusBar()
        {
            if (!Program.Windows.Items.ContainsKey("statusBar"))
            {
                var popup = new BrowserPopupForm("statusbar-frame", Program.Settings.BaseURL + "tradersbook-ui/#statusBar", WindowTypes.StatusBar);
                //var popup = new BrowserPopupForm("statusbar-frame", "http://localhost:8080/dev.app#statusBar", WindowTypes.StatusBar);

                popup.ShowInTaskbar = false;
                popup.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                popup.Text = String.Empty;
                popup.ControlBox = false;
                popup.Width = 720;
                popup.Height = 96;
                popup.SetTopNoActive();
            }
            /*
            else
            {
                BrowserPopupForm sbWnd = Program.Windows.Items["statusBar"];
                sbWnd.Left = 40;
                sbWnd.Top = 40;
                sbWnd.SetTopNoActive(false);
            }
            */
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                log4net.ILog logger = log4net.LogManager.GetLogger("Unhandled Exception");
                logger.Error(ex);
            }
            catch (Exception exc)
            {

            }
        }

        public static string findUserDataPath()
        {
            string path = "";
            try
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Caesar";
                //path = "C:\\Users\\Public\\Caesar";
                Directory.CreateDirectory(path);
            }
            catch (Exception e)
            {
                string msg = "Can not create a directory " + path;
                MessageBox.Show(e.Message, "Fatal error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return path;
        }

        public static string _findUserDataPath()
        {
            string path = "";
            //string location = Properties.Settings.Default.UserDataLocation;

            string dataFolder = "Caesar\\" + Environment.UserName;

            try
            {

                path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\" + dataFolder;
                path = dataFolder;
                //string userName = Environment.UserName;
                Directory.CreateDirectory(path);
            }
            catch
            {
                try
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + dataFolder;
                    Directory.CreateDirectory(path);
                }
                catch
                {
                    string msg = "Can not create a directory " + path;
                    MessageBox.Show(msg, "Fatal error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return path;
        }

    }
}
