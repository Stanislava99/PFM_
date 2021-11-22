using System.ComponentModel.DataAnnotations;

namespace pfm.Database.Entities
{
    public class SplitTransactions
    {
        [Key]
        public int Id { get; set; }
        public double Amount { get; set; }

        [Required]
        public string CategoriesId { get; set; }
        public Categories Categories { get; set; }

        [Required]
        public string TransactionsId { get; set; }
        public Transactions Transactions { get; set; }

    }
}