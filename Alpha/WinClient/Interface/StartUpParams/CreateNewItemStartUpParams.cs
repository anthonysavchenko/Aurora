using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;

namespace Taumis.Alpha.WinClient.Aurora.Interface.StartUpParams
{
    /// <summary>
    /// Параметры запуска юз-кейса для создания нового элемента
    /// </summary>
    public class CreateNewItemStartUpParams : BaseDataStartUpParams<string>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="_domainObject"></param>
        public CreateNewItemStartUpParams(string id)
            : base(id)
        {
        }
    }
}