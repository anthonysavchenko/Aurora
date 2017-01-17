using Microsoft.Practices.CompositeUI;
using System;

namespace Taumis.EnterpriseLibrary.Win.Modules.CommonModule
{
    /// <summary>
    /// Модуль
    /// </summary>
    public sealed class CommonModule : ModuleInit, IModule
    {

        #region Открытые свойства

        /// <summary>
        /// Юзкейз
        /// </summary>
        [ServiceDependency]
        public WorkItem rootWorkItem
        {
            set;
            private get;
        }

        #endregion

        #region Открытые методы

        /// <summary>
        /// Загрузить модуль
        /// </summary>
        public override void Load()
        {
            throw new UsecaseException("Ошибка загрузки");
        }

        /// <summary>
        /// Загрузить модуль
        /// </summary>
        /// <param name="moduleControllerType">Тип контроллера модуля</param>
        /// <param name="usecaseName">Имя юзкейза</param>
        public void Load(Type moduleControllerType, string usecaseName)
        {
            if (moduleControllerType == null)
            {
                throw new UsecaseException("Ошибка загрузки");
            }

            base.Load();

            CommonControlledWorkItem workItem;
            if (String.IsNullOrEmpty(usecaseName))
            {
                workItem = rootWorkItem.WorkItems.AddNew<CommonControlledWorkItem>();
            }
            else
            {
                workItem = rootWorkItem.WorkItems.AddNew<CommonControlledWorkItem>(usecaseName);
            }
            workItem.ControllerType = moduleControllerType;
            workItem.Controller.Load();
        }

        #endregion

    }
}
