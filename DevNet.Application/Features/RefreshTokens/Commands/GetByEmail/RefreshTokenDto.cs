using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevNet.Application.Features.RefreshTokens.Commands.GetByEmail
{
    public sealed record RefreshTokenDto(
        Guid Id,
        string RefreshToken,
        string Email,
        DateTime ExpiresAt,
        bool IsRevoked,
        Guid ApplicationUserId,
        string FirstName,
        string LastName
    );
}