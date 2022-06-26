using ContactsApp.ContactService.Entities;
using ContactsApp.ContactService.UnitOfWork.Repositories;
using ContactsApp.Core.Entities;
using ContactsApp.Core.Interfaces.Repository;
using ContactsApp.Core.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;

namespace ContactsApp.ContactService.UnitOfWork
{

    public interface IContactUnitOfWork : IUnitOfWork
    {
        IEfCoreRepository<Person> PersonRepository { get; }
        IEfCoreRepository<ContactInformation> ContactInformationRepository { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task RollbackAsync();
        Task CommitAsync();
        Task SaveChangesAsync();
    }

}