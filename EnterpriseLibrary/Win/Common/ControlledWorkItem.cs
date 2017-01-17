
using Microsoft.Practices.CompositeUI;

namespace Taumis.Infrastructure.Interface
{
    /// <summary>
    /// Represents a WorkItem that uses a WorkItem controller to perform its business logic.
    /// Представление для Рабочего элемента использующего контроллер для выполнения бизнес-логики.
    /// </summary>
    /// <typeparam name="TController"></typeparam>
    public sealed class ControlledWorkItem<TController> : WorkItem
    {
        private TController _controller;

        /// <summary>
        /// Gets the controller.
        /// </summary>
        public TController Controller
        {
            get { return _controller; }
        }

        /// <summary>
        /// See <see cref="M:Microsoft.Practices.ObjectBuilder.IBuilderAware.OnBuiltUp(System.String)"/> for more information.
        /// </summary>
        public override void OnBuiltUp(string id)
        {
            base.OnBuiltUp(id);

            _controller = Items.AddNew<TController>();
        }
    }
}
