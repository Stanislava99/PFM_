using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TinyCsvParser;
using System.IO;
using System.Text;
using pfm.Database.Entities;
using pfm.Database.Contracts;
using TinyCsvParser.Mapping;
using pfm.Database;
using pfm.Helper;

namespace pfm.Services
{
    public class CategoriesService : ICategoriesService
    {
         private readonly IRepository<Categories> _categoriesRepository;
        private readonly pfm_databaseContext _dbContext;
        public CategoriesService(IRepository<Categories> categoriesRepository, pfm_databaseContext dbContext)
        {
            _categoriesRepository = categoriesRepository;
            _dbContext = dbContext;
        }
      public async Task<bool> AddCategories(HttpRequest request)
        {
            
            var reader = new StreamReader(request.Body, Encoding.UTF8);
            var parsedDataString = await reader.ReadToEndAsync().ConfigureAwait(false);

            CsvReaderOptions csvReaderOptions = new CsvReaderOptions(new[] { "\n" });
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');

            CategoriesMappingModel csvMapper = new CategoriesMappingModel();

            CsvParser<Categories> csvParser = new CsvParser<Categories>(csvParserOptions, csvMapper);
            var result = csvParser
                         .ReadFromString(csvReaderOptions, parsedDataString)
                         .ToList();

            result.Remove(result[result.Count - 1]);

            List<Categories> list = new List<Categories>();

            for (int i = 3; i < result.Count; i++)
            {
                var dataForDb = new Categories
                {
                    Code = result[i].Result.Code,
                    ParentCode = result[i].Result.ParentCode,
                    Name = result[i].Result.Name
                };
                list.Add(dataForDb);
            }

            var count = await _dbContext.Categories.SaveRangeAsync(list);
          
            return true;
         }

         public async Task<List<Categories>> GetCategories (string parentId)
         {
             var categories = await _categoriesRepository.GetCategories(parentId);
        

            if (categories == null)
            {
                return await _categoriesRepository.Get(null);
            }
            
            return await _categoriesRepository.Get(parentId);

         }
        
        
    }
    public class CategoriesMappingModel : CsvMapping<Categories>
    {
        public CategoriesMappingModel()
       : base()
        {
            MapProperty(0, m => m.Code);
            MapProperty(1, m => m.ParentCode);
            MapProperty(2, m => m.Name);

        }
    }
    
   

}