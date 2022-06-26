using ContactsApp.Core.Interfaces.Repository;
using ContactsApp.ReportService.Entities;
using ContactsApp.ReportService.UnitOfWork.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using MongoDB.Driver;

namespace ContactsApp.ReportService.UnitOfWork
{

    public class UnitOfWork : IReportUnitOfWork
    {
        private readonly IMongoDatabase _database;
        private ReportRepository _reportRepository;

        public UnitOfWork(IMongoDatabase database)
        {
            _database = database;
        }

        public IRepository<Report> ReportRepository => _reportRepository ?? new ReportRepository(_database);
    }

}