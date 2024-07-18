using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics_Project
{
    public class Circle
    {
        public int Rad;
        public int XC;
        public int YC;
        public float thRadian;
        public void Drawcircle(Graphics g)
        {
            for (float i = 0; i < 360; i += 30f)
            {
                Pen pn = new Pen(Color.Gray, 5);
                float x = (float)((Rad - 20) * Math.Cos(i));
                float y = (float)((Rad - 20) * Math.Sin(i));

                x += XC;
                y += YC;
                g.FillEllipse(Brushes.Black, x, y, 5, 5);

                float x2 = (float)((Rad) * Math.Cos(i));
                float y2 = (float)((Rad) * Math.Sin(i));

                x2 += XC;
                y2 += YC;
                g.DrawLine(pn, x, y, x2, y2);
            }
            for (float i = 0; i < 360; i += 0.1f)
            {
                float x = (float)((Rad) * Math.Cos(i));
                float y = (float)((Rad) * Math.Sin(i));

                x += XC;
                y += YC;
                g.FillEllipse(Brushes.Black, x, y, 5, 5);
            }
            for (float i = 0; i < 360; i += 0.1f)
            {
                float thRadian = (float)(i * Math.PI / 180);
                float x = (float)((Rad - 20) * Math.Cos(thRadian));
                float y = (float)((Rad - 20) * Math.Sin(thRadian));
                x += XC;
                y += YC;
                g.FillEllipse(Brushes.Black, x, y, 5, 5);
            }
        }
        public PointF Getnextpoint(int theta)
        {

            PointF p = new PointF();

            thRadian = (float)(theta * Math.PI / 180);

            p.X = (float)(Rad * Math.Cos(thRadian)) + XC;
            p.Y = (float)(Rad * Math.Sin(thRadian)) + YC;
            return p;
        }
    }
}
