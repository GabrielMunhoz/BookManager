using BookManager.Domain.Interface.Entities;

namespace BookManager.Domain.Entity;

public class Base: IBaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }

    public Base()
    {
        Id = Guid.NewGuid();
        CreateDate = DateTime.Now;
    }
}
