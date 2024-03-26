using CPServer.Domain.Services.Interfaces;
using Grpc.Core;

namespace CPServer.GrpcServices
{
    public class AccountGrpcService : Account.AccountBase
    {
        private readonly IAccountDataService accountDataService;

        public AccountGrpcService(IAccountDataService accountDataService)
        {
            this.accountDataService = accountDataService;
        }

        public override async Task GetAccountsDropdown(
            ReadAccountsRequest request,
            IServerStreamWriter<ReadAccountsResponse> responseStream,
            ServerCallContext context)
        {
            foreach (var dropdownData in await accountDataService.GetCustomerAccountsDropdownAsync())
            {
                await responseStream.WriteAsync(
                    new ReadAccountsResponse
                    {
                        Id = dropdownData.Value,
                        Name = dropdownData.Text
                    });
            }
        }

    }
}
