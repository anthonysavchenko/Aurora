
using DevExpress.XtraGrid.Localization;
using Taumis.Alpha.Infrastructure.Interface.Properties;


namespace Taumis.Infrastructure.Interface.Services.Localizers
{
    /// <summary>
    /// Класс - русификатор грида.
    /// </summary>
    public class RussianGridLocalizer : GridLocalizer
    {
        public override string Language { get { return "Russian"; } }

        public override string GetLocalizedString(GridStringId id)
        {
            string ret = "";
            switch (id)
            {
                case GridStringId.CardViewNewCard: ret = Resources.GridStringId_CardViewNewCard; break;
                case GridStringId.CardViewQuickCustomizationButton: ret = Resources.GridStringId_CardViewQuickCustomizationButton; break;
                case GridStringId.CardViewQuickCustomizationButtonFilter: ret = Resources.GridStringId_CardViewQuickCustomizationButtonFilter; break;
                case GridStringId.CardViewQuickCustomizationButtonSort: ret = Resources.GridStringId_CardViewQuickCustomizationButtonSort; break;
                case GridStringId.ColumnViewExceptionMessage: ret = Resources.GridStringId_ColumnViewExceptionMessage; break;
                case GridStringId.CustomFilterDialog2FieldCheck: ret = Resources.GridStringId_CustomFilterDialog2FieldCheck; break;
                case GridStringId.CustomFilterDialogCancelButton: ret = Resources.GridStringId_CustomFilterDialogCancelButton; break;
                case GridStringId.CustomFilterDialogCaption: ret = Resources.GridStringId_CustomFilterDialogCaption; break;
                case GridStringId.CustomFilterDialogClearFilter: ret = Resources.GridStringId_CustomFilterDialogClearFilter; break;
                case GridStringId.CustomFilterDialogFormCaption: ret = Resources.GridStringId_CustomFilterDialogFormCaption; break;
                case GridStringId.CustomFilterDialogOkButton: ret = Resources.GridStringId_CustomFilterDialogOkButton; break;
                case GridStringId.CustomFilterDialogRadioAnd: ret = Resources.GridStringId_CustomFilterDialogRadioAnd; break;
                case GridStringId.CustomFilterDialogRadioOr: ret = Resources.GridStringId_CustomFilterDialogRadioOr; break;
                case GridStringId.CustomizationBands: ret = Resources.GridStringId_CustomizationBands; break;
                case GridStringId.CustomizationCaption: ret = Resources.GridStringId_CustomizationCaption; break;
                case GridStringId.CustomizationColumns: ret = Resources.GridStringId_CustomizationColumns; break;
                case GridStringId.CustomizationFormBandHint: ret = Resources.GridStringId_CustomizationFormBandHint; break;
                case GridStringId.CustomizationFormColumnHint: ret = Resources.GridStringId_CustomizationFormColumnHint; break;
                case GridStringId.FileIsNotFoundError: ret = Resources.GridStringId_FileIsNotFoundError; break;
                case GridStringId.FilterBuilderApplyButton: ret = Resources.GridStringId_FilterBuilderApplyButton; break;
                case GridStringId.FilterBuilderCancelButton: ret = Resources.GridStringId_FilterBuilderCancelButton; break;
                case GridStringId.FilterBuilderCaption: ret = Resources.GridStringId_FilterBuilderCaption; break;
                case GridStringId.FilterBuilderOkButton: ret = Resources.GridStringId_FilterBuilderOkButton; break;
                case GridStringId.FilterPanelCustomizeButton: ret = Resources.GridStringId_FilterPanelCustomizeButton; break;
                case GridStringId.GridGroupPanelText: ret = Resources.GridStringId_GridGroupPanelText; break;
                case GridStringId.GridNewRowText: ret = Resources.GridStringId_GridNewRowText; break;
                case GridStringId.LayoutModifiedWarning: ret = Resources.GridStringId_LayoutModifiedWarning; break;
                case GridStringId.LayoutViewButtonApply: ret = Resources.GridStringId_LayoutViewButtonApply; break;
                case GridStringId.LayoutViewButtonCancel: ret = Resources.GridStringId_LayoutViewButtonCancel; break;
                case GridStringId.LayoutViewButtonCustomizeHide: ret = Resources.GridStringId_LayoutViewButtonCustomizeHide; break;
                case GridStringId.LayoutViewButtonCustomizeShow: ret = Resources.GridStringId_LayoutViewButtonCustomizeShow; break;
                case GridStringId.LayoutViewButtonLoadLayout: ret = Resources.GridStringId_LayoutViewButtonLoadLayout; break;
                case GridStringId.LayoutViewButtonOk: ret = Resources.GridStringId_LayoutViewButtonOk; break;
                case GridStringId.LayoutViewButtonPreview: ret = Resources.GridStringId_LayoutViewButtonPreview; break;
                case GridStringId.LayoutViewButtonReset: ret = Resources.GridStringId_LayoutViewButtonReset; break;
                case GridStringId.LayoutViewButtonSaveLayout: ret = Resources.GridStringId_LayoutViewButtonSaveLayout; break;
                case GridStringId.LayoutViewButtonShrinkToMinimum: ret = Resources.GridStringId_LayoutViewButtonShrinkToMinimum; break;
                case GridStringId.LayoutViewCloseZoomBtnHintClose: ret = Resources.GridStringId_LayoutViewCloseZoomBtnHintClose; break;
                case GridStringId.LayoutViewCloseZoomBtnHintZoom: ret = Resources.GridStringId_LayoutViewCloseZoomBtnHintZoom; break;
                case GridStringId.LayoutViewColumnModeBtnHint: ret = Resources.GridStringId_LayoutViewColumnModeBtnHint; break;
                case GridStringId.LayoutViewCustomizationFormCaption: ret = Resources.GridStringId_LayoutViewCustomizationFormCaption; break;
                case GridStringId.LayoutViewCustomizeBtnHint: ret = Resources.GridStringId_LayoutViewCustomizeBtnHint; break;
                case GridStringId.LayoutViewGroupCaptions: ret = Resources.GridStringId_LayoutViewGroupCaptions; break;
                case GridStringId.LayoutViewGroupCards: ret = Resources.GridStringId_LayoutViewGroupCards; break;
                case GridStringId.LayoutViewGroupCustomization: ret = Resources.GridStringId_LayoutViewGroupCustomization; break;
                case GridStringId.LayoutViewGroupFields: ret = Resources.GridStringId_LayoutViewGroupFields; break;
                case GridStringId.LayoutViewGroupHiddenItems: ret = Resources.GridStringId_LayoutViewGroupHiddenItems; break;
                case GridStringId.LayoutViewGroupIndents: ret = Resources.GridStringId_LayoutViewGroupIndents; break;
                case GridStringId.LayoutViewGroupIntervals: ret = Resources.GridStringId_LayoutViewGroupIntervals; break;
                case GridStringId.LayoutViewGroupLayout: ret = Resources.GridStringId_LayoutViewGroupLayout; break;
                case GridStringId.LayoutViewGroupPropertyGrid: ret = Resources.GridStringId_LayoutViewGroupPropertyGrid; break;
                case GridStringId.LayoutViewGroupTreeStructure: ret = Resources.GridStringId_LayoutViewGroupTreeStructure; break;
                case GridStringId.LayoutViewGroupView: ret = Resources.GridStringId_LayoutViewGroupView; break;
                case GridStringId.LayoutViewLabelCaptionLocation: ret = Resources.GridStringId_LayoutViewLabelCaptionLocation; break;
                case GridStringId.LayoutViewLabelCardArrangeRule: ret = Resources.GridStringId_LayoutViewLabelCardArrangeRule; break;
                case GridStringId.LayoutViewLabelCardEdgeAlignment: ret = Resources.GridStringId_LayoutViewLabelCardEdgeAlignment; break;
                case GridStringId.LayoutViewLabelGroupCaptionLocation: ret = Resources.GridStringId_LayoutViewLabelGroupCaptionLocation; break;
                case GridStringId.LayoutViewLabelHorizontal: ret = Resources.GridStringId_LayoutViewLabelHorizontal; break;
                case GridStringId.LayoutViewLabelPadding: ret = Resources.GridStringId_LayoutViewLabelPadding; break;
                case GridStringId.LayoutViewLabelScrollVisibility: ret = Resources.GridStringId_LayoutViewLabelScrollVisibility; break;
                case GridStringId.LayoutViewLabelShowCardBorder: ret = Resources.GridStringId_LayoutViewLabelShowCardBorder; break;
                case GridStringId.LayoutViewLabelShowCardCaption: ret = Resources.GridStringId_LayoutViewLabelShowCardCaption; break;
                case GridStringId.LayoutViewLabelShowCardExpandButton: ret = Resources.GridStringId_LayoutViewLabelShowCardExpandButton; break;
                case GridStringId.LayoutViewLabelShowFieldBorder: ret = Resources.GridStringId_LayoutViewLabelShowFieldBorder; break;
                case GridStringId.LayoutViewLabelShowFieldHint: ret = Resources.GridStringId_LayoutViewLabelShowFieldHint; break;
                case GridStringId.LayoutViewLabelShowFilterPanel: ret = Resources.GridStringId_LayoutViewLabelShowFilterPanel; break;
                case GridStringId.LayoutViewLabelShowHeaderPanel: ret = Resources.GridStringId_LayoutViewLabelShowHeaderPanel; break;
                case GridStringId.LayoutViewLabelShowLines: ret = Resources.GridStringId_LayoutViewLabelShowLines; break;
                case GridStringId.LayoutViewLabelSpacing: ret = Resources.GridStringId_LayoutViewLabelSpacing; break;
                case GridStringId.LayoutViewLabelTextAlignment: ret = Resources.GridStringId_LayoutViewLabelTextAlignment; break;
                case GridStringId.LayoutViewLabelTextIndent: ret = Resources.GridStringId_LayoutViewLabelTextIndent; break;
                case GridStringId.LayoutViewLabelVertical: ret = Resources.GridStringId_LayoutViewLabelVertical; break;
                case GridStringId.LayoutViewLabelViewMode: ret = Resources.GridStringId_LayoutViewLabelViewMode; break;
                case GridStringId.LayoutViewPanBtnHint: ret = Resources.GridStringId_LayoutViewPanBtnHint; break;
                case GridStringId.LayoutViewRowModeBtnHint: ret = Resources.GridStringId_LayoutViewRowModeBtnHint; break;
                case GridStringId.MenuColumnBestFit: ret = Resources.GridStringId_MenuColumnBestFit; break;
                case GridStringId.MenuColumnBestFitAllColumns: ret = Resources.GridStringId_MenuColumnBestFitAllColumns; break;
                case GridStringId.MenuColumnClearFilter: ret = Resources.GridStringId_MenuColumnClearFilter; break;
                case GridStringId.MenuColumnClearSorting: ret = Resources.GridStringId_MenuColumnClearSorting; break;
                case GridStringId.MenuColumnColumnCustomization: ret = Resources.GridStringId_MenuColumnColumnCustomization; break;
                case GridStringId.MenuColumnFilter: ret = Resources.GridStringId_MenuColumnFilter; break;
                case GridStringId.MenuColumnFilterEditor: ret = Resources.GridStringId_MenuColumnFilterEditor; break;
                case GridStringId.MenuColumnGroup: ret = Resources.GridStringId_MenuColumnGroup; break;
                case GridStringId.MenuColumnSortAscending: ret = Resources.GridStringId_MenuColumnSortAscending; break;
                case GridStringId.MenuColumnSortDescending: ret = Resources.GridStringId_MenuColumnSortDescending; break;
                case GridStringId.MenuColumnUnGroup: ret = Resources.GridStringId_MenuColumnUnGroup; break;
                case GridStringId.MenuFooterAverage: ret = Resources.GridStringId_MenuFooterAverage; break;
                case GridStringId.MenuFooterAverageFormat: ret = Resources.GridStringId_MenuFooterAverageFormat; break;
                case GridStringId.MenuFooterCount: ret = Resources.GridStringId_MenuFooterCount; break;
                case GridStringId.MenuFooterCountFormat: ret = Resources.GridStringId_MenuFooterCountFormat; break;
                case GridStringId.MenuFooterCountGroupFormat: ret = Resources.GridStringId_MenuFooterCountGroupFormat; break;
                case GridStringId.MenuFooterCustomFormat: ret = Resources.GridStringId_MenuFooterCustomFormat; break;
                case GridStringId.MenuFooterMax: ret = Resources.GridStringId_MenuFooterMax; break;
                case GridStringId.MenuFooterMaxFormat: ret = Resources.GridStringId_MenuFooterMaxFormat; break;
                case GridStringId.MenuFooterMin: ret = Resources.GridStringId_MenuFooterMin; break;
                case GridStringId.MenuFooterMinFormat: ret = Resources.GridStringId_MenuFooterMinFormat; break;
                case GridStringId.MenuFooterNone: ret = Resources.GridStringId_MenuFooterNone; break;
                case GridStringId.MenuFooterSum: ret = Resources.GridStringId_MenuFooterSum; break;
                case GridStringId.MenuFooterSumFormat: ret = Resources.GridStringId_MenuFooterSumFormat; break;
                case GridStringId.MenuGroupPanelClearGrouping: ret = Resources.GridStringId_MenuGroupPanelClearGrouping; break;
                case GridStringId.MenuGroupPanelFullCollapse: ret = Resources.GridStringId_MenuGroupPanelFullCollapse; break;
                case GridStringId.MenuGroupPanelFullExpand: ret = Resources.GridStringId_MenuGroupPanelFullExpand; break;
                case GridStringId.PopupFilterAll: ret = Resources.GridStringId_PopupFilterAll; break;
                case GridStringId.PopupFilterBlanks: ret = Resources.GridStringId_PopupFilterBlanks; break;
                case GridStringId.PopupFilterCustom: ret = Resources.GridStringId_PopupFilterCustom; break;
                case GridStringId.PopupFilterNonBlanks: ret = Resources.GridStringId_PopupFilterNonBlanks; break;
                case GridStringId.PrintDesignerBandedView: ret = Resources.GridStringId_PrintDesignerBandedView; break;
                case GridStringId.PrintDesignerBandHeader: ret = Resources.GridStringId_PrintDesignerBandHeader; break;
                case GridStringId.PrintDesignerCardView: ret = Resources.GridStringId_PrintDesignerCardView; break;
                case GridStringId.PrintDesignerDescription: ret = Resources.GridStringId_PrintDesignerDescription; break;
                case GridStringId.PrintDesignerGridView: ret = Resources.GridStringId_PrintDesignerGridView; break;
                case GridStringId.MenuColumnGroupBox: ret = Resources.GridStringId_MenuColumnGroupBox; break;
              // ...
                default:
                    ret = base.GetLocalizedString(id);
                    break;
            }
            return ret;
        }
    }
}
