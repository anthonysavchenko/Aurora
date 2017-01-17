
using Microsoft.Practices.CompositeUI;

namespace Taumis.Infrastructure.Interface
{
    /// <summary>
    /// Base class for a WorkItem controller.
    /// </summary>
    public abstract class WorkItemController : IWorkItemController
    {
        private WorkItem _workItem;

        /// <summary>
        /// Gets or sets the work item.
        /// </summary>
        /// <value>The work item.</value>
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get { return _workItem; }
            set { _workItem = value; }
        }

        public virtual void Run()
        {
        }
    }
}
