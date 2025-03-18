using BookManager.Domain.Interface.Entities;

namespace BookManager.Domain.Entity;

public class BaseEntity: IBaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }

    public BaseEntity()
    {
        Id = Guid.NewGuid();
        CreateDate = DateTime.Now;
    }
}
