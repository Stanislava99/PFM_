using  System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using pfm.Helpers.Pagination;

namespace pfm.Helper
{
    public static class Extensions
    {
        public static async ValueTask<int> SaveRangeAsync<T>(this DbSet<T> dbset, IEnumerable<T> items, DbContext? context = null, CancellationToken cancellationToken = default) where T : class
        {
            var count = 0;
            context = context ?? dbset.GetService<ICurrentDbContext>().Context;

            var keys = context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Select(e => e.Name);

            foreach (var item in items)
            {
                var existing = context.SameOrDefault(item, keys);

                // If we hit a duplicate key, we need to save and then resume adding.
                if (existing != null)
                {
                    count += await context.SaveChangesAsync();
                    existing.CurrentValues.SetValues(item);
                }
                else
                    context.Add(item);
                if (cancellationToken.IsCancellationRequested)
                    break;
            }
            count += await context.SaveChangesAsync();
            return count;
        }
         public static EntityEntry<T>? SameOrDefault<T>(this DbContext context, T item, IEnumerable<string> keys) where T : class
        {
            var entry = context.Entry(item);
            foreach (var entity in context.ChangeTracker.Entries<T>())
            {
                bool mismatch = false;
                foreach (var key in keys)
                {
                    if (!Equals(entity.Property(key).CurrentValue, entry.Property(key).CurrentValue))
                    {
                        mismatch = true;
                        break;
                    }
                }
                if (!mismatch)
                    return entity;
            }
            return default;
        }

         public static void AddPagination(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            var camelCase = new JsonSerializerSettings();
            camelCase.ContractResolver = new CamelCasePropertyNamesContractResolver();
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCase));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}