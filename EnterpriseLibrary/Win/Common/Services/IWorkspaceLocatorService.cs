using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace Taumis.EnterpriseLibrary.Win.Services
{
    /// <summary>
    /// Интерфейс сервиса определения воркспэйса
    /// </summary>
    public interface IWorkspaceLocatorService
    {
        IWorkspace FindContainingWorkspace(WorkItem workItem, object smartPart);
    }
}
