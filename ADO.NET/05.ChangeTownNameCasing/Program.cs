using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _05.ChangeTownNameCasing
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var connection = new SqlConnection(
                "Server=;Database=MinionsDB;Integrated Security =");
            connection.Open();

            using (connection)
            {
                if (IsCountry(input, connection))
                {
                    var uppercaseTowns = new List<string>();
                    try
                    {
                        var countryIdQuery = "SELECT Id FROM Countries WHERE Name = @input";
                        var commandCountryId = new SqlCommand(countryIdQuery, connection);
                        commandCountryId.Parameters.AddWithValue("@input", input);
                        int countryId = 0;

                        countryId = (int)commandCountryId.ExecuteScalar();

                        var townsFromCountryQuery = "SELECT Name FROM Towns WHERE CountryId = @countryId";
                        var commandTowns = new SqlCommand(townsFromCountryQuery, connection);
                        commandTowns.Parameters.AddWithValue("@countryId", countryId);
                        var reader = commandTowns.ExecuteReader();

                        while (reader.Read())
                        {
                            var city = (string)reader["Name"];
                            var upperCaseTown = city.ToUpper();
                            uppercaseTowns.Add(upperCaseTown);
                        }
                        reader.Close();
                        Console.WriteLine($"{uppercaseTowns.Count} town names were affected.");
                        Console.WriteLine("[ " + string.Join(",", uppercaseTowns) + " ]");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("No town names were affected.");
                }
            }
        }
        public static bool IsCountry(string countryInput, SqlConnection connection)
        {
            var allCountries = new List<string>();
            var allCountriesQuery = "SELECT Name FROM Countries";
            var command = new SqlCommand(allCountriesQuery, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                string country = (string)reader["Name"];
                allCountries.Add(country);
            }
            reader.Close();

            if (allCountries.Contains(countryInput))
            {
                return true;
            }
            return false;
        }
    }
}
