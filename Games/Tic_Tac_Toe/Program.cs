using System.Drawing;
using RetroSharp;

namespace Tic_Tac_Toe
{
    [RasterGraphics(640, 480)]
    [ScreenBorder(30, 20, KnownColor.DimGray)]
    [AspectRatio(4, 3)]
    [Characters(80, 25, KnownColor.White)]
    public class Program : RetroApplication
    {
        static void Main(string[] args)
        {
            Initialize();

            OpenConsoleWindow(4, 4, 66, 25, "Welcome to Tic Tac Toe!");

            WriteLine("Controlls in the game:");
            WriteLine();
            WriteLine("Move the mouse to controll pointer on the screen.");
            WriteLine("Press the left mouse button while inside an cell to place an X.");
            WriteLine("Press Q to restart the current game.");
            WriteLine("Press ESC to close the application.");
            WriteLine();
            WriteLine("To play, press ENTER.");

            ReadLine();
            ClearConsoleWindowArea();
            Clear();

            // Draw board
            FillRectangle(170, 189, 470, 191, Color.White);
            FillRectangle(170, 289, 470, 291, Color.White);
            FillRectangle(269, 90, 271, 390, Color.White);
            FillRectangle(170, 389, 470, 391, Color.White);
            FillRectangle(170, 89, 470, 91, Color.White);
            FillRectangle(169, 90, 171, 390, Color.White);
            FillRectangle(369, 90, 371, 390, Color.White);
            FillRectangle(469, 90, 471, 390, Color.White);

            int MousePointerTexture = AddSpriteTexture(GetResourceBitmap("MouseCoursor.png"), System.Drawing.Color.FromArgb(0, 0, 255), true);
            bool GameOver = false;
            bool Done = false;
            Point Point = GetMousePointer();
            Point MouseDownPoint;
            Sprite MousePointer = CreateSprite(Point.X, Point.Y, MousePointerTexture);
            char[,] Grid = new char[,] {{'-', '-', '-'},
                                        {'-', '-', '-'},
                                        {'-', '-', '-'}};

            OnKeyPressed += (sender, e) =>
            {
                switch (e.Character)
                {
                    case '\x1b':
                        Done = true;
                        break;

                    case 'q':
                    case 'Q':
                        GameOver = true;
                        break;
                }
            };

            OnUpdateModel += (sender, e) => 
            {
                if (!GridControll.IsMovePossible(Grid) || GridControll.CheckWinner(Grid) != 0)
                    GameOver = true;
            };

            OnMouseMove += (sender, e) =>
            {
                Point = e.Position;
                MousePointer.SetPosition(Point);
            };

            OnMouseDown += (sender, e) =>
            {
                if (GameOver)
                    return;

                MouseDownPoint = e.Position;
                
                // Check if mouse is down inside an cell and if that cell is empty
                if (GridControll.PointInsideCell(MouseDownPoint, 170, 290, 270, 390) && Grid[2, 0] == '-')
                {
                    Grid[2, 0] = 'X';
                    DrawLine(180, 380, 260, 300, Color.White);
                    DrawLine(181, 380, 261, 300, Color.White);
                    DrawLine(179, 380, 259, 300, Color.White);
                    DrawLine(260, 380, 180, 300, Color.White);
                    DrawLine(261, 380, 181, 300, Color.White);
                    DrawLine(259, 380, 179, 300, Color.White);
                    Grid = Opponent.OpponentMove(Grid);
                }
                else if (GridControll.PointInsideCell(MouseDownPoint, 170, 190, 270, 290) && Grid[1, 0] == '-')
                {
                    Grid[1, 0] = 'X';
                    DrawLine(180, 280, 260, 200, Color.White);
                    DrawLine(181, 280, 261, 200, Color.White);
                    DrawLine(179, 280, 259, 200, Color.White);
                    DrawLine(260, 280, 180, 200, Color.White);
                    DrawLine(261, 280, 181, 200, Color.White);
                    DrawLine(259, 280, 179, 200, Color.White);
                    Grid = Opponent.OpponentMove(Grid);
                }
                else if (GridControll.PointInsideCell(MouseDownPoint, 170, 90, 270, 190) && Grid[0, 0] == '-')
                {
                    Grid[0, 0] = 'X';
                    DrawLine(180, 180, 260, 100, Color.White);
                    DrawLine(181, 180, 261, 100, Color.White);
                    DrawLine(179, 180, 259, 100, Color.White);
                    DrawLine(260, 180, 180, 100, Color.White);
                    DrawLine(261, 180, 181, 100, Color.White);
                    DrawLine(259, 180, 179, 100, Color.White);
                    Grid = Opponent.OpponentMove(Grid);
                }
                else if (GridControll.PointInsideCell(MouseDownPoint, 270, 290, 370, 390) && Grid[2, 1] == '-')
                {
                    Grid[2, 1] = 'X';
                    DrawLine(280, 380, 360, 300, Color.White);
                    DrawLine(281, 380, 361, 300, Color.White);
                    DrawLine(279, 380, 359, 300, Color.White);
                    DrawLine(360, 380, 280, 300, Color.White);
                    DrawLine(361, 380, 281, 300, Color.White);
                    DrawLine(359, 380, 279, 300, Color.White);
                    Grid = Opponent.OpponentMove(Grid);
                }
                else if (GridControll.PointInsideCell(MouseDownPoint, 270, 190, 370, 290) && Grid[1, 1] == '-')
                {
                    Grid[1, 1] = 'X';
                    DrawLine(280, 280, 360, 200, Color.White);
                    DrawLine(281, 280, 361, 200, Color.White);
                    DrawLine(279, 280, 359, 200, Color.White);
                    DrawLine(360, 280, 280, 200, Color.White);
                    DrawLine(361, 280, 281, 200, Color.White);
                    DrawLine(359, 280, 279, 200, Color.White);
                    Grid = Opponent.OpponentMove(Grid);
                }
                else if (GridControll.PointInsideCell(MouseDownPoint, 270, 90, 370, 190) && Grid[0, 1] == '-')
                {
                    Grid[0, 1] = 'X';
                    DrawLine(280, 180, 360, 100, Color.White);
                    DrawLine(281, 180, 361, 100, Color.White);
                    DrawLine(279, 180, 359, 100, Color.White);
                    DrawLine(360, 180, 280, 100, Color.White);
                    DrawLine(361, 180, 281, 100, Color.White);
                    DrawLine(359, 180, 279, 100, Color.White);
                    Grid = Opponent.OpponentMove(Grid);
                }
                else if (GridControll.PointInsideCell(MouseDownPoint, 370, 290, 470, 390) && Grid[2, 2] == '-')
                {
                    Grid[2, 2] = 'X';
                    DrawLine(380, 380, 460, 300, Color.White);
                    DrawLine(381, 380, 461, 300, Color.White);
                    DrawLine(379, 380, 459, 300, Color.White);
                    DrawLine(460, 380, 380, 300, Color.White);
                    DrawLine(461, 380, 381, 300, Color.White);
                    DrawLine(459, 380, 379, 300, Color.White);
                    Grid = Opponent.OpponentMove(Grid);
                }
                else if (GridControll.PointInsideCell(MouseDownPoint, 370, 190, 470, 290) && Grid[1, 2] == '-')
                {
                    Grid[1, 2] = 'X';
                    DrawLine(380, 280, 460, 200, Color.White);
                    DrawLine(381, 280, 461, 200, Color.White);
                    DrawLine(379, 280, 459, 200, Color.White);
                    DrawLine(460, 280, 380, 200, Color.White);
                    DrawLine(461, 280, 381, 200, Color.White);
                    DrawLine(459, 280, 379, 200, Color.White);
                    Grid = Opponent.OpponentMove(Grid);
                }
                else if (GridControll.PointInsideCell(MouseDownPoint, 370, 90, 470, 190) && Grid[0, 2] == '-')
                {
                    Grid[0, 2] = 'X';
                    DrawLine(380, 180, 460, 100, Color.White);
                    DrawLine(381, 180, 461, 100, Color.White);
                    DrawLine(379, 180, 459, 100, Color.White);
                    DrawLine(460, 180, 380, 100, Color.White);
                    DrawLine(461, 180, 381, 100, Color.White);
                    DrawLine(459, 180, 379, 100, Color.White);
                    Grid = Opponent.OpponentMove(Grid);
                }
            };

            while (!Done)
            {
                while (!Done && !GameOver)
                {
                    Sleep(1000);
                }

                if (GameOver)
                {
                    // Check who is the winner of the game
                    switch (GridControll.CheckWinner(Grid))
                    {
                        case 10:
                            ClearRaster();
                            OpenConsoleWindow(4, 4, 66, 25, "Game Over!");
                            WriteLine();
                            WriteLine("Your opponent (O) is the winner of the game!");
                            WriteLine();
                            WriteLine("To close application, press ESC");
                            WriteLine("To play again, press ENTER.");

                            ReadLine();
                            ClearConsoleWindowArea();
                            Clear();

                            GameOver = false;
                            Grid = new char[,] {{'-', '-', '-'},
                                                {'-', '-', '-'},
                                                {'-', '-', '-'}};

                            FillRectangle(170, 189, 470, 191, Color.White);
                            FillRectangle(170, 289, 470, 291, Color.White);
                            FillRectangle(269, 90, 271, 390, Color.White);
                            FillRectangle(170, 389, 470, 391, Color.White);
                            FillRectangle(170, 89, 470, 91, Color.White);
                            FillRectangle(169, 90, 171, 390, Color.White);
                            FillRectangle(369, 90, 371, 390, Color.White);
                            FillRectangle(469, 90, 471, 390, Color.White);
                            break;

                        case -10:
                            ClearRaster();
                            OpenConsoleWindow(4, 4, 66, 25, "Game Over!");
                            WriteLine();
                            WriteLine("Congratulations you (X) is the winner of the game!");
                            WriteLine();
                            WriteLine("To close application, press ESC");
                            WriteLine("To play again, press ENTER.");

                            ReadLine();
                            ClearConsoleWindowArea();
                            Clear();

                            GameOver = false;
                            Grid = new char[,] {{'-', '-', '-'},
                                                {'-', '-', '-'},
                                                {'-', '-', '-'}};

                            FillRectangle(170, 189, 470, 191, Color.White);
                            FillRectangle(170, 289, 470, 291, Color.White);
                            FillRectangle(269, 90, 271, 390, Color.White);
                            FillRectangle(170, 389, 470, 391, Color.White);
                            FillRectangle(170, 89, 470, 91, Color.White);
                            FillRectangle(169, 90, 171, 390, Color.White);
                            FillRectangle(369, 90, 371, 390, Color.White);
                            FillRectangle(469, 90, 471, 390, Color.White);
                            break;

                        default:
                            if (!GridControll.IsMovePossible(Grid))
                            {
                                ClearRaster();
                                OpenConsoleWindow(4, 4, 66, 25, "Game Over!");
                                WriteLine();
                                WriteLine("It's Tie between you (X) and your opponent (O).");
                                WriteLine();
                                WriteLine("To close application, press ESC");
                                WriteLine("To play again, press ENTER.");
                            }
                            else
                            {
                                ClearRaster();
                                OpenConsoleWindow(4, 4, 66, 25, "Game Over!");
                                WriteLine();
                                WriteLine("You restarted the game.");
                                WriteLine();
                                WriteLine("To close application, press ESC");
                                WriteLine("To play again, press ENTER.");
                            }

                            ReadLine();
                            ClearConsoleWindowArea();
                            Clear();

                            GameOver = false;
                            Grid = new char[,] {{'-', '-', '-'},
                                                {'-', '-', '-'},
                                                {'-', '-', '-'}};

                            FillRectangle(170, 189, 470, 191, Color.White);
                            FillRectangle(170, 289, 470, 291, Color.White);
                            FillRectangle(269, 90, 271, 390, Color.White);
                            FillRectangle(170, 389, 470, 391, Color.White);
                            FillRectangle(170, 89, 470, 91, Color.White);
                            FillRectangle(169, 90, 171, 390, Color.White);
                            FillRectangle(369, 90, 371, 390, Color.White);
                            FillRectangle(469, 90, 471, 390, Color.White);
                            break;
                    }
                }
            }

            Terminate();
        }

        public static void OpenConsoleWindow(int x1, int y1, int x2, int y2, string Title)
        {
            SetConsoleWindowArea(x1 - 1, y1 - 1, x2 + 1, y2 + 1);
            ClearConsole();

            SetConsoleWindowArea(x1, y1, x2, y2);

            WriteLine(Title);

            SetConsoleWindowArea(x1, y1 + 2, x2, y2);
        }
    }
}

