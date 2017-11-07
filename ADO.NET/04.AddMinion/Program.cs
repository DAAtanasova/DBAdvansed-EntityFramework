using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class Program
{
    static void Main()
    {
        string[] minionInput = Console.ReadLine().Split();
        string[] villainInput = Console.ReadLine().Split();

        string minionName = minionInput[1];
        int minionAge = int.Parse(minionInput[2]);
        string minionTown = minionInput[3];

        string villain = villainInput[1];

        var connection = new SqlConnection(
            "Server = .;Database = MinionsDB;Integrated Security = true");
        connection.Open();

        using (connection)
        {
            try
            {
                string allTownsQuery = "SELECT Name FROM Towns";
                var cmdAllTowns = new SqlCommand(allTownsQuery, connection);
                var readerAllTowns = cmdAllTowns.ExecuteReader();
                var towns = new List<string>();

                while (readerAllTowns.Read())
                {
                    towns.Add((string)readerAllTowns["Name"]);
                }
                readerAllTowns.Close();

                if (!towns.Contains(minionTown))
                {
                    string insertTownQuery = "INSERT INTO Towns(Name,CountryId) VALUES (@town,1)";
                    var cmdInserTown = new SqlCommand(insertTownQuery, connection);
                    cmdInserTown.Parameters.AddWithValue("@town", minionTown);
                    cmdInserTown.ExecuteNonQuery();
                    Console.WriteLine($"Town {minionTown} was added to the database.");
                }

                string allVillainsQuery = "SELECT Name FROM Villains";
                var cmdAllVillains = new SqlCommand(allVillainsQuery, connection);
                var readerAllVillains = cmdAllVillains.ExecuteReader();
                var villains = new List<string>();

                while (readerAllVillains.Read())
                {
                    villains.Add((string)readerAllVillains["Name"]);
                }
                readerAllVillains.Close();

                if (!villains.Contains(villain))
                {
                    string insertVillainQuery = "INSERT INTO Villains(Name,EvilnessFactorId) VALUES (@villain,4)";
                    var cmdInsertVillain = new SqlCommand(insertVillainQuery, connection);
                    cmdInsertVillain.Parameters.AddWithValue("@villain", villain);
                    cmdInsertVillain.ExecuteNonQuery();
                    Console.WriteLine($"Villain {villain} was added to the database.");
                }

                string minionTownIdQuery = "SELECT Id FROM Towns where Name = @minionTown";
                var cmdMinionTownId = new SqlCommand(minionTownIdQuery, connection);
                cmdMinionTownId.Parameters.AddWithValue("@minionTown", minionTown);
                int townId = (int)cmdMinionTownId.ExecuteScalar();

                string insertMinionQuery = "INSERT INTO Minions(Name,Age,TownId) VALUES (@name,@age,@townId)";
                var cmdInsertMinion = new SqlCommand(insertMinionQuery, connection);
                cmdInsertMinion.Parameters.AddWithValue("@name", minionName);
                cmdInsertMinion.Parameters.AddWithValue("@age", minionAge);
                cmdInsertMinion.Parameters.AddWithValue("@townId", townId);
                cmdInsertMinion.ExecuteNonQuery();

                string minionIdQuery = "SELECT Id FROM Minions WHERE Name = @minionName";
                var cmdMinionId = new SqlCommand(minionIdQuery, connection);
                cmdMinionId.Parameters.AddWithValue("@minionName", minionName);
                int minionId = (int)cmdMinionId.ExecuteScalar();

                string villainIdQuery = "SELECT Id FROM Villains WHERE Name = @villainName";
                var cmdVillainId = new SqlCommand(villainIdQuery, connection);
                cmdVillainId.Parameters.AddWithValue("@villainName", villain);
                int villainId = (int)cmdVillainId.ExecuteScalar();

                string insertVillainMinionsQuery = "INSERT INTO MinionsVillains(MinionId,VillainId) VALUES (@minionId,@VillainId)";
                var cmdInsertVillainMinion = new SqlCommand(insertVillainMinionsQuery, connection);
                cmdInsertVillainMinion.Parameters.AddWithValue("@minionId", minionId);
                cmdInsertVillainMinion.Parameters.AddWithValue("@VillainId", villainId);
                cmdInsertVillainMinion.ExecuteNonQuery();
                Console.WriteLine($"Successfully added {minionName} to be minion of {villain}.");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
