using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShip.UI
{
    public class Players
    {
        private readonly string _userName1;
        private readonly string _userName2;

        private readonly Random rnd = new Random();
        private readonly int _index;
      

        

        public static readonly string[] possibleLetters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

        private string _playerToStart;
        private string _playerToGoSecond;

        public string PlayerToStart
        {
            get { return _playerToStart; }
           
        }
        public string PlayerToGoSecond
        {
            get { return _playerToGoSecond; }
            
        }

        private int userNumber = 1;

        public Players()
        {
            _index = rnd.Next(2);

            do
            {
                if (userNumber == 1)
                {
                    Console.Write("Enter first player's name: ");
                    _userName1 = Console.ReadLine();
                    userNumber++;

                }
                else
                {
                    Console.Write("Enter second player's name: ");
                    _userName2 = Console.ReadLine();
                    userNumber++;
                }

            } while (userNumber <= 2);

            GetFirstPlayer();
            GetSecondPlayer();


        }
        
        private string GetFirstPlayer()
        {
            if (_index == 0)
            {

                _playerToStart = _userName1;
            }

            else
            { 

                _playerToStart = _userName2;
                
            }

            return _playerToStart;
        }
        private string GetSecondPlayer()
        {
            if (_index == 0)
            {

                _playerToGoSecond = _userName2;
            }

            else
            {

                _playerToGoSecond = _userName1;

            }

            return _playerToGoSecond;
        }
        

        public static int[] GetUserCoordinates()
        {
            

            while (true)
            {
                Console.WriteLine("Enter coordinates (Rows are A-J, and Columns are 1-10)");

                string coordinates = Console.ReadLine();
                int[] finalCoordinates = new int[2];


                finalCoordinates[0] = 0;
                finalCoordinates[1] = 0;

                for (int i = 0; i < possibleLetters.Length; i++)
                {
                    if (coordinates.ToUpper().StartsWith(possibleLetters[i]))
                    {
                        finalCoordinates[0] = i + 1;
                        break;
                    }

                    
                }

                Int32.TryParse(coordinates.Substring(1), out finalCoordinates[1]);
               

                if (finalCoordinates[0] >= 1 && finalCoordinates[1] >=1 && finalCoordinates[0] <= 10 && finalCoordinates[1] <= 10)
                {
                    return finalCoordinates;
                }

                else
                {
                    Console.WriteLine("Coordinates were not formatted correctly, try again!");
                    continue;
                }
            }
        }

        public static void TitleScreen()
        {
            int waveCheck = 0;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("~");
                waveCheck++;

            } while (waveCheck < 1000);

            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to BattleShip!");
            Console.ForegroundColor = ConsoleColor.Blue;

            do
            {
                Console.Write("~");
                waveCheck++;

            } while (waveCheck < 2000);

            Console.ResetColor();
            Console.WriteLine("Press any key to Continue");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
