using System.Net;

namespace MediAssist.Application.Abstract.Services
{
    public interface IFirebaseAuthService
    {
        Task<HttpStatusCode> SignInWithEmailAndPasswordAsync(string email, string password);

        Task<HttpStatusCode> SignUpWithEmailAndPasswordAsync(string email, string password);
    }
}
