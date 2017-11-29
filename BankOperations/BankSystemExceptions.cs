using System;

namespace Bank
{
    public class BankSystemException : Exception
    {
        public BankSystemException(string message) : base(message)
        {
        }
    }
}