using Application.Command.Order.Create;
using Application.Common;
using Application.Common.Exceptions;
using Application.Services;
using AutoMapper;
using FluentValidation;
using MediatR;
namespace Application.Command.Order.Update
{
    public class UpdateOrderCommandHandler : CommandHandlerBase, IRequestHandler<UpdateOrderCommand, Result<UpdateOrderCommandResponse>>
    {
        private readonly IOrderService _orderService;
        private readonly IValidator<UpdateOrderCommand> _updateOrderCommandValidaor;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IOrderService orderService, IValidator<UpdateOrderCommand> validator, IMapper mapper)
        {
            _orderService = orderService;
            _updateOrderCommandValidaor = validator;
            _mapper = mapper;
        }


        public async Task<Result<UpdateOrderCommandResponse>> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = Validate(command, _updateOrderCommandValidaor);
                if (!validationResult.IsValid)
                    return Result<UpdateOrderCommandResponse>
                     .Failure(validationResult.Errors.Select(e => e.ErrorMessage), _mapper.Map<UpdateOrderCommandResponse>(command));

                var result = await _orderService.SetOrderStatus(command.OrderId, command.Status);
                if (!result.Succeeded)
                    return Result<UpdateOrderCommandResponse>.Failure(result.Errors);

                var response = _mapper.Map<UpdateOrderCommandResponse>(result.Data);
                return Result<UpdateOrderCommandResponse>.Success(response);

            }
            catch (ArgumentException ex)
            {
                return Result<UpdateOrderCommandResponse>.Failure(new[] { "An unexpected error occurred.", ex.Message });
            }
            catch (Exception ex)
            {
                return Result<UpdateOrderCommandResponse>.Failure(new[] { "An unexpected error occurred.", ex.Message });

            }
        }
    }
}
