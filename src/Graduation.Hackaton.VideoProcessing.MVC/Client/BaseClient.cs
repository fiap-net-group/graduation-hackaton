using Polly.Retry;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Graduation.Hackaton.VideoProcessing.MVC.Client
{
    public class BaseClient
    {
        protected HttpClient Client { get; }
        private readonly JsonSerializerOptions _serializeOptions;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

        public BaseClient(AsyncRetryPolicy<HttpResponseMessage> retryPolicy,
                        JsonSerializerOptions jsonSerializer,
                        HttpClient client)
        {
            _retryPolicy = retryPolicy;
            Client = client;
            _serializeOptions = jsonSerializer;
        }

        protected async Task<TResponse> SendAuthenticatedAsync<TResponse>(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {

                var apiResponse = await _retryPolicy.ExecuteAsync(async action =>
                {
                    return await Client.SendAsync(request, cancellationToken);
                },
                cancellationToken);

                return await apiResponse.Content.ReadFromJsonAsync<TResponse>(_serializeOptions, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        protected async Task<TResponse> SendAsync<TResponse>(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var apiResponse = await _retryPolicy.ExecuteAsync(async action =>
                {
                    return await Client.SendAsync(request, cancellationToken);
                },
                cancellationToken);

                return await apiResponse.Content.ReadFromJsonAsync<TResponse>(_serializeOptions, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
