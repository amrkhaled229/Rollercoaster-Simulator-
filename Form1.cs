using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Graphics_Project
{
    public class BG
    {
        public Rectangle rcSrc, rcDst;
        public Bitmap img;
    }
    public class Car
    {
        public Rectangle rcSrc, rcDst;
        public Bitmap img;
    }
    public class circleButton{
        public int r, X, Y;
        public Color clr;
    }
    public class LineSegment
    {
        public PointF ptS, ptE;

        public void DrawYourSelf(Graphics g)
        {
            g.DrawLine(Pens.Black, ptS.X, ptS.Y, ptE.X, ptE.Y);
            g.FillEllipse(Brushes.Red, ptS.X - 5, ptS.Y - 5, 10, 10);
            g.FillEllipse(Brushes.Red, ptE.X - 5, ptE.Y - 5, 10, 10);
        }
    }
    public partial class Form1 : Form
    {
        Bitmap off;
        List<LineSegment> ListOfLines = new List<LineSegment>();
        Timer tt = new Timer();
        List<BG> LBackground = new List<BG>();
        List<Car> LHero = new List<Car>();
        List<BG> LButtons = new List<BG>();
        List<BG> LFinish = new List<BG>();
        int currX = 0;
        List<Line> PtSt = new List<Line>();
        List<Line> PtEnd = new List<Line>();
        List<Circle> circlesList = new List<Circle>();
        List<Curve> curvesList = new List<Curve>();
        List<circleButton> speedList = new List<circleButton>();
        Char currDrawing = ' ';
        bool isMoving = false;
        int carSpeed = 10;
        float timeCurve = 0.01f;
        int posCurve;
        int posCircle;
        int thball = 90;
        int flagCir = 0;
        Char carOn;
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            this.MouseDown += Form1_MouseDown;
            tt.Tick += Tt_Tick;
            tt.Start();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            for(int i = 0;i< speedList.Count; i++)
            {
                if (e.X >= speedList[i].X && e.X <= speedList[i].X + 160 && e.Y >= speedList[i].Y && e.Y <= speedList[i].Y + 160) 
                {
                    if(i == 0)
                    {
                        carSpeed += 5;
                    }
                    else if(i == 1)
                    {
                        if (carSpeed > 5)
                        {
                            carSpeed -= 5;
                        }
                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D1)
            {
                Line pnnSt = new Line();
                Line pnnEn = new Line();
                pnnSt.X = currX;
                pnnSt.Y = 750;
                pnnEn.X = currX + 200;
                pnnEn.Y = 750;
                currX += 200;
                pnnSt.calc(pnnEn);
                PtSt.Add(pnnSt);
                PtEnd.Add(pnnEn);
                currDrawing = 'L';
            }
            if (e.KeyCode == Keys.D3)
            {
                Curve pnn = new Curve();
                Point pt = new Point(currX,750);//0
                pnn.ControlPoints.Add(pt);
                pt = new Point(currX + 250, 750);//1
                pnn.ControlPoints.Add(pt);
                pt = new Point(currX + 100, 750 - 200);//2
                pnn.ControlPoints.Add(pt);
                pt = new Point(currX + 50, 750 - 300);//3
                pnn.ControlPoints.Add(pt);
                pt = new Point(currX + 200, 750 - 400);//4
                pnn.ControlPoints.Add(pt);
                pt = new Point(currX + 350, 750 - 300);//5
                pnn.ControlPoints.Add(pt);
                pt = new Point(currX + 300, 750 - 200);//6
                pnn.ControlPoints.Add(pt);
                pt = new Point(currX + 150, 750);//7
                pnn.ControlPoints.Add(pt);
                pt = new Point(currX + 400, 750);//8
                pnn.ControlPoints.Add(pt);
                curvesList.Add(pnn);
                currX += 400;
                currDrawing = 'R';
            }
            if (e.KeyCode == Keys.Right)
            {
                if(currDrawing == 'L')
                {
                    PtEnd[PtEnd.Count - 1].X += 10;
                    currX += 10;
                }
                else if(currDrawing == 'C')
                {
                    circlesList[circlesList.Count - 1].Rad += 10;
                    circlesList[circlesList.Count - 1].YC -= 10;
                }
                else if (currDrawing == 'R')
                {
                    for(int i=2;i< 7; i++)
                    {
                        Point point = curvesList[curvesList.Count - 1].ControlPoints[i];
                        point.Y -= 10;
                        curvesList[curvesList.Count - 1].ControlPoints[i] = point;
                    }
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                if (PtEnd[PtEnd.Count - 1].X - PtSt[PtEnd.Count - 1].X >= 30)
                {
                    if (currDrawing == 'L')
                    {
                        PtEnd[PtEnd.Count - 1].X -= 10;
                        currX -= 10;
                    }
                    else if (currDrawing == 'C')
                    {
                        circlesList[circlesList.Count - 1].Rad -= 10;
                        circlesList[circlesList.Count - 1].YC += 10;
                    }
                    else if (currDrawing == 'R')
                    {
                        for (int i = 2; i < 7; i++)
                        {
                            Point point = curvesList[curvesList.Count - 1].ControlPoints[i];
                            point.Y += 10;
                            curvesList[curvesList.Count - 1].ControlPoints[i] = point;
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.D2)
            {
                Circle pnn = new Circle();
                pnn.Rad = 100;
                pnn.XC = currX;
                pnn.YC = 750 - 100;
                circlesList.Add(pnn);
                Line pnnSt = new Line();
                Line pnnEn = new Line();
                pnnSt.X = currX;
                pnnSt.Y = 750;
                pnnEn.X = currX + 200;
                pnnEn.Y = 750;
                currX += 200;
                pnnSt.calc(pnnEn);
                PtSt.Add(pnnSt);
                PtEnd.Add(pnnEn);
                currDrawing = 'C';
            }
            if(e.KeyCode == Keys.Space)
            {
                CreateCar();
                int tempY = 0;
                while (tempY <= this.Height)
                {
                    BG pnn = new BG();
                    pnn.img = new Bitmap("finish.jpg");
                    pnn.rcSrc = new Rectangle(0, 0, pnn.img.Width, pnn.img.Height);
                    pnn.rcDst = new Rectangle(currX, tempY, 30, 200);
                    LFinish.Add(pnn);
                    tempY += 200;
                }
                isMoving = true;
                while (LBackground[0].rcSrc.X > 0)
                {
                    LBackground[0].rcSrc.X -= carSpeed;
                    for (int i = 0; i < PtEnd.Count; i++)
                    {
                        PtSt[i].X += carSpeed;
                        PtEnd[i].X += carSpeed;
                    }
                    for (int i = 0; i < circlesList.Count; i++)
                    {
                        circlesList[i].XC += carSpeed;
                    }
                    for (int i = 0; i < curvesList.Count; i++)
                    {
                        for (int j = 0; j < curvesList[i].ControlPoints.Count; j++)
                        {
                            Point tempPT = curvesList[i].ControlPoints[j];
                            tempPT.X += carSpeed;
                            curvesList[i].ControlPoints[j] = tempPT;
                        }
                    }
                    for (int i = 0; i < LFinish.Count; i++)
                    {
                        LFinish[i].rcDst.X += carSpeed;
                    }
                }
                
            }
            if(e.KeyCode == Keys.Up)
            {
                carSpeed += 5;
            }
            if (e.KeyCode == Keys.Down)
            {
                if (carSpeed > 5)
                {
                    carSpeed -= 5;
                }
            }
        }
        private void Tt_Tick(object sender, EventArgs e)
        {
            if((currX > this.ClientSize.Width - 500 && isMoving == false) || (LHero.Count > 0 && LHero[0].rcDst.X > this.ClientSize.Width - 500 && isMoving == true))
            {
                LBackground[0].rcSrc.X += carSpeed;
                for(int i = 0;i < PtEnd.Count; i++)
                {
                    PtSt[i].X -= carSpeed;
                    PtEnd[i].X -= carSpeed;
                }
                for(int i = 0;i<LFinish.Count; i++)
                {
                    LFinish[i].rcDst.X -= carSpeed;
                }
                for(int i=0;i < circlesList.Count; i++)
                {
                    circlesList[i].XC -= carSpeed;
                }
                for(int i=0;i< curvesList.Count; i++)
                {
                    for(int j = 0; j < curvesList[i].ControlPoints.Count; j++)
                    {
                        Point tempPT = curvesList[i].ControlPoints[j];
                        tempPT.X -= carSpeed;
                        curvesList[i].ControlPoints[j]= tempPT;
                    }
                }
                if (LHero.Count > 0)
                {
                    LHero[0].rcDst.X -= carSpeed;
                }
                currX -= carSpeed;
            }
            if(isMoving == true)
            {
                for (int i = 0; i < PtEnd.Count; i++){
                    if (PtSt[i].X <= LHero[0].rcDst.X && PtEnd[i].X >= LHero[0].rcDst.X && carOn != 'C')
                    {
                        carOn = 'L';
                        break;
                    }
                }
                for(int i = 0; i < circlesList.Count; i++)
                {
                    if (circlesList[i].XC <= LHero[0].rcDst.X && circlesList[i].XC + circlesList[i].Rad >= LHero[0].rcDst.X)
                    {
                        if (posCircle != i && carOn != 'C')
                        {
                            thball = 90;
                            flagCir = 0;
                        }
                        carOn = 'C';
                        posCircle = i;
                        break;
                    }
                }
                for(int i = 0;i <curvesList.Count; i++)
                {
                    if (curvesList[i].ControlPoints[0].X <= LHero[0].rcDst.X && curvesList[i].ControlPoints[8].X >= LHero[0].rcDst.X)
                    {
                        carOn = 'R';
                        if(posCurve!=i)
                        {
                            timeCurve = 0.01f;
                        }
                        posCurve = i;
                        break;
                    }
                }
                if(carOn == 'L')
                {
                    LHero[0].rcDst.X += carSpeed;
                }
                else if(carOn == 'R')
                {
                    PointF ppt = curvesList[posCurve].CalcCurvePointAtTime(timeCurve);
                    LHero[0].rcDst.X = (int)ppt.X;
                    LHero[0].rcDst.Y = (int)ppt.Y - 70;

                    timeCurve += (0.001f * carSpeed);
                }
                else if(carOn == 'C' && flagCir == 0)
                {
                    PointF pball = circlesList[posCircle].Getnextpoint(thball);
                    LHero[0].rcDst.X = (int)pball.X;
                    LHero[0].rcDst.Y = (int)pball.Y - 70;
                    thball -= (int)(0.5* carSpeed);
                    if(thball <= -270)
                    {
                        carOn = 'L';
                        flagCir = 1;
                    }
                }
                else
                {
                    LHero[0].rcDst.X += carSpeed;
                    carOn = 'L';
                }
            }
            if(isMoving == true)
            {
                if (LHero[0].rcDst.X >= LFinish[0].rcDst.X)
                {
                    tt.Stop();
                }
            }
            DrawDBuff(this.CreateGraphics());
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDBuff(e.Graphics);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            CreateBackGround();
            //CreateCar();
            DrawButtons();
        }
        void DrawButtons()
        {
            BG pnn = new BG();
            pnn.img = new Bitmap("button_line.png");
            pnn.rcSrc = new Rectangle(0, 0, pnn.img.Width, pnn.img.Height);
            pnn.rcDst = new Rectangle(this.Width - 220, 50, 200, 80);
            LButtons.Add(pnn);

            pnn = new BG();
            pnn.img = new Bitmap("button_circle.png");
            pnn.rcSrc = new Rectangle(0, 0, pnn.img.Width, pnn.img.Height);
            pnn.rcDst = new Rectangle(this.Width - 220, 150, 200, 80);
            LButtons.Add(pnn);

            pnn = new BG();
            pnn.img = new Bitmap("button_curve.png");
            pnn.rcSrc = new Rectangle(0, 0, pnn.img.Width, pnn.img.Height);
            pnn.rcDst = new Rectangle(this.Width - 220, 250, 200, 80);
            LButtons.Add(pnn);

            circleButton pnn2 = new circleButton();
            pnn2.X = this.Width - 100;
            pnn2.Y = 360;
            pnn2.r = 80;
            pnn2.clr = Color.Gray;
            speedList.Add(pnn2);
            pnn2 = new circleButton();
            pnn2.X = this.Width - 220;
            pnn2.Y = 360;
            pnn2.r = 80;
            pnn2.clr = Color.Gray;
            speedList.Add(pnn2);


            pnn2 = new circleButton();
            pnn2.X = this.Width - 100;
            pnn2.Y = 350;
            pnn2.r = 80;
            pnn2.clr = Color.GhostWhite;
            speedList.Add(pnn2);
            pnn2 = new circleButton();
            pnn2.X = this.Width - 220;
            pnn2.Y = 350;
            pnn2.r = 80;
            pnn2.clr = Color.GhostWhite;
            speedList.Add(pnn2);


            
        }
        void CreateCar()
        {
            Car pnn = new Car();
            pnn.img = new Bitmap("car.png");
            pnn.rcSrc = new Rectangle(0, 0, pnn.img.Width, pnn.img.Height);
            pnn.rcDst = new Rectangle(0, 750 - 70, 100, 70);
            LHero.Add(pnn);
        }
        void CreateBackGround()
        {
            BG pnn = new BG();
            pnn.img = new Bitmap("background.png");
            pnn.rcSrc = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            pnn.rcDst = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            LBackground.Add(pnn);
        }
        void DrawScene(Graphics g)
        {
            g.Clear(Color.White);

            for (int i = 0; i < ListOfLines.Count; i++)
            {
                LineSegment ptrav = ListOfLines[i];
                ptrav.DrawYourSelf(g);
            }
            for (int i = 0; i < LBackground.Count; i++)
            {
                if (LBackground[i].rcSrc.X + ClientSize.Width > LBackground[i].img.Width)
                {
                    //1
                    int cxRem = LBackground[i].img.Width - LBackground[i].rcSrc.X;
                    Rectangle Dst1 = new Rectangle(0, 0, cxRem, ClientSize.Height);
                    Rectangle Src1 = new Rectangle(LBackground[i].rcSrc.X, 0, cxRem, ClientSize.Height);
                    g.DrawImage(LBackground[i].img, Dst1, Src1, GraphicsUnit.Pixel);
                    //2
                    int cxRem2 = ClientSize.Width - cxRem;
                    Rectangle src2 = new Rectangle(0, 0, cxRem2, ClientSize.Height);
                    Rectangle Dst2 = new Rectangle(cxRem, 0, cxRem2, ClientSize.Height);
                    g.DrawImage(LBackground[i].img, Dst2, src2, GraphicsUnit.Pixel);
                }
                else
                {
                    g.DrawImage(LBackground[i].img, LBackground[i].rcDst, LBackground[i].rcSrc, GraphicsUnit.Pixel);
                }
            }
            for (int i = 0; i < LFinish.Count; i++)
            {
                g.DrawImage(LFinish[i].img, LFinish[i].rcDst, LFinish[i].rcSrc, GraphicsUnit.Pixel);
            }
            for (int i = 0; i < LButtons.Count; i++)
            {
                g.DrawImage(LButtons[i].img, LButtons[i].rcDst, LButtons[i].rcSrc, GraphicsUnit.Pixel);

            }
            for(int i = 0; i < circlesList.Count; i++)
            {
                circlesList[i].Drawcircle(g);
            }
            Pen pn = new Pen(Color.Black, 5);
            Brush br = new SolidBrush(Color.Gray);
            for (int i=0;i< PtSt.Count; i++)
            {
                g.DrawLine(pn, PtSt[i].X, PtSt[i].Y, PtEnd[i].X, PtEnd[i].Y);
                g.DrawLine(pn, PtSt[i].X, PtSt[i].Y - 20, PtEnd[i].X, PtEnd[i].Y - 20);
                
                //g.FillRectangle(br, PtSt[i].X + 95, PtSt[i].Y - 20, 5, 20);
                float tempX;
                for(int j = 0; j < PtSt.Count; j++)
                {
                    tempX = PtSt[j].X;
                    while(tempX < PtEnd[j].X)
                    {
                        g.FillRectangle(br, tempX, PtSt[j].Y - 20, 5, 20);
                        tempX += 50;
                    }
                }
                g.DrawLine(pn, (PtSt[i].X + PtEnd[i].X) / 2 , PtSt[i].Y, ((PtSt[i].X + PtEnd[i].X) / 2) - 50, PtSt[i].Y + 200);
                g.DrawLine(pn, (PtSt[i].X + PtEnd[i].X) / 2, PtSt[i].Y, ((PtSt[i].X + PtEnd[i].X) / 2) + 50, PtSt[i].Y + 200);
            }
            for(int i = 0; i < curvesList.Count; i++)
            {
                curvesList[i].DrawCurve(g);
                PointF tempX2 = curvesList[i].CalcCurvePointAtTime(0.5f);
                g.DrawLine(pn, tempX2.X , tempX2.Y, curvesList[i].ControlPoints[7].X , curvesList[i].ControlPoints[7].Y + 190);
                g.DrawLine(pn, tempX2.X, tempX2.Y, curvesList[i].ControlPoints[1].X, curvesList[i].ControlPoints[1].Y + 190);
            }
            for (int i = 0; i < LHero.Count; i++)
            {
                g.DrawImage(LHero[i].img, LHero[i].rcDst, LHero[i].rcSrc, GraphicsUnit.Pixel);
            }
            for (int i = 0; i < speedList.Count; i++)
            {
                pn = new Pen(Color.DarkBlue, 5);
                g.FillEllipse(new SolidBrush(speedList[i].clr), speedList[i].X, speedList[i].Y, speedList[i].r, speedList[i].r);
                g.DrawEllipse(pn, speedList[i].X, speedList[i].Y, speedList[i].r, speedList[i].r);
            }

            Font Ft = new Font("System", 50);
            g.DrawString("+", Ft, new SolidBrush(Color.Black), speedList[0].X + 10, speedList[0].Y - 5);
            g.DrawString("-", Ft, new SolidBrush(Color.Black), speedList[1].X + 20, speedList[1].Y - 10);
            Ft = new Font("System", 30);
            g.DrawString("Speed: " + carSpeed, Ft, new SolidBrush(Color.Black), speedList[1].X, speedList[1].Y + 100);
            
        }
        void DrawDBuff(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}
