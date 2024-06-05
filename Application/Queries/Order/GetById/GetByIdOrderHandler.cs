using Application.Command.Order.Create;
using Application.Common.Exceptions;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Queries.Order.GetAll
{
    public class GetByIdOrderHandler : IRequestHandler<GetByIdOrderQuery, Result<GetByIdOrderResponse>>
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public GetByIdOrderHandler(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task<Result<GetByIdOrderResponse>> Handle(GetByIdOrderQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var orders = await _orderService.GetOrderAsync(query.OrderId);
                var response = _mapper.Map<GetByIdOrderResponse>(orders);
                return Result<GetByIdOrderResponse>.Success(response);
            }
            catch (ArgumentException ex)
            {
                return Result<GetByIdOrderResponse>.Failure(new[] { "An unexpected error occurred.", ex.Message });
            }
            catch (Exception ex)
            {
                return Result<GetByIdOrderResponse>.Failure(new[] { "An unexpected error occurred.", ex.Message });
            }


        }
    }
}
