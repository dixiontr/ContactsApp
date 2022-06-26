using ContactsApp.Core.Interfaces.Repository;
using ContactsApp.Core.Interfaces.UnitOfWork;
using ContactsApp.ReportService.Entities;

namespace ContactsApp.ReportService.UnitOfWork
{

    public interface IReportUnitOfWork : IUnitOfWork
    {
        IRepository<Report> ReportRepository { get; }

    }

}