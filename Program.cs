using System.Runtime.CompilerServices;

namespace Car_plates
{
    internal class Program
    {
        //here is declared a method that generates the formatted plate number
        static string GeneratePlateNumber(string shifrat)
        {
            Random random = new Random(); //it creates a new random object
            int numrat = random.Next(100, 1000);//it generates a number from 100 to 999
            char shkronjaPare = (char)('A' + random.Next(0, 26)); //this generates a letter from A to Z, 0 is "A", 26 is "Z"
            char shkronjaDyte = (char)('A' + random.Next(0, 26)); // this generates a letter too, in the same way as the first one, for the second letter of the plates 
            
            
            return $"{shifrat} / {numrat} - {shkronjaPare}{shkronjaDyte}";//it returns the format of plate number
        }

        static void Main(string[] args)
        { 
            A:
            Console.WriteLine("Shenoje qytetin: "); //it asks the user to enter the name of the city
            string komuna = Console.ReadLine().ToLower(); //this reads, stores and converts to lowercase the whole string entered by the user
         
            //here is created a dictionary that will map each string with its property that is also string in this case
            var shifrat = new Dictionary<string, string>
            {
                {"prishtine", "01" },
                {"podujeve", "01" },
                {"mitrovice", "02" },
                {"skenderaj", "02" },
                {"vushtrri", "02" },
                {"peje", "03" },
                {"kline", "03" },
                {"prizren", "04" },
                {"suhareke", "04" },
                {"ferizaj", "05" },
                {"shtime", "05" },
                {"gjilan", "06" },
                {"gracanice", "06" },
                {"gjakove", "07" },
                {"malisheve", "07" }
            };

            if (shifrat.ContainsKey(komuna)) //this checks if the entered city by the user is in the dictionary
            {
                string tabelat = GeneratePlateNumber(shifrat[komuna]); //this calls the method above this main program, and generates the formatted plate number
                Console.WriteLine("Targat tuaja: " + tabelat); //this prints the formatted plate number
            }
            else //if the condition above isn't met, then it will print the message below
            {
                Console.WriteLine("Emri i qytetit nuk eshte dhene sic duhet!");
                goto A;
            }


        }
    }
}
