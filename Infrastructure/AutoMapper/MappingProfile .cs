using Application.Command.Order.Create;
using Application.Command.Order.Update;
using Application.Queries.Order;
using Application.Queries.Order.GetAll;
using AutoMapper;
using Common.Entities.Interfaces;
using Domain.Entities;
using System.Reflection;

namespace Infrastructure.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHaveCustomMapping)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("CreateMappings");
                methodInfo?.Invoke(instance, new object[] { this });
            }

            CreateMap<Order, GetOrderBaseInfoResponse>()
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDisplayName()))
           .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id));



            CreateMap<Domain.Entities.Order, CreateOrderCommandResponse>().IncludeBase<Order, GetOrderBaseInfoResponse>();
            CreateMap<Domain.Entities.Order, UpdateOrderCommandResponse>().IncludeBase<Order, GetOrderBaseInfoResponse>();
            CreateMap<Order, GetAllOrderResponse>().IncludeBase<Order, GetOrderBaseInfoResponse>();
            CreateMap<Order, GetByIdOrderResponse>().IncludeBase<Order, GetOrderBaseInfoResponse>();

      
            CreateMap<CreateOrderCommand, Domain.Entities.Order>();
            CreateMap<CreateOrderCommand, CreateOrderCommandResponse>();


        }
    }
}
