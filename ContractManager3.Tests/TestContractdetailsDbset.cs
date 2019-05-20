using ContractManager3.Models;
using ContractManager3.Tests.TestDbSet;
using System.Linq;

namespace ContractManager3.Tests
{
    class TestContractdetailDbset: TestDbSet<ContractDetail>
    {
        public ContractDetail Find(string[] keyValues)
        {
           return this.SingleOrDefault(c => c.PriceDescription == (string)keyValues.FirstOrDefault());
        }

        public ContractDetail Find(int[] keyValues)
        {
            return this.SingleOrDefault(d => d.Contract_ID == (int)keyValues.Single());
        }

    }

    class TestContractHoursDbset : TestDbSet<ContractHour>
    {
        public ContractHour Find(string[] keyValues)
        {
            return this.SingleOrDefault(c => c.Boxingday == (string)keyValues.FirstOrDefault());
        }

        public ContractHour Find(int[] keyValues)
        {
            return this.SingleOrDefault(d => d.Transaction_ID == (int)keyValues.Single());
        }

    }

    class TestPropertyDbset : TestDbSet<Property>
    {
        public Property Find(string[] keyValues)
        {
            return this.SingleOrDefault(c => c.Prop_County == (string)keyValues.FirstOrDefault());
        }

        public Property Find(int[] keyValues)
        {
            return this.SingleOrDefault(d => d.Property_ID == (int)keyValues.Single());
        }

    }

    class TestSupplierDbset : TestDbSet<Supplier>
    {
        public Supplier Find(string[] keyValues)
        {
            return this.SingleOrDefault(c => c.SupplierName == (string)keyValues.FirstOrDefault());
        }

        public Supplier Find(int[] keyValues)
        {
            return this.SingleOrDefault(d => d.Supplier_ID == (int)keyValues.Single());
        }

    }

}
