using System;
using System.Collections.Generic;
using System.Text;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class PaymentMethod
    {
        public PaymentMethod()
        {
        }

        public PaymentMethod(PaymentMethodType type, User user, BankAccount bankAccount)
        {
            this.Type = type;
            this.User = user;
            this.BankAccount = bankAccount;
        }
        public PaymentMethod(PaymentMethodType type, User user, CreditCard creditCard)
        {
            this.Type = type;
            this.User = user;
            this.CreditCard = creditCard;
        }
        public int Id { get; set; }
        public PaymentMethodType Type { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int? BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        public int? CreditCardId { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}
