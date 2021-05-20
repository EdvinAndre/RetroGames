using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Timers;
using RetroSharp;

namespace Defender
{
    public class Enemy : MovingObject
    {
        public bool EnemyFire = false;
        public int VelocityY;
        private Sprite Sprite;
        private Gun gun = Gun.EnemyShots;
        private Ship Target;
        private bool goingDown = false;
        private double angle, angleToRadians, speed;
        private System.Random Rnd = new System.Random();
        private static Timer timer = new Timer();

        public Enemy(int X, int Y, int VX, int VY, Sprite Sprite, Ship Target)
            : base(X, Y, VX, VY)
        {
            this.Sprite = Sprite;
            this.Sprite.X = X;
            this.Sprite.Y = Y;
            this.Sprite.SetPosition((int)(this.x), (int)(this.y));
            this.Target = Target;
        }
        
        public void MoveStep()
        {
            if (this.y <= 150 || goingDown)
            {
                angleToRadians = Rnd.Next(25, 45) * (Math.PI / 180);
                angle = Math.Atan(angleToRadians);
                speed = 2.001;
                this.vx = (int)(speed * Math.Cos(angle));
                this.vy = (int)(speed * Math.Sin(angle));

                if (this.y < 20 || goingDown)
                {
                    goingDown = true;
                    this.vy = -this.vy;
                }

                this.x -= this.vx;
                this.y -= this.vy;

                this.Sprite.Move(-this.vx, -this.vy);
            }
            else
            {
                this.vx = -1;
                this.vy = VelocityY;

                this.x += this.vx;
                this.y += this.vy;

                this.Sprite.Move(this.vx, this.vy);
            }
        }
        
        public void DisposeSprite()
        {
            this.Sprite.Dispose();
        }

        public void StartTiming()
        {
            EnemyFire = false;
            timer.Interval = 2000;

            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            EnemyFire = true;
        }

        public void Fire(LinkedList<EnemyShot> EnemyShots)
        {
            switch (this.gun)
            {
                case Gun.EnemyShots:
                    EnemyShots.AddLast(new EnemyShot(this.X + this.vx, this.Y, this.VX, this.VY, Color.White, this.Target));
                    break;
            }
        }

    }
}
