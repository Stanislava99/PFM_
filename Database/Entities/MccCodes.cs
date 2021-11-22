using System.ComponentModel.DataAnnotations;

#nullable disable

namespace pfm.Database.Entities
{
    public partial class MccCodes
    {
        [Key]
        public string Code { get; set; }
        public string MercahntType { get; set; }
    }
}
