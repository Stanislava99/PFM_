using System;
using Microsoft.EntityFrameworkCore;
using pfm.Database.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pfm.Helper;
using pfm.DTO;
using pfm.Models;
using pfm.Database.Entities;


namespace pfm.Database
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly pfm_databaseContext _dbContex;
        private readonly DbSet<T> entities;

        public Repository(pfm_databaseContext dbContex)
        {
            _dbContex = dbContex;
            entities = _dbContex.Set<T>();
        }

        public async Task Add(T entity)
        {
            await entities.AddAsync(entity);
        }

        public async Task AddRange(ICollection<T> collection)
        {
            await entities.AddRangeAsync(collection);
        }

        public IQueryable<T> AsQueryable()
        {
            return entities.AsQueryable<T>();
        }

        public void Delete(T entity)
        {
            entities.Remove(entity);
        }

        public async Task<T> GetById(int id)
        {
            return await entities.FindAsync(id);
        }
        public async Task<T> GetById(string id)
        {
            return await entities.FindAsync(id);
        }


        public async Task<ICollection<T>> List()
        {
            return await entities.ToListAsync();
        }

        public void Update(T entity)
        {
            entities.Attach(entity).State = EntityState.Modified;
        }

        public async Task<bool> SaveAll()
        {

            return await _dbContex.SaveChangesAsync() > 0;
        }

        public void UpdateCategoryById (string id, string parentCode, string name)
        {
            var category = _dbContex.Categories.First( a => a.Code == id );
            category.ParentCode = parentCode;
            category.Name = name;
        }

        public async Task<List<Categories>> Get(string parentId = null)
        {
            IQueryable<Categories> query;

            if(string.IsNullOrEmpty(parentId))
            {
                query = _dbContex.Categories.AsQueryable<Categories>();
            }
            else
            {
                query = _dbContex.Categories.Where(c => c.ParentCode == parentId).AsQueryable<Categories>();
            }

            return await query.ToListAsync<Categories>();
        }
        public async Task<Categories> GetCategories (string parentId)
        {
            return await _dbContex.Categories.FindAsync(parentId);
        }
        
       public async Task<PageSortedList<TransactionDto>> GetTransactions(int page, int pageSize, string sortBy, SortOrder sortOrder)
        {
            var query = _dbContex.Transactions.AsQueryable();

            var total = await query.CountAsync();

            var totalPages = total = (int)Math.Ceiling(total * 1.0 / pageSize);

            // if(!string.IsNullOrEmpty(sortBy))
            // {
            //     if(sortOrder == SortOrder.Desc)
            //     {
            //         query = query.OrderByDescending(sortBy, p => p.Id);
            //     }
            //     else
            //     {
            //         query = query.OrderBy(sortBy, p => p.Id);
            //         sortOrder = SortOrder.Asc;
            //     }
            // }
            // else
            // {
            //     query = query.OrderBy(p => p.Id);
            // }

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var items = await query.ToListAsync();

            List<TransactionDto> transactions = new List<TransactionDto>();

            foreach(var item in items)
            {
                TransactionDto transaction = new TransactionDto();

                transaction.Id = item.Id;
                transaction.BenificaryName = item.BeneficiaryName;
                transaction.Date = item.Date;
                transaction.Direction = item.Direction;
                transaction.Amount = item.Amount;
                transaction.Description = item.Description;
                transaction.Currency = item.Currency;
                transaction.Mcc = item.Mcc;
                transaction.Kind = item.Kind;

                transactions.Add(transaction);
            }

            return new PageSortedList<TransactionDto>()
            {
                Page = page,
                PageSize = pageSize,
                SortBy = sortBy,
                SortOrder = sortOrder,
                TotalCount = total,
                TotalPages = totalPages == 0 ? 1 : totalPages,
                Items = transactions,
            };
        }
    }
}
