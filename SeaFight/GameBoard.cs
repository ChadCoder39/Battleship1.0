using System;

namespace SeaFight
{
public class GameBoard
{
        public const int BoardSize = 10;
        private char[,] board;
        private bool[,] hasBeenHit;
        private bool[,] shipHit;

        public GameBoard()
        {
            board = new char[BoardSize, BoardSize];
            hasBeenHit = new bool[BoardSize, BoardSize];
            shipHit = new bool[BoardSize, BoardSize];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    board[row, col] = '~';
                    hasBeenHit[row, col] = false;
                    shipHit[row, col] = false;
                }
            }
        }

        public void PrintBoard(bool showShips)
        {
            Console.WriteLine("   A B C D E F G H I J");
            for (int row = 0; row < BoardSize; row++)
            {
                Console.Write($"{row + 1,2} ");
                for (int col = 0; col < BoardSize; col++)
                {
                    char cellToShow = board[row, col];
                    if (showShips && IsShipLocation(row, col))
                    {
                        if (shipHit[row, col])
                            cellToShow = 'x'; // if ship hitted
                        else
                            cellToShow = '#'; // if ship atack was unsuccessfull
                    }
                    else if (!showShips && cellToShow == 'O')
                        cellToShow = '~'; // enemy cant see our sheeps

                    Console.Write(cellToShow + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }


        public bool PlaceShip(int row, int col)
        {
            if (row < 0 || row >= BoardSize || col < 0 || col >= BoardSize)
                return false;

            if (board[row, col] == '~')
            {
                board[row, col] = 'O';
                shipHit[row, col] = false;
                return true;
            }

            return false;
        }

        public void PlacePlayerShips()
        {
            Console.WriteLine("Place your ships on the board");

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Ship {i + 1}:");
                Console.Write("Drop coordinate x (A-J): ");
                char xChar = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (!char.TryParse(xChar.ToString().ToUpper(), out char x) || x < 'A' || x > 'J')
                {
                    Console.WriteLine("Wrong x coordinate.Try again");
                    i--;
                    continue;
                }

                Console.Write("Drop y coordinate (1-10): ");
                if (!int.TryParse(Console.ReadLine(), out int y) || y < 1 || y > 10)
                {
                    Console.WriteLine("Wrong y coordinate.Try again.");
                    i--;
                    continue;
                }

                int row = y - 1;
                int col = x - 'A';

                if (!PlaceShip(row, col))
                {
                    Console.WriteLine("Impossible to place sheep here.Try again.");
                    i--;
                    continue;
                }

                PrintBoard(showShips: true);
            }
        }


        public bool IsShipLocation(int row, int col)
        {
            return board[row, col] == 'O';
        }

        public bool HasBeenHit(int row, int col)
        {
            return hasBeenHit[row, col];
        }

        public void MarkShipAsHit(int row, int col)
        {
            shipHit[row, col] = true;
        }
        public void MarkAsHit(int row, int col)
        {
            hasBeenHit[row, col] = true;
        }
}

}

