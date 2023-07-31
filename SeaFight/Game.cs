using System;


namespace SeaFight
{
    public enum ShipType
    {
        OneDeck = 1,
        TwoDeck = 2,
        ThreeDeck = 3,
        FourDeck = 4
    }
    class Game
    {
        private GameBoard playerBoard;
        private GameBoard computerBoard;

        public Game()
        {
            playerBoard = new GameBoard();
            computerBoard = new GameBoard();
        }

        private void PlaceComputerShips()
        {
            Random random = new Random();
            int shipsToPlace = 5;

            while (shipsToPlace > 0)
            {
                int row = random.Next(0, 10);
                int col = random.Next(0, 10);

                if (computerBoard.PlaceShip(row, col))
                    shipsToPlace--;
            }
        }

        private void PlayerTurn()
        {
            Console.WriteLine("Your turn");
            Console.Write("Write coordinate x (A-J): ");
            char xChar = Console.ReadKey().KeyChar;
            Console.WriteLine();

            if (!char.TryParse(xChar.ToString().ToUpper(), out char x) || x < 'A' || x > 'J')
            {
                Console.WriteLine("Invalid x coordinate.Try again please");
                return;
            }

            Console.Write("Write coordinate y 1-10: ");
            if (!int.TryParse(Console.ReadLine(), out int y) || y < 1 || y > 10)
            {
                Console.WriteLine("Invalid y coordinate.Try again please");
                return;
            }

            int row = y - 1;
            int col = x - 'A';

            if (computerBoard.HasBeenHit(row, col))
            {
                Console.WriteLine("You have already fired at this cell.Write another coordinate");
                return;
            }

            if (computerBoard.IsShipLocation(row, col))
            {
                Console.WriteLine("Hit");
                computerBoard.MarkAsHit(row, col);
                computerBoard.MarkShipAsHit(row, col); //marking hitted ship
            }
            else
            {
                Console.WriteLine("Miss");
                computerBoard.MarkAsHit(row, col);
            }
        }

        private void ComputerTurn()
        {
            Console.WriteLine("Computers turn");
            Random random = new Random();
            int row, col;

            do
            {
                row = random.Next(0, 10);
                col = random.Next(0, 10);
            }
            while (playerBoard.HasBeenHit(row, col));

            if (playerBoard.IsShipLocation(row, col))
            {
                Console.WriteLine("Computer got your ship");
                playerBoard.MarkAsHit(row, col);
                playerBoard.MarkShipAsHit(row, col); // marking ship as hitted
            }
            else
            {
                Console.WriteLine("Computer missed");
                playerBoard.MarkAsHit(row, col);
            }
        }

        public void Start()
        {
            Console.WriteLine("Welcome to SeaFight");
            playerBoard.PlacePlayerShips();
            PlaceComputerShips();

            while (true)
            {
                Console.WriteLine("Your field");
                playerBoard.PrintBoard(showShips: true);

                Console.WriteLine("Computer^s field:");
                computerBoard.PrintBoard(showShips: false);

                PlayerTurn();

                if (CheckGameOver(playerBoard))
                {
                    Console.WriteLine("You won!");
                    break;
                }

                ComputerTurn();

                if (CheckGameOver(computerBoard))
                {
                    Console.WriteLine("Computer won!");
                    break;
                }
            }
        }

        private bool CheckGameOver(GameBoard board)
        {
            for (int row = 0; row < GameBoard.BoardSize; row++)
            {
                for (int col = 0; col < GameBoard.BoardSize; col++)
                {
                    if (board.IsShipLocation(row, col) && !board.HasBeenHit(row, col))
                        return false;
                }
            }
            return true;
        }
    }
}
