namespace Domain.IEntity;

public interface IIsDeletedEntity : IBaseEntity
{
    bool IsDeleted { get; set; }
}