using System.Globalization;
using System.Security.Claims;
using BankAccountService.Common;
using BankAccountService.Common.Factories;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountService.Controllers;

public abstract class ControllerWithAuthBase : ControllerBase
{
    protected async Task<Result<long>> GetUserId()
    {
        var ci = new CultureInfo("us-Us");
        string? idString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idString is null)
            return await new ResultFactory().FailureAsync<long>("Token don't contain NameIdentifier").ConfigureAwait(false);
        try
        {
            long userId = long.Parse(idString, ci);
            return await new ResultFactory().SuccessAsync(userId).ConfigureAwait(false);
        }
        catch (FormatException)
        {
            return await new ResultFactory().FailureAsync<long>("Invalid NameIdentifier format").ConfigureAwait(false);
        }
        catch (OverflowException)
        {
            return await new ResultFactory().FailureAsync<long>("NameIdentifier is too long").ConfigureAwait(false);
        }
    }
}