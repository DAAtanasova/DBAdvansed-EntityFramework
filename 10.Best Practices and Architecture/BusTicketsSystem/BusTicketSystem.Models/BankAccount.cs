using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketSystem.Models
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }

        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }

        public CustomerBankAcc BankAccCustomer { get; set; }
    }
}
