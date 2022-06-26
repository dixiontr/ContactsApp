using ContactsApp.Core.Interfaces.Repository;
using ContactsApp.ReportService.Entities;
using MongoDB.Driver;

namespace ContactsApp.ReportService.UnitOfWork.Repositories
{

    public class ReportRepository : MongoGenericRepository<Report>, IRepository<Report>
    {
        private static string _collectionName = "reports";
        public ReportRepository(IMongoDatabase database) : base(database, _collectionName)
        {
        }
    }

}