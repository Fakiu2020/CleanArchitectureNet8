
using Common.Entities.Interfaces;

namespace Common.Entities
{

    public abstract class Entity : BaseEntity, IEntity
    {
        public int Id { get; set; }

    }

    public abstract class EntityGuid : BaseEntity, IEntityGuid
    {
        public Guid Id { get; set; }

    }

    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            IsEnabled = true;
            CreationDate = DateTime.Now;
        }


        public DateTime CreationDate { get; private set; }
        public DateTime? LastUpdate { get; set; }
        public bool IsEnabled { get; set; }
    }


}
