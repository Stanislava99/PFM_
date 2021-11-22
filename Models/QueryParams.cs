using System;

namespace pfm.Models
{
    public class QueryParams
    {
        private const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        private int pageSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public string Name { get; set; }
        public string BeneficiaryName {get;set;}

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public int Status { get; set; } = -1;

        public int StatusCode { get; set; }

        public string IpAddress { get; set; }

        public string User { get; set; }

        public int Year { get; set; }
           public double FromAmount { get; set; }
        public double ToAmount { get; set; }
        public string Direction { get; set; }


    }
}
