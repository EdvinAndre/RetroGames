using System;
using System.Drawing;
using System.Collections.Generic;
using RetroSharp;
using System.Threading;

namespace Defender
{
    [RasterGraphics(640, 480)]
    [ScreenBorder(30, 20, KnownColor.DimGray)]
    [AspectRatio(4, 3)]
    [Characters(40, 30, KnownColor.White)]
    public class Program : RetroApplication
    {

        public static void Main(string[] args)
        {
            Initialize();

            int ShipTexture = AddSpriteTexture(GetResourceBitmap("Graphics.Ship.png"), System.Drawing.Color.Black, true);
            int EnemyTexture = AddSpriteTexture(GetResourceBitmap("Graphics.Enemy.png"), System.Drawing.Color.Black, true);
            int shipX, shipY;
            System.Random Rnd = new System.Random();
            int i, j;
            double d;
            double ToRadians = Math.PI / 180;
            int EnemyY = Rnd.Next(100, 300);
            int EnemyX = Rnd.Next(600, 620);
            int StarX = Rnd.Next(1, 640);
            int StarY = Rnd.Next(10, 350);
            int Level = 1;
            int Lives = 2;
            int Score = 0;
            int HighScore = 0;
            bool Collision = false;
            bool Done = false;
            bool GameOver = false;
            bool LevelUp = false;
            bool RightKeyIsPressed = false;
            bool LeftKeyIsPressed = false;
            bool Left = false;
            bool Right = false;
            bool Up = false;
            bool Down = false;

            OpenConsoleWindow(4, 4, 35, 25, "Welcome to Defender!", false);

            WriteLine("Keys in the game:");
            WriteLine();
            WriteLine("RIGHT: Accelerate forwards");
            WriteLine("LEFT: Accelerate backwards");
            WriteLine("UP: Moves Ship up in y-axis");
            WriteLine("DOWN: Moves Ship down in y-axis");
            WriteLine("SPACE: Fire");
            WriteLine("ESC: Close application");
            WriteLine();
            WriteLine("To play, press ENTER.");

            ReadLine();
            ClearConsoleWindowArea();

            LinkedList<EnemyShot> EnemyShots = new LinkedList<EnemyShot>();
            LinkedList<ShipLazer> ShipLazers = new LinkedList<ShipLazer>();
            LinkedList<Explosion> Explosions = new LinkedList<Explosion>();
            LinkedList<Enemy> Enemies = new LinkedList<Enemy>();
            LinkedList<Star> Stars = new LinkedList<Star>();
            LinkedList<MountainPixel> MountainRangePixels = new LinkedList<MountainPixel>();
            Ship Ship = new Ship(100, 250, 0, 0, CreateSprite(100, 250, ShipTexture));
            Ship.Position = Ship;

            Clear();
            FillRectangle(0, 0, 640, 0, Color.FromKnownColor(KnownColor.DarkGray));
            SetClipArea(0, 8, 639, 480);

            Write("Ships: ");
            int LivesPos = CursorX;
            Write(Lives.ToString("D2"));

            Write(" Score: ");
            int ScorePos = CursorX;
            Write(Score.ToString("D5"));

            Write(" Lvl: ");
            int LevelPos = CursorX;
            Write(Level.ToString("D2"));

            for (double Y = 400, X = 15000; X >= -5000; Y -= Math.Cos((int)X-- / 5 & -11))
            {
                MountainRangePixels.AddLast(new MountainPixel((int)X, (int)Y, 0, 0));
            }

            OnUpdateModel += (sener, e) =>
            {
                if (GameOver)
                    return;

                double ElapsedSeconds = e.Seconds;

                if (!RightKeyIsPressed || !LeftKeyIsPressed)
                {
                    foreach (Star star in Stars)
                    {
                        star.Draw(Star.StarColor);
                    }

                    foreach (MountainPixel pixel in MountainRangePixels)
                    {
                        pixel.Draw(MountainPixel.TerrainColor);
                    }
                }

                if (Enemies.Count < Level)
                {
                    Enemies.AddLast(new Enemy(EnemyX, EnemyY, 0, 0, CreateSprite(EnemyX, EnemyY, EnemyTexture), Ship.Position));
                    EnemyY = Rnd.Next(100, 300);
                    EnemyX = Rnd.Next(600, 620);
                }

                if (Stars.Count < 50)
                {
                    Stars.AddLast(new Star(StarX, StarY, 0, 0));
                    StarX = Rnd.Next(1, 640);
                    StarY = Rnd.Next(10, 350);
                }

                foreach (Enemy enemy in Enemies)
                {
                    foreach (MountainPixel pixel in MountainRangePixels)
                    {
                        if (pixel.X == enemy.X)
                            enemy.VelocityY = Math.Sign((pixel.Y - 70) - enemy.Y);
                    }

                    enemy.MoveStep();

                    if (MovingObject.CheckCollision(enemy.X, enemy.Y, 18, 16, Ship.X, Ship.Y, 36, 12))
                    {
                        Enemies.Remove(enemy);
                        enemy.DisposeSprite();

                        for (i = 0; i <= 15; i++)
                        {
                            d = Random() * 360 * ToRadians;
                            j = Random(10, 200);

                            Explosions.AddLast(new Explosion(enemy.X, enemy.Y,
                                          enemy.VX + Math.Cos(d) * j,
                                          enemy.VY + Math.Sin(d) * j,
                                          Blend(Color.LimeGreen, Color.Yellow, Rnd.Next(3)),
                                          1));
                        }

                        Score += 150;
                        GotoXY(ScorePos, 0);
                        Write(Score.ToString("D5"));
                    }

                    if (enemy.X < -18)
                    {
                        Enemies.Remove(enemy);
                        enemy.DisposeSprite();
                    }
                    
                    if (EnemyShots.Count < 1/*Enemies.Count*/ || enemy.EnemyFire)
                    {
                        enemy.EnemyFire = false;
                        enemy.StartTiming();
                        enemy.Fire(EnemyShots);
                    }
                }

                if (RightKeyIsPressed)
                {
                    Star.VelocityX = -3;
                    MountainPixel.VelocityX = -4;

                    foreach (Star star in Stars)
                    {
                        star.Draw(Color.Black);
                        star.Move(ElapsedSeconds);
                        star.Draw(Star.StarColor);
                    }

                    foreach (MountainPixel pixel in MountainRangePixels)
                    {
                        pixel.Draw(Color.Black);
                        pixel.Move(ElapsedSeconds);
                        pixel.Draw(MountainPixel.TerrainColor);
                    }
                }
                else if (LeftKeyIsPressed)
                {
                    Star.VelocityX = 3;
                    MountainPixel.VelocityX = 4;

                    foreach (Star star in Stars)
                    {
                        star.Draw(Color.Black);
                        star.Move(ElapsedSeconds);
                        star.Draw(Star.StarColor);
                    }

                    foreach (MountainPixel pixel in MountainRangePixels)
                    {
                        pixel.Draw(Color.Black);
                        pixel.Move(ElapsedSeconds);
                        pixel.Draw(MountainPixel.TerrainColor);
                    }
                }

                foreach (Explosion particle in Explosions)
                {
                    particle.Draw(Color.Black);
                    particle.Move(ElapsedSeconds);
                    particle.Draw(particle.color);
                }

                foreach (EnemyShot shot in EnemyShots)
                {
                    shot.Draw(Color.Black);
                    shot.Move(ElapsedSeconds);
                    shot.Draw(Color.White);
                    
                    if (shot.p <= 0)
                        EnemyShots.Remove(shot);

                    if (shot.Y >= 479 || shot.Y <= 20 || shot.X <= 0)
                    {
                        shot.Draw(Color.Black);
                        EnemyShots.Remove(shot);
                    }

                    if (MovingObject.CheckCollision(Ship.X, Ship.Y, 36, 12, shot.X, shot.Y, 3, 3))
                    {
                        Collision = true;
                        shot.Draw(Color.Black);
                        EnemyShots.Clear();
                    }
                }
                
                foreach (ShipLazer lazer in ShipLazers)
                {
                    lazer.Draw(Color.Black);
                    lazer.Move(ElapsedSeconds);
                    lazer.Draw(Color.Red);

                    if (lazer.X <= 0)
                    {
                        lazer.Draw(Color.Black);
                        ShipLazers.Remove(lazer);

                    }
                }

                LinkedListNode<Enemy> EnemyNode = Enemies.First;
                LinkedListNode<ShipLazer> ShotNode = ShipLazers.First;
                ShipLazer CurrentShot;
                Enemy CurrentEnemy;

                while (ShotNode != null)
                {
                    CurrentShot = ShotNode.Value;

                    while (EnemyNode != null)
                    {
                        CurrentEnemy = EnemyNode.Value;

                        if (MovingObject.CheckCollision(CurrentEnemy.X, CurrentEnemy.Y, 18, 16, CurrentShot.X, CurrentShot.Y, 20, 2))
                        {
                            Enemies.Remove(EnemyNode);
                            CurrentEnemy.DisposeSprite();
                            ShipLazers.Remove(ShotNode);
                            CurrentShot.Draw(Color.Black);
                            
                            for (i = 0; i <= 15; i++)
                            {
                                d = Random() * 360 * ToRadians;
                                j = Random(10, 200);

                                Explosions.AddLast(new Explosion(CurrentEnemy.X, CurrentEnemy.Y,
                                              CurrentEnemy.VX + Math.Cos(d) * j,
                                              CurrentEnemy.VY + Math.Sin(d) * j,
                                              Blend(Color.LimeGreen, Color.Yellow, Rnd.Next(3)),
                                              1));
                            }

                            Score += 150;
                            GotoXY(ScorePos, 0);
                            Write(Score.ToString("D5"));
                        }
                        else
                        {
                            EnemyNode = EnemyNode.Next;
                        }
                    }

                    ShotNode = ShotNode.Next;
                }

                if ((Collision) && (Lives > 0))
                {
                    Collision = false;
                    Ship.DisposeSprite();
                    for (i = 0; i <= 120; i++)
                    {
                        d = Random() * 360 * ToRadians;
                        j = Random(10, 200);

                        Explosions.AddLast(new Explosion(Ship.X, Ship.Y,
                                              Ship.VX + Math.Cos(d) * j,
                                              Ship.VY + Math.Sin(d) * j,
                                              Color.White, 2));
                    }

                    Lives--;
                    GotoXY(LivesPos, 0);
                    Write(Lives.ToString("D2"));

                    if (Lives > 0)
                    {
                        Ship = new Ship(100, 250, 0, 0, CreateSprite(100, 250, ShipTexture));
                        Ship.Position = Ship;
                    }
                }
                else if (Lives == 0)
                {
                    GameOver = true;
                }

                if ((Score == 2250 && Level == 1) || (Score == 5250 && Level == 2) || (Score == 9000 && Level == 3))
                {
                    ClearRaster();
                    Ship.DisposeSprite();
                    LevelUp = true;
                }
            };

            OnKeyDown += (sener, e) =>
            {
                switch (e.Key)
                {
                    case Key.Escape:
                        Done = true;
                        break;

                    case Key.Left:
                        Left = true;
                        shipX = Ship.X;
                        shipY = Ship.Y;
                        LeftKeyIsPressed = true;
                        if (Right)
                        {
                            Right = false;
                            RightKeyIsPressed = false;
                            UpdateSpriteTexture(ShipTexture, GetResourceBitmap("Graphics.ReversedShip.png"), new Point(0, 0), Color.Black, true);
                        }
                        Ship.Left();
                        break;

                    case Key.Right:
                        Right = true;
                        RightKeyIsPressed = true;
                        if (Left)
                        {
                            Left = false;
                            LeftKeyIsPressed = false;
                            UpdateSpriteTexture(ShipTexture, GetResourceBitmap("Graphics.Ship.png"), new Point(0, 0), Color.Black, true);
                        }
                        Ship.Right();
                        break;

                    case Key.Up:
                        Up = true;
                        Ship.Up();
                        break;

                    case Key.Down:
                        Down = true;
                        Ship.Down();
                        break;

                    case Key.Space:
                        Ship.Fire(ShipLazers);
                        break;
                }
            };

            OnKeyUp += (sener, e) =>
            {
                switch (e.Key)
                {
                    case Key.Left:
                        LeftKeyIsPressed = false;
                        Ship.NoLeftRight();
                        break;

                    case Key.Right:
                        RightKeyIsPressed = false;
                        Ship.NoLeftRight();
                        break;

                    case Key.Up:
                        Up = false;
                        if (Down)
                            Ship.Down();
                        else
                            Ship.NoUpDown();
                        break;

                    case Key.Down:
                        Down = false;
                        if (Up)
                            Ship.Up();
                        else
                            Ship.NoUpDown();
                        break;
                }
            };

            while (!Done)
            {
                while (!GameOver && !Done && !LevelUp)
                {
                    Sleep(1000);
                }

                if (!Done && !LevelUp)
                {
                    foreach (Enemy enemy in Enemies)
                    {
                        enemy.DisposeSprite();
                    }

                    ClearRaster();
                    OpenConsoleWindow(4, 4, 35, 25, "Game Over!", false);

                    WriteLine();
                    Write("Score: ");
                    WriteLine(Score.ToString("D5"));

                    Write("Level: ");
                    WriteLine(Level.ToString("D2"));

                    if (Score > HighScore)
                    {
                        WriteLine("New HighScore!");

                        HighScore = Score;
                    }

                    WriteLine();
                    WriteLine("To close application, press ESC");
                    WriteLine("To play again, press ENTER.");

                    ReadLine();
                    ClearConsoleWindowArea();
                    Clear();

                    EnemyShots.Clear();
                    ShipLazers.Clear();
                    Explosions.Clear();
                    Enemies.Clear();
                    MountainRangePixels.Clear();
                    Level = 1;
                    Lives = 2;
                    Score = 0;
                    GameOver = false;
                    Ship = new Ship(100, 250, 0, 0, CreateSprite(100, 250, ShipTexture));
                    Ship.Position = Ship;

                    for (double Y = 400, X = 15000; X >= -5000; Y -= Math.Cos((int)X-- / 5 & -11))
                    {
                        MountainRangePixels.AddLast(new MountainPixel((int)X, (int)Y, 0, 0));
                    }

                    Write("Ships: ");
                    Write(Lives.ToString("D2"));

                    Write(" Score: ");
                    Write(Score.ToString("D5"));

                    Write(" Lvl: ");
                    Write(Level.ToString("D2"));
                }
                else if (!Done && !GameOver)
                {
                    foreach (Enemy enemy in Enemies)
                    {
                        enemy.DisposeSprite();
                    }

                    OpenConsoleWindow(4, 4, 35, 25, "ATTACK WAVE " + Level, true);
                    GotoXY((ConsoleWindowWidth - 9) / 2, 0);
                    WriteLine("COMPLETED");

                    Thread.Sleep(3000);
                    ClearConsoleWindowArea();
                    Clear();

                    EnemyShots.Clear();
                    ShipLazers.Clear();
                    Explosions.Clear();
                    Enemies.Clear();
                    MountainRangePixels.Clear();
                    LevelUp = false;
                    GameOver = false;
                    Lives++;
                    Level++;
                    Ship = new Ship(100, 250, 0, 0, CreateSprite(100, 250, ShipTexture));
                    Ship.Position = Ship;

                    for (double Y = 400, X = 15000; X >= -5000; Y -= Math.Cos((int)X-- / 5 & -11))
                    {
                        MountainRangePixels.AddLast(new MountainPixel((int)X, (int)Y, 0, 0));
                    }

                    Write("Ships: ");
                    Write(Lives.ToString("D2"));

                    Write(" Score: ");
                    Write(Score.ToString("D5"));

                    Write(" Lvl: ");
                    Write(Level.ToString("D2"));
                }
            }

            Terminate();
        }

        public static void OpenConsoleWindow(int x1, int y1, int x2, int y2, string Title, bool AlignTitleCenter)
        {
            SetConsoleWindowArea(x1 - 1, y1 - 1, x2 + 1, y2 + 1);
            ClearConsole();

            SetConsoleWindowArea(x1, y1, x2, y2);

            if (!AlignTitleCenter)
            {
                WriteLine(Title);
            }
            else
            {
                GotoXY((ConsoleWindowWidth - Title.Length) / 2, 0);
                WriteLine(Title);
            }

            SetConsoleWindowArea(x1, y1 + 2, x2, y2);
        }
    }
}
