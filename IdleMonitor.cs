using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Caesar
{


    class IdleMonitor
    {

        private double idleTimeoutSeconds;
        
        private Timer _timer;

        public static void Start(double idleTimeoutSeconds = 1200, int timerIntervalSeconds = 5)
        {
            IdleMonitor monitor = new IdleMonitor(idleTimeoutSeconds, timerIntervalSeconds * 1000);
        }

        private IdleMonitor(double idleTimeoutSeconds, int timerInterval)
        {

            this.idleTimeoutSeconds = idleTimeoutSeconds;
            _timer = new Timer();
            _timer.Tick += new EventHandler(TimerTick);
            _timer.Interval = timerInterval;
            _timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (this.IsUserIdle())
            {
                doLogout();
            }
        }

        public bool IsUserIdle()
        {
            uint idleTime = (uint)Environment.TickCount - GetLastInputEventTickCount();
            idleTime = ((idleTime > 0) ? (idleTime / 1000) : 0);
            //user is idle for 5 min
            return (idleTime >= this.idleTimeoutSeconds);
        }

        private void doLogout()
        {
            string js = @"
                var btns = Ext.ComponentQuery.query('dynamictoolbar button[path=\'#logout\']');
                if (btns.length > 0) btns[0].fireEvent('click', btns[0]);
            ";
            if (Program.Windows.Items.ContainsKey("landingPage")) Program.Windows.Items["landingPage"].Browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync(js);
        }

        private uint GetLastInputEventTickCount()
        {
            LASTINPUTINFO lii = new LASTINPUTINFO();
            lii.cbSize = (uint)Marshal.SizeOf(lii);
            lii.dwTime = 0;

            return GetLastInputInfo(out lii) ? lii.dwTime : 0;
        }

        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(out LASTINPUTINFO plii);

        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            public static readonly int SizeOf =
                   Marshal.SizeOf(typeof(LASTINPUTINFO));

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dwTime;
        }

    }
}
