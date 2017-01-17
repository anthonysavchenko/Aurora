using Microsoft.Practices.CompositeUI;
using System;

namespace Taumis.EnterpriseLibrary.Win.Modules.CommonModule
{
    /// <summary>
    /// Юзкейз с контроллером
    /// </summary>
    public sealed class CommonControlledWorkItem : WorkItem
    {
        /// <summary>
        /// Контроллер
        /// </summary>
        public ICommonModuleController Controller
        {
            private set;
            get;
        }

        /// <summary>
        /// Тип контроллера
        /// </summary>
        public Type ControllerType
        {
            set
            {
                Controller = (ICommonModuleController)Items.AddNew(value);
            }
        }
    }
}
