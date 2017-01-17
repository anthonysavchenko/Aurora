using DevExpress.XtraCharts;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.ReportView
{
    /// <summary>
    /// Пустая структура параметров для отчетов, у которых нет параметров
    /// </summary>
    public struct EmptyReportParams
    {
    }

    /// <summary>
    /// Базовый перезентер отчетов
    /// </summary>
    abstract public class BaseReportViewPresenter<TView, TParamsType> : BaseDomainPresenter<TView>, IBaseReportViewPresenter
        where TView : IBaseReportView
        where TParamsType : struct
    {
        // Признаки занятости процессов генерации таблицы и диаграммы отчета
        bool _isGridInProgress = false;
        bool _isDiagramInProgress = false;

        /// <summary>
        /// Данные табличной части отчета
        /// </summary>
        protected DataTable _gridData;

        /// <summary>
        /// Данные диаграммной части отчета
        /// </summary>
        protected List<Series> _diagramData;

        /// <summary>
        /// Параметры, с которыми запущен процесс вычисления данных отчета
        /// </summary>
        private TParamsType _params;

        /// <summary>
        /// Признак того, что генерация отчета уже запущена 
        /// </summary>
        public bool IsGenerationRunning
        {
            get
            {
                return (_isGridInProgress || _isDiagramInProgress);
            }
        }

        /// <summary>
        /// Возвращает параметры отчета
        /// </summary>
        protected virtual TParamsType GetReportParams()
        {
            // Параметров отчета нет
            return new TParamsType();
        }

        /// <summary>
        /// Устанавливает шапку отчета
        /// </summary>
        /// <param name="_params">Параметры отчета</param>
        protected virtual void SetReportTitle(TParamsType _params)
        {
        }

        /// <summary>
        /// Запускает генерацию данных для табличной и диаграммной частей отчета
        /// </summary>
        public void GenerateReport()
        {
            // Запускаем отображение прогрессбара
            ShowMarqueeProgressBarFired(this, EventArgs.Empty);

            // Берем параметры для отчета
            _params = GetReportParams();

            _isGridInProgress = true;
            _isDiagramInProgress = true;

            // Запускаем генерацию табличной части
            StartGenerateReport(this, new EventArgs<object>(_params));

            // Запускаем генерацию диаграммной части
            StartGenerateDiagram(this, new EventArgs<object>(_params));
        }

        #region Виртуальные функции для перегрузки

        /// <summary>
        /// Возвращает данные для табличной части отчета
        /// </summary>
        /// <param name="_params">Параметры отчета</param>
        /// <returns>Данные табличной части отчета</returns>
        virtual protected DataTable GetGridData(TParamsType _params)
        {
            return null;
        }

        /// <summary>
        /// Возвращает данные для диаграммной части отчета
        /// </summary>
        /// <param name="_params">Параметры отчета</param>
        /// <returns>Данные диаграммной части отчета</returns>
        virtual protected List<Series> GetDiagramData(TParamsType _params)
        {
            return null;
        }

        /// <summary>
        /// Обрабатывает данные табличной части отчета 
        /// </summary>
        virtual protected void ProcessGridData()
        {
        }

        /// <summary>
        /// Обрабатывает данные диаграммной части отчета
        /// </summary>
        virtual protected void ProcessDiagramData()
        {
        }

        /// <summary>
        /// Оповещает о полном окончании процесса генерации отчета
        /// </summary>
        virtual protected void OnReportGenerationCompleted()
        {
        }

        #endregion

        #region Публикаторы событий

        /// <summary>
        /// Событие начала генерации табличной части
        /// </summary>
        [EventPublication(CommonEventNames.GenerateReportFired, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<object>> StartGenerateReport;

        /// <summary>
        /// Событие окончания генерации табличной части
        /// </summary>
        [EventPublication(CommonEventNames.ReportGenerated, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<DataTable>> ReportGenerated;

        /// <summary>
        /// Событие начала генерации диаграммной части
        /// </summary>
        [EventPublication(CommonEventNames.GenerateDiagramFired, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<object>> StartGenerateDiagram;

        /// <summary>
        /// Событие окончания генерации диаграммной части
        /// </summary>
        [EventPublication(CommonEventNames.DiagramGenerated, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<List<Series>>> DiagramGenerated;

        /// <summary>
        /// Событие начала отображения прогрессбара
        /// </summary>
        [EventPublication(CommonEventNames.ShowMarqueeProgressBarFired, PublicationScope.WorkItem)]
        public event EventHandler ShowMarqueeProgressBarFired;

        /// <summary>
        /// Событие окончания отображения прогрессбара
        /// </summary>
        [EventPublication(CommonEventNames.HideMarqueeProgressBarFired, PublicationScope.WorkItem)]
        public event EventHandler HideMarqueeProgressBarFired;

        #endregion

        #region Подписчики на события

        /// <summary>
        /// Подписчик на запрос генерации табличной части
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="eventArgs">Аргументы события</param>
        [EventSubscription(CommonEventNames.GenerateReportFired, ThreadOption.Background)]
        public void OnGenerateReportFired(object sender, EventArgs<object> e)
        {
            if (this == sender && _isGridInProgress)
            {
                // Загружаем таблицу данных отчета.
                DataTable _dataTable = GetGridData((TParamsType)e.Data); ;

                // Сообщаем об окончании вычислений
                ReportGenerated(this, new EventArgs<DataTable>(_dataTable));
            }
        }

        /// <summary>
        /// Подписчик на событие окончания генерации табличной части
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="eventArgs">Аргументы события</param>
        [EventSubscription(CommonEventNames.ReportGenerated, ThreadOption.UserInterface)]
        public void OnReportGenerated(object sender, EventArgs<DataTable> e)
        {
            if (this == sender && _isGridInProgress)
            {
                _gridData = e.Data;

                _isGridInProgress = false;

                if (!_isDiagramInProgress)
                {
                    ProccessReportGenerationCompletion();
                }
            }
        }

        /// <summary>
        /// Подписчик на запрос генерации диаграммной части
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [EventSubscription(CommonEventNames.GenerateDiagramFired, ThreadOption.Background)]
        public void OnGenerateDiagramFired(object sender, EventArgs<object> e)
        {
            if (this == sender && _isDiagramInProgress)
            {
                // Загружаем таблицу данных отчета.
                List<Series> _series = GetDiagramData((TParamsType)e.Data); ;

                // Сообщаем об окончании вычислений
                DiagramGenerated(this, new EventArgs<List<Series>>(_series));
            }
        }

        /// <summary>
        /// Подписчик на событие окончания генерации диаграммной части
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [EventSubscription(CommonEventNames.DiagramGenerated, ThreadOption.UserInterface)]
        public void OnDiagramGenerated(object sender, EventArgs<List<Series>> e)
        {
            if (this == sender && _isDiagramInProgress)
            {
                _diagramData = e.Data;

                _isDiagramInProgress = false;

                if (!_isGridInProgress)
                {
                    ProccessReportGenerationCompletion();
                }
            }
        }

        /// <summary>
        /// Обновляет отчет по событию "Обновить".
        /// </summary>
        [EventSubscription(CommonEventNames.RefreshItemFired, ThreadOption.UserInterface)]
        public virtual void OnNeedRefresh(object sender, EventArgs eventArgs)
        {
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            // Проверям, что генерация не запущена
            if (!IsGenerationRunning)
            {
                View.UpdateReport();
            }
            else
            {
                MessageBox.Show("Дождитесь окончания формирования отчета");
            }
        }

        #endregion

        #region Private members

        /// <summary>
        /// Выполняет действия по завершению вычисления данных, необходимых для отчета
        /// </summary>
        private void ProccessReportGenerationCompletion()
        {
            SetReportTitle(_params);

            ProcessGridData();

            ProcessDiagramData();

            HideMarqueeProgressBarFired(this, EventArgs.Empty);

            OnReportGenerationCompleted();
        }

        #endregion
    }
}