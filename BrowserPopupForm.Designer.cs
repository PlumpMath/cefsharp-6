namespace Caesar
{
    partial class BrowserPopupForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserPopupForm));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.downloadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wndMenu_Root = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.regularToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.xLargeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.xXLargeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.xXLargeToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.xXXLargeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDesktopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetDesktopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.launchDesktopToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.devToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.devtoolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.landingPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appMenu_exitApp = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Caesar";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuSeparator,
            this.downloadsToolStripMenuItem,
            this.wndMenu_Root,
            this.bbToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(136, 76);
            // 
            // contextMenuSeparator
            // 
            this.contextMenuSeparator.Name = "contextMenuSeparator";
            this.contextMenuSeparator.Size = new System.Drawing.Size(132, 6);
            // 
            // downloadsToolStripMenuItem
            // 
            this.downloadsToolStripMenuItem.Name = "downloadsToolStripMenuItem";
            this.downloadsToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.downloadsToolStripMenuItem.Text = "Downloads";
            this.downloadsToolStripMenuItem.Click += new System.EventHandler(this.downloadsToolStripMenuItem_Click);
            // 
            // wndMenu_Root
            // 
            this.wndMenu_Root.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomToolStripMenuItem1,
            this.saveDesktopToolStripMenuItem,
            this.resetDesktopToolStripMenuItem,
            this.launchDesktopToolStripMenuItem1,
            this.closeAllToolStripMenuItem1});
            this.wndMenu_Root.Name = "wndMenu_Root";
            this.wndMenu_Root.Size = new System.Drawing.Size(135, 22);
            this.wndMenu_Root.Text = "Window";
            // 
            // zoomToolStripMenuItem1
            // 
            this.zoomToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.regularToolStripMenuItem1,
            this.xLargeToolStripMenuItem1,
            this.xXLargeToolStripMenuItem1,
            this.xXLargeToolStripMenuItem2,
            this.xXXLargeToolStripMenuItem1});
            this.zoomToolStripMenuItem1.Name = "zoomToolStripMenuItem1";
            this.zoomToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.zoomToolStripMenuItem1.Tag = "0.50";
            this.zoomToolStripMenuItem1.Text = "Zoom";
            // 
            // regularToolStripMenuItem1
            // 
            this.regularToolStripMenuItem1.Name = "regularToolStripMenuItem1";
            this.regularToolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
            this.regularToolStripMenuItem1.Tag = "0";
            this.regularToolStripMenuItem1.Text = "Regular";
            this.regularToolStripMenuItem1.Click += new System.EventHandler(this.regularToolStripMenuItem_Click);
            // 
            // xLargeToolStripMenuItem1
            // 
            this.xLargeToolStripMenuItem1.Name = "xLargeToolStripMenuItem1";
            this.xLargeToolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
            this.xLargeToolStripMenuItem1.Tag = "0.25";
            this.xLargeToolStripMenuItem1.Text = "Large";
            this.xLargeToolStripMenuItem1.Click += new System.EventHandler(this.regularToolStripMenuItem_Click);
            // 
            // xXLargeToolStripMenuItem1
            // 
            this.xXLargeToolStripMenuItem1.Name = "xXLargeToolStripMenuItem1";
            this.xXLargeToolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
            this.xXLargeToolStripMenuItem1.Tag = "0.5";
            this.xXLargeToolStripMenuItem1.Text = "X - Large";
            this.xXLargeToolStripMenuItem1.Click += new System.EventHandler(this.regularToolStripMenuItem_Click);
            // 
            // xXLargeToolStripMenuItem2
            // 
            this.xXLargeToolStripMenuItem2.Name = "xXLargeToolStripMenuItem2";
            this.xXLargeToolStripMenuItem2.Size = new System.Drawing.Size(135, 22);
            this.xXLargeToolStripMenuItem2.Tag = "0.80";
            this.xXLargeToolStripMenuItem2.Text = "XX - Large";
            this.xXLargeToolStripMenuItem2.Click += new System.EventHandler(this.regularToolStripMenuItem_Click);
            // 
            // xXXLargeToolStripMenuItem1
            // 
            this.xXXLargeToolStripMenuItem1.Name = "xXXLargeToolStripMenuItem1";
            this.xXXLargeToolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
            this.xXXLargeToolStripMenuItem1.Tag = "1.8";
            this.xXXLargeToolStripMenuItem1.Text = "XXX - Large";
            this.xXXLargeToolStripMenuItem1.Click += new System.EventHandler(this.regularToolStripMenuItem_Click);
            // 
            // saveDesktopToolStripMenuItem
            // 
            this.saveDesktopToolStripMenuItem.Name = "saveDesktopToolStripMenuItem";
            this.saveDesktopToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.saveDesktopToolStripMenuItem.Text = "Save Desktop";
            this.saveDesktopToolStripMenuItem.Click += new System.EventHandler(this.OnSaveLayout);
            // 
            // resetDesktopToolStripMenuItem
            // 
            this.resetDesktopToolStripMenuItem.Name = "resetDesktopToolStripMenuItem";
            this.resetDesktopToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.resetDesktopToolStripMenuItem.Text = "Reset Desktop";
            this.resetDesktopToolStripMenuItem.Click += new System.EventHandler(this.OnResetLayouts);
            // 
            // launchDesktopToolStripMenuItem1
            // 
            this.launchDesktopToolStripMenuItem1.Name = "launchDesktopToolStripMenuItem1";
            this.launchDesktopToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.launchDesktopToolStripMenuItem1.Text = "Launch Desktop";
            this.launchDesktopToolStripMenuItem1.Click += new System.EventHandler(this.launchDesktopToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem1
            // 
            this.closeAllToolStripMenuItem1.Name = "closeAllToolStripMenuItem1";
            this.closeAllToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.closeAllToolStripMenuItem1.Text = "Close All";
            this.closeAllToolStripMenuItem1.Click += new System.EventHandler(this.OnCloseAll);
            // 
            // bbToolStripMenuItem
            // 
            this.bbToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.devToolStripMenuItem,
            this.appMenu_exitApp});
            this.bbToolStripMenuItem.Name = "bbToolStripMenuItem";
            this.bbToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.bbToolStripMenuItem.Text = "Application";
            this.bbToolStripMenuItem.Click += new System.EventHandler(this.OnCloseAll);
            // 
            // devToolStripMenuItem
            // 
            this.devToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.devtoolsToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.openWindowToolStripMenuItem});
            this.devToolStripMenuItem.Name = "devToolStripMenuItem";
            this.devToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.devToolStripMenuItem.Text = "Dev";
            // 
            // devtoolsToolStripMenuItem
            // 
            this.devtoolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.landingPageToolStripMenuItem,
            this.statusBarToolStripMenuItem});
            this.devtoolsToolStripMenuItem.Name = "devtoolsToolStripMenuItem";
            this.devtoolsToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.devtoolsToolStripMenuItem.Text = "Devtools";
            // 
            // landingPageToolStripMenuItem
            // 
            this.landingPageToolStripMenuItem.Name = "landingPageToolStripMenuItem";
            this.landingPageToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.landingPageToolStripMenuItem.Text = "Landing Page";
            this.landingPageToolStripMenuItem.Click += new System.EventHandler(this.landingPageToolStripMenuItem_Click);
            // 
            // statusBarToolStripMenuItem
            // 
            this.statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            this.statusBarToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.statusBarToolStripMenuItem.Text = "Status Bar";
            this.statusBarToolStripMenuItem.Click += new System.EventHandler(this.statusBarToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // openWindowToolStripMenuItem
            // 
            this.openWindowToolStripMenuItem.Name = "openWindowToolStripMenuItem";
            this.openWindowToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.openWindowToolStripMenuItem.Text = "Open Window";
            this.openWindowToolStripMenuItem.Click += new System.EventHandler(this.openNewToolStripMenuItem_Click);
            // 
            // appMenu_exitApp
            // 
            this.appMenu_exitApp.Name = "appMenu_exitApp";
            this.appMenu_exitApp.Size = new System.Drawing.Size(94, 22);
            this.appMenu_exitApp.Text = "Exit";
            this.appMenu_exitApp.Click += new System.EventHandler(this.OnAppExit);
            // 
            // BrowserPopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 387);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BrowserPopupForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BrowserPopupForm_FormClosing);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BrowserPopupForm_MouseDown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem bbToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem appMenu_exitApp;
        private System.Windows.Forms.ToolStripSeparator contextMenuSeparator;
        private System.Windows.Forms.ToolStripMenuItem wndMenu_Root;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveDesktopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetDesktopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem launchDesktopToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem devToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem devtoolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem regularToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem xLargeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem xXLargeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem xXLargeToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem xXXLargeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem landingPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
    }
}

