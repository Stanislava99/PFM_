using pfm.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace pfm.Database.Entities
{
    public class Categories
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Code { get; set; }
        public string? ParentCode { get; set; }
        public string Name { get; set; }

        public ICollection<Transactions> Transactions { get; set; }
    }
}