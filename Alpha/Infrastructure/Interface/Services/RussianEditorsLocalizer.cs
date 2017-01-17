
using DevExpress.XtraEditors.Controls;

using Taumis.Alpha.Infrastructure.Interface.Properties;

namespace Taumis.Infrastructure.Interface.Services.Localizers
{
    public class RussianEditorsLocalizer : Localizer
    {
        public override string Language { get { return "Russian"; } }
        public override string GetLocalizedString(StringId id)
        {
            string ret = "";
            switch (id)
            {
                case StringId.Apply: ret = Resources.StringId_Apply; break;
                case StringId.CalcButtonBack: ret = Resources.StringId_CalcButtonBack; break;
                case StringId.Cancel: ret = Resources.StringId_Cancel; break;
                case StringId.CaptionError: ret = Resources.StringId_CaptionError; break;
                case StringId.CheckChecked: ret = Resources.StringId_CheckChecked; break;
                case StringId.CheckIndeterminate: ret = Resources.StringId_CheckIndeterminate; break;
                case StringId.CheckUnchecked: ret = Resources.StringId_CheckUnchecked; break;
                case StringId.DateEditClear: ret = Resources.StringId_DateEditClear; break;
                case StringId.DateEditToday: ret = Resources.StringId_DateEditToday; break;
                case StringId.FilterClauseAnyOf: ret = Resources.StringId_FilterClauseAnyOf; break;
                case StringId.FilterClauseBeginsWith: ret = Resources.StringId_FilterClauseBeginsWith; break;
                case StringId.FilterClauseBetween: ret = Resources.StringId_FilterClauseBetween; break;
                case StringId.FilterClauseBetweenAnd: ret = Resources.StringId_FilterClauseBetweenAnd; break;
                case StringId.FilterClauseContains: ret = Resources.StringId_FilterClauseContains; break;
                case StringId.FilterClauseDoesNotContain: ret = Resources.StringId_FilterClauseDoesNotContain; break;
                case StringId.FilterClauseDoesNotEqual: ret = Resources.StringId_FilterClauseDoesNotEqual; break;
                case StringId.FilterClauseEndsWith: ret = Resources.StringId_FilterClauseEndsWith; break;
                case StringId.FilterClauseEquals: ret = Resources.StringId_FilterClauseEquals; break;
                case StringId.FilterClauseGreater: ret = Resources.StringId_FilterClauseGreater; break;
                case StringId.FilterClauseGreaterOrEqual: ret = Resources.StringId_FilterClauseGreaterOrEqual; break;
                case StringId.FilterClauseIsNotNull: ret = Resources.StringId_FilterClauseIsNotNull; break;
                case StringId.FilterClauseIsNull: ret = Resources.StringId_FilterClauseIsNull; break;
                case StringId.FilterClauseLess: ret = Resources.StringId_FilterClauseLess; break;
                case StringId.FilterClauseLessOrEqual: ret = Resources.StringId_FilterClauseLessOrEqual; break;
                case StringId.FilterClauseLike: ret = Resources.StringId_FilterClauseLike; break;
                case StringId.FilterClauseNoneOf: ret = Resources.StringId_FilterClauseNoneOf; break;
                case StringId.FilterClauseNotBetween: ret = Resources.StringId_FilterClauseNotBetween; break;
                case StringId.FilterClauseNotLike: ret = Resources.StringId_FilterClauseNotLike; break;
                case StringId.FilterCriteriaToStringBetween: ret = Resources.StringId_FilterCriteriaToStringBetween; break;
                case StringId.FilterCriteriaToStringBinaryOperatorBitwiseAnd: ret = Resources.StringId_FilterCriteriaToStringBinaryOperatorBitwiseAnd; break;
                case StringId.FilterCriteriaToStringBinaryOperatorBitwiseOr: ret = Resources.StringId_FilterCriteriaToStringBinaryOperatorBitwiseOr; break;
                case StringId.FilterCriteriaToStringBinaryOperatorBitwiseXor: ret = Resources.StringId_FilterCriteriaToStringBinaryOperatorBitwiseXor; break;
                case StringId.FilterCriteriaToStringBinaryOperatorDivide: ret = Resources.StringId_FilterCriteriaToStringBinaryOperatorDivide; break;
                case StringId.FilterCriteriaToStringBinaryOperatorEqual: ret = Resources.StringId_FilterCriteriaToStringBinaryOperatorEqual; break;
                case StringId.FilterCriteriaToStringBinaryOperatorGreater: ret = Resources.StringId_FilterCriteriaToStringBinaryOperatorGreater; break;
                case StringId.FilterCriteriaToStringBinaryOperatorGreaterOrEqual: ret = Resources.StringId_FilterCriteriaToStringBinaryOperatorGreaterOrEqual; break;
                case StringId.FilterCriteriaToStringBinaryOperatorLess: ret = Resources.StringId_FilterCriteriaToStringBinaryOperatorLess; break;
                case StringId.FilterCriteriaToStringBinaryOperatorLessOrEqual: ret = Resources.StringId_FilterCriteriaToStringBinaryOperatorLessOrEqual; break;
                case StringId.FilterCriteriaToStringBinaryOperatorMinus: ret = Resources.StringId_FilterCriteriaToStringBinaryOperatorMinus; break;
                case StringId.FilterCriteriaToStringBinaryOperatorModulo: ret = Resources.StringId_FilterCriteriaToStringBinaryOperatorModulo; break;
                case StringId.FilterCriteriaToStringBinaryOperatorMultiply: ret = Resources.StringId_FilterCriteriaToStringBinaryOperatorMultiply; break;
                case StringId.FilterCriteriaToStringBinaryOperatorNotEqual: ret = Resources.StringId_FilterCriteriaToStringBinaryOperatorNotEqual; break;
                case StringId.FilterCriteriaToStringBinaryOperatorPlus: ret = Resources.StringId_FilterCriteriaToStringBinaryOperatorPlus; break;
                case StringId.FilterCriteriaToStringGroupOperatorAnd: ret = Resources.StringId_FilterCriteriaToStringGroupOperatorAnd; break;
                case StringId.FilterCriteriaToStringGroupOperatorOr: ret = Resources.StringId_FilterCriteriaToStringGroupOperatorOr; break;
                case StringId.FilterCriteriaToStringIn: ret = Resources.StringId_FilterCriteriaToStringIn; break;
                case StringId.FilterCriteriaToStringIsNotNull: ret = Resources.StringId_FilterCriteriaToStringIsNotNull; break;
                case StringId.FilterCriteriaToStringUnaryOperatorBitwiseNot: ret = Resources.StringId_FilterCriteriaToStringUnaryOperatorBitwiseNot; break;
                case StringId.FilterCriteriaToStringUnaryOperatorIsNull: ret = Resources.StringId_FilterCriteriaToStringUnaryOperatorIsNull; break;
                case StringId.FilterCriteriaToStringUnaryOperatorMinus: ret = Resources.StringId_FilterCriteriaToStringUnaryOperatorMinus; break;
                case StringId.FilterCriteriaToStringUnaryOperatorNot: ret = Resources.StringId_FilterCriteriaToStringUnaryOperatorNot; break;
                case StringId.FilterCriteriaToStringUnaryOperatorPlus: ret = Resources.StringId_FilterCriteriaToStringUnaryOperatorPlus; break;
                case StringId.FilterEmptyEnter: ret = Resources.StringId_FilterEmptyEnter; break;
                case StringId.FilterEmptyValue: ret = Resources.StringId_FilterEmptyValue; break;
                case StringId.FilterGroupAnd: ret = Resources.StringId_FilterGroupAnd; break;
                case StringId.FilterGroupNotAnd: ret = Resources.StringId_FilterGroupNotAnd; break;
                case StringId.FilterGroupNotOr: ret = Resources.StringId_FilterGroupNotOr; break;
                case StringId.FilterGroupOr: ret = Resources.StringId_FilterGroupOr; break;
                case StringId.FilterMenuClearAll: ret = Resources.StringId_FilterMenuClearAll; break;
                case StringId.FilterMenuConditionAdd: ret = Resources.StringId_FilterMenuConditionAdd; break;
                case StringId.FilterMenuGroupAdd: ret = Resources.StringId_FilterMenuGroupAdd; break;
                case StringId.FilterMenuRowRemove: ret = Resources.StringId_FilterMenuRowRemove; break;
                case StringId.FilterShowAll: ret = Resources.StringId_FilterShowAll; break;
                case StringId.FilterToolTipElementAdd: ret = Resources.StringId_FilterToolTipElementAdd; break;
                case StringId.FilterToolTipKeysAdd: ret = Resources.StringId_FilterToolTipKeysAdd; break;
                case StringId.FilterToolTipKeysRemove: ret = Resources.StringId_FilterToolTipKeysRemove; break;
                case StringId.FilterToolTipNodeAdd: ret = Resources.StringId_FilterToolTipNodeAdd; break;
                case StringId.FilterToolTipNodeRemove: ret = Resources.StringId_FilterToolTipNodeRemove; break;
                case StringId.ImagePopupEmpty: ret = Resources.StringId_ImagePopupEmpty; break;
                case StringId.ImagePopupPicture: ret = Resources.StringId_ImagePopupPicture; break;
                case StringId.InvalidValueText: ret = Resources.StringId_InvalidValueText; break;
                case StringId.LookUpColumnDefaultName: ret = Resources.StringId_LookUpColumnDefaultName; break;
                case StringId.LookUpEditValueIsNull: ret = Resources.StringId_LookUpEditValueIsNull; break;
                case StringId.MaskBoxValidateError: ret = Resources.StringId_MaskBoxValidateError; break;
                case StringId.NotValidArrayLength: ret = Resources.StringId_NotValidArrayLength; break;
                case StringId.OK: ret = Resources.StringId_OK; break;
                case StringId.PictureEditCopyImageError: ret = Resources.StringId_PictureEditCopyImageError; break;
                case StringId.PictureEditMenuCopy: ret = Resources.StringId_PictureEditMenuCopy; break;
                case StringId.PictureEditMenuCut: ret = Resources.StringId_PictureEditMenuCut; break;
                case StringId.PictureEditMenuDelete: ret = Resources.StringId_PictureEditMenuDelete; break;
                case StringId.PictureEditMenuLoad: ret = Resources.StringId_PictureEditMenuLoad; break;
                case StringId.PictureEditMenuPaste: ret = Resources.StringId_PictureEditMenuPaste; break;
                case StringId.PictureEditMenuSave: ret = Resources.StringId_PictureEditMenuSave; break;
                case StringId.PictureEditOpenFileError: ret = Resources.StringId_PictureEditOpenFileError; break;
                case StringId.PictureEditOpenFileTitle: ret = Resources.StringId_PictureEditOpenFileTitle; break;
                case StringId.PictureEditSaveFileTitle: ret = Resources.StringId_PictureEditSaveFileTitle; break;
                case StringId.PreviewPanelText: ret = Resources.StringId_PreviewPanelText; break;
                case StringId.TabHeaderButtonClose: ret = Resources.StringId_TabHeaderButtonClose; break;
                case StringId.TabHeaderButtonNext: ret = Resources.StringId_TabHeaderButtonNext; break;
                case StringId.TabHeaderButtonPrev: ret = Resources.StringId_TabHeaderButtonPrev; break;
                case StringId.TextEditMenuCopy: ret = Resources.StringId_TextEditMenuCopy; break;
                case StringId.TextEditMenuCut: ret = Resources.StringId_TextEditMenuCut; break;
                case StringId.TextEditMenuDelete: ret = Resources.StringId_TextEditMenuDelete; break;
                case StringId.TextEditMenuPaste: ret = Resources.StringId_TextEditMenuPaste; break;
                case StringId.TextEditMenuSelectAll: ret = Resources.StringId_TextEditMenuSelectAll; break;
                case StringId.TextEditMenuUndo: ret = Resources.StringId_TextEditMenuUndo; break;
                case StringId.UnknownPictureFormat: ret = Resources.StringId_UnknownPictureFormat; break;
                case StringId.XtraMessageBoxAbortButtonText: ret = Resources.StringId_XtraMessageBoxAbortButtonText; break;
                case StringId.XtraMessageBoxCancelButtonText: ret = Resources.StringId_XtraMessageBoxCancelButtonText; break;
                case StringId.XtraMessageBoxIgnoreButtonText: ret = Resources.StringId_XtraMessageBoxIgnoreButtonText; break;
                case StringId.XtraMessageBoxNoButtonText: ret = Resources.StringId_XtraMessageBoxNoButtonText; break;
                case StringId.XtraMessageBoxOkButtonText: ret = Resources.StringId_XtraMessageBoxOkButtonText; break;
                case StringId.XtraMessageBoxRetryButtonText: ret = Resources.StringId_XtraMessageBoxRetryButtonText; break;
                case StringId.XtraMessageBoxYesButtonText: ret = Resources.StringId_XtraMessageBoxYesButtonText; break;
                // Хинты встроенного навигатора.
                case StringId.NavigatorAppendButtonHint: ret = Resources.StringId_NavigatorAppendButtonHint; break;
                case StringId.NavigatorCancelEditButtonHint: ret = Resources.StringId_NavigatorCancelEditButtonHint; break;
                case StringId.NavigatorEditButtonHint: ret = Resources.StringId_NavigatorEditButtonHint; break;
                case StringId.NavigatorEndEditButtonHint: ret = Resources.StringId_NavigatorEndEditButtonHint; break;
                case StringId.NavigatorFirstButtonHint: ret = Resources.StringId_NavigatorFirstButtonHint; break;
                case StringId.NavigatorLastButtonHint: ret = Resources.StringId_NavigatorLastButtonHint; break;
                case StringId.NavigatorNextButtonHint: ret = Resources.StringId_NavigatorNextButtonHint; break;
                case StringId.NavigatorNextPageButtonHint: ret = Resources.StringId_NavigatorNextPageButtonHint; break;
                case StringId.NavigatorPreviousButtonHint: ret = Resources.StringId_NavigatorPreviousButtonHint; break;
                case StringId.NavigatorPreviousPageButtonHint: ret = Resources.StringId_NavigatorPreviousPageButtonHint; break;
                case StringId.NavigatorRemoveButtonHint: ret = Resources.StringId_NavigatorRemoveButtonHint; break;
                case StringId.NavigatorTextStringFormat: ret = Resources.StringId_NavigatorTextStringFormat; break;

                default:
                    ret = base.GetLocalizedString(id);
                    break;
            }
            return ret;
        }
    }

}
