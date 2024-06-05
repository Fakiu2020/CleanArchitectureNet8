using AutoMapper;

namespace Common.Entities.Interfaces
{

    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configuration);
    }


}
