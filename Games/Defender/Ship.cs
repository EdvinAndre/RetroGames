using System;
using System.Drawing;
using System.Collections.Generic;
using RetroSharp;

namespace Defender
{
    public enum Gun
    {
        ShipLazers,
        EnemyShots
    }
    public class Ship : MovingObject
    {
        private Sprite Sprite;
        private Ship position = null;
        private Gun gun = Gun.ShipLazers;
        //private int minX = 98;
        //private int maxX = 200;
        private int lastVX = 5;

        public Ship(int X, int Y, int VX, int VY, Sprite Sprite)
            : base(X, Y, VX, VY)
        {
            this.Sprite = Sprite;
            this.Sprite.X = X;
            this.Sprite.Y = Y;
            this.Sprite.SetPosition((int)(this.x), (int)(this.y));
        }

        public Ship Position
        {
            get { return this.position; }
            internal set { this.position = value; }
        }

        public void Left()
        {
            this.vx = -5;
            this.lastVX = this.vx;
            //this.x += this.vx;
            /*if ((this.x > minX) && (this.x < maxX))
                this.sprite.Move(this.vx, 0);*/
        }

        public void Right()
        {
            this.vx = 5;
            this.lastVX = this.vx;
            //this.x += this.vx;
            /*if ((this.x > minX) && (this.x < maxX))
                this.sprite.Move(this.vx, 0);*/
        }

        public void Up()
        {
            this.vy = -5;
            this.y += this.vy;
            this.Sprite.Move(0, this.vy);
        }

        public void Down()
        {
            this.vy = 5;
            this.y += this.vy;
            this.Sprite.Move(0, this.vy);
        }

        public void NoLeftRight()
        {
            this.vx = 0;
            this.x += this.vx;
            this.Sprite.Move(this.vx, 0);
        }

        public void NoUpDown()
        {
            this.vy = 0;
            this.y += this.vy;
            this.Sprite.Move(0, this.vy);
        }

        public void DisposeSprite()
        {
            this.Sprite.Dispose();
        }

        /*public void MoveBack()
        {
            this.sprite.MoveBack();
        }*/

        public void Fire(LinkedList<ShipLazer> ShipLazers)
        {
            switch (this.gun)
            {
                case Gun.ShipLazers:
                    ShipLazers.AddLast(new ShipLazer(this.Position.X + this.vx, this.Position.Y + 9, this.lastVX, 0, Color.Red));
                    break;
            }
        }
    }
}