using FlooringOrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderingSystem
{
    public class AddOrder
    {
        public static bool CheckDateValidity(DateTime date)
        {
            if(date > DateTime.Today)
            {
                return true;
            }

            Console.WriteLine("Date must be in the future!");
            return false;
        }

        public static bool CheckNameValidity(string name)
        {
            char[] validCharacters = new char[] { 'a', 'b', 'c', 'd', 'e', 'f','g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '.', ',' };
            char[] characters = name.ToLower().ToCharArray();
            foreach(char character in characters)
            {
                if(!validCharacters.Contains(character))
                {
                    return false;
                }
                else
                {
                    continue;
                }
            }
            return true;
        }

        


    }
}
