using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketSystem.Models
{
    public class CustomerBankAcc
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}
