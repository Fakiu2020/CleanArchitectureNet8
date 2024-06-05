namespace Common.Entities.Interfaces
{

    public interface IEntity : IEntity<int>
    {
    }

    public interface IEntityGuid : IEntity<Guid>
    {
    }


}
