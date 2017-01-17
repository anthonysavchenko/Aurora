namespace CustomerWebSite.Constants
{
    /// <summary>
    /// Константы для доступа к объектам в сессии веб сервера
    /// </summary>
    public static class SessionState
    {
        /// <summary>
        /// Текущий лицевой счет
        /// </summary>
        public const string SELECTED_ACCOUNT = "SharedController/SelectedAccount";

        /// <summary>
        /// Списко лицевых счетов пользователя
        /// </summary>
        public const string ACCOUNTS = "SharedController/Accounts";
    }
}