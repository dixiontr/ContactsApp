using ContactsApp.ContactService.Context;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.ContactService.Services
{

    public static class ServiceInjector
    {
        public static IServiceCollection AddPostgreSQL(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddEntityFrameworkNpgsql().AddDbContext<ContactContext>(opt =>
            opt.UseNpgsql(connectionString));

            return serviceCollection;
        }
        
    }

}