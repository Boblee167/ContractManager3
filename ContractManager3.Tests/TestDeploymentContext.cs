using ContractManager3.Models;
using System;
using System.Data.Entity;

namespace ContractManager3.Tests
{
    class TestDeploymentContext : IDeploymentsContext
    {
        public TestDeploymentContext()
        {
            this.ContractDetail = new TestContractdetailDbset();
            this.ContractHours = new TestContractHoursDbset();
            this.Property = new TestPropertyDbset();
            this.Supplier = new TestSupplierDbset();
        }

        public DbSet<ContractDetail> ContractDetail { get; set; }
        public DbSet<ContractHour> ContractHours { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
              
        public int SaveChanges()
        {
            return 0;
        }



        public void MarkAsModified(Object item) { }
        public void Dispose() { }

        int IDeploymentsContext.SaveChanges()
        {
            throw new NotImplementedException();
        }

        void IDeploymentsContext.MarkAsModified(object item)
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }

}
