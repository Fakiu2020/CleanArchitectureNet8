using Application.Common;

namespace Application.Services
{
    public interface ITokenService
    {
        string? CreateJwtSecurityToken(int userId);

    }
}
