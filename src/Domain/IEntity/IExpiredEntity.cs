namespace Domain.IEntity;

public interface IExpiredEntity : IBaseEntity
{
    DateTimeOffset ExpireAt { get; set; }
}