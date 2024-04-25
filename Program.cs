using System;
using System.Collections.Generic;
using System.Linq;

namespace Car_plates
{
    internal class Program
    {
        //here is declared a method to generate the formatted plate number
        static string GeneratePlateNumber(string code)
        {
            Random random = new Random(); //here is created a new random object
            int numbers = random.Next(100, 1000); //this generates a number from 100 to 1000
            char firstLetter = (char)('A' + random.Next(0, 26)); //this generates a letter from "A" to "Z"
            char secondLetter = (char)('A' + random.Next(0, 26)); //this does the same thing as above line, generates a letter from "A" to "Z"

            return $"{code} / {numbers} - {firstLetter}{secondLetter}"; //this returns the formatted plate number
        }

        static void Main(string[] args)
        {
            //here is created a dictionary to map city codes to city names, using arrays to map multiple cities in a city code
            var codesToCities = new Dictionary<string, string[]>
            {
                {"01", new string[] {"prishtine", "podujeve", "lipjan", "drenas"}},
                {"02", new string[] {"mitrovice", "skenderaj", "vushtrri"}},
                {"03", new string[] {"peje", "kline", "istog", "decan", "junik"}},
                {"04", new string[] {"prizren", "suhareke"}},
                {"05", new string[] {"ferizaj", "shtime"}},
                {"06", new string[] {"gjilan", "gracanice"}},
                {"07", new string[] {"gjakove", "malisheve"}}
            };

            //this inverts the dictionary to map from city names to codes
            var citiesToCodes = codesToCities
                .SelectMany(pair => pair.Value, (pair, city) => new { City = city, Code = pair.Key })
                .ToDictionary(pair => pair.City, pair => pair.Code);

            while (true)
            {
                Console.WriteLine("Shënoni qytetin tuaj: "); //it prompts the user to enter the city name
                string city = Console.ReadLine().ToLower(); //it reads the input and converts it to lowercase

                if (citiesToCodes.TryGetValue(city, out string code))
                {
                    string plate = GeneratePlateNumber(code); //this generates the formatted plate number
                    Console.WriteLine("Targat tuaja: " + plate); //it displays the plate number on console
                    break;
                }
                else //if the condition above isn't met, then it prints the message below
                {
                    Console.WriteLine("Emri i qytetit nuk është dhënë siç duhet! Provoni përsëri.");
                }
            }
        }
    }
}
