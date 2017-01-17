using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerWebSite.Models.PaymentsAndChargesModels
{
    /// <summary>
    /// Итого за период
    /// </summary>
    public class PeriodTotal
    {
        [DisplayFormat(DataFormatString = "{0:MMMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Period { get; set; }
        
        public IEnumerable<CommonOperation> Opers { get; set; }

        [Display(Name = "Переплата(-)/Недоплата(+)")]
        [DataType(DataType.Currency)]
        public decimal Overpayment { get; set; }

        private decimal _total;
        [Display(Name = "Итого")]
        [DataType(DataType.Currency)]
        public decimal Total
        {
            get
            {
                return _total + Overpayment;
            }
            set
            {
                _total = value;
            }
        }
    }
}