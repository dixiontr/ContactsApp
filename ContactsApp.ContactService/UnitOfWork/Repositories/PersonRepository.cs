using ContactsApp.ContactService.Entities;
using ContactsApp.Core.Interfaces.Repository;
using ContactsApp.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.ContactService.UnitOfWork.Repositories
{

    public class PersonRepository : EfCoreGenericRepository<Person>, IRepository<Person>
    {
        public PersonRepository(DbContext context) : base(context)
        {
        }
    }

}