using SouvenirApi.Application.Bases;

namespace SouvenirApi.Application.Features.Auth.Exceptions
{
    public class EmailAddressShouldBeValidException : BaseException
    {
        public EmailAddressShouldBeValidException() : base("Böyle bir email bulunamadı.")
        {

        }
    }
}
