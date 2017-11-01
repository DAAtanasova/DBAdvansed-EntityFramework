using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    static void Main()
    {
        try
        {

            var buyers = new List<Person>();
            var allProducts = new List<Product>();

            var peopleMoney = Console.ReadLine().Split(new[] { ';', '=' }, StringSplitOptions.RemoveEmptyEntries);
            var productsPrices = Console.ReadLine().Split(new[] { ';', '=' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < peopleMoney.Length; i += 2)
            {
                var name = peopleMoney[i];
                    var money = decimal.Parse(peopleMoney[i + 1]);
                    var person = new Person(name, money);
                    buyers.Add(person);
                }

                for (int i = 0; i < productsPrices.Length; i += 2)
                {
                    var productName = productsPrices[i];
                    var price = decimal.Parse(productsPrices[i + 1]);
                    var product = new Product(productName, price);
                    allProducts.Add(product);
                }

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                var inputArgs = input.Split(new[] {' '});
                string buyerName = inputArgs[0];
                string productName = inputArgs[1];
                Person buyer = buyers.FirstOrDefault(b => b.Name == buyerName);
                Product buyProduct = allProducts.FirstOrDefault(p => p.Name == productName);
                buyer.AddProduct(buyProduct);
            }

            foreach (var person in buyers)
            {
                Console.WriteLine(person);
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
