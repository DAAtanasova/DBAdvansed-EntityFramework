using System;
using System.Data.SqlClient;

namespace _01.CreateDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                ["Data Source"] = ,
                ["Integrated Security"] =
            };

            SqlConnection connection = new SqlConnection(builder.ToString());
            connection.Open();
            using (connection)
            {
                try
                {
                    SqlCommand cmdDB = new SqlCommand("CREATE DATABASE MinionsDB", connection);
                    cmdDB.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            builder["Initial catalog"] = "MinionsDB";
            connection = new SqlConnection(builder.ToString());
            connection.Open();
            using (connection)
            {
                try
                {
                    string sqlCreateTableCountries =
                         "CREATE TABLE Countries(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50))";
                    string sqlCreateTableTowns =
                        "CREATE TABLE Towns(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50),CountryId INT FOREIGN KEY REFERENCES Countries(Id) NOT NULL)";
                    string sqlCreateTableMinions =
                        "CREATE TABLE Minions(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50),Age INT,TownId INT FOREIGN KEY REFERENCES Towns(Id))";
                    string sqlCreateTableEvilnessFactors =
                        "CREATE TABLE EvilnessFactors(Id INT PRIMARY KEY ,Name VARCHAR(10) UNIQUE NOT NULL)";
                    string sqlCreateTableVillains =
                        "CREATE TABLE Villains(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50),EvilnessFactorId INT FOREIGN KEY REFERENCES EvilnessFactors(Id))";
                    string sqlCreateTableMinionsVillains =
                        "CREATE TABLE MinionsVillains(MinionId INT FOREIGN KEY REFERENCES Minions(Id),VillainId INT FOREIGN KEY REFERENCES Villains(Id))";

                    ExecuteCommand(sqlCreateTableCountries, connection);
                    ExecuteCommand(sqlCreateTableTowns, connection);
                    ExecuteCommand(sqlCreateTableMinions, connection);
                    ExecuteCommand(sqlCreateTableEvilnessFactors, connection);
                    ExecuteCommand(sqlCreateTableVillains, connection);
                    ExecuteCommand(sqlCreateTableMinionsVillains, connection);

                    string insertCountriesSQL =
                        "INSERT INTO Countries VALUES ('Bulgaria'), ('United Kingdom'), ('United States of America'), ('France')";
                    string insertTownsSQL =
                        "INSERT INTO Towns (Name, CountryId) VALUES ('Sofia',1), ('Burgas',1), ('Varna', 1), ('London', 2),('Liverpool', 2),('Ocean City', 3),('Paris', 4)";
                    string insertMinionsSQL =
                        "INSERT INTO Minions (Name, Age, TownId) VALUES ('bob',10,1),('kevin',12,2),('stuart',9,3), ('rob',22,3), ('michael',5,2),('pep',3,2)";
                    string insertEvilnessFactorsSQL =
                        "INSERT INTO EvilnessFactors VALUES (1, 'Super Good'), (2, 'Good'), (3, 'Bad'), (4, 'Evil'), (5, 'Super Evil')";
                    string insertVillainsSQL =
                        "INSERT INTO Villains (Name, EvilnessFactorId) VALUES ('Gru', 2),('Victor', 4),('Simon Cat', 3),('Pusheen', 1),('Mammal', 5)";
                    string insertMinionsVillainsSQL =
                        "INSERT INTO MinionsVillains VALUES (1, 2), (3, 1), (1, 3), (3, 3), (4, 1), (2, 2), (1, 1), (3, 4), (1, 4), (1, 5), (5, 1)";

                    ExecuteCommand(insertCountriesSQL, connection);
                    ExecuteCommand(insertTownsSQL, connection);
                    ExecuteCommand(insertMinionsSQL, connection);
                    ExecuteCommand(insertEvilnessFactorsSQL, connection);
                    ExecuteCommand(insertVillainsSQL, connection);
                    ExecuteCommand(insertMinionsVillainsSQL, connection);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public static void ExecuteCommand(string cmdText, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(cmdText, connection);
            command.ExecuteNonQuery();
        }
    }
}
