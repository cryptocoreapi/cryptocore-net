using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cryptocore.Net
{
    public interface ICryptocoreHttpClient : IDisposable
    {
        /// <summary>
        /// Get request.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<string> GetAsync(string path, CancellationToken token = default);

        /// <summary>
        /// Get request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<string> GetAsync(CryptocoreHttpRequest request, CancellationToken token = default);

        /// <summary>
        /// Post request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<string> PostAsync(CryptocoreHttpRequest request, CancellationToken token = default);

        /// <summary>
        /// Put request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<string> PutAsync(CryptocoreHttpRequest request, CancellationToken token = default);

        /// <summary>
        /// Delete request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<string> DeleteAsync(CryptocoreHttpRequest request, CancellationToken token = default);
    }
}