using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TinyCsvParser;
using System.IO;
using System.Text;
using pfm.Database.Contracts;
using pfm.Database.Entities;
using pfm.DTO;
using pfm.Models;
using TinyCsvParser.Mapping;
using pfm.Helper;
using pfm.Database;
using System;

namespace pfm.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transactions> _transactionsRepository;
        private readonly IRepository<Categories> _categoriesRepository;
        private readonly IRepository<SplitTransactions> _splitTransRepository;
         private readonly pfm_databaseContext _dbContext;
        public TransactionService(IRepository<Transactions> transactionsRepository, IRepository<Categories> categoriesRepository,IRepository<SplitTransactions> splitTransRepository, pfm_databaseContext dbContext)
        {
            _transactionsRepository = transactionsRepository;
            _categoriesRepository = categoriesRepository;
            _splitTransRepository = splitTransRepository;
            _dbContext = dbContext;
        }


        public async Task<bool> AddTransaction(HttpRequest request)
        {

            request.Body.Position = 0;
            var reader = new StreamReader(request.Body, Encoding.UTF8);
            var parsedDataString = await reader.ReadToEndAsync().ConfigureAwait(false);

            CsvReaderOptions csvReaderOptions = new CsvReaderOptions(new[] { "\n" });
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');

            TransactionsMappingModel csvMapper = new TransactionsMappingModel();

            CsvParser<Transactions> csvParser = new CsvParser<Transactions>(csvParserOptions, csvMapper);
            var result = csvParser
                         .ReadFromString(csvReaderOptions, parsedDataString)
                         .ToList();

            result.Remove(result[result.Count - 1]);

            List<Transactions> list = new List<Transactions>();
            for (int i = 3; i < result.Count; i++)
            {
                Transactions dataForDb = new Transactions
                {
                    Id = result[i].Result.Id,
                    BeneficiaryName = result[i].Result.BeneficiaryName,
                    Date = result[i].Result.Date,
                    Direction = result[i].Result.Direction,
                    Amount = result[i].Result.Amount,
                    Description = result[i].Result.Description,
                    Currency = result[i].Result.Currency,
                    Kind = result[i].Result.Kind,
                    Mcc = result[i].Result.Mcc
                };
                list.Add(dataForDb);
            }

          await _transactionsRepository.AddRange(list);
          var resultFromRepo = await _transactionsRepository.SaveAll();

            if (resultFromRepo)
            {
                return true;
            }

            return false;
        }
        
         public async Task<bool> ImportMccCodes()
        {
            var data = System.IO.File.ReadAllText(@"./Database/Files/mcc_codes.csv");
        
            CsvReaderOptions csvReaderOptions = new CsvReaderOptions(new[] { "\n" });
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');

            MccCodesMappingModel csvMapper = new MccCodesMappingModel();

            CsvParser<MccCodes> csvParser = new CsvParser<MccCodes>(csvParserOptions, csvMapper);
            var result = csvParser
                         .ReadFromString(csvReaderOptions, data)
                         .ToList();

            List<MccCodes> list = new List<MccCodes>();
            for (int i = 0; i < result.Count; i++)
            {
                MccCodes dataForDb = new MccCodes
                {
                    Code = result[i].Result.Code,
                    MercahntType = result[i].Result.MercahntType,
                };
                list.Add(dataForDb);
            }


            var count = await _dbContext.MccCodes.SaveRangeAsync(list);
            _dbContext.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteTransactions(int id)
        {
            var dataFromDb = await _transactionsRepository.GetById(id);
            if (dataFromDb != null)
            {
            
                _transactionsRepository.Delete(dataFromDb);
                var result = await _transactionsRepository.SaveAll();
                if (result)
                {
                    return true;
                }
            }

            return false;
        }
        
           public async Task<PagedList<Transactions>> GetPagedListTransactions(QueryParams transactionsParams)
        {
            var query = _transactionsRepository.AsQueryable();

            var split = _splitTransRepository.AsQueryable();
            


            if (!string.IsNullOrEmpty(transactionsParams.BeneficiaryName))
                query = query.Where(x => x.BeneficiaryName.Contains(transactionsParams.BeneficiaryName));

            if (!string.IsNullOrEmpty(transactionsParams.Direction))
                query = query.Where(x => x.Direction.Contains(transactionsParams.Direction));

            if (transactionsParams.FromAmount > 0 && transactionsParams.ToAmount > 0)
            {
                var from = transactionsParams.FromAmount;
                var to = transactionsParams.ToAmount;
                if (from <= to)
                    query = query.Where(x => from <= x.Amount && x.Amount <= to);
            }

            if (!string.IsNullOrEmpty(transactionsParams.FromDate) && !string.IsNullOrEmpty(transactionsParams.ToDate))
            {
                var dateFrom = Convert.ToDateTime(transactionsParams.FromDate);
                var dateTo = Convert.ToDateTime(transactionsParams.ToDate);

                if (dateFrom <= dateTo) query = query.Where(x => (DateTime)(object)x.Date >= dateFrom && (DateTime)(object)x.Date <= dateTo);
            }


            return await PagedList<Transactions>.ToPagedList(query, transactionsParams.PageNumber, transactionsParams.PageSize);
          
        }
        
        
         public async Task<bool> Categorize(string transactionId, string categorizeId)
        {
            var transaction = await _transactionsRepository.GetById(transactionId);
            var category = await _categoriesRepository.GetById(categorizeId);

            if(transaction != null && category != null) transaction.Categories = category;

            var resultFromRepo = await _transactionsRepository.SaveAll();

            if (resultFromRepo)
            {
    
                return true;
            }

            return false;
        }
      

           public async Task<bool> Split(string transactionId, List<SingleCategorySplit> splits) 
        {

            var transaction =  _dbContext.Transactions.FirstOrDefault(x => x.Id == transactionId);
           
            if(transaction == null) { return false; }

            double totalAmount = 0.0;
            foreach (var split in splits)
            {
                totalAmount += split.Amount;
            }

             
            if(transaction.Amount < totalAmount && totalAmount > 0.0)
            {
                return false;
            }

            transaction.isSplited = true;
            transaction.Amount =- totalAmount;

            List<SplitTransactions> list = new List<SplitTransactions>();

            
            foreach(var split in splits)
            {
                var splitTransaction = new SplitTransactions {
                    Amount = split.Amount,
                    TransactionsId = transactionId,
                    CategoriesId = split.CatCode
                    };

                list.Add(splitTransaction);
                _dbContext.Add(splitTransaction);
               
            }


        var resultFromRepo =  _dbContext.SaveChanges();
    
                if(resultFromRepo > 0) return true;

                return false;
    }
    }
    
       public class TransactionsMappingModel : CsvMapping<Transactions>
    {
        public TransactionsMappingModel()
        : base()
        {
            MapProperty(0, m => m.Id);
            MapProperty(1, m => m.BeneficiaryName);
            MapProperty(2, m => m.Date);
            MapProperty(3, m => m.Direction);
            MapProperty(4, m => m.Amount);
            MapProperty(5, m => m.Description);
            MapProperty(6, m => m.Currency);
            MapProperty(7, m => m.Mcc);
            MapProperty(8, m => m.Kind);
        }
    }
     public class MccCodesMappingModel : CsvMapping<MccCodes>
    {
        public MccCodesMappingModel()
     : base()
        {
            MapProperty(0, m => m.Code);
            MapProperty(1, m => m.MercahntType);
        }
    }
}