using CleanArchitecture.Application.Common.Models;
using MediatR;

namespace Application.Command.Auth
{
    public class LoginCommand : IRequest<ServiceResult<LoginCommandResponse>>
    {
        public required string UserName { get; init; }
        public required string Password { get; init; }
    }
}
