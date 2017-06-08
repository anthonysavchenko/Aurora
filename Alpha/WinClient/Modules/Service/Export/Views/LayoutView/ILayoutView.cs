using System;
using System.Collections.Generic;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Enums;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export
{
    /// <summary>
    /// Интерфейс вью формы
    /// </summary>
    public interface ILayoutView : IBaseLayoutView
    {
        /// <summary>
        /// Открывает указанную старницу мастера эскпорта
        /// </summary>
        /// <param name="page">Страница</param>
        void SelectPage(WizardPages page);
        
        /// <summary>
        /// Выбранное действие мастера экспорта данных
        /// </summary>
        WizardAction WizardAction { get; }

        /// <summary>
        /// Путь для сохранения файлов с экспортируемыми данными
        /// </summary>
        string OutputPath { get; set; }

        /// <summary>
        /// Файл с шаблоном
        /// </summary>
        string TemplatePath { get; set; }

        /// <summary>
        /// Учетный период, за который будут экспортированы начисления
        /// </summary>
        DateTime Period { get; set; }

        /// <summary>
        /// Выбран формат сбербанка
        /// </summary>
        bool SbrfChecked { get; set; }

        /// <summary>
        /// Выбран формат примсоцбанка
        /// </summary>
        bool PrimSocBankChecked { get; set; }

        /// <summary>
        /// Флаг экспорта данных ГИС ЖКХ: "только новые" / "все" абоненты
        /// </summary>
        bool GisZhkhOnlyNew { get; set; }

        /// <summary>
        /// Учетный период, от которого будут выбираться данные льготников
        /// </summary>
        DateTime StartPeriod { get; set; }

        /// <summary>
        /// Устанавливает значение прогресс-бара
        /// </summary>
        /// <param name="percent">Значение в процентах</param>
        void SetProgress(int percent);

        /// <summary>
        /// Сбрасывает значение прогресс-бара к исходному
        /// </summary>
        void ResetProgress();

        /// <summary>
        /// Информация о результате экспорта
        /// </summary>
        string ResultText { set; }

        bool ServiceMatchingTableProgressBarVisible { set; }
        void ClearServiceMatchingTable();
        void AddRowToServiceMatchingTable(
            int serviceTypeID, 
            string serviceTypeName, 
            List<string> matchingValues, 
            string selectedValue, 
            int tabIndex);
        Dictionary<int, string> GetServiceMatchingDict();
    }
}