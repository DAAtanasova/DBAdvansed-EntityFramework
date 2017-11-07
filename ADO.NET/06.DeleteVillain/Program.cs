using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class Program
{
    static void Main(string[] args)
    {
        var inputId = int.Parse(Console.ReadLine());

        var connection = new SqlConnection("Server=.;Database=MinionsDB;Integrated Security = true");
        connection.Open();
        using (connection)
        {
            try
            {
                string villainNameQuery = "SELECT Name FROM Villains WHERE Id = @inputId";
                var cmdVillainName = new SqlCommand(villainNameQuery, connection);
                cmdVillainName.Parameters.AddWithValue("@inputId", inputId);
                string villainName = (string)cmdVillainName.ExecuteScalar();
                if (String.IsNullOrWhiteSpace(villainName))
                {
                    
                    throw new ArgumentException("No such villain was found.");
                }

                string deleteVillMinionsQuery = "DELETE FROM MinionsVillains WHERE VillainId = @inputId";
                var cmdDeleteVillMin = new SqlCommand(deleteVillMinionsQuery, connection);
                cmdDeleteVillMin.Parameters.AddWithValue("@inputId", inputId);
                int minionsCount = cmdDeleteVillMin.ExecuteNonQuery();

                string deleteVillainQuery = "DELETE FROM Villains WHERE Id = @inputId";
                var cmdDeleteVillain = new SqlCommand(deleteVillainQuery, connection);
                cmdDeleteVillain.Parameters.AddWithValue("@inputId", inputId);
                cmdDeleteVillain.ExecuteNonQuery();

                Console.WriteLine($"{villainName} was deleted.");
                Console.WriteLine($"{minionsCount} minions was released.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
