namespace BankAccountService.Infrastructure.Services.JWT;

public class GrpcServicesSettings
{
    public GrpcServicesSettings() { }

    public GrpcServicesSettings(string currencyServiceAddress, string transactionServiceAddress)
    {
        CurrencyServiceAddress = currencyServiceAddress;
        TransactionServiceAddress = transactionServiceAddress;
    }

    public string CurrencyServiceAddress { get; set; } = string.Empty;
    public string TransactionServiceAddress { get; set; } = string.Empty;
}