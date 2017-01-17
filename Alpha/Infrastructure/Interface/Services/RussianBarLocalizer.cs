using DevExpress.XtraBars.Localization;

namespace Taumis.Infrastructure.Interface.Services.Localizers
{
    public class RussianBarLocalizer : BarLocalizer
    {
        public override string Language { get { return "Russian"; } }
        public override string GetLocalizedString(BarString id)
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
