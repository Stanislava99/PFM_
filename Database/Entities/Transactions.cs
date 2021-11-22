using pfm.Database.Entities;
using pfm.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pfm.Database.Entities
{   
    public class Transactions
    {

        //public Transactions()
        //{
        //    this.SplitTransactions = new List<SplitTransactions>();
        //}

        [Key]
        public string Id { get; set; }

        [MaxLength(255)]
        public string? BeneficiaryName { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string Direction { get; set; }
        [Required]
        public double Amount { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        [Required, MaxLength(3), MinLength(3)]
        public string Currency { get; set; }

        public bool isSplited { get; set; }

      //  [ForeignKey("MccCodes")]
        public int? Mcc { get; set; }
        //public MccCodes MccCode { get; set; }

        [Required]
        public string Kind { get; set; }

        //public int CategoryCode { get; set; }
        public Categories Categories { get; set; }
        

    }
}