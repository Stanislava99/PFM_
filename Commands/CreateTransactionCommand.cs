using pfm.Models;

namespace pfm.Commands
{
    public class CreateTransactionCommand
    {
        public string BeneficiaryName { get; set; }
        public string Date { get; set; }    
        public Directions direction { get; set; }
        public double amount { get; set; }
        public string currency { get; set; }
        public MccCode mccCode { get; set; }
        public TransactionKind transactionKind { get; set; }
        
    }
}