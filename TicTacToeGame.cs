using System;

public class TicTacToeGame
{
    private const int BoardSize = 3;
    private const char EmptyCell = '•';
    private char[,] board = new char[BoardSize, BoardSize];
    private readonly Random random = new Random();

    public void StartGame()
    {
        InitializeBoard();
        PrintBoard();

        while (true)
        {
            Console.WriteLine("Enter row and column separated by a space: ");
            string userInput = Console.ReadLine();

            if (IsValidMove(userInput))
            {
                int userRow = int.Parse(userInput.Split(' ')[0]);
                int userCol = int.Parse(userInput.Split(' ')[1]);

                MakeMove(userRow, userCol, '×');
                PrintBoard();

                if (CheckWin('×'))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nCongratulations! You won!");
                    Console.ResetColor();
                    return;
                }

                if (CheckDraw())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("It's a draw! Better luck next time!");
                    Console.ResetColor();
                    return;
                }

                // Computer's move
                int computerRow, computerCol;
                do
                {
                    computerRow = random.Next(BoardSize);
                    computerCol = random.Next(BoardSize);
                } while (board[computerRow, computerCol] != EmptyCell);

                MakeMove(computerRow, computerCol, '○');
                PrintBoard();

                if (CheckWin('○'))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nComputer wins! Try again!");
                    Console.ResetColor();
                    return;
                }

                if (CheckDraw())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("It's a draw! Better luck next time!");
                    Console.ResetColor();
                    return;
                }
            }
            else
            {
                PrintBoard();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Invalid move! Please try again.");
                Console.ResetColor();
            }
        }
    }

    private void InitializeBoard()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                board[i, j] = EmptyCell;
            }
        }
    }

    private void PrintBoard()
    {
        Console.Clear();
        for (int i = 0; i < BoardSize; i++)
        {
            Console.Write(" ");
            for (int j = 0; j < BoardSize; j++)
            {
                Console.Write(GetCellSymbol(board[i, j]) + " ");
            }
            Console.WriteLine();
        }
    }

    private bool IsValidMove(string userInput)
    {
        return userInput.Split(' ').Length == 2 &&
               int.TryParse(userInput.Split(' ')[0], out int intValue) &&
               int.TryParse(userInput.Split(' ')[1], out int intValue2) &&
               int.Parse(userInput.Split(' ')[0]) >= 0 &&
               int.Parse(userInput.Split(' ')[0]) < BoardSize &&
               int.Parse(userInput.Split(' ')[1]) >= 0 &&
               int.Parse(userInput.Split(' ')[1]) < BoardSize &&
               board[int.Parse(userInput.Split(' ')[0]), int.Parse(userInput.Split(' ')[1])] == EmptyCell;
    }

    private void MakeMove(int row, int col, char symbol)
    {
        board[row, col] = symbol;
    }

    private bool CheckWin(char symbol)
    {
        for (int i = 0; i < BoardSize; i++)
        {
            if (board[i, 0] == symbol && board[i, 1] == symbol && board[i, 2] == symbol)
            {
                return true;
            }
            if (board[0, i] == symbol && board[1, i] == symbol && board[2, i] == symbol)
            {
                return true;
            }
        }

        if (board[0, 0] == symbol && board[1, 1] == symbol && board[2, 2] == symbol)
        {
            return true;
        }

        if (board[0, 2] == symbol && board[1, 1] == symbol && board[2, 0] == symbol)
        {
            return true;
        }

        return false;
    }

    private bool CheckDraw()
    {
        return board.Cast<char>().All(cell => cell != EmptyCell);
    }

    private string GetCellSymbol(char cell)
    {
        if (cell == '×')
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            return cell.ToString();
        }
        else if (cell == '○')
        {
            Console.ForegroundColor = ConsoleColor.Red;
            return cell.ToString();
        }
        else
        {
            Console.ResetColor();
            return cell.ToString();
        }
    }
}
