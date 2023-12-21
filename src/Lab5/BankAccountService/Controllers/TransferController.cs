using BankAccountService.Application.Features.Transfers.Commands.Transfer;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.CardToCardTransfer;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.CardToDepositTranfer;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.CardToSavingsTransfer;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.DepositToCardTransfer;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.SavingsToCardTransfer;
using BankAccountService.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransferController : ControllerBase
{
    private readonly CardToCardTransferCommandHandler _cardToCardTransfer;
    private readonly CardToSavingsTransferCommandHandler _cardToSavingsTransfer;
    private readonly CardToDepositTransferCommandHandler _cardToDepositTransfer;
    private readonly DepositToCardTransferCommandHandler _depositToCardTransfer;
    private readonly SavingsToCardTransferCommandHandler _savingsToCardTransfer;

    public TransferController(CardToCardTransferCommandHandler cardToCardTransfer, CardToSavingsTransferCommandHandler cardToSavingsTransfer, CardToDepositTransferCommandHandler cardToDepositTransfer, DepositToCardTransferCommandHandler depositToCardTransfer, SavingsToCardTransferCommandHandler savingsToCardTransfer)
    {
        _cardToCardTransfer = cardToCardTransfer;
        _cardToSavingsTransfer = cardToSavingsTransfer;
        _cardToDepositTransfer = cardToDepositTransfer;
        _depositToCardTransfer = depositToCardTransfer;
        _savingsToCardTransfer = savingsToCardTransfer;
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPost]
    [Route("cardToCard")]
    public async Task<ActionResult<Result<TransferResponse>>> TransferCardToCard([FromBody] TransferCommand request)
    {
        Result<TransferResponse> result = await _cardToCardTransfer.Handle(request, default).ConfigureAwait(false);
        if (result.Succeeded is false) return BadRequest(result);
        return result;
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPost]
    [Route("cardToSaving")]
    public async Task<ActionResult<Result<TransferResponse>>> TransferCardToSaving([FromBody] TransferCommand request)
    {
        Result<TransferResponse> result = await _cardToSavingsTransfer.Handle(request, default).ConfigureAwait(false);
        if (result.Succeeded is false) return BadRequest(result);
        return result;
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPost]
    [Route("cardToDeposit")]
    public async Task<ActionResult<Result<TransferResponse>>> TransferCardToDeposit([FromBody] TransferCommand request)
    {
        Result<TransferResponse> result = await _cardToCardTransfer.Handle(request, default).ConfigureAwait(false);
        if (result.Succeeded is false) return BadRequest(result);
        return result;
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPost]
    [Route("depositToCard")]
    public async Task<ActionResult<Result<TransferResponse>>> TransferDepositToCard([FromBody] TransferCommand request)
    {
        Result<TransferResponse> result = await _depositToCardTransfer.Handle(request, default).ConfigureAwait(false);
        if (result.Succeeded is false) return BadRequest(result);
        return result;
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPost]
    [Route("savingsToCard")]
    public async Task<ActionResult<Result<TransferResponse>>> TransferSavingsToCard([FromBody] TransferCommand request)
    {
        Result<TransferResponse> result = await _cardToCardTransfer.Handle(request, default).ConfigureAwait(false);
        if (result.Succeeded is false) return BadRequest(result);
        return result;
    }
}