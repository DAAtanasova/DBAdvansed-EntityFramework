using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusTicketSystem.Data;
using BusTicketSystem.Models;

namespace BusTicketSystem.Client.Actions.Command
{
   
    public class PublishReviewCommand
    { 
        //publish-review {Customer ID} {Grade} {Bus Company Name} {Content}
        public static string Execute(string[] data)
        {
            int customerId = int.Parse(data[1]);
            decimal grade = decimal.Parse(data[2]);
            string busCompanyName = data[3];
            string content = data.Length == 5 ? data[4] : null;
         
            using(var db = new BusTicketContext())
            {
                var customer = db.Customers.Find(customerId);

                if (customer == null)
                {
                    throw new ArgumentException("No customer with given Id!");
                }

                var busCompany = db.BusCompanies.FirstOrDefault(b => b.Name == busCompanyName);
                if(busCompany == null)
                {
                    throw new ArgumentException("No such Bus Company");
                }

                if(grade < 1.0m || grade> 10.0m)
                {
                    throw new ArgumentException("Invalid grade value");
                }

                var review = new Review
                {
                    BusCompanyId = busCompany.BusCompanyId,
                    Content = content,
                    CustomerId = customerId,
                    Grade = grade
                };
                db.Reviews.Add(review);
                db.SaveChanges();

                return "Successfully added review.";
            }
        }
    }
}
