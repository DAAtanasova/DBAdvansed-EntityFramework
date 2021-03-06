﻿using System.Collections.Generic;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class User
    {
        public User()
        {
        }
        public User(string firstName, string lastName, string email, string pass)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Password = pass;
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<PaymentMethod> PaymentMethods { get; set; } = new HashSet<PaymentMethod>();
    }
}
