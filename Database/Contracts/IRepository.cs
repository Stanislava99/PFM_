using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pfm.Helper;
using pfm.DTO;
using pfm.Models;
using pfm.Database.Entities;

namespace pfm.Database.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(int id);    
        Task<T> GetById(string id);  
        Task<ICollection<T>> List();
        Task AddRange(ICollection<T> collection);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> AsQueryable();
        Task<bool> SaveAll();
        Task<List<Categories>> Get(string parentId = null);
        Task<Categories> GetCategories (string parentId);
        Task<PageSortedList<TransactionDto>> GetTransactions(int page, int pageSize, string sortBy, SortOrder sortOrder);
    }
}
