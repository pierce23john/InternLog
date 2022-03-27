namespace InternLog.Domain.Entities.Base
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; }
    }
}