using CurrencyConversion;

namespace BankAccountService.Infrastructure.Extensions;

public static class CurrencyDecimalExtension
{
    public static DecimalProto ConvertToCurrencyDecimalProto(this decimal value)
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