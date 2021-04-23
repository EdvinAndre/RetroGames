using System;
using System.Drawing;
using RetroSharp;

namespace Defender
{
    public class Shot : MovingObject
    {
        private Color Color;

        public Shot(int X, int Y, int VX, int VY, Color Color)
            : base(X, Y, VX, VY)
        {
            this.Color = Color;

            RetroApplication.Raster[X, Y] = Color.Black;
        }
    }
}