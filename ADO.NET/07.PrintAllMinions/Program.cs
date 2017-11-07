using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class Program
{
    static void Main(string[] args)
    {
        var connection = new SqlConnection("Server=;Database=minionsDB;Integrated Security ");
        connection.Open();

        using (connection)
        {
            try
            {
                var command = new SqlCommand("SELECT Name FROM Minions", connection);
                var reader = command.ExecuteReader();
                var minions = new List<string>();
                using (reader)
                {
                    try
                    {
                        while (reader.Read())
                        {
                            string minion = (string)reader[0];
                            minions.Add(minion);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                for (int i = 0; i < minions.Count / 2; i++)
                {
                    Console.WriteLine(minions[i]);
                    Console.WriteLine(minions[minions.Count - 1 - i]);
                }
                if (minions.Count % 2 == 1)
                {
                    Console.WriteLine(minions[minions.Count / 2]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
