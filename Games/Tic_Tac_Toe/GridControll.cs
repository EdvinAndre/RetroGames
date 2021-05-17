using System.Drawing;

namespace Tic_Tac_Toe
{
    public class GridControll
    {
        // Checks if there is any moves left on the game-board.
        public static bool IsMovePossible(char[,] Board)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i, j] == '-')
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Checks who is the winner of the current game.
        /// </summary>
        /// <param name="Board">The game-board to be checked.</param>
        /// <returns>
        /// 10 if the winner is (O);
        /// -10 if the winner is (X);
        /// 0 if it's draw.
        /// </returns>
        public static int CheckWinner(char[,] Board)
        {
            // Check for row winner
            for (int row = 0; row < 3; row++)
            {
                if (Board[row, 0] == Board[row, 1] &&
                    Board[row, 1] == Board[row, 2])
                {
                    if (Board[row, 0] == 'O')
                        return 10;
                    else if (Board[row, 0] == 'X')
                        return -10;
                }
            }

            // Check for collumn winner
            for (int collumn = 0; collumn < 3; collumn++)
            {
                if (Board[0, collumn] == Board[1, collumn] &&
                    Board[1, collumn] == Board[2, collumn])
                {
                    if (Board[0, collumn] == 'O')
                        return 10;
                    else if (Board[0, collumn] == 'X')
                        return -10;
                }
            }

            // Check for diagonal winner
            if (Board[0, 0] == Board[1, 1] && Board[1, 1] == Board[2, 2])
            {
                if (Board[0, 0] == 'O')
                    return 10;
                else if (Board[0, 0] == 'X')
                    return -10;
            }

            // Check for diagonal winner
            if (Board[0, 2] == Board[1, 1] && Board[1, 1] == Board[2, 0])
            {
                if (Board[0, 2] == 'O')
                    return 10;
                else if (Board[0, 2] == 'X')
                    return -10;
            }

            // If the result of the game is an Tie
            return 0;
        }

        // Check if a given point is inside an cell of the game-board.
        public static bool PointInsideCell(Point Point, int X1, int Y1, int X2, int Y2)
        {
            bool InsideRangeX = (int)Point.X > X1 && (int)Point.X < X2;

            bool InsideRangeY = (int)Point.Y > Y1 && (int)Point.Y < Y2;

            return InsideRangeX && InsideRangeY;
        }
    }
}
