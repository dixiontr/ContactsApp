using Microsoft.EntityFrameworkCore.Storage;

namespace ContactsApp.Core.Interfaces.UnitOfWork
{

    public interface IUnitOfWork
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task RollbackAsync();
        Task CommitAsync();
        Task SaveChangesAsync();

    }

}