using Application.Common;
using Application.Common.Exceptions;
using AutoMapper;
using Common.Entities.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Command.Order.Create
{
    public class CreateOrderCommand : IRequest<Result<CreateOrderCommandResponse>>, IHaveCustomMapping
    {
        public int AccountId { get; set; }
        public string AssetName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public char Operation { get; set; }


        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CreateOrderCommand, Domain.Entities.Order>()
                           .ForMember(dest => dest.Id, opt => opt.Ignore())
                           .ForMember(dest => dest.Status, opt => opt.Ignore())
                           .ForMember(dest => dest.CreationDate, opt => opt.Ignore())
                           .ForMember(dest => dest.LastUpdate, opt => opt.Ignore())
                           .ForMember(dest => dest.IsEnabled, opt => opt.Ignore())

                            .ForMember(dest => dest.Operation, opt => opt.MapFrom(src => char.ToUpperInvariant(src.Operation)))
                           .ForMember(dest => dest.TotalAmount, opt => opt.Ignore());

        }
    }
}
