namespace Bank
{
    public class Customer
    {
        public int SortCode { get; set; }
        public int AccountNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? AmountInAccount { get; set; }
        public string Address { get; set; }
    }
}
