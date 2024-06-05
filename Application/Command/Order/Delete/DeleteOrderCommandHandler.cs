using Application.Command.Order.Create;
using Application.Common;
using Application.Common.Exceptions;
using Application.Services;
using AutoMapper;
using FluentValidation;
using MediatR;
namespace Application.Command.Order.Delete
{
    public class DeleteOrderCommandHandler : CommandHandlerBase, IRequestHandler<DeleteOrderCommand, Result<DeleteOrderCommandResponse>>
    {
        private readonly IOrderService _orderService;
        private readonly IValidator<DeleteOrderCommand> _updateOrderCommandValidaor;
        private readonly IMapper _mapper;

        public DeleteOrderCommandHandler(IOrderService orderService, IValidator<DeleteOrderCommand> validator, IMapper mapper)
        {
            _orderService = orderService;
            _updateOrderCommandValidaor = validator;
            _mapper = mapper;
        }


        public async Task<Result<DeleteOrderCommandResponse>> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = Validate(command, _updateOrderCommandValidaor);
                if (!validationResult.IsValid)
                    return Result<DeleteOrderCommandResponse>
                     .Failure(validationResult.Errors.Select(e => e.ErrorMessage), _mapper.Map<DeleteOrderCommandResponse>(command));

                var result = await _orderService.RemoveLoginOrderAsync(command.OrderId);
                if (!result.Succeeded)
                    return Result<DeleteOrderCommandResponse>.Failure(result.Errors);

                return Result<DeleteOrderCommandResponse>.Success();

            }
            catch (ArgumentException ex)
            {
                return Result<DeleteOrderCommandResponse>.Failure(new[] { "An unexpected error occurred.", ex.Message });
            }
            catch (Exception ex)
            {
                return Result<DeleteOrderCommandResponse>.Failure(new[] { "An unexpected error occurred.", ex.Message });

            }
        }
    }
}
