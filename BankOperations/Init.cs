namespace Bank
{
    class Init
    {
        public string Start()
        {
            var payer = new Customer{
                FirstName = "Achilles",
                LastName = "Chatzianastassiou",
                SortCode = 909090,
                AccountNumber = 12345678,
                AmountInAccount = 1000,
                Address = "Foo street"
            };
            var payee = new Customer
            {
                FirstName = "Noob",
                LastName = "Zor",
                SortCode= 121212,
                AccountNumber = 87654321,
                AmountInAccount = 5000,
                Address = "Bar street"
            };
          
            double transactionAmount = 1000.00;

            //This is for part 3 of the question
            var transactionHistory = DatabaseOperations.GetBankAccountTransactionHistory(payer);

            //This is for part 1 of the question
            return BankOperations.TransferMoneyFromOneBankAccountToAnother(payer, payee, transactionAmount);

        }

    }

}
