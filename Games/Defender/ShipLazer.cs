using System;
using System.Drawing;
using RetroSharp;

namespace Defender
{
    public class ShipLazer : Shot
    {
        public Color Color;
        private double LifeTime = 2;
        private double p = 1;

        public ShipLazer(int X, int Y, int VX, int VY, Color Color)
            : base(X, Y, VX, VY, Color)
        {
            this.Color = Color;
            this.vx = VX;
        }

        public override void Move(double ElapsedSeconds)
        {
            base.Move(ElapsedSeconds);

            this.LifeTime -= ElapsedSeconds;
            this.p = LifeTime / 2;
            this.x += this.vx;
        }

        public override bool Draw(Color Color)
        {
            Color cl = RetroApplication.Blend(Color.Black, Color, p);

            int xd = (int)(this.X + 0.5);
            int yd = (int)(this.Y + 0.5);

            RetroApplication.Raster[xd, yd] = cl;
            RetroApplication.Raster[xd + 1, yd] = cl;
            RetroApplication.Raster[xd + 2, yd] = cl;
            RetroApplication.Raster[xd + 3, yd] = cl;
            RetroApplication.Raster[xd + 4, yd] = cl;
            RetroApplication.Raster[xd + 5, yd] = cl;
            RetroApplication.Raster[xd + 6, yd] = cl;
            RetroApplication.Raster[xd + 7, yd] = cl;
            RetroApplication.Raster[xd + 8, yd] = cl;
            RetroApplication.Raster[xd + 9, yd] = cl;
            RetroApplication.Raster[xd + 10, yd] = cl;
            RetroApplication.Raster[xd + 11, yd] = cl;
            RetroApplication.Raster[xd + 12, yd] = cl;
            RetroApplication.Raster[xd + 13, yd] = cl;
            RetroApplication.Raster[xd + 14, yd] = cl;
            RetroApplication.Raster[xd + 15, yd] = cl;
            RetroApplication.Raster[xd + 16, yd] = cl;
            RetroApplication.Raster[xd + 17, yd] = cl;
            RetroApplication.Raster[xd + 18, yd] = cl;
            RetroApplication.Raster[xd + 19, yd] = cl;
            RetroApplication.Raster[xd + 20, yd] = cl;
            RetroApplication.Raster[xd, yd - 1] = cl;
            RetroApplication.Raster[xd + 1, yd - 1] = cl;
            RetroApplication.Raster[xd + 2, yd - 1] = cl;
            RetroApplication.Raster[xd + 3, yd - 1] = cl;
            RetroApplication.Raster[xd + 4, yd - 1] = cl;
            RetroApplication.Raster[xd + 5, yd - 1] = cl;
            RetroApplication.Raster[xd + 6, yd - 1] = cl;
            RetroApplication.Raster[xd + 7, yd - 1] = cl;
            RetroApplication.Raster[xd + 8, yd - 1] = cl;
            RetroApplication.Raster[xd + 9, yd - 1] = cl;
            RetroApplication.Raster[xd + 10, yd - 1] = cl;
            RetroApplication.Raster[xd + 11, yd - 1] = cl;
            RetroApplication.Raster[xd + 12, yd - 1] = cl;
            RetroApplication.Raster[xd + 13, yd - 1] = cl;
            RetroApplication.Raster[xd + 14, yd - 1] = cl;
            RetroApplication.Raster[xd + 15, yd - 1] = cl;
            RetroApplication.Raster[xd + 16, yd - 1] = cl;
            RetroApplication.Raster[xd + 17, yd - 1] = cl;
            RetroApplication.Raster[xd + 18, yd - 1] = cl;
            RetroApplication.Raster[xd + 19, yd - 1] = cl;
            RetroApplication.Raster[xd + 20, yd - 1] = cl;
            RetroApplication.Raster[xd, yd + 1] = cl;

            return true;
        }
    }
}