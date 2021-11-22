using pfm.Models;
using System.Collections.Generic;

namespace pfm.Commands
{
    public class SplitTransactionCommand
    {
        public List<SingleCategorySplit> splits {get; set;}
    }
}