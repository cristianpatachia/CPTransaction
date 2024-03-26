namespace CPClient.Services.Interfaces;

public interface IAccountGrpcClientService
{
    Task<List<ReadAccountsResponse>> GetAccountsDropdown();
}