using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Configuration;
using Microsoft.Practices.CompositeUI.Services;
using System;
using System.Xml;
using Taumis.EnterpriseLibrary.Win.Common.Properties;

namespace Taumis.Infrastructure.Library.Services
{
    /// <summary>
    /// Класс загружающий модули из xml профайл каталога
    /// </summary>
    public class XmlStreamDependentModuleEnumerator : IModuleEnumerator
    {
        /// <summary>
        /// Хранилище модулей
        /// </summary>
        [ServiceDependency]
        public IModuleInfoStore ModuleInfoStore { get; set; }

        /// <summary>
        /// Возвращает массив модулей <see cref="T:Microsoft.Practices.CompositeUI.Configuration.IModuleInfo"/>
        /// перечисленный в профайл каталоге
        /// </summary>
        /// <returns>Массив модулей</returns>
        public IModuleInfo[] EnumerateModules()
        {
            string xml = ModuleInfoStore.GetModuleListXml();

            if (String.IsNullOrEmpty(xml))
                return new DependentModuleInfo[0];

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            switch (doc.FirstChild.NamespaceURI)
            {
                case SolutionProfileV1Parser.Namespace:
                    return new SolutionProfileV1Parser().Parse(xml);

                case SolutionProfileV2Parser.Namespace:
                    return new SolutionProfileV2Parser().Parse(xml);

                default:
                    throw new InvalidOperationException(Resources.InvalidSolutionProfileSchema);
            }
        }
    }
}