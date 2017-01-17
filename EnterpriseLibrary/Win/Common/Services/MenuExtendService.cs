using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.WinForms.UIElements;
using Microsoft.Practices.ObjectBuilder;
using System.Windows.Forms;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.EnterpriseLibrary.Win.Services
{
    /// <summary>
    /// Сервис добавления пунктов меню
    /// </summary>
    [Service]
    public class MenuExtendService
    {
        private WorkItem _rootWorkItem;

        /// <summary>
        /// Сервис добавления элементов в главное меню систеы.
        /// </summary>
        [InjectionConstructor]
        public MenuExtendService([ServiceDependency] WorkItem rootWorkItem)
        {
            _rootWorkItem = rootWorkItem;
        }
        // Запустить процедуру добавления элемента главного меню системы.
        public bool Run(ToolStripMenuItem subMenuItem, string uIExtensionSiteName)
        {
            // Если элемент главного меню уже существует.
            if (_rootWorkItem.UIExtensionSites.Contains(uIExtensionSiteName))
            {
                UIExtensionSite uies = _rootWorkItem.UIExtensionSites[uIExtensionSiteName];
                uies.Add(subMenuItem);
            }
            else
            // Иначе создать элемент главного меню, затем его адаптер (способный добавлять элементы подменю), 
            // а после уже добавить через адаптер сам новый элемент.
            {
                ToolStripMenuItem item = new ToolStripMenuItem(uIExtensionSiteName);

                ToolStripItemCollectionUIAdapter uiAdapter = new ToolStripItemCollectionUIAdapter(item.DropDownItems);

                // Добавляем новый элемент в подменю элемента главного меню усилиями адаптера.
                uiAdapter.Add(subMenuItem);

                // Добавляем элемент в главное меню системы.
                _rootWorkItem.UIExtensionSites[CommonUIExtensionSiteNames.MainMenu].Add(item);

                // Регистрируем элемент главного меню для использования другими загружаемыми модулями.
                _rootWorkItem.UIExtensionSites.RegisterSite(uIExtensionSiteName, uiAdapter);
            }
            return false;
        }

        // Запустить процедуру добавления вложенных элементов главного меню системы.
        public bool Run(ToolStripMenuItem subMenuItem, string[] _menuNames)
        {
            if (_menuNames.Length > 0)
            {
                UIExtensionSite _lastUies = null;
                for (int i = 0; i < _menuNames.Length; i++)
                {
                    if (_rootWorkItem.UIExtensionSites.Contains(string.Join("\\", _menuNames, 0, i + 1)))
                    {
                        _lastUies = _rootWorkItem.UIExtensionSites[string.Join("\\", _menuNames, 0, i + 1)];
                    }
                    else
                    {
                        ToolStripMenuItem item = new ToolStripMenuItem(_menuNames[i]);
                        ToolStripItemCollectionUIAdapter uiAdapter = new ToolStripItemCollectionUIAdapter(item.DropDownItems);

                        // Добавляем элемент в  меню.
                        _rootWorkItem.UIExtensionSites[i == 0 ? CommonUIExtensionSiteNames.MainMenu : string.Join("\\", _menuNames, 0 , i)].Add(item);

                        // Регистрируем элемент главного меню для использования другими загружаемыми модулями.
                        _rootWorkItem.UIExtensionSites.RegisterSite(string.Join("\\", _menuNames, 0, i + 1), uiAdapter);

                        _lastUies = _rootWorkItem.UIExtensionSites[string.Join("\\", _menuNames, 0, i + 1)];
                    }
                }

                _lastUies.Add(subMenuItem);
            }
            return false;
        }
    }
}
