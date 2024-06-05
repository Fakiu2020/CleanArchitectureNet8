using Application.Common.Exceptions;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Command
{
    public abstract class CommandHandlerBase
    {
        protected IEnumerable<string> Notifications;

        protected ValidationResult Validate<TCommand>(
        TCommand command,
        IValidator<TCommand> validator)
        {
            var validationResult = validator.Validate(command);
            Notifications = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return validationResult;
        }

        public Result Return() => new(!Notifications.Any(), Notifications);
    }
}
