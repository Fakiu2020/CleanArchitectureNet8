using Application.Manangers;
using Application.Services;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Command.Auth
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ServiceResult<LoginCommandResponse>>
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;


        public LoginCommandHandler(ITokenService tokenService,
            IIdentityService identityService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }
        public async Task<ServiceResult<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetUserIdByUserName(request.UserName);
            if (userId is null)
                return ServiceResult.Failed<LoginCommandResponse>(ServiceError.WrongUserNameOrPassword);
            if (!await _identityService.CheckPasswordAsync(userId.Value, request.Password))
                return ServiceResult.Failed<LoginCommandResponse>(ServiceError.WrongUserNameOrPassword);

            string token = _tokenService.CreateJwtSecurityToken(userId.Value) ?? string.Empty;

            return ServiceResult.Success(new LoginCommandResponse { Token = token });
        }
    }
}
