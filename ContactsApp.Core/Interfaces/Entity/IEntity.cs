namespace ContactsApp.Core.Interfaces.Entity
{

    public interface IEntity
    {
        Guid Id { get; set; }
        DateTimeOffset CreatedOn { get; set; }
        DateTimeOffset UpdatedOn { get; set; }
    }

}