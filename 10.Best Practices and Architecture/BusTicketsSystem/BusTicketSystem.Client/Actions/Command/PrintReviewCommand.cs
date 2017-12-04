using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusTicketSystem.Data;

namespace BusTicketSystem.Client.Actions.Command
{
    public class PrintReviewCommand
    {
        //print-reviews {Bus Company ID}
        public static string Execute(string[] data)
        {
            int busCompanyId = int.Parse(data[1]);

            using(var db = new BusTicketContext())
            {
                var busCompany = db.BusCompanies.Find(busCompanyId);
                if(busCompany == null)
                {
                    throw new ArgumentException("No Bus Company with given Id!");
                }

                var reviews = db.Reviews
                    .Where(b => b.BusCompanyId == busCompanyId)
                    .Select(r => new
                    {
                        Id = r.ReviewId,
                        Grade = r.Grade,
                        PublishedOn = r.PublishedOn,
                        Customer = r.Customer.FirstName + " " + r.Customer.LastName,
                        content = r.Content
                    }).ToList();

                if(reviews.Count == 0)
                {
                    return "[no reviews]";
                }

                var sb = new StringBuilder();
                sb.AppendLine($"BusCompany:{busCompany.Name}");
                foreach (var rev in reviews)
                {
                    var content = rev.content == null ? "[no content]" : rev.content;
                    sb.AppendLine($"ID: {rev.Id} Grade: {rev.Grade} Date: {rev.PublishedOn}");
                    sb.AppendLine($"Customer: {rev.Customer}");
                    sb.AppendLine($"Content:{content}");
                    sb.AppendLine();
                }

                return sb.ToString().Trim();
            }
        }
    }
}
