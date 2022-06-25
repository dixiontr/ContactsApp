using ContactsApp.ContactService.Entities;
using ContactsApp.Core.Interfaces.Repository;
using ContactsApp.Core.Interfaces.UnitOfWork;

namespace ContactsApp.ContactService.UnitOfWork
{

    public interface IContactUnitOfWork : IUnitOfWork
    {
        IRepository<Person> PersonRepository { get; }
        IRepository<ContactInformation> ContactInformationRepository { get; }
    }

}