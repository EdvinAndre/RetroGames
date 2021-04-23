using System;
using System.Drawing;
using RetroSharp;

namespace Defender
{
    public class MountainPixel : MovingObject
    {
        public static Color TerrainColor = Color.SaddleBrown;
        public static int VelocityX;

        public MountainPixel(int X, int Y, int VX, int VY)
            : base(X, Y, VX, VY)
        {
            this.x = X;
            this.y = Y;
        }

        public override void Move(double ElapsedSeconds)
        {
            this.x += VelocityX;
        }

        public override bool Draw(Color Color)
        {
            //Color cl = RetroApplication.Blend(Color.Black, Color, 1);
            Color cl = Color;

            RetroApplication.Raster[this.x, this.y] = cl;
            RetroApplication.Raster[this.x, this.y + 1] = cl;

            return true;
        }
    }

    public class Star : MovingObject
    {
        public int R;
        public int G;
        public int B;
        public static Color StarColor;
        public static int VelocityX;
        public System.Random Rnd = new System.Random();

        public Star(int X, int Y, int VX, int VY)
            : base(X, Y, VX, VY)
        {
            this.x = X;
            this.y = Y;
        }

        public override void Move(double ElapsedSeconds)
        {
            base.Move(ElapsedSeconds);
            this.x += VelocityX;
        }

        public override bool Draw(Color Color)
        {
            this.R = Rnd.Next(0, 255);
            this.G = Rnd.Next(0, 255);
            this.B = Rnd.Next(0, 255);
            StarColor = Color.FromArgb(this.R, this.G, this.B);
            Color cl = RetroApplication.Blend(Color.Black, Color, 1);

            RetroApplication.Raster[this.x, this.y] = cl;
            RetroApplication.Raster[this.x + 1, this.y] = cl;
            RetroApplication.Raster[this.x, this.y - 1] = cl;
            RetroApplication.Raster[this.x + 1, this.y - 1] = cl;

            return true;
        }

    }
}
