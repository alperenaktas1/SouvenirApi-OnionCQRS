using SouvenirApi.Application.Bases;
using SouvenirApi.Application.Features.Auth.Exceptions;
using SouvenirApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouvenirApi.Application.Features.Auth.Rules
{
    public class AuthRules : BaseRules
    {
        public Task UserShouldNotBeExist(User? user)
        {
            if (user is not null)
            {
                throw new UserAlreadyExistException();
            }
            return Task.CompletedTask;

        }

        public Task EmailOrPasswordShouldNotBInvalid(User? user,bool checkPassword)
        {
            if (user is null || !checkPassword ) throw new EmailOrPasswordShouldNotBInvalidException();
            return Task.CompletedTask;
        }

        public Task RefreshTokenShouldNotBeExpired(DateTime? expiryDate)
        {
            if (expiryDate <= DateTime.Now)
            {
                throw new RefreshTokenShouldNotBeExpiredException();
            }
            return Task.CompletedTask;
        }
    }
}
