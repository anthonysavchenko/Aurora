using System;

namespace Taumis.EnterpriseLibrary.Win.Modules.CommonModule
{
	/// <summary>
	/// Имя юэкейза
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class UsecaseNameAttribute : Attribute
	{

        #region Открытые методы

        /// <summary>
		/// Конструктор
		/// </summary>
        /// <param name="Name">Имя</param>
        public UsecaseNameAttribute(string name)
		{
            UsecaseName = name;
        }

        #endregion

        #region Открытые свойства

        /// <summary>
		/// Имя
		/// </summary>
        public string UsecaseName
		{
            private set;
			get;
		}

        #endregion

    }
}
