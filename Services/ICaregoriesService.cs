using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using pfm.Database.Entities;
using System.Collections.Generic;

namespace pfm.Services
{
    public interface ICategoriesService
    {
        Task<List<Categories>> GetCategories (string parentId);
        Task<bool> AddCategories (HttpRequest request);
        
    }
}