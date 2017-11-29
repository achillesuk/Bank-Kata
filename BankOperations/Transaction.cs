using System;

namespace Bank
{
    public class Transaction 
    {
        public Customer Payer;
        public Customer Payee;
        public double Amount;
        public Guid Id;
        public DateTime Date;
    }
}
