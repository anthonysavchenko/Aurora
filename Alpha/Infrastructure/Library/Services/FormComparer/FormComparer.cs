using System.Collections.Generic;
using Taumis.Alpha.Infrastructure.Interface.Models;

namespace Taumis.Alpha.Infrastructure.Library.Services.FormComparer
{
    static public class FormComparer
    {
        static public List<Diff> Compare(List<Building> printForms, List<Building> fillForms)
        {
            var diffs = new List<Diff>();

            if ((printForms == null || printForms.Count < 1) && (fillForms == null || fillForms.Count < 1))
            {
                diffs.Add(NewBuildingDiff(null, null, BuildingDiffType.NoForms));
            }
            else if (printForms == null || printForms.Count < 1)
            {
                diffs.Add(NewBuildingDiff(null, null, BuildingDiffType.NoPrintForms));
            }
            else if (fillForms == null || fillForms.Count < 1)
            {
                diffs.Add(NewBuildingDiff(null, null, BuildingDiffType.NoFillForms));
            }
            else
            {
                printForms
                    .ForEach(printForm =>
                        Compare(
                            printForm,
                            fillForms
                                .Find(fillForm =>
                                    printForm.Number.ToUpper() == fillForm.Number.ToUpper()),
                            diffs));

                fillForms
                    .FindAll(fillForm =>
                        !printForms.Exists(printForm =>
                            printForm.Number.ToUpper() == fillForm.Number.ToUpper()))
                    .ForEach(notInPrintForms =>
                        diffs.Add(NewBuildingDiff(null, notInPrintForms, BuildingDiffType.NoPrintForm)));
            }

            return diffs;
        }

        static private void Compare(Building printForm, Building fillForm, List<Diff> diffs)
        {
            if (printForm == null)
            {
                diffs.Add(NewBuildingDiff(null, fillForm, BuildingDiffType.NoPrintForm));
            }
            else if (fillForm == null)
            {
                diffs.Add(NewBuildingDiff(printForm, null, BuildingDiffType.NoFillForm));
            }
            else
            {
                printForm.Customers
                    .ForEach(pfCustomer =>
                        Compare(
                            pfCustomer,
                            fillForm.Customers
                                .Find(ffCustomer =>
                                    pfCustomer.Address.Building.ToUpper() ==
                                        ffCustomer.Address.Building.ToUpper()
                                    && pfCustomer.Address.Apartment.ToUpper() ==
                                        ffCustomer.Address.Apartment.ToUpper()),
                            diffs));

                fillForm.Customers
                    .FindAll(ffCustomer =>
                        !printForm.Customers.Exists(pfCustomer =>
                            pfCustomer.Address.Building.ToUpper() == ffCustomer.Address.Building.ToUpper()
                            && pfCustomer.Address.Apartment.ToUpper() == ffCustomer.Address.Apartment.ToUpper()))
                    .ForEach(notInPrintForm =>
                        diffs.Add(NewCustomerDiff(null, notInPrintForm, CustomerDiffType.NoPrintFormCustomer)));
            }
        }

        static private void Compare(Customer pfCustomer, Customer ffCustomer, List<Diff> diffs)
        {
            if (pfCustomer == null || ffCustomer == null)
            {
                if (pfCustomer == null)
                {
                    diffs.Add(NewCustomerDiff(null, ffCustomer, CustomerDiffType.NoPrintFormCustomer));
                }
                else if (!pfCustomer.IsNorm)
                {
                    diffs.Add(NewCustomerDiff(pfCustomer, null, CustomerDiffType.NoFillFormCustomer));
                }
            }
            else if (pfCustomer.IsNorm)
            {
                diffs.Add(NewCustomerDiff(pfCustomer, ffCustomer, CustomerDiffType.NormAndCounter));
            }
            else
            {
                if (pfCustomer.PrevDate != ffCustomer.PrevDate)
                {
                    diffs.Add(NewCustomerDiff(pfCustomer, ffCustomer, CustomerDiffType.PrevDateTime));
                }

                if (pfCustomer.Counter is SingleCounter && ffCustomer.Counter is DoubleCounter)
                {
                    diffs.Add(NewCustomerDiff(pfCustomer, ffCustomer, CustomerDiffType.SingleAndDoubleCounter));
                }
                else if (pfCustomer.Counter is DoubleCounter && ffCustomer.Counter is SingleCounter)
                {
                    diffs.Add(NewCustomerDiff(pfCustomer, ffCustomer, CustomerDiffType.DoubleAndSingleCounter));
                }
                else if (pfCustomer.Counter is SingleCounter
                    && ffCustomer.Counter is SingleCounter
                    && (pfCustomer.Counter as SingleCounter).prevValue !=
                        (ffCustomer.Counter as SingleCounter).prevValue)
                {
                    diffs.Add(NewCustomerDiff(pfCustomer, ffCustomer, CustomerDiffType.SingleCounterValue));
                }
                else if (pfCustomer.Counter is DoubleCounter && ffCustomer.Counter is DoubleCounter)
                {
                    if ((pfCustomer.Counter as DoubleCounter).PrevDayValue !=
                        (ffCustomer.Counter as DoubleCounter).PrevDayValue)
                    {
                        diffs.Add(NewCustomerDiff(pfCustomer, ffCustomer, CustomerDiffType.DoubleCounterDayValue));
                    }

                    if ((pfCustomer.Counter as DoubleCounter).PrevNightValue !=
                        (ffCustomer.Counter as DoubleCounter).PrevNightValue)
                    {
                        diffs.Add(NewCustomerDiff(pfCustomer, ffCustomer, CustomerDiffType.DoubleCounterNightValue));
                    }
                }
            }
        }

        static private CustomerDiff NewCustomerDiff(Customer pfCustomer, Customer ffCustomer, CustomerDiffType dType)
        {
            return new CustomerDiff()
            {
                PrintFormCustomer = pfCustomer,
                FillFormCustomer = ffCustomer,
                diffType = dType,
            };
        }

        static private BuildingDiff NewBuildingDiff(Building pfBuilding, Building ffBuilding, BuildingDiffType dType)
        {
            return new BuildingDiff()
            {
                PrintFormBuilding = pfBuilding,
                FillFormBuilding = ffBuilding,
                diffType = dType,
            };
        }
    }
}
