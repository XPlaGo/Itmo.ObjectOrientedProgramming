using System.Globalization;
using System.Security.Claims;
using AutoMapper;
using BankAccountService.Application.Features.Transfers.Commands.Transfer;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.CardToCardTransfer;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.CardToSavingsTransfer;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.SavingsToCardTransfer;
using BankAccountService.Common;
using BankAccountService.Common.Factories;
using BankAccountService.Models.Requests.Transfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransferController : ControllerBase
{
    private readonly CardToCardTransferCommandHandler _cardToCardTransfer;
    private readonly CardToSavingsTransferCommandHandler _cardToSavingsTransfer;
    private readonly SavingsToCardTransferCommandHandler _savingsToCardTransfer;
    private readonly IMapper _mapper;

    public TransferController(CardToCardTransferCommandHandler cardToCardTransfer, CardToSavingsTransferCommandHandler cardToSavingsTransfer, SavingsToCardTransferCommandHandler savingsToCardTransfer, IMapper mapper)
    {
        _cardToCardTransfer = cardToCardTransfer;
        _cardToSavingsTransfer = cardToSavingsTransfer;
        _savingsToCardTransfer = savingsToCardTransfer;
        _mapper = mapper;
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPost]
    [Route("cardToCard")]
    public async Task<ActionResult<Result<TransferResponse>>> TransferCardToCard([FromBody] TransferRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<long> userIdResult = await GetUserId().ConfigureAwait(false);
        if (userIdResult.Succeeded is false)
            return Unauthorized(await new ResultFactory().FailureAsync<TransferResponse>(userIdResult.Messages).ConfigureAwait(false));

        TransferCommand command = _mapper.Map<TransferRequest, TransferCommand>(
            request,
            opt => opt.AfterMap((src, dest) => dest.FromUserId = userIdResult.Data));
        Result<TransferResponse> result = await _cardToCardTransfer.Handle(command, default).ConfigureAwait(false);

        if (result.Succeeded is false) return BadRequest(result);

        return result;
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPost]
    [Route("cardToSaving")]
    public async Task<ActionResult<Result<TransferResponse>>> TransferCardToSaving([FromBody] TransferRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<long> userIdResult = await GetUserId().ConfigureAwait(false);
        if (userIdResult.Succeeded is false)
            return Unauthorized(await new ResultFactory().FailureAsync<TransferResponse>(userIdResult.Messages).ConfigureAwait(false));

        TransferCommand command = _mapper.Map<TransferRequest, TransferCommand>(
            request,
            opt => opt.AfterMap((src, dest) => dest.FromUserId = userIdResult.Data));
        Result<TransferResponse> result = await _cardToSavingsTransfer.Handle(command, default).ConfigureAwait(false);

        if (result.Succeeded is false) return BadRequest(result);

        return result;
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPost]
    [Route("savingsToCard")]
    public async Task<ActionResult<Result<TransferResponse>>> TransferSavingsToCard([FromBody] TransferRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<long> userIdResult = await GetUserId().ConfigureAwait(false);
        if (userIdResult.Succeeded is false)
            return Unauthorized(await new ResultFactory().FailureAsync<TransferResponse>(userIdResult.Messages).ConfigureAwait(false));

        TransferCommand command = _mapper.Map<TransferRequest, TransferCommand>(
            request,
            opt => opt.AfterMap((src, dest) => dest.FromUserId = userIdResult.Data));
        Result<TransferResponse> result = await _savingsToCardTransfer.Handle(command, default).ConfigureAwait(false);

        if (result.Succeeded is false) return BadRequest(result);

        return result;
    }

    private async Task<Result<long>> GetUserId()
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