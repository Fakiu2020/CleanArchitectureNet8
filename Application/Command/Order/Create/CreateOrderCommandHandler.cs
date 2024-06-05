using Application.Common;
using Application.Common.Exceptions;
using Application.Services;
using AutoMapper;
using FluentValidation;
using MediatR;
namespace Application.Command.Order.Create
{
    public class CreateOrderCommandHandler : CommandHandlerBase, IRequestHandler<CreateOrderCommand, Result<CreateOrderCommandResponse>>
    {
        private readonly IOrderService _orderService;
        private readonly IValidator<CreateOrderCommand> _createOrderCommandValidaor;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IOrderService orderService, IValidator<CreateOrderCommand> validator, IMapper mapper)
        {
            _orderService = orderService;
            _createOrderCommandValidaor = validator;
            _mapper = mapper;
        }


        public async Task<Result<CreateOrderCommandResponse>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = Validate(command, _createOrderCommandValidaor);
                if (!validationResult.IsValid)
                    return Result<CreateOrderCommandResponse>
                     .Failure(validationResult.Errors.Select(e => e.ErrorMessage), _mapper.Map<CreateOrderCommandResponse>(command));

                var order = _mapper.Map<Domain.Entities.Order>(command);
                await _orderService.AddOrderAsync(order);

                var response = _mapper.Map<CreateOrderCommandResponse>(order);
                return Result<CreateOrderCommandResponse>.Success(response);

            }
            catch (ArgumentException ex)
            {
                return Result<CreateOrderCommandResponse>.Failure(new[] { "An unexpected error occurred.", ex.Message }, _mapper.Map<CreateOrderCommandResponse>(command));
            }
            catch (Exception ex)
            {
                return Result<CreateOrderCommandResponse>.Failure(new[] { "An unexpected error occurred.", ex.Message }, _mapper.Map< CreateOrderCommandResponse>(command));

            }
        }
    }
}
