using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;
using BattleShip.UI;

namespace BattleShip.UI
{
    class Workflow
    {
        public Players newPlayers { get; set; }
        private Board _playerToStartBoard;
        private Board _playerToGoSecondBoard;
        private int winnerInt = 0;
        private bool _playerContinue;


        public void WorkFlow()
        {
            Players.TitleScreen();
            do
            {
                Players newPlayers = new Players();

                Console.WriteLine($"{newPlayers.PlayerToStart} will go first");

                Console.WriteLine($"{newPlayers.PlayerToStart}, time to set up your board");

                _playerToStartBoard = SetUpBoard();

                Console.WriteLine($"{newPlayers.PlayerToGoSecond}, time to set up your board");

                _playerToGoSecondBoard = SetUpBoard();

                while (winnerInt == 0)
                {
                    PlayerTurn(_playerToGoSecondBoard, newPlayers.PlayerToStart);
                    PlayerTurn(_playerToStartBoard, newPlayers.PlayerToGoSecond);
                }

                PlayerContinue();

            } while (_playerContinue == true);


        }

        private void PlayerTurn(Board board, string player)
        {
            do
            {

                DisplayMap(board);

                Console.WriteLine($"{player}, choose coordinates to fire on your enemies map!");

                int[] coordinates = Players.GetUserCoordinates();

                Coordinate Coordinate = new Coordinate(coordinates[0], coordinates[1]);

                FireShotResponse yourTurn = board.FireShot(Coordinate);

                if (yourTurn.ShotStatus.Equals(ShotStatus.Invalid))
                {
                    Console.WriteLine("Coordinates were invalid, repeat turn.");
                    continue;
                }
                else if (yourTurn.ShotStatus.Equals(ShotStatus.Duplicate))
                {

                    Console.WriteLine("Shot was a dupliacte, repeat turn.");
                    continue;
                }
                else if (yourTurn.ShotStatus.Equals(ShotStatus.Hit))
                {
                    Console.WriteLine("Direct Hit!");
                    Console.WriteLine("Click any button to go to continue to next turn.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
                else if (yourTurn.ShotStatus.Equals(ShotStatus.HitAndSunk))
                {
                    Console.WriteLine($"Direct Hit and {SunkenShip(Coordinate, board)} was sunk!");
                    Console.WriteLine("Click any button to go to continue to next turn.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
                else if (yourTurn.ShotStatus.Equals(ShotStatus.Miss))
                {
                    Console.WriteLine("Miss!");
                    Console.WriteLine("Click any button to go to continue to next turn.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
                else
                    Console.WriteLine("You have won the game! Congratulations!");
                winnerInt++;
                Console.WriteLine("Click any button to go to continue to next turn.");
                Console.ReadKey();
                Console.Clear();

                break;

            } while (true);

        }

        private Board SetUpBoard()
        {

            Board userBoard = new Board();

            for (int i = 0; i < 5; i++)
            {
                do
                {
                    Console.WriteLine($"You will need to enter starting coordinates and a direction for your {Enum.GetName(typeof(ShipType), i)}.");

                    int[] coordinates = Players.GetUserCoordinates();

                    Console.Write("Enter a direction(Up, Down, Left, Right): ");

                    string direction = Console.ReadLine();

                    PlaceShipRequest request = new PlaceShipRequest()
                    {
                        Coordinate = new Coordinate(coordinates[0], coordinates[1]),
                        Direction = (ShipDirection)Enum.Parse(typeof(ShipDirection), direction, true),
                        ShipType = (ShipType)Enum.ToObject(typeof(ShipType), i)
                    };

                    var response = userBoard.PlaceShip(request);

                    if (response.Equals(ShipPlacement.NotEnoughSpace))
                    {
                        Console.WriteLine($"{response}, please enter again.");
                        continue;
                    }
                    else if (response.Equals(ShipPlacement.Overlap))
                    {
                        Console.WriteLine($"{response}, please enter again.");
                        continue;
                    }
                    else
                        break;

                } while (true);
            }

            Console.Clear();

            return userBoard;
        }

        private void DisplayMap(Board userBoard)
        {
            Console.Clear();

            for (int i = 0; i <= 10; i++)
            {

                Console.Write(" ");

                for (int j = 0; j <= 10; j++)
                {
                    if (i == 0 && j != 0)
                    {
                        Console.Write(j + " ");
                    }
                    else if (i > 0 && j == 0)
                    {
                        Console.Write(Players.possibleLetters[i - 1] + " ");
                    }
                    else
                    {
                        Coordinate checkCoordinate = new Coordinate(i, j);
                        ShotHistory response = userBoard.CheckCoordinate(checkCoordinate);
                        if (response.Equals(ShotHistory.Hit))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("H ");
                            Console.ResetColor();

                        }
                        else if (response.Equals(ShotHistory.Miss))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("M ");
                            Console.ResetColor();
                        }
                        else
                            Console.Write("  ");
                    }

                }
                Console.Write("\n");

            }
        }

        private ShipType SunkenShip(Coordinate coordinate, Board board)
        {
            ShipType ship = new ShipType();
            for (int i = 0; i < board.Ships.Length; i++)
            {
                if (board.Ships[i].BoardPositions.Contains(coordinate) && board.Ships[i].IsSunk)
                {
                    ship = board.Ships[i].ShipType;
                }
                else
                    continue;
            }
            return ship;
        }

        private bool PlayerContinue()
        {
            while (true)
            {
                Console.WriteLine("Would you like to play again? (y/n)");
                string response = Console.ReadLine();
                if (response.ToLower().Equals("y"))
                {
                    _playerContinue = true;
                    return _playerContinue;
                }
                else if (response.ToLower().Equals("n"))
                {
                    _playerContinue = false;
                    return _playerContinue;
                }
                else
                {
                    Console.WriteLine("Improperly formated response. Please try again.");
                    continue;

                }
            }
        }
    }
}


