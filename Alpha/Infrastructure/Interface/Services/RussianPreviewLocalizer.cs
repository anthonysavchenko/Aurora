
using DevExpress.XtraPrinting.Localization;

//using Taumis.Infrastructure.Interface.Properties;

namespace Taumis.Infrastructure.Interface.Services.Localizers
{
    public class RussianPreviewLocalizer : PreviewLocalizer
    {
        public override string Language { get { return "Russian"; } }
        public override string GetLocalizedString(PreviewStringId id)
        {
            string _res = "";
            switch (id)
            {
                //case PreviewStringId.Button_Ok: _res =  Resources.ButtonOK; break;
                default:
                    _res  = base.GetLocalizedString(id);
                    break;
            }
            return _res;
        }
    }

}
