namespace pfm.DTO
{
    public class TransactionDto
    {
        public string Id {get; set;}
        public string BenificaryName { get; set; }
        public string Date { get; set; }
        public string Direction { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public int? Mcc { get; set; }
        public string Kind { get; set; }
    }
}