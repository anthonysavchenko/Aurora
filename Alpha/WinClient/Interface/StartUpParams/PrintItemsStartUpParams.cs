using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;

namespace Taumis.Alpha.WinClient.Aurora.Interface.StartUpParams
{
    /// <summary>
    /// Параметры запуска модуля для печати объектов
    /// </summary>
    public class PrintItemsStartUpParams : BaseDataStartUpParams<string[]>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="_ids">Список идентификаторов</param>
        public PrintItemsStartUpParams(string[] _ids)
            : base(_ids)
        {
        }
    }
}
