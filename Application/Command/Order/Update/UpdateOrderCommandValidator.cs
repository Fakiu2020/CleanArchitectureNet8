using Domain.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.Order.Update
{
    public class UpdateCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is required.");
            RuleFor(x => x.Status).IsInEnum().WithMessage("Invalid order status.");
        }
    }

}
