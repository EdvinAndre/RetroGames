using System;
using System.Drawing;
using RetroSharp;

namespace Defender
{
    public abstract class MovingObject
    {
        protected int x;
        protected int y;
        protected int prevX;
        protected int prevY;
        protected int vx;
        protected int vy;
        protected bool dead;

        public MovingObject(int X, int Y, int VelocityX, int VelocityY)
        {
            this.x = this.prevX = X;
            this.y = this.prevY = Y;
            this.vx = VelocityX;
            this.vy = VelocityY;
        }

        public bool Dead { get { return this.dead; } }

        public int X { get { return this.x; } }
        public int Y { get { return this.y; } }
        public int PrevX { get { return this.prevX; } }
        public int PrevY { get { return this.prevY; } }
        public int VX { get { return this.vx; } }
        public int VY { get { return this.vy; } }

        public virtual bool Draw(Color Color) { return false; }

        public virtual void Die()
        {
            this.dead = true;
        }

        public static bool CheckCollision(int oneX, int oneY, int oneWidth, int oneHeight, int twoX, int twoY, int twoWidth, int twoHeight)
        {
            bool collisionX = oneX + oneWidth >= twoX && twoX + twoWidth >= oneX;

            bool collisionY = oneY + oneHeight >= twoY && twoY + twoHeight >= oneY;

            return collisionX && collisionY;
        }

        public virtual void Move(double ElapsedSeconds)
        {
            this.x += (int)(this.VX * ElapsedSeconds);
            this.y += (int)(this.VY * ElapsedSeconds);

            if (this.X < -30)
                this.x += RetroApplication.RasterWidth + 60;

            if (this.X > RetroApplication.RasterWidth + 30)
                this.x -= RetroApplication.RasterWidth + 60;

            if (this.Y < -30)
                this.y += RetroApplication.RasterHeight + 60;

            if (this.Y > RetroApplication.RasterHeight + 30)
                this.y -= RetroApplication.RasterHeight + 60;
        }
    }
}