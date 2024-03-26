using CPClient.Domain.Constants;
using Grpc.Core;
using Grpc.Net.Client;

namespace CPClient.Services.GrpcClientServiceBase
{
    public class GrpcClientServiceBase<T> where T : ClientBase<T>
    {
        private IConfiguration _configuration;
        protected T _client;

        public GrpcClientServiceBase(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = InitializeClient();
        }

        private T InitializeClient()
        {
            var cpServerAddress = _configuration.GetConnectionString(ClientConstants.CPServerAddress);
            var channel = GrpcChannel.ForAddress(cpServerAddress);
            var client = Activator.CreateInstance(
                type: typeof(T),
                args: new[] { channel })
                as T;

            return client;
        }
    }
}
