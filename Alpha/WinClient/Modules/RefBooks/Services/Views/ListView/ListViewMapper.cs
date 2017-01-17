using DomItem = Protos.Domain.RefBook.CargoNameETSNG;

namespace Transposoft.VCT.WinClient.Protos.Modules.RefBook.ServiceTypes
{
    /// <summary>
    /// Преобразовывает из View в Домен и обратно.
    /// </summary>
    public static class ListViewMapper
    {
        /// <summary>
        /// Из вида в домен.
        /// </summary>
        public static void ViewToBus(IListView _view, DomItem _dom)
        {
            // Полное название (обязательное).
            _dom.Aka = _view.Aka;
            
            // Шифр (обязательное).
            _dom.CodeNumber = _view.CodeNumber;
        }
    }
}
