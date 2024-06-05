using Application.Common.Exceptions;
using Application.Services;
using CleanArchitecture.Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Command.User.CreateUser;

public class  CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, ServiceResult<CreateUserCommandResponse>>
{
    private readonly IIdentityService _identityService;

    public CreateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
     }

    public async Task<ServiceResult<CreateUserCommandResponse>> Handle(CreateUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        try
        {

            (Result createUserResult, int userId) = await _identityService.CreateUserAsync(request.UserName, request.Password, request.FirstName, request.LastName, request.UserName);

            if (!createUserResult.Succeeded)
                throw new ValidationException(createUserResult.Errors);

            return ServiceResult.Success(new CreateUserCommandResponse { Id = userId });
        }
        catch (Exception ex )
        {
            return ServiceResult.Failed(new CreateUserCommandResponse { Id = int.MinValue }, ServiceError.InternalServerError(ex.Message));
        }
    }
}