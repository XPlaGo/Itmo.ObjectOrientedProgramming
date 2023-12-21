using Transaction;

namespace BankAccountService.Infrastructure.Extensions;

public static class TransactionDecimalExtension
{
    public static DecimalProto ConvertToTransactionDecimalProto(this decimal value)
    {
        int[] bits = decimal.GetBits(value);
        return new DecimalProto
        {
            V1 = bits[0],
            V2 = bits[1],
            V3 = bits[2],
            V4 = bits[3],
        };
    }
}