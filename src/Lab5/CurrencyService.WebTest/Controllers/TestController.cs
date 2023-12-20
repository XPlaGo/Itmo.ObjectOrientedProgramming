using CurrencyService.Application.Features.CurrencyExchanges.Conversion;
using CurrencyService.Common;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyService.WebTest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController
{
    private readonly ConversionCommandHandler _conversion;

    public TestController(ConversionCommandHandler conversion)
    {
        _conversion = conversion;
    }

    [HttpPost]
    [Route("convert")]
    public async Task<ActionResult<Result<ConversionDto>>> Convert(ConversionCommand command)
    {
        return await _conversion.Handle(command, default).ConfigureAwait(false);
    }
}