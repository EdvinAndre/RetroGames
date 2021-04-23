using System;
using System.Drawing;
using RetroSharp;

namespace Defender
{
    public class Explosion : MovingObject
    {
        public Color color;
        private double LifeTime;
        private double TotalTime;
        private double p = 1;

        public Explosion(double X, double Y, double VelocityX, double VelocityY, Color Color, double LifeTime)
            : base((int)X, (int)Y, (int)VelocityX, (int)VelocityY)
        {
            color = Color;
            this.LifeTime = this.TotalTime = LifeTime;
        }

        public override void Move(double ElapsedSeconds)
        {
            base.Move(ElapsedSeconds);

            this.LifeTime -= ElapsedSeconds;
            this.p = this.LifeTime / this.TotalTime;
        }

        public override bool Draw(Color color)
        {
            Color cl = RetroApplication.Blend(Color.Black, color, p);

            RetroApplication.Raster[x - 1, y] = cl;
            RetroApplication.Raster[x, y] = cl;
            RetroApplication.Raster[x + 1, y] = cl;
            RetroApplication.Raster[x, y - 1] = cl;
            RetroApplication.Raster[x, y + 1] = cl;
            RetroApplication.Raster[x - 1, y - 1] = cl;
            RetroApplication.Raster[x - 1, y + 1] = cl;
            RetroApplication.Raster[x + 1, y - 1] = cl;
            RetroApplication.Raster[x + 1, y + 1] = cl;

            return true;
        }


    }
}