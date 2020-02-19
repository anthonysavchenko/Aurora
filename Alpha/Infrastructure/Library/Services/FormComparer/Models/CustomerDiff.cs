using Taumis.Alpha.Infrastructure.Library.Services.FormParser.Models;

namespace Taumis.Alpha.Infrastructure.Library.Services.FormComparer.Models
{
    public class CustomerDiff : Diff
    {
        public Customer PrintFormCustomer;
        public Customer FillFormCustomer;
        public CustomerDiffType CustomerDiffType;
    }
}
