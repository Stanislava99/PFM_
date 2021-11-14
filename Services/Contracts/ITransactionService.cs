using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;


using pfm.DTO;
using pfm.Database.Entities;

namespace pfm.Service.Contracts
{
    public interface ITransactionService
    {
        Task<bool> AddTransaction(HttpRequest request);
        Task<bool> DeleteTransaction(int id);
        Task<bool> UpdateTransaction(TransactionDto transactionDto);
        Task<PagedList<TransactionEntity>> GetPagedListTransaction(QueryParams transactionParams);
        Task <ICollection<TransactionDto>> GetAllTransactions();

    }
}