using FlooringOrderingSystem.Models.Responses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderingSystem.UI.Helpers
{
    public class Helpers
    {
        public static DateTime GetOrderDate(string prompt)
        {
            DateTime temp;

            while(true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if(!DateTime.TryParseExact(input, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out temp))
                {
                    Console.WriteLine("You must enter a valid date.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    return temp;
                }
            }
        }
        public static string GetRequiredStringFromUser(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input cannot be empty or null.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    return input;
                }
            }
        }

        public static decimal GetRequiredDecimalFromUser(string prompt)
        {
            decimal output;

            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!decimal.TryParse(input, out output))
                {
                    Console.WriteLine("You must enter valid decimal.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    if (output < 100)
                    {
                        Console.WriteLine("Minumum order size is 100 square feet");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }

                    return output;
                }
            }
        }
        public static string GetValidatedNameFromUser(string prompt)
        {
            string customerName;

            while (true)
            {
                string name = GetRequiredStringFromUser(prompt);
                bool validName = AddOrder.CheckNameValidity(name);
                if (validName == true)
                {
                    customerName = name;
                    break;
                }
            }

            return customerName;
        }

        public static string GetYesNoAnswerFromUser(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + " (Y/N)? ");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("You must enter Y/N.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    if (input != "Y" && input != "N")
                    {
                        Console.WriteLine("You must enter Y/N.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }

                    return input;
                }
            }
        }
    }
}
