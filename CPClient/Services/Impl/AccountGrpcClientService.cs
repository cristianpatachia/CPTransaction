using CPClient.Services.GrpcClientServiceBase;
using CPClient.Services.Interfaces;
using Grpc.Core;

namespace CPClient.Services.Impl
{
    public class AccountGrpcClientService : GrpcClientServiceBase<Account.AccountClient>, IAccountGrpcClientService
    {
        public AccountGrpcClientService(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<List<ReadAccountsResponse>> GetAccountsDropdown()
        {
            var result = new List<ReadAccountsResponse>();

            using (var call = _client.GetAccountsDropdown(new ReadAccountsRequest()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var item = call.ResponseStream.Current;
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
