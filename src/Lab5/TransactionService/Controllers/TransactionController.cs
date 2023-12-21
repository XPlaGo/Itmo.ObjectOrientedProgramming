using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransactionService.Application.Features.Transactions.Commands.CreateTransaction;
using TransactionService.Application.Features.Transactions.Commands.DeleteTransaction;
using TransactionService.Application.Features.Transactions.Commands.UpdateTransaction;
using TransactionService.Application.Features.Transactions.Queries.GetAllByUserId;
using TransactionService.Common;

namespace TransactionService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly CreateTransactionCommandHandler _createTransaction;
    private readonly DeleteTransactionCommandHandler _deleteTransaction;
    private readonly UpdateTransactionCommandHandler _updateTransaction;
    private readonly GetAllByUserIdQueryHandler _getAllByUserId;

    public TransactionController(
        CreateTransactionCommandHandler createTransaction,
        DeleteTransactionCommandHandler deleteTransaction,
        UpdateTransactionCommandHandler updateTransaction,
        GetAllByUserIdQueryHandler getAllByUserId)
    {
        _createTransaction = createTransaction;
        _deleteTransaction = deleteTransaction;
        _updateTransaction = updateTransaction;
        _getAllByUserId = getAllByUserId;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Result<string>>> CreateTransaction([FromBody] CreateTransactionCommand command)
    {
        Result<string> result = await _createTransaction
            .Handle(command, default)
            .ConfigureAwait(false);

        if (result.Succeeded is false) return BadRequest(result);

        return result;
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Result<string>>> UpdateTransaction([FromBody] UpdateTransactionCommand command)
    {
        Result<string> result = await _updateTransaction
            .Handle(command, default)
            .ConfigureAwait(false);

        if (result.Succeeded is false) return BadRequest(result);

        return result;
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Result<string>>> DeleteTransaction([FromBody] DeleteTransactionCommand command)
    {
        Result<string> result = await _deleteTransaction
            .Handle(command, default)
            .ConfigureAwait(false);

        if (result.Succeeded is false) return BadRequest(result);

        return result;
    }

    [HttpGet]
    [Authorize(Roles = "User,Admin")]
    public async Task<ActionResult<Result<IReadOnlyList<GetAllByUserIdResponse>>>> GetAllByUserTransaction()
    {
        var ci = new CultureInfo("us-Us");
        string? idString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idString is null) return Unauthorized("Token don't contain NameIdentifier");

        try
        {
            long userId = long.Parse(idString, ci);

            Result<IReadOnlyList<GetAllByUserIdResponse>> result = await _getAllByUserId
                .Handle(new GetAllByUserIdRequest(userId), default)
                .ConfigureAwait(false);

            if (result.Succeeded is false) return BadRequest(result);

            return result;
        }
        catch (FormatException)
        {
            return Unauthorized("Invalid NameIdentifier format");
        }
        catch (OverflowException)
        {
            return Unauthorized("NameIdentifier is too long");
        }
    }
}