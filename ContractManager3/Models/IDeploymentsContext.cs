using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ContractManager3.Models
{
    public interface IDeploymentsContext:IDisposable
    {
        DbSet<Supplier> Supplier { get; }
        DbSet<Property> Property { get; }
        DbSet<ContractDetail> ContractDetails { get; }
        DbSet<ContractHour> ContractHours { get; }
        int SaveChanges();
        void MarkAsModified(Object item);
              
    }
}
