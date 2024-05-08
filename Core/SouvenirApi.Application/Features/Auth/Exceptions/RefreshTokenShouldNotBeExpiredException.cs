using SouvenirApi.Application.Bases;

namespace SouvenirApi.Application.Features.Auth.Exceptions
{
    public class RefreshTokenShouldNotBeExpiredException : BaseException
    {
        public  RefreshTokenShouldNotBeExpiredException() : base("Oturum süresi sona ermiştir. Lütfen Tekrar giriş yapın.")
        {

        }
    }
}
