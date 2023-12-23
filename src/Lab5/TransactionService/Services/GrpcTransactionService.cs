using AutoMapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Transaction;
using TransactionService.Application.Features.Transactions.Commands.CreateTransaction;
using TransactionService.Application.Features.Transactions.Commands.DeleteTransaction;
using TransactionService.Application.Features.Transactions.Commands.UpdateTransaction;
using TransactionService.Common;

namespace TransactionService.Services;

public class GrpcTransactionService : TransactionServiceProto.TransactionServiceProtoBase
{
    private readonly CreateTransactionCommandHandler _createTransaction;
    private readonly DeleteTransactionCommandHandler _deleteTransaction;
    private readonly UpdateTransactionCommandHandler _updateTransaction;
    private readonly IMapper _mapper;

    public GrpcTransactionService(
        CreateTransactionCommandHandler createTransaction,
        DeleteTransactionCommandHandler deleteTransaction,
        UpdateTransactionCommandHandler updateTransaction,
        IMapper mapper)
    {
        _createTransaction = createTransaction;
        _deleteTransaction = deleteTransaction;
        _updateTransaction = updateTransaction;
        _mapper = mapper;
    }

    [Authorize(Roles = "Admin,Internal")]
    public override async Task<TransactionTokenResultProto> Create(TransactionRequestProto request, ServerCallContext context)
    {
        CreateTransactionCommand command = _mapper.Map<TransactionRequestProto, CreateTransactionCommand>(request);
        Result<string> result = await _createTransaction.Handle(command, default).ConfigureAwait(false);

        return TokenResultMap(result);
    }

    [Authorize(Roles = "Admin,Internal")]
    public override async Task<TransactionTokenResultProto> Delete(TransactionTokenRequestProto request, ServerCallContext context)
    {
        DeleteTransactionCommand command = _mapper.Map<TransactionTokenRequestProto, DeleteTransactionCommand>(request);
        Result<string> result = await _deleteTransaction.Handle(command, default).ConfigureAwait(false);

        return TokenResultMap(result);
    }

    [Authorize(Roles = "Admin,Internal")]
    public override async Task<TransactionTokenResultProto> Update(TransactionRequestProto request, ServerCallContext context)
    {
        UpdateTransactionCommand command = _mapper.Map<TransactionRequestProto, UpdateTransactionCommand>(request);
        Result<string> result = await _updateTransaction.Handle(command, default).ConfigureAwait(false);

        return TokenResultMap(result);
    }

    private static TransactionTokenResultProto TokenResultMap(Result<string> result)
    {
        var resultProto = new TransactionTokenResultProto
        {
            Succeeded = result.Succeeded,
            Data = result.Data,
        };

        foreach (string message in result.Messages) resultProto.Messages.Add(message);

        return resultProto;
    }
}