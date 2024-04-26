using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Car_plates
{
    internal class Program
    {
        static string GeneratePlateNumber(string code)
        {
            Random random = new Random();
            int numbers = random.Next(100, 1000);
            char firstLetter = (char)('A' + random.Next(0, 26));
            char secondLetter = (char)('A' + random.Next(0, 26));

            return $"{code} / {numbers} - {firstLetter}{secondLetter}";
        }

        static void Main(string[] args)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Car_plates;Trusted_Connection=True;MultipleActiveResultSets=true";

            // Dictionary setup as before
            var codesToCities = new Dictionary<string, string[]>
            {
                {"01", new string[] {"prishtine", "podujeve", "lipjan", "drenas"}},
                // Add other codes and cities
            };

            var citiesToCodes = codesToCities
                .SelectMany(pair => pair.Value, (pair, city) => new { City = city, Code = pair.Key })
                .ToDictionary(pair => pair.City, pair => pair.Code);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Connection successful! \n");

                while (true)
                {
                    Console.WriteLine("Shënoni qytetin tuaj: ");
                    string city = Console.ReadLine().ToLower();

                    if (citiesToCodes.TryGetValue(city, out string code))
                    {
                        string plate = GeneratePlateNumber(code);
                        Console.WriteLine("Targat tuaja: " + plate);

                        // Inserting the data into the database
                        string insertQuery = "INSERT INTO Plates (City, Plates) VALUES (@City, @Plates)";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            try
                            {
                                command.Parameters.AddWithValue("@City", city);
                                command.Parameters.AddWithValue("@Plates", plate);

                                int result = command.ExecuteNonQuery(); // This should return the number of rows affected
                                Console.WriteLine($"{result} rows inserted.");
                            }
                            catch (SqlException ex)
                            {
                                Console.WriteLine("SQL Error: " + ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("General Error: " + ex.Message);
                            }
                        }


                        // Display all entries from the database
                        string selectQuery = "SELECT City, Plate FROM Plates";
                        SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"City: {reader["City"]}, Plate: {reader["Plates"]}");
                            }
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Emri i qytetit nuk është dhënë siç duhet! Provoni përsëri. \n");
                    }
                }
            }
        }
    }
}
