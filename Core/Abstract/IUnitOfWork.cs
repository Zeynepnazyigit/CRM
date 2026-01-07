using Core.Abstracts.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ICustomerRepository CustomerRepository { get; }
        IActivityRepository ActivityRepository { get; }
        IContactRepository ContactRepository { get; }
        ILeadRepository LeadRepository { get; }
        IOpportunityRepository OpportunityRepository { get; }

        Task CommitAsync();
    }
}
