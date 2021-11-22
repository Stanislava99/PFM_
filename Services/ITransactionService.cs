using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using pfm.DTO;
using pfm.Models;
using pfm.Database.Entities;
using System.Collections.Generic;
using pfm.Helper;

namespace pfm.Services
{
    public interface ITransactionService
    {
        Task<bool> AddTransaction(HttpRequest request);
        Task<bool> DeleteTransactions(int id);
        
        Task<PagedList<Transactions>> GetPagedListTransactions(QueryParams transactionsParams);
        Task<bool> ImportMccCodes();
        Task<bool> Split(string transactionId,List<SingleCategorySplit> splits) ;
        Task<bool> Categorize(string transactionId, string categorizeId);
    }
}