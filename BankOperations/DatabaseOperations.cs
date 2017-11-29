using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Bank
{
    static class DatabaseOperations
    {
        private const string ConnectionString = "Data Source=192.168.0.1;Initial Catalog=BankDatabase;Integrated Security=SSPI;";

        //This is for part 2 of the question
        public static bool TransferMoneyFromOneAccountToAnother(Transaction transaction)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var getAmountInAccountForPayee = new SqlCommand(
                        "select amountinaccount from Bank.Client where sortcode = '"
                        + transaction.Payee.SortCode + "' and accountnumber = '" + transaction.Payee.AccountNumber +
                        "'");

                    var getAmountInAccountForPayer = new SqlCommand(
                        "select amountinaccount from Bank.Client where sortcode = '"
                        + transaction.Payer.SortCode + "' and accountnumber = '" + transaction.Payer.AccountNumber +
                        "'");

                    connection.Open();

                    var amountInPayer = getAmountInAccountForPayer.ExecuteScalar();
                    var amountInPayee = getAmountInAccountForPayee.ExecuteScalar();

                    if (!(Convert.ToDouble(amountInPayer) < transaction.Amount))
                    {
                        var issuePaymentToPayee = new SqlCommand(
                            "insert into Bank.Accounts (amountinaccount, transactionid, date) values ('" + Convert.ToDouble(amountInPayee) +
                            transaction.Amount + "', '" + transaction.Id + "', '" + transaction.Date + "') where sortcode = '" 
                            + transaction.Payee.SortCode + "' and accountnumber = '" 
                            + transaction.Payer.AccountNumber + "'");
                        using (connection)
                        {
                            issuePaymentToPayee.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        throw new BankSystemException("Insufficient funds into the Payer's account");
                    }

                    connection.Close();

                }
                return true;
            }
            catch (SqlException e)
            {
                throw new BankSystemException("Something went wrong: " + e);
            }
        }


        public static List<Transaction> GetBankAccountTransactionHistory(Customer customer)
        {
            var listOfTransactions = new List<Transaction>();

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    {
                        var command = new SqlCommand(
                            "select * from Bank.Client where sortcode = '"
                            + customer.SortCode + "' and accountnumber = '" + customer.AccountNumber +
                            "'");

                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                                throw new Exception("SQL Reader cannot read");
                            listOfTransactions.Add(new Transaction
                            {
                                Payer = new Customer
                                {
                                    AccountNumber = reader.GetOrdinal("fromaccountnumber"),
                                    SortCode = reader.GetOrdinal(""),
                                    Address = null,
                                    AmountInAccount = null,
                                    FirstName = reader.GetOrdinal("fromfirstname").ToString(),
                                    LastName = reader.GetOrdinal("fromlastname").ToString()

                                },

                                Amount = Convert.ToDouble(reader.GetOrdinal("amountinaccount")),
                                Date = new DateTime(reader.GetOrdinal("transactiondate")),
                                Id = Guid.Parse(reader.GetOrdinal("transactionId").ToString()),

                            });

                            connection.Close();
                        }

                    }

                }
            }
            catch (SqlException e)
            {
                throw new BankSystemException("Something went wrong: " + e);
            }

            return listOfTransactions;
        }
    }
}
