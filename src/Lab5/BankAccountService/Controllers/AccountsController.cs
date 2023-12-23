using AutoMapper;
using BankAccountService.Application.Features.Accounts.Commands.CreateAccount;
using BankAccountService.Application.Features.Accounts.Commands.CreateAccount.Card;
using BankAccountService.Application.Features.Accounts.Commands.CreateAccount.Savings;
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
    private readonly IMapper _mapper;

    public AccountsController(CreateCardAccountCommandHandler createCardAccount, CreateSavingsAccountCommandHandler createSavingsAccount, IMapper mapper)
    {
        _createCardAccount = createCardAccount;
        _createSavingsAccount = createSavingsAccount;
        _mapper = mapper;
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPost]
    [Route("createCard")]
    public async Task<ActionResult<Result<CreateAccountResponse>>> CreateCard([FromBody] CreateAccountRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<long> userIdResult = await GetUserId().ConfigureAwait(false);
        if (userIdResult.Succeeded is false)
            return Unauthorized(await new ResultFactory().FailureAsync<TransferResponse>(userIdResult.Messages).ConfigureAwait(false));

        CreateAccountCommand command = _mapper.Map<CreateAccountRequest, CreateAccountCommand>(
            request,
            opt => opt.AfterMap((src, dest) => dest.UserId = userIdResult.Data));
        Result<CreateAccountResponse> result = await _createCardAccount.Handle(command, default).ConfigureAwait(false);

        if (result.Succeeded is false) return BadRequest(result);

        return result;
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPost]
    [Route("createSavings")]
    public async Task<ActionResult<Result<CreateAccountResponse>>> CreateSavings([FromBody] CreateAccountRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<long> userIdResult = await GetUserId().ConfigureAwait(false);
        if (userIdResult.Succeeded is false)
            return Unauthorized(await new ResultFactory().FailureAsync<TransferResponse>(userIdResult.Messages).ConfigureAwait(false));

        CreateAccountCommand command = _mapper.Map<CreateAccountRequest, CreateAccountCommand>(
            request,
            opt => opt.AfterMap((src, dest) => dest.UserId = userIdResult.Data));
        Result<CreateAccountResponse> result = await _createSavingsAccount.Handle(command, default).ConfigureAwait(false);

        if (result.Succeeded is false) return BadRequest(result);

        return result;
    }
}