using pfm.Services.Contracts;
using pfm.Database.Entities;

namespace pfm.Services
{
    public class TransactionService : ITransactionService 
    {
        private readonly IRepository<TransactionEntity> _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService (IRepository<TransactionEntity> transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<bool> addTransactions(HttpRequest request)
        {
            
        }
    }
}