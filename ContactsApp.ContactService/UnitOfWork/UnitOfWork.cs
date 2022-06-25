using ContactsApp.ContactService.Context;
using ContactsApp.ContactService.Entities;
using ContactsApp.ContactService.UnitOfWork.Repositories;
using ContactsApp.Core.Interfaces.Repository;
using ContactsApp.Core.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ContactsApp.ContactService.UnitOfWork
{

    public class UnitOfWork : IContactUnitOfWork
    {
        private ContactContext _context;
        private ContactInformationRepository _contactInformationRepository;
        private PersonRepository _personRepository;

        public UnitOfWork(ContactContext context)
        {
            _context = context;
        }
        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return _context.Database.BeginTransactionAsync();
        }

        public Task RollbackAsync()
        {
            return _context.Database.RollbackTransactionAsync();
        }

        public Task CommitAsync()
        {
            return _context.Database.CommitTransactionAsync();
        }

        public IRepository<Person> PersonRepository => _personRepository ?? new PersonRepository(_context);
        public IRepository<ContactInformation> ContactInformationRepository =>
            _contactInformationRepository ?? new ContactInformationRepository(_context);
    }

}