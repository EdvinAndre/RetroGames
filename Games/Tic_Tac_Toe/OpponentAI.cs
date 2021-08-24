using System;
using System.Drawing;
using RetroSharp;

namespace Tic_Tac_Toe
{
    public class Opponent
    {
        /// <summary>
        /// Finds the best possible move for the opponent.
        /// </summary>
        /// <param name="Board">The game-board to be checked.</param>
        /// <returns>The board after the move.</returns>
        public static char[,] OpponentMove(char[,] Board)
        {
            int Best = -1000;
            int BestMoveRow = -1, BestMoveCol = -1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i, j] == '-')
                    {
                        char[,] TestBoard = Board.Clone() as char[,];
                        TestBoard[i, j] = 'O';
                        int MoveVal = MiniMax(TestBoard, 0, false);

                        if (MoveVal > Best)
                        {
                            BestMoveRow = i;
                            BestMoveCol = j;
                            Best = MoveVal;
                        }

                    }
                }
            }

            Board[BestMoveRow, BestMoveCol] = 'O';

            if (BestMoveRow == 2 && BestMoveCol == 0)
            {
                RetroApplication.DrawEllipse(220, 340, 40, 40, Color.White);
                RetroApplication.DrawEllipse(220, 340, 39, 39, Color.White);
                RetroApplication.DrawEllipse(220, 340, 38, 38, Color.White);
            }
            else if (BestMoveRow == 1 && BestMoveCol == 0)
            {
                RetroApplication.DrawEllipse(220, 240, 40, 40, Color.White);
                RetroApplication.DrawEllipse(220, 240, 39, 39, Color.White);
                RetroApplication.DrawEllipse(220, 240, 38, 38, Color.White);
            }
            else if (BestMoveRow == 0 && BestMoveCol == 0)
            {
                RetroApplication.DrawEllipse(220, 140, 40, 40, Color.White);
                RetroApplication.DrawEllipse(220, 140, 39, 39, Color.White);
                RetroApplication.DrawEllipse(220, 140, 38, 38, Color.White);
            }
            else if (BestMoveRow == 2 && BestMoveCol == 1)
            {
                RetroApplication.DrawEllipse(320, 340, 40, 40, Color.White);
                RetroApplication.DrawEllipse(320, 340, 39, 39, Color.White);
                RetroApplication.DrawEllipse(320, 340, 38, 38, Color.White);
            }
            else if (BestMoveRow == 1 && BestMoveCol == 1)
            {
                RetroApplication.DrawEllipse(320, 240, 40, 40, Color.White);
                RetroApplication.DrawEllipse(320, 240, 39, 39, Color.White);
                RetroApplication.DrawEllipse(320, 240, 38, 38, Color.White);
            }
            else if (BestMoveRow == 0 && BestMoveCol == 1)
            {
                RetroApplication.DrawEllipse(320, 140, 40, 40, Color.White);
                RetroApplication.DrawEllipse(320, 140, 39, 39, Color.White);
                RetroApplication.DrawEllipse(320, 140, 38, 38, Color.White);
            }
            else if (BestMoveRow == 2 && BestMoveCol == 2)
            {
                RetroApplication.DrawEllipse(420, 340, 40, 40, Color.White);
                RetroApplication.DrawEllipse(420, 340, 39, 39, Color.White);
                RetroApplication.DrawEllipse(420, 340, 38, 38, Color.White);
            }
            else if (BestMoveRow == 1 && BestMoveCol == 2)
            {
                RetroApplication.DrawEllipse(420, 240, 40, 40, Color.White);
                RetroApplication.DrawEllipse(420, 240, 39, 39, Color.White);
                RetroApplication.DrawEllipse(420, 240, 38, 38, Color.White);
            }
            else if (BestMoveRow == 0 && BestMoveCol == 2)
            {
                RetroApplication.DrawEllipse(420, 140, 40, 40, Color.White);
                RetroApplication.DrawEllipse(420, 140, 39, 39, Color.White);
                RetroApplication.DrawEllipse(420, 140, 38, 38, Color.White);
            }

            return Board;
        }

        /// <summary>
        /// The minimax function considers all the possible paths of the game.
        /// </summary>
        /// <param name="Board">The game-board to be checked.</param>
        /// <param name="Depth">The depth of the game-board.</param>
        /// <param name="IsMaximizing">Whether to maximize or not.</param>
        /// <returns>The value of the board.</returns>
        private static int MiniMax(char[,] Board, int Depth, bool IsMaximizing)
        {
            // If the opponent has won the game.
            if (GridControll.CheckWinner(Board) == 10)
            {
                return 10;
            }
            else if (GridControll.CheckWinner(Board) == -10)
            {
                return -10;
            }
            
            // If there is any possible moves left.
            if (GridControll.IsMovePossible(Board) == false)
            {
                return 0;
            }

            if (IsMaximizing)
            {
                int Best = -1000;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Board[i, j] == '-')
                        {
                            char[,] TestBoard = Board.Clone() as char[,];
                            // Try the move
                            TestBoard[i, j] = 'O';
                            Best = Math.Max(Best, MiniMax(TestBoard, Depth + 1, false));
                        }
                    }
                }

                return Best;
            }
            else
            {
                int Best = 1000;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Board[i, j] == '-')
                        {
                            char[,] TestBoard = Board.Clone() as char[,];
                            // Try the move
                            TestBoard[i, j] = 'X';
                            Best = Math.Min(Best, MiniMax(TestBoard, Depth + 1, true));
                        }
                    }
                }

                return Best;
            }
        }
    }
}
