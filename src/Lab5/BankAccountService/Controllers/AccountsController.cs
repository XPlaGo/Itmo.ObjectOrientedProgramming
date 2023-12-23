using AutoMapper;
using BankAccountService.Application.Features.Accounts.Commands.CreateAccount;
using BankAccountService.Application.Features.Accounts.Commands.CreateAccount.Card;
using BankAccountService.Application.Features.Accounts.Commands.CreateAccount.Savings;
using BankAccountService.Application.Features.Accounts.Queries.GetAccount;
using BankAccountService.Application.Features.Accounts.Queries.GetAccount.GetCardAccount;
using BankAccountService.Application.Features.Accounts.Queries.GetAccount.GetSavingsAccount;
using BankAccountService.Application.Features.Transfers.Commands.Transfer;
using BankAccountService.Common;
using BankAccountService.Common.Factories;
using BankAccountService.Models.Requests.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerWithAuthBase
{
    private readonly CreateCardAccountCommandHandler _createCardAccount;
    private readonly CreateSavingsAccountCommandHandler _createSavingsAccount;
    private readonly GetCardAccountQueryHandler _getCardAccount;
    private readonly GetSavingsAccountQueryHandler _getSavingsAccount;
    private readonly IMapper _mapper;

    public AccountsController(CreateCardAccountCommandHandler createCardAccount, CreateSavingsAccountCommandHandler createSavingsAccount, GetCardAccountQueryHandler getCardAccount, GetSavingsAccountQueryHandler getSavingsAccount, IMapper mapper)
    {
        _createCardAccount = createCardAccount;
        _createSavingsAccount = createSavingsAccount;
        _getCardAccount = getCardAccount;
        _getSavingsAccount = getSavingsAccount;
        _mapper = mapper;
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPost]
    [Route("card")]
    public async Task<ActionResult<Result<CreateAccountResponse>>> CreateCard([FromBody] CreateAccountRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<long> userIdResult = await GetUserId().ConfigureAwait(false);
        if (userIdResult.Succeeded is false)
            return Unauthorized(await new ResultFactory().FailureAsync<TransferResponse>(userIdResult.Messages).ConfigureAwait(false));

        CreateAccountCommand command = _mapper.Map<CreateAccountRequest, CreateAccountCommand>(
            request,
            opt => opt.AfterMap((_, dest) => dest.UserId = userIdResult.Data));
        Result<CreateAccountResponse> result = await _createCardAccount.Handle(command, default).ConfigureAwait(false);

        if (result.Succeeded is false) return BadRequest(result);

        return result;
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPost]
    [Route("savings")]
    public async Task<ActionResult<Result<CreateAccountResponse>>> CreateSavings([FromBody] CreateAccountRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<long> userIdResult = await GetUserId().ConfigureAwait(false);
        if (userIdResult.Succeeded is false)
            return Unauthorized(await new ResultFactory().FailureAsync<TransferResponse>(userIdResult.Messages).ConfigureAwait(false));

        CreateAccountCommand command = _mapper.Map<CreateAccountRequest, CreateAccountCommand>(
            request,
            opt => opt.AfterMap((_, dest) => dest.UserId = userIdResult.Data));
        Result<CreateAccountResponse> result = await _createSavingsAccount.Handle(command, default).ConfigureAwait(false);

        if (result.Succeeded is false) return BadRequest(result);

        return result;
    }

    [Authorize(Roles = "User,Admin")]
    [HttpGet]
    [Route("card")]
    public async Task<ActionResult<Result<GetAccountResponse>>> GetCard([FromBody] GetMyAccountRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<long> userIdResult = await GetUserId().ConfigureAwait(false);
        if (userIdResult.Succeeded is false)
            return Unauthorized(await new ResultFactory().FailureAsync<TransferResponse>(userIdResult.Messages).ConfigureAwait(false));

        GetAccountRequest command = _mapper.Map<GetMyAccountRequest, GetAccountRequest>(
            request,
            opt => opt.AfterMap((_, dest) => dest.UserId = userIdResult.Data));
        Result<GetAccountResponse> result = await _getCardAccount.Handle(command, default).ConfigureAwait(false);

        if (result.Succeeded is false) return BadRequest(result);

        return result;
    }

    [Authorize(Roles = "User,Admin")]
    [HttpGet]
    [Route("savings")]
    public async Task<ActionResult<Result<GetAccountResponse>>> GetSavings([FromBody] GetMyAccountRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<long> userIdResult = await GetUserId().ConfigureAwait(false);
        if (userIdResult.Succeeded is false)
            return Unauthorized(await new ResultFactory().FailureAsync<TransferResponse>(userIdResult.Messages).ConfigureAwait(false));

        GetAccountRequest command = _mapper.Map<GetMyAccountRequest, GetAccountRequest>(
            request,
            opt => opt.AfterMap((_, dest) => dest.UserId = userIdResult.Data));
        Result<GetAccountResponse> result = await _getSavingsAccount.Handle(command, default).ConfigureAwait(false);

        if (result.Succeeded is false) return BadRequest(result);

        return result;
    }
}