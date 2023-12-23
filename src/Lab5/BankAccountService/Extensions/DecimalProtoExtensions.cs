using Transfer;

namespace BankAccountService.Extensions;

public static class DecimalProtoExtensions
{
    public static decimal ConvertToDecimal(this DecimalProto value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return new decimal(new[] { value.V1, value.V2, value.V3, value.V4 });
    }
}