using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar
{
    public class ChildWindows
    {
        public event EventHandler Changed;

        
        public Dictionary<String, BrowserPopupForm> Items;
        public void RegisterWindow(BrowserPopupForm form)
        {
            this.Items.Add(form.WindowId, form);
            if (Changed != null) Changed(this, EventArgs.Empty);

        }
        public void Remove(string windowId)
        {
            this.Items.Remove(windowId);
            if (Changed != null) Changed(this, EventArgs.Empty);

        }


    }
}
