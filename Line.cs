using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Graphics_Project
{
    public class Line
    {
        public float X, Y;
        float dy, dx, m;
        public float cx, cy;
        int speed = 10;
        public void calc(Line End)
        {
            dy = End.Y - Y;
            dx = End.X - X;
            m = dy / dx;
            cx = X;
            cy = Y;
        }
        public bool CalcNextPoint(Line End)
        {
            if (Math.Abs(dx) > Math.Abs(dy))
            {
                if (X <= End.X && Y <= End.Y)
                {
                    cx += speed;
                    cy += m * speed;
                    if (cx >= End.X)
                    {
                        return false;
                    }

                }
                else if (X >= End.X && Y <= End.Y)
                {
                    cx -= speed;
                    cy -= m * speed;
                    if (cx <= End.X)
                    {
                        return false;
                    }
                }
                else if (X <= End.X && Y >= End.Y)
                {
                    cx += speed;
                    cy += m * speed;
                    if (cx >= End.X)
                    {
                        return false;
                    }
                }
                else
                {
                    cx -= speed;
                    cy -= m * speed;
                    if (cx <= End.X)
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (X <= End.X && Y <= End.Y)
                {
                    cy += speed;
                    cx += 1 / m * speed;
                    if (cy >= End.Y)
                    {
                        return false;
                    }
                }
                else if (X >= End.X && Y <= End.Y)
                {
                    cy += speed;
                    cx += 1 / m * speed;
                    if (cy >= End.Y)
                    {
                        return false;
                    }
                }
                else if (X <= End.X && Y >= End.Y)
                {
                    cy -= speed;
                    cx -= 1 / m * speed;
                    if (cy <= End.Y)
                    {
                        return false;
                    }
                }
                else
                {
                    cy -= speed;
                    cx -= 1 / m * speed;
                    if (cy <= End.Y)
                    {
                        return false;
                    }
                }

            }
            return true;
        }
    }
}
