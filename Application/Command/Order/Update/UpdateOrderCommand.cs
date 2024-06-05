using Application.Command.Order.Create;
using Application.Common;
using Application.Common.Exceptions;
using AutoMapper;
using Common.Entities.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Command.Order.Update
{
    public class UpdateOrderCommand : IRequest<Result<UpdateOrderCommandResponse>>
    {
        public int OrderId { get; set; }

        public OrderStatusTypeEnum Status { get; set; }

    }
}
