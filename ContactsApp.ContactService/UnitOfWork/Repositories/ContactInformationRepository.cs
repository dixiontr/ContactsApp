using ContactsApp.ContactService.Entities;
using ContactsApp.Core.Interfaces.Repository;
using ContactsApp.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.ContactService.UnitOfWork.Repositories
{

    public class ContactInformationRepository : EfCoreGenericRepository<ContactInformation>, IRepository<ContactInformation>
    {
        public ContactInformationRepository(DbContext context) : base(context)
        {
        }
    }

}