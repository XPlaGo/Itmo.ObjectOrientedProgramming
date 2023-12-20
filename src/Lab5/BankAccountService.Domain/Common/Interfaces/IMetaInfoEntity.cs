namespace BankAccountService.Domain.Common.Interfaces;

public interface IMetaInfoEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}