using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerWebSite.Models.PaymentsAndChargesModels
{
    public class CommonOperation
    {
        public enum OperationType
        {
            Charge,
            Payment
        }

        public int ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreationDateTime { get; set; }
        
        public OperationType Type { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Period { get; set; }

        [Display(Name = "Сумма")]
        [DataType(DataType.Currency)]
        public decimal Value { get; set; }
    }
}