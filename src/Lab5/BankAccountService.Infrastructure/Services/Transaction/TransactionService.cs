using AutoMapper;
using BankAccountService.Application.Interfaces.Services;
using BankAccountService.Application.Models.Transaction;
using BankAccountService.Common;
using BankAccountService.Common.Factories;
using BankAccountService.Infrastructure.Services.JWT;
using Grpc.Core;
using Grpc.Net.Client;
using Transaction;

namespace BankAccountService.Infrastructure.Services.Transaction;

public class TransactionService : ITransactionService
{
    private readonly GrpcServicesSettings _settings;
    private readonly IAuthTokenService _tokenService;
    private readonly IMapper _mapper;

    public TransactionService(GrpcServicesSettings settings, IAuthTokenService tokenService, IMapper mapper)
    {
        _settings = settings;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<Result<string>> Create(CreateTransactionRequest request)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(request);

            string jwtToken =
                await _tokenService.GenerateInternalAccessToken("BankAccountService").ConfigureAwait(false);

            using var channel = GrpcChannel.ForAddress(_settings.TransactionServiceAddress);
            var client = new TransactionServiceProto.TransactionServiceProtoClient(channel);

            TransactionRequestProto transactionRequest = _mapper.Map<CreateTransactionRequest, TransactionRequestProto>(request);

            var headers = new Metadata { { "Authorization", $"Bearer {jwtToken}" } };

            TransactionTokenResultProto response = await client.CreateAsync(transactionRequest, headers).ConfigureAwait(false);

            return new Result<string>
            {
                Messages = response.Messages.ToList(),
                Succeeded = response.Succeeded,
                Data = response.Data,
            };
        }
        catch (RpcException exception)
        {
            return await new ResultFactory()
                .FailureAsync<string>(exception.Message)
                .ConfigureAwait(false);
        }
    }

    public async Task<Result<string>> Delete(DeleteTransactionRequest request)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(request);

            string jwtToken =
                await _tokenService.GenerateInternalAccessToken("BankAccountService").ConfigureAwait(false);

            using var channel = GrpcChannel.ForAddress(_settings.TransactionServiceAddress);
            var client = new TransactionServiceProto.TransactionServiceProtoClient(channel);

            TransactionTokenRequestProto transactionRequest = _mapper.Map<DeleteTransactionRequest, TransactionTokenRequestProto>(request);

            var headers = new Metadata { { "Authorization", $"Bearer {jwtToken}" } };

            TransactionTokenResultProto response = await client.DeleteAsync(transactionRequest, headers).ConfigureAwait(false);

            return new Result<string>
            {
                Messages = response.Messages.ToList(),
                Succeeded = response.Succeeded,
                Data = response.Data,
            };
        }
        catch (RpcException exception)
        {
            return await new ResultFactory()
                .FailureAsync<string>(exception.Message)
                .ConfigureAwait(false);
        }
    }

    public async Task<Result<string>> Update(UpdateTransactionRequest request)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(request);

            string jwtToken =
                await _tokenService.GenerateInternalAccessToken("BankAccountService").ConfigureAwait(false);

            using var channel = GrpcChannel.ForAddress(_settings.TransactionServiceAddress);
            var client = new TransactionServiceProto.TransactionServiceProtoClient(channel);

            TransactionRequestProto transactionRequest = _mapper.Map<UpdateTransactionRequest, TransactionRequestProto>(request);

            var headers = new Metadata { { "Authorization", $"Bearer {jwtToken}" } };

            TransactionTokenResultProto response = await client.UpdateAsync(transactionRequest, headers).ConfigureAwait(false);

            return new Result<string>
            {
                Messages = response.Messages.ToList(),
                Succeeded = response.Succeeded,
                Data = response.Data,
            };
        }
        catch (RpcException exception)
        {
            return await new ResultFactory()
                .FailureAsync<string>(exception.Message)
                .ConfigureAwait(false);
        }
    }
}