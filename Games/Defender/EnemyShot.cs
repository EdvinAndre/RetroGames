using System;
using System.Drawing;
using RetroSharp;

namespace Defender
{
    public class EnemyShot : Shot
    {
        public double p = 1;
        private Ship Target;
        private double LifeTime = 4;
        private double angle, speed;

        public EnemyShot(int X, int Y, int VX, int VY, Color Color, Ship Target)
            : base(X, Y, VX, VY, Color)
        {
            this.Target = Target;

            if (this.y <= this.Target.Y && this.x >= this.Target.X)
                angle = Math.Atan2((this.Target.Y - this.y), (this.Target.X - this.x));
            else if (this.y <= this.Target.Y && this.x < this.Target.X)
                angle = Math.Atan2((this.Target.Y - this.y), (this.x - this.Target.X));
            else if (this.y > this.Target.Y && this.x >= this.Target.X)
                angle = Math.Atan2((this.Target.Y - this.y), (this.Target.X - this.x));
            else if (this.y > this.Target.Y && this.x < this.Target.X)
                angle = Math.Atan2((this.Target.Y - this.y), (this.x - this.Target.X));

            speed = 2.05;
            this.vx = (int)(speed * Math.Cos(angle));
            this.vy = (int)(speed * Math.Sin(angle));
        }

        public override void Move(double ElpsedSeconds)
        {
            base.Move(ElpsedSeconds);

            this.LifeTime -= ElpsedSeconds;
            this.p = LifeTime / 2;

            this.x += this.vx;
            this.y += this.vy;
        }

        public override bool Draw(Color Color)
        {
            Color cl = RetroApplication.Blend(Color.Black, Color, p);

            RetroApplication.Raster[x - 2, y] = cl;
            RetroApplication.Raster[x - 1, y] = cl;
            RetroApplication.Raster[x, y] = cl;
            RetroApplication.Raster[x + 1, y] = cl;
            RetroApplication.Raster[x + 2, y] = cl;
            RetroApplication.Raster[x + 3, y] = cl;
            RetroApplication.Raster[x, y - 1] = cl;
            RetroApplication.Raster[x + 1, y - 1] = cl;
            RetroApplication.Raster[x, y - 2] = cl;
            RetroApplication.Raster[x + 1, y - 2] = cl;
            RetroApplication.Raster[x, y + 1] = cl;
            RetroApplication.Raster[x + 1, y + 1] = cl;
            RetroApplication.Raster[x, y + 2] = cl;
            RetroApplication.Raster[x + 1, y + 2] = cl;
            RetroApplication.Raster[x + 2, y + 1] = cl;
            RetroApplication.Raster[x + 3, y + 1] = cl;
            RetroApplication.Raster[x - 1, y + 1] = cl;
            RetroApplication.Raster[x - 2, y + 1] = cl;
            RetroApplication.Raster[x, y + 3] = cl;
            RetroApplication.Raster[x + 1, y + 3] = cl;

            return true;
        }
    }
}