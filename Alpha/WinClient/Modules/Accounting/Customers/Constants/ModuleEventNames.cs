namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Constants
{
    public static class ModuleEventNames
    {
        /// <summary>
        /// Печать квитанций для списка абонентов (вид список)
        /// </summary>
        public const string PRINT_LIST = "event://Customers/PrintList";

        /// <summary>
        /// Печать квитанции для абонента (вид платежи и начисления)
        /// </summary>
        public const string PRINT_PAYMENTS_AND_CHARGES = "event://Customers/PrintPaymmentsAndCharges";
    }
}