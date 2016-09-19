using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Caesar
{
    public class FormLayout
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string targetUrl { get; set; }
        public int WindowState { get; set; }
        public bool inDesktop { get; set; }
        public double ZoomLevel { get; set; }

    }

    public class Layouts
    {
        public double ZoomLevel;
        public void ResetWindowLayouts()
        {
            this.Items = new Dictionary<string, FormLayout>();
            Save();
        }

        private static string path { get { return Program.UserDataDirectory + "/layout.json"; } }

        public Dictionary<string, FormLayout> Items;

        public void Save()
        {
            string settingsJson = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(path, settingsJson);
        }


        public static Layouts Load()
        {
            Layouts layouts;

            if (!File.Exists(path))
            {
                layouts = new Layouts() { Items = new Dictionary<string, FormLayout>() , ZoomLevel = 0 };
            }
            else
            {
                layouts = JsonConvert.DeserializeObject<Layouts>(File.ReadAllText(path));
            }
            return layouts;
        }


        public static FormLayout GetDefaultLayout(string windowId)
        {
            FormLayout defaultLayout = new FormLayout
            {
                X = 20,
                Y = 20,
                Width = 800,
                Height = 600
            };
            if (windowId == "#statusBar")
            {
                defaultLayout = new FormLayout
                {
                    X = 20,
                    Y = 20,
                    Width = 551,
                    Height = 115
                };
            }
            else if (windowId == "landingPage")
            {
                defaultLayout = new FormLayout
                {
                    X = 200,
                    Y = 200,
                    Width = 415,
                    Height = 265
                };
            }
            return defaultLayout;
        }

        public FormLayout GetLayout(string windowId)
        {
            FormLayout layout;
            //if (this.Items.ContainsKey(windowId) && (this.Items[windowId].WindowState == 2 || (this.Items[windowId].X >= 0 && this.Items[windowId].Y >= 0)))
            if (this.Items.ContainsKey(windowId))
            {
                layout = this.Items[windowId];
                if (layout.X < 0 || layout.Y < 0 || layout.WindowState != 0) {
                    layout.X = 50;
                    layout.Y = 50;
                    layout.WindowState = 0;
                }
            }
            else
            {
                layout = GetDefaultLayout(windowId);
            }
            return layout;
        }
    }
}
