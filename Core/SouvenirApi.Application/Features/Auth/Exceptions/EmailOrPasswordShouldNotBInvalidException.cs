using SouvenirApi.Application.Bases;

namespace SouvenirApi.Application.Features.Auth.Exceptions
{
    public class EmailOrPasswordShouldNotBInvalidException : BaseException
    {
        public EmailOrPasswordShouldNotBInvalidException() : base("Kullanıcı adı veya şifre yanlıştır.")
        {

        }
    }
}
