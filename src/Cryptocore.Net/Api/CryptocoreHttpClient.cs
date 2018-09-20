using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Logging;

namespace Cryptocore.Net
{
    public class CryptocoreHttpClient : ICryptocoreHttpClient
    {
        #region Public Constants

        /// <summary>
        /// Get the base endpoint URL.
        /// </summary>
        public static readonly string EndpointUrl = "https://api.cryptocore.ai/";

        /// <summary>
        /// Get the successful test response string.
        /// </summary>
        public static readonly string SuccessfulTestResponse = "{}";

        #endregion Public Constants

        #region Public Properties

        /// <summary>
        /// Singleton.
        /// </summary>
        public static CryptocoreHttpClient Instance => Initializer.Value;
        
        #endregion Public Properties

        #region Internal

        /// <summary>
        /// Lazy initializer.
        /// </summary>
        internal static Lazy<CryptocoreHttpClient> Initializer
            = new Lazy<CryptocoreHttpClient>(() => new CryptocoreHttpClient(), true);

        #endregion Internal

        #region Protected Fields

        protected readonly ILogger<CryptocoreHttpClient> Logger;

        #endregion Protected Fields

        #region Private Fields

        private readonly HttpClient _httpClient;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger">Optional <see cref="ILogger{JsonProvider}"/>.</param>
        public CryptocoreHttpClient(ILogger<CryptocoreHttpClient> logger = null)
        {
            Logger = logger;
            //
            var uri = new Uri(EndpointUrl);

            try
            {
                _httpClient = new HttpClient
                {
                    BaseAddress = uri
                };
            }
            catch (Exception e)
            {
                var message = $"{nameof(HttpClient)}: Failed to create HttpClient.";
                Logger?.LogError(e, message);
                throw new CryptocoreApiException(message, e);
            }
        }

        #endregion Constructors
        
        #region Public Methods
        
        public Task<string> GetAsync(string path, CancellationToken token = default)
            => GetAsync(new CryptocoreHttpRequest(path), token);

        public Task<string> GetAsync(CryptocoreHttpRequest request, CancellationToken token = default)
        {
            return RequestAsync(HttpMethod.Get, request, token);
        }

        public Task<string> PostAsync(CryptocoreHttpRequest request, CancellationToken token = default)
        {
            return RequestAsync(HttpMethod.Post, request, token);
        }

        public Task<string> PutAsync(CryptocoreHttpRequest request, CancellationToken token = default)
        {
            return RequestAsync(HttpMethod.Put, request, token);
        }

        public Task<string> DeleteAsync(CryptocoreHttpRequest request, CancellationToken token = default)
        {
            return RequestAsync(HttpMethod.Delete, request, token);
        }

        #endregion Public Methods
        
        #region Private Methods

        private async Task<string> RequestAsync(HttpMethod method, CryptocoreHttpRequest request, CancellationToken token = default)
        {
            Throw.IfNull(request, nameof(request));

            token.ThrowIfCancellationRequested();

            var requestMessage = request.CreateMessage(method);

            Logger?.LogDebug($"{nameof(CryptocoreHttpClient)}.{nameof(RequestAsync)}: [{method.Method}] \"{requestMessage.RequestUri}\"");

            using (var response = await _httpClient.SendAsync(requestMessage, token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync()
                        .ConfigureAwait(false);
                    
                    return json;
                }

                if (response.StatusCode == HttpStatusCode.GatewayTimeout)
                {
                    throw new CryptocoreUnknownStatusException();
                }

                var error = await response.Content.ReadAsStringAsync()
                    .ConfigureAwait(false);

                var errorCode = 0;
                string errorMessage = null;

                // ReSharper disable once InvertIf
                if (!string.IsNullOrWhiteSpace(error) && error.IsJsonObject())
                {
                    try // to parse server error response.
                    {
                        var jObject = JObject.Parse(error);

                        errorCode = jObject["code"]?.Value<int>() ?? 0;
                        errorMessage = jObject["message"]?.Value<string>();
                    }
                    catch (Exception e)
                    {
                        Logger?.LogError(e, $"{nameof(CryptocoreHttpClient)}.{nameof(RequestAsync)} failed to parse server error response: \"{error}\"");
                    }
                }

                throw new CryptocoreHttpException(response.StatusCode, response.ReasonPhrase, errorCode, errorMessage);
            }
        }

        #endregion
        
        #region IDisposable

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _httpClient?.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable
    }
}