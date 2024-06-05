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

namespace Application.Queries.Order.GetAll
{
    public class GetAllOrderHandler : IRequestHandler<GetAllOrderQuery, Result<List<GetAllOrderResponse>>>
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public GetAllOrderHandler(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllOrderResponse>>> Handle(GetAllOrderQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                var response = _mapper.Map<List<GetAllOrderResponse>>(orders);
                return Result<List<GetAllOrderResponse>>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<List<GetAllOrderResponse>>.Failure(new[] { "An unexpected error occurred.", ex.Message });
            }


        }
    }
}
