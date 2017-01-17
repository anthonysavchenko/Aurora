using System;
using System.Collections.Generic;
using System.Text;

using Taumis.Infrastructure.Interface.Constants;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPrinting.Localization;
using DevExpress.XtraBars.Localization;

namespace Taumis.Infrastructure.Interface.Services
{
    /// <summary>
    /// Производит локализацию контролов.
    /// </summary>
    public class LocalizerService
    {
        /// <summary>
        /// Производит локализацию DevExpress контролов
        /// </summary>
        static public void LocalizeDevExpress()
        {
            GridLocalizer.Active = new Localizers.RussianGridLocalizer();
            Localizer.Active = new Localizers.RussianEditorsLocalizer();
            PreviewLocalizer.Active = new Localizers.RussianPreviewLocalizer();
            BarLocalizer.Active = new Localizers.RussianBarLocalizer();
            
        }
    }
}
