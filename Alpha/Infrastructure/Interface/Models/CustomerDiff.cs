namespace Taumis.Alpha.Infrastructure.Interface.Models
{
    public class CustomerDiff : Diff
    {
        public Customer PrintFormCustomer;
        public Customer FillFormCustomer;
        public CustomerDiffType diffType;
    }
}
