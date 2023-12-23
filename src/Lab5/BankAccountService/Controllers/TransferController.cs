using AutoMapper;
using BankAccountService.Application.Features.Transfers.Commands.Transfer;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.CardToCardTransfer;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.CardToSavingsTransfer;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.SavingsToCardTransfer;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.WithCash;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.WithCash.TopUpCardAccountCommands;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.WithCash.WithdrawFromCardAccountCommands;
using BankAccountService.Common;
using BankAccountService.Common.Factories;
using BankAccountService.Models.Requests.Transfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransferController : ControllerWithAuthBase
{
    private readonly CardToCardTransferCommandHandler _cardToCardTransfer;
    private readonly CardToSavingsTransferCommandHandler _cardToSavingsTransfer;
    private readonly SavingsToCardTransferCommandHandler _savingsToCardTransfer;
    private readonly TopUpCardAccountCommandHandler _topUpCard;
    private readonly WithdrawFromCardAccountCommandHandler _withdrawFromCard;
    private readonly IMapper _mapper;

    public TransferController(
        CardToCardTransferCommandHandler cardToCardTransfer,
        CardToSavingsTransferCommandHandler cardToSavingsTransfer,
        SavingsToCardTransferCommandHandler savingsToCardTransfer,
        TopUpCardAccountCommandHandler topUpCard,
        WithdrawFromCardAccountCommandHandler withdrawFromCard,
        IMapper mapper)
    {
        _cardToCardTransfer = cardToCardTransfer;
        _cardToSavingsTransfer = cardToSavingsTransfer;
        _savingsToCardTransfer = savingsToCardTransfer;
        _mapper = mapper;
        _topUpCard = topUpCard;
        _withdrawFromCard = withdrawFromCard;
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

    [Authorize(Roles = "User,Admin")]
    [HttpPost]
    [Route("topUpCard")]
    public async Task<ActionResult<Result<WithCashCardAccountResponse>>> TopUpCard([FromBody] WithCashRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<long> userIdResult = await GetUserId().ConfigureAwait(false);
        if (userIdResult.Succeeded is false)
            return Unauthorized(await new ResultFactory().FailureAsync<TransferResponse>(userIdResult.Messages).ConfigureAwait(false));

        WithCashCardAccountCommand command = _mapper.Map<WithCashRequest, WithCashCardAccountCommand>(
            request,
            opt => opt.AfterMap((src, dest) => dest.UserId = userIdResult.Data));
        Result<WithCashCardAccountResponse> result = await _topUpCard.Handle(command, default).ConfigureAwait(false);

        if (result.Succeeded is false) return BadRequest(result);

        return result;
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPost]
    [Route("withdrawFromCard")]
    public async Task<ActionResult<Result<WithCashCardAccountResponse>>> WithdrawFromCard([FromBody] WithCashRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<long> userIdResult = await GetUserId().ConfigureAwait(false);
        if (userIdResult.Succeeded is false)
            return Unauthorized(await new ResultFactory().FailureAsync<TransferResponse>(userIdResult.Messages).ConfigureAwait(false));

        WithCashCardAccountCommand command = _mapper.Map<WithCashRequest, WithCashCardAccountCommand>(
            request,
            opt => opt.AfterMap((src, dest) => dest.UserId = userIdResult.Data));
        Result<WithCashCardAccountResponse> result = await _withdrawFromCard.Handle(command, default).ConfigureAwait(false);

        if (result.Succeeded is false) return BadRequest(result);

        return result;
    }
}