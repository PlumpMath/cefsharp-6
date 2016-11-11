using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace Caesar
{
    public class UserSettings
    {
        public string Layouts { get; set; }

        private string baseURL;
        public string BaseURL {
            get {
                //return "http://xashmun19qap.ash.pwj.com:8081/";
                return this.baseURL;
            }
            set { this.baseURL = value; }
        }

        private string cbwURL;
        public string CBW_URL {
            get
            {
                //return "http://xashmun16qap.ash.pwj.com:9090/merit-web/";
                return this.cbwURL;
            }
            set { this.cbwURL = value; }
            
        }
        public string CometdMode { get; set; }
        public string AppMode { get; set; }
        public static string SettingsFilePath { get { return Program.UserDataDirectory + "\\settings.json"; } }
        public int MaximumNumberOfTRBScreens { get; set; }


        public void Save()
        {
            string settingsJson = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(SettingsFilePath, settingsJson);
        }



        public static UserSettings Load()
        {
            UserSettings settings = new UserSettings()
            {
                BaseURL = "https://newmerit-ui-prod.she.pwj.com/",
                CBW_URL = "http://localhost:8080/index-dev.jsp",
                CometdMode = "Browser",
                AppMode = "prod",
                MaximumNumberOfTRBScreens = 4
            };
            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    settings = JsonConvert.DeserializeObject<UserSettings>(File.ReadAllText(SettingsFilePath));
                }
                else
                {
                    settings.Save();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Fatal error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return settings;

        }
    }
}
