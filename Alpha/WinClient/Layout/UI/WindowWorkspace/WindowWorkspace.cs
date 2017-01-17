﻿using System.Windows.Forms;



namespace Taumis.Infrastructure.Library.UI
{
    public class WindowWorkspace : Microsoft.Practices.CompositeUI.WinForms.WindowWorkspace
    {
        IWin32Window _owner;

        public WindowWorkspace()
        {
        }

        public WindowWorkspace(IWin32Window owner)
            : base(owner)
        {
            _owner = owner;
        }

        protected override void OnShow(Control smartPart, 
			Microsoft.Practices.CompositeUI.WinForms.WindowSmartPartInfo smartPartInfo)
        {
            GetOrCreateForm(smartPart);
            OnApplySmartPartInfo(smartPart, smartPartInfo);
            base.OnShow(smartPart, smartPartInfo);
        }

        protected override void OnClose(Control smartPart)
        {
            Form host = Windows[smartPart];
            host.Hide();
			
			base.OnClose(smartPart);
        }

        protected override void OnApplySmartPartInfo(Control smartPart, 
			Microsoft.Practices.CompositeUI.WinForms.WindowSmartPartInfo smartPartInfo)
        {
            base.OnApplySmartPartInfo(smartPart, smartPartInfo);
            /*if (smartPartInfo is WindowSmartPartInfo)
            {
                WindowSmartPartInfo spi = (WindowSmartPartInfo)smartPartInfo;
                if (spi.Keys.ContainsKey(WindowWorkspaceSetting.AcceptButton))
                    Windows[smartPart].AcceptButton = (IButtonControl)spi.Keys[WindowWorkspaceSetting.AcceptButton];
                if (spi.Keys.ContainsKey(WindowWorkspaceSetting.CancelButton))
                    Windows[smartPart].CancelButton = (IButtonControl)spi.Keys[WindowWorkspaceSetting.CancelButton];
                if (spi.Keys.ContainsKey(WindowWorkspaceSetting.FormBorderStyle))
                    Windows[smartPart].FormBorderStyle = (FormBorderStyle)spi.Keys[WindowWorkspaceSetting.FormBorderStyle];
            }*/
        }

        protected new Form GetOrCreateForm(Control control)
        {
            bool resizeRequired = !Windows.ContainsKey(control);
            Form form = base.GetOrCreateForm(control);
            form.ShowInTaskbar = (_owner == null);
            
            if (resizeRequired)
                form.ClientSize = control.Size;
           
            return form;
        }
    }
}
