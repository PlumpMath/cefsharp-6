using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caesar
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            RadioButton rbCometdMode = this.rbgCometd.Controls.OfType<RadioButton>().FirstOrDefault(i => i.Checked);
            Program.Settings.CometdMode = rbCometdMode.Name.EndsWith("Browser") ? "Browser" : "DotNet";
            Program.Settings.BaseURL = this.textBaseURL.Text;
            Program.Settings.CBW_URL = this.textCBW_URL.Text;

            Program.Settings.Save();

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SettingsForm_Shown(object sender, EventArgs e)
        {
            string cometdMode = Program.Settings.CometdMode;
            this.rbgCometd.Controls.OfType<RadioButton>().ToList().ForEach(i => {
                i.Checked = i.Name.EndsWith(cometdMode);
            });
            this.textBaseURL.Text = Program.Settings.BaseURL;
            this.textCBW_URL.Text = Program.Settings.CBW_URL;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}

