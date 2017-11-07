using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

    public class Program
    {
        static void Main()
        {
            var connection = new SqlConnection(
    "Server = .;Database=MinionsDB;Integrated Security = true");
            connection.Open();
            using (connection)
            {
                var allVillains = new List<Villains>();
                var command = new SqlCommand(@"SELECT V.Name, COUNT(VillainId) as Count FROM MinionsVillains AS MV
INNER JOIN Villains AS V ON V.Id = MV.VillainId
GROUP BY V.Name", connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var name = (string)reader["Name"];
                    var minionsCount =(int)reader["Count"];
                    Villains villain = new Villains()
                    {
                        Name = name,
                        MinionsCount = minionsCount
                    };
                    allVillains.Add(villain);
                }
                var resultVillains = allVillains
                    .Where(v => v.MinionsCount >= 2)
                    .OrderByDescending(m => m.MinionsCount)
                    .ToList();

                foreach (var villain in resultVillains)
                {
                    Console.WriteLine(villain);
                }
               
            }
        }
    }
}
