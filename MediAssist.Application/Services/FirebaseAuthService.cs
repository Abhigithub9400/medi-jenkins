using MediAssist.Application.Abstract.Services;
using MediAssist.Infrastructure.Abstract.Configurations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace MediAssist.Application.Services
{
    public class FirebaseAuthService : IFirebaseAuthService
    {
        #region PRIVATE FIELDS
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        #endregion

        #region CONSTRUCTOR
        public FirebaseAuthService(IAppSettings appSettings)
        {
            _apiKey = appSettings.ApiKey;
            _httpClient = new HttpClient();
        }
        #endregion

        #region PUBLIC METHODS
        public async Task<HttpStatusCode> SignInWithEmailAndPasswordAsync(string email, string password)
        {
            var payload = new
            {
                email,
                password,
                returnSecureToken = true
            };

            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={_apiKey}", content);

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> SignUpWithEmailAndPasswordAsync(string email, string password)
        {
            var payload = new
            {
                email,
                password,
                returnSecureToken = true
            };

            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={_apiKey}", content);

            return response.StatusCode;
        }
        #endregion

        #region PRIVATE METHODS
        private class FirebaseAuthResponse
        {
            [JsonProperty("idToken")]
            public string IdToken { get; set; }
        }
        #endregion
    }
}
