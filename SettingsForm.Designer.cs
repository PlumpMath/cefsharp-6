namespace Caesar
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBaseURL = new System.Windows.Forms.TextBox();
            this.rbCometD_DotNet = new System.Windows.Forms.RadioButton();
            this.rbCometD_Browser = new System.Windows.Forms.RadioButton();
            this.rbgCometd = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.textCBW_URL = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rbgCometd.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(437, 96);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(351, 96);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Base URL";
            // 
            // textBaseURL
            // 
            this.textBaseURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBaseURL.Location = new System.Drawing.Point(74, 10);
            this.textBaseURL.Name = "textBaseURL";
            this.textBaseURL.Size = new System.Drawing.Size(438, 20);
            this.textBaseURL.TabIndex = 3;
            // 
            // rbCometD_DotNet
            // 
            this.rbCometD_DotNet.AutoSize = true;
            this.rbCometD_DotNet.Location = new System.Drawing.Point(244, 6);
            this.rbCometD_DotNet.Name = "rbCometD_DotNet";
            this.rbCometD_DotNet.Size = new System.Drawing.Size(185, 17);
            this.rbCometD_DotNet.TabIndex = 6;
            this.rbCometD_DotNet.TabStop = true;
            this.rbCometD_DotNet.Text = ".NET CometD. Single connection ";
            this.rbCometD_DotNet.UseVisualStyleBackColor = true;
            // 
            // rbCometD_Browser
            // 
            this.rbCometD_Browser.AutoSize = true;
            this.rbCometD_Browser.Location = new System.Drawing.Point(3, 5);
            this.rbCometD_Browser.Name = "rbCometD_Browser";
            this.rbCometD_Browser.Size = new System.Drawing.Size(210, 17);
            this.rbCometD_Browser.TabIndex = 7;
            this.rbCometD_Browser.TabStop = true;
            this.rbCometD_Browser.Text = "Browser CometD. Multiple connections.";
            this.rbCometD_Browser.UseVisualStyleBackColor = true;
            this.rbCometD_Browser.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // rbgCometd
            // 
            this.rbgCometd.Controls.Add(this.rbCometD_Browser);
            this.rbgCometd.Controls.Add(this.rbCometD_DotNet);
            this.rbgCometd.Location = new System.Drawing.Point(74, 58);
            this.rbgCometd.Name = "rbgCometd";
            this.rbgCometd.Size = new System.Drawing.Size(438, 28);
            this.rbgCometd.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "CometD";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textCBW_URL
            // 
            this.textCBW_URL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textCBW_URL.Location = new System.Drawing.Point(74, 36);
            this.textCBW_URL.Name = "textCBW_URL";
            this.textCBW_URL.Size = new System.Drawing.Size(438, 20);
            this.textCBW_URL.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "CBW URL";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 131);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textCBW_URL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rbgCometd);
            this.Controls.Add(this.textBaseURL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Shown += new System.EventHandler(this.SettingsForm_Shown);
            this.rbgCometd.ResumeLayout(false);
            this.rbgCometd.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBaseURL;
        private System.Windows.Forms.RadioButton rbCometD_DotNet;
        private System.Windows.Forms.RadioButton rbCometD_Browser;
        private System.Windows.Forms.Panel rbgCometd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textCBW_URL;
        private System.Windows.Forms.Label label3;
    }
}