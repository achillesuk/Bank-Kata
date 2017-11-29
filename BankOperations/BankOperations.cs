using System;

namespace Bank
{
    public static class BankOperations
    {
        public static string TransferMoneyFromOneBankAccountToAnother(Customer payer, Customer payee, double amount)
        {
            var transaction = new Transaction
            {
                Payer = payer,
                Payee = payee,
                Amount = amount,
                Date = new DateTime(),
                Id = new Guid()
            };

            try
            {
                if (Pay(transaction))
                    return "The operation completed successfully";
                else
                    return "The operation failed";
            }
            catch (Exception e)
            {
                throw new BankSystemException("Something went wrong with the transfer " + e);
            }
        }


        public static bool Pay(Transaction transaction)
        {
            return DatabaseOperations.TransferMoneyFromOneAccountToAnother(transaction);
        }
    }
    
}

