using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.Proxy;
using CefSharp.Internals;
using CefSharp.SchemeHandler;

namespace Caesar
{
    public static class CefBrowserSettings
    {
        //public const string DefaultUrl = "custom://cefsharp/home.html";
        //public const string DefaultUrl = "http://goto/new-merit-qa";
        //public const string DefaultUrl = "https://localhost:86/";
        public const string DefaultUrl = "http://xashmun17qap.ash.pwj.com:8080/web-sso/";
    //public const string DefaultUrl = "http://localhost:9091/dev.app#traderBook";
    //public const string DefaultUrl = "http://xashmun18qap.ash.pwj.com:8080/web-sso/";
 


    public static void Init(string userDataDir)
    {
        // Set Google API keys, used for Geolocation requests sans GPS.  See http://www.chromium.org/developers/how-tos/api-keys
        // Environment.SetEnvironmentVariable("GOOGLE_API_KEY", "");
        // Environment.SetEnvironmentVariable("GOOGLE_DEFAULT_CLIENT_ID", "");
        // Environment.SetEnvironmentVariable("GOOGLE_DEFAULT_CLIENT_SECRET", "");

        //Chromium Command Line args
        //http://peter.sh/experiments/chromium-command-line-switches/
        //NOTE: Not all relevant in relation to `CefSharp`, use for reference purposes only.

        var settings = new CefSettings();
        //settings.RemoteDebuggingPort = 8088;
        //The location where cache data will be stored on disk. If empty an in-memory cache will be used for some features and a temporary disk cache for others.
        //HTML5 databases such as localStorage will only persist across sessions if a cache path is specified. 

        //if (!Directory.Exists(DownloadDirectory)) Directory.CreateDirectory(DownloadDirectory);

        settings.CachePath = userDataDir + "\\cache";
        //settings.UserAgent = "CefSharp Browser" + Cef.CefSharpVersion; // Example User Agent
        //settings.CefCommandLineArgs.Add("renderer-process-limit", "1");
        //settings.CefCommandLineArgs.Add("renderer-startup-dialog", "1");
        //settings.CefCommandLineArgs.Add("enable-media-stream", "1"); //Enable WebRTC
        //settings.CefCommandLineArgs.Add("no-proxy-server", "1"); //Don't use a proxy server, always make direct connections. Overrides any other proxy server flags that are passed.
        //settings.CefCommandLineArgs.Add("debug-plugin-loading", "1"); //Dumps extra logging about plugin loading to the log file.
        //settings.CefCommandLineArgs.Add("disable-plugins-discovery", "1"); //Disable discovering third-party plugins. Effectively loading only ones shipped with the browser plus third-party ones as specified by --extra-plugin-dir and --load-plugin switches
        //settings.CefCommandLineArgs.Add("enable-system-flash", "1"); //Automatically discovered and load a system-wide installation of Pepper Flash.

        //settings.CefCommandLineArgs.Add("ppapi-flash-path", @"C:\WINDOWS\SysWOW64\Macromed\Flash\pepflashplayer32_18_0_0_209.dll"); //Load a specific pepper flash version (Step 1 of 2)
        //settings.CefCommandLineArgs.Add("ppapi-flash-version", "18.0.0.209"); //Load a specific pepper flash version (Step 2 of 2)

        //NOTE: For OSR best performance you should run with GPU disabled:
        // `--disable-gpu --disable-gpu-compositing --enable-begin-frame-scheduling`
        // (you'll loose WebGL support but gain increased FPS and reduced CPU usage).
        // http://magpcss.org/ceforum/viewtopic.php?f=6&t=13271#p27075
        //https://bitbucket.org/chromiumembedded/cef/commits/e3c1d8632eb43c1c2793d71639f3f5695696a5e8

        //NOTE: The following function will set all three params
        //settings.SetOffScreenRenderingBestPerformanceArgs();
        //settings.CefCommandLineArgs.Add("disable-gpu", "1");
        //settings.CefCommandLineArgs.Add("disable-gpu-compositing", "1");
        //settings.CefCommandLineArgs.Add("enable-begin-frame-scheduling", "1");

        //settings.CefCommandLineArgs.Add("disable-gpu-vsync", "1"); //Disable Vsync

        //Disables the DirectWrite font rendering system on windows.
        //Possibly useful when experiencing blury fonts.
        //settings.CefCommandLineArgs.Add("disable-direct-write", "1");

        settings.MultiThreadedMessageLoop = true;



        var proxy = ProxyConfig.GetProxyInformation();
        switch (proxy.AccessType)
        {
            case InternetOpenType.Direct:
                {
                    //Don't use a proxy server, always make direct connections.
                    settings.CefCommandLineArgs.Add("no-proxy-server", "1");
                    break;
                }
            case InternetOpenType.Proxy:
                {
                    settings.CefCommandLineArgs.Add("proxy-server", proxy.ProxyAddress);
                    break;
                }
            case InternetOpenType.PreConfig:
                {
                    settings.CefCommandLineArgs.Add("proxy-auto-detect", "1");
                    break;
                }
        }

        settings.LogSeverity = LogSeverity.Error;




        //settings.RegisterExtension(new CefExtension("cefsharp/example", Resources.extension));

        settings.FocusedNodeChangedEnabled = true;

        Cef.OnContextInitialized = delegate
        {
            var cookieManager = Cef.GetGlobalCookieManager();
            cookieManager.SetStoragePath(userDataDir + "\\cookies", true);
            //cookieManager.SetSupportedSchemes("custom");
        }; 

        if (!Cef.Initialize(settings))
        {
            throw new Exception("Unable to Initialize Cef");
        }
    }

}
}
