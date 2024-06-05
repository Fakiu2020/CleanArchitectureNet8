using Application.Common;
using Application.Common.Exceptions;
using Application.Queries.Order;
using AutoMapper;
using Common.Entities.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Command.Order.Create
{
    public class CreateOrderCommandResponse : GetOrderBaseInfoResponse, IHaveCustomMapping
    {

        public decimal Commission { get; set; }
        public decimal Tax { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.Order, CreateOrderCommandResponse>();

        }

    }
}
