namespace Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams
{
    /// <summary>
    /// Параметры запуска юз-кейса содержащие данные
    /// </summary>
    /// <typeparam name="TDataType">Тип данных</typeparam>
    public class BaseDataStartUpParams<TDataType> : AnyStartUpParams
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="_data">Данные</param>
        public BaseDataStartUpParams(TDataType _data)
        {
            Data = _data;
        }

        /// <summary>
        /// Данные
        /// </summary>
        public TDataType Data
        {
            get; 
            set;
        }
    }
}
