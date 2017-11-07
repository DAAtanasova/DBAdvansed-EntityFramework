using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

public class Program
{

    static void Main(string[] args)
    {
        var inputId = int.Parse(Console.ReadLine());

        var connection = new SqlConnection(
            "Server=;Database = MinionsDB;Integrated Security = ");
        connection.Open();
        using (connection)
        {
            try
            {
                string villainSQL = "SELECT Name FROM Villains " +
                                    "WHERE Id = @villianId";
                
                var commandVillain = new SqlCommand(villainSQL, connection);
                commandVillain.Parameters.AddWithValue("@villianId", inputId);
                var readerVillain = commandVillain.ExecuteReader();

                if (!readerVillain.HasRows)
                {
                    Console.WriteLine($"No villain with ID {inputId} exists in the database.");
                }

                else
                {
                    while (readerVillain.Read())
                    {
                        var villain = (string)readerVillain["Name"];
                        Console.WriteLine($"Villain: {villain}");
                    }
                    readerVillain.Close();

                    string minionsSQL = "SELECT Name, Age FROM Minions as m " +
                                        "INNER JOIN MinionsVillains AS MV ON MV.MinionId = M.Id " +
                                        "WHERE MV.VillainId = @villianId";
                    var commandMinions = new SqlCommand(minionsSQL, connection);
                    commandMinions.Parameters.AddWithValue("@villianId", inputId);
                    var readerMininons = commandMinions.ExecuteReader();
                    int counter = 1;

                    if (!readerMininons.HasRows)
                    {
                        Console.WriteLine("(no minions)");
                    }
                    else
                    {
                        while (readerMininons.Read())
                        {

                            var minionName = (string)readerMininons["Name"];
                            var minionAge = (int)readerMininons["Age"];
                            Console.WriteLine($"{counter}. {minionName} {minionAge}");
                            counter++;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
