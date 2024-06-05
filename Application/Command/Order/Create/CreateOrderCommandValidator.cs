using Domain.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.Order.Create
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {

        public CreateOrderCommandValidator()
        {
            RuleFor(v => v.AssetName).NotEmpty().WithMessage("AssetName is required");
            RuleFor(v => v.AccountId).NotEmpty().WithMessage("AccountId is required");
            RuleFor(v => v.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.").WithName("Cantidad");
            
            RuleFor(x => x.Operation).NotEmpty()
            .Must(op => char.ToUpper(op) == Operation.Buy || char.ToUpper(op) == Operation.Sell)
            .WithMessage("Operation must be a single character 'C' or 'V'.");

        }

        
    }
}
