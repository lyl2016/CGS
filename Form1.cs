﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace ThirdGroup
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        /*-- 定义变量 --*/
        Color BackColor1 = Color.White;
        Color ForeColor1 = Color.Black;
        public int MenuID, PressNum, FirstX, FirstY, OldX, OldY, PointNum, SaveNumber;
        public int R, XL, XR, YU, YD;
        Point[] group = new Point[100];
        Point[] pointsgroup = new Point[4];
        bool is_mainpicbox_vis = false;
        public struct EdgeInfo
        {
            int ymax, ymin;//Y的上下端点
            float k, xmin;//斜率倒数和X的下端点
            public int YMax { get { return ymax; } set { ymax = value; } }
            public int YMin { get { return ymin; } set { ymin = value; } }
            public float XMin { get { return xmin; } set { xmin = value; } }
            public float K { get { return k; } set { k = value; } }
            public EdgeInfo(int x1, int y1, int x2, int y2)
            {
                ymax = y2; ymin = y1; xmin = (float)x1; k = (float)(x1 - x2) / (float)(y1 - y2);
            }
        }//扫描线填充算法预定义结构
        Point3D[] modegroup = new Point3D[50];
        //string filepath_polyline;//represent a path of a polyline shapefile
        string filepath_polygon;//represent a path of a polygon shapefile
                                //private int FirstX;//用于扫描线填充【待实现】
        public bool is_polylineshp_open = false;//mark the status that a polyline file open
        public bool is_polygonshp_open = false;//mark the status that a polygon file open
                                               //PolyLines polylines;//用于Form1内调用
        PolyGons polygons;//用于Form1内调用
                          //public bool inname_form = true;///mark the method to represent of coordinate system
        ///true for screen coordinate, false for the coordinate of shapefile
        ///


        /*-- 绘图方法 --*/
        private void DDALine1(int x0, int y0, int x1, int y1)
        {
            int x, flag;
            float m, y;
            Graphics g = CreateGraphics();
            if (x0 == x1 && y0 == y1)
            {
                return;
            }
            if (x0 == x1)
            {
                if (y0 > y1)
                {
                    x = y0; y0 = y1; y1 = x;
                }
                for (x = y0; x <= y1; x++)
                {
                    g.DrawRectangle(Pens.Red, x1, x, 1, 1);
                }
                return;
            }
            if (y0 == y1)
            {
                if (x0 > x1)
                {
                    x = x0; x0 = x1; x1 = x;
                }
                for (x = x0; x <= x1; x++)
                {
                    g.DrawRectangle(Pens.Red, x, y0, 1, 1);
                }
                return;
            }
            if (x0 > x1)
            {
                x = x0; x0 = x1; x1 = x;
                x = y0; y0 = y1; y1 = x;
            }
            flag = 0;
            if (x1 - x0 > y1 - y0 && y1 - y0 > 0)
                flag = 1;
            if (x1 - x0 > y0 - y1 && y0 - y1 > 0)
            {
                flag = 2; y0 = -y0; y1 = -y1;
            }
            if (y1 - y0 > x1 - x0)
            {
                flag = 3; x = x0; x0 = y0; y0 = x; x = x1; x1 = y1; y1 = x;
            }
            if (y0 - y1 > x1 - x0)
            {
                flag = 4; x = x0; x0 = -y0; y0 = x; x = x1; x1 = -y1; y1 = x;
            }
            m = (float)(y1 - y0) / (float)(x1 - x0);
            for (x = x0, y = (float)y0; x <= x1; x++, y += m)
            {
                if (flag == 1) g.DrawRectangle(Pens.Red, x, (int)(y + 0.5), 1, 1);
                if (flag == 2) g.DrawRectangle(Pens.Red, x, -(int)(y + 0.5), 1, 1);
                if (flag == 3) g.DrawRectangle(Pens.Red, (int)(y + 0.5), x, 1, 1);
                if (flag == 4) g.DrawRectangle(Pens.Red, (int)(y + 0.5), -x, 1, 1);
            }
        }
        //DDA直线绘图方法
        private void MidLine1(int x0, int y0, int x1, int y1)
        {
            int x, y, d, flag;
            Graphics g = CreateGraphics();
            if (x0 == x1 && y0 == y1)
            {
                return;
            }
            if (x0 == x1)
            {
                if (y0 > y1)
                {
                    x = y0; y0 = y1; y1 = x;
                }
                for (y = y0; y <= y1; y++)
                {
                    g.DrawRectangle(Pens.Red, x1, y, 1, 1);
                }
                return;
            }
            if (y0 == y1)
            {
                if (x0 > x1)
                {
                    x = x0; x0 = x1; x1 = x;
                }
                for (x = x0; x <= x1; x++)
                {
                    g.DrawRectangle(Pens.Red, x, y0, 1, 1);
                }
                return;
            }
            if (x0 > x1)
            {
                x = x0; x0 = x1; x1 = x;
                x = y0; y0 = y1; y1 = x;
            }
            flag = 0;
            if (x1 - x0 > y1 - y0 && y1 - y0 > 0)
                flag = 1;
            if (x1 - x0 > y0 - y1 && y0 - y1 > 0)
            {
                flag = 2; y0 = -y0; y1 = -y1;
            }
            if (y1 - y0 > x1 - x0)
            {
                flag = 3; x = x0; x0 = y0; y0 = x; x = x1; x1 = y1; y1 = x;
            }
            if (y0 - y1 > x1 - x0)
            {
                flag = 4; x = x0; x0 = -y0; y0 = x; x = x1; x1 = -y1; y1 = x;
            }
            x = x0; y = y0; d = (x1 - x0) - 2 * (y1 - y0);
            while (x < x1 + 1)
            {
                if (flag == 1) g.DrawRectangle(Pens.Red, x, y, 1, 1);
                if (flag == 2) g.DrawRectangle(Pens.Red, x, -y, 1, 1);
                if (flag == 3) g.DrawRectangle(Pens.Red, y, x, 1, 1);
                if (flag == 4) g.DrawRectangle(Pens.Red, y, -x, 1, 1);
                x++;
                if (d > 0)
                {
                    d -= 2 * (y1 - y0);
                }
                else
                {
                    y++; d -= 2 * ((y1 - y0) - (x1 - x0));
                }
            }
        }
        //中点直线绘图方法
        private void BresenhamCircle1(int x0, int y0, int x1, int y1)
        {
            int r, d, x, y;
            Graphics g = CreateGraphics();
            r = (int)(Math.Sqrt((x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0)) + 0.5);
            x = 0; y = r; d = 3 - 2 * r;
            while (x < y || x == y)
            {
                g.DrawRectangle(Pens.Blue, x + x0, y + y0, 1, 1);
                g.DrawRectangle(Pens.Red, -x + x0, y + y0, 1, 1);
                g.DrawRectangle(Pens.Green, x + x0, -y + y0, 1, 1);
                g.DrawRectangle(Pens.Yellow, -x + x0, -y + y0, 1, 1);
                g.DrawRectangle(Pens.Black, y + x0, x + y0, 1, 1);
                g.DrawRectangle(Pens.Red, -y + x0, x + y0, 1, 1);
                g.DrawRectangle(Pens.Red, y + x0, -x + y0, 1, 1);
                g.DrawRectangle(Pens.Red, -y + x0, -x + y0, 1, 1);
                x += 1;
                if (d < 0 || d == 0)
                {
                    d = d + 4 * x + 6;
                }
                else
                {
                    y -= 1; d = d + 4 * (x - y) + 10;
                }
            }
        }
        //Bresenham画圆绘图方法
        private void CohenCut1(int x1, int y1, int x2, int y2)
        {
            int code1 = 0, code2 = 0, code, x = 0, y = 0;
            Graphics g = CreateGraphics();
            g.DrawLine(Pens.Red, x1, y1, x2, y2);//画原始线段
            code1 = encode(x1, y1);
            code2 = encode(x2, y2);
            Console.WriteLine("Begining code:");
            Console.WriteLine(Convert.ToString(code1) + " " + Convert.ToString(code2));
            Console.WriteLine(Convert.ToString(x1) + " " + Convert.ToString(y1));
            Console.WriteLine(Convert.ToString(x2) + " " + Convert.ToString(y2));
            while (code1 != 0 || code2 != 0)
            {
                Console.WriteLine("Loop Enter.");
                if ((code1 & code2) != 0)
                {
                    Console.WriteLine("return void");
                    return;
                } //完全不可见
                code = code1;
                if (code1 == 0) code = code2;
                if ((1 & code) != 0)
                {
                    x = XL;
                    y = y1 + (y2 - y1) * (x - x1) / (x2 - x1);
                    Console.WriteLine(Convert.ToString(x) + " 1 & code != 0 " + Convert.ToString(y));
                }
                else if ((2 & code) != 0)
                {
                    x = XR;
                    y = y1 + (y2 - y1) * (x - x1) / (x2 - x1);
                    Console.WriteLine(Convert.ToString(x) + " 2 & code != 0 " + Convert.ToString(y));
                }
                else if ((8 & code) != 0)
                {
                    y = YD;
                    x = x1 + (x2 - x1) * (y - y1) / (y2 - y1);
                    Console.WriteLine(Convert.ToString(x) + " 8 & code != 0 " + Convert.ToString(y));
                }
                else if ((4 & code) != 0)
                {
                    y = YU;
                    x = x1 + (x2 - x1) * (y - y1) / (y2 - y1);
                    Console.WriteLine(Convert.ToString(x) + " 4 & code != 0 " + Convert.ToString(y));
                }
                if (code == code1)
                {
                    x1 = x; y1 = y; code1 = encode(x, y);
                }
                else
                {
                    x2 = x; y2 = y; code2 = encode(x, y);
                }
                Console.WriteLine("Loop End.");
                Console.WriteLine(Convert.ToString(code1) + " " + Convert.ToString(code2));
            }
            Console.WriteLine("Loop Out draw line.");
            Pen MyPen = new Pen(Color.Yellow, 3);//创建一支粗笔
            g.DrawLine(MyPen, x1, y1, x2, y2);//画裁剪线段
        }
        private int encode(int x, int y)
        {
            int code = 0;//编码位规定：YU-YD-XR-XL
            if (x >= XL && x <= XR && y >= YD && y <= YU) code = 0;
            if (x < XL && y >= YD && y <= YU) code = 1;
            if (x > XR && y >= YD && y <= YU) code = 2;
            if (x >= XL && x <= XR && y > YU) code = 4;
            if (x >= XL && x <= XR && y < YD) code = 8;
            if (x < XL && y > YU) code = 5;
            if (x > XR && y > YU) code = 6;
            if (x < XL && y < YD) code = 9;
            if (x > XR && y < YD) code = 10;
            Console.WriteLine(Convert.ToString(x) + " " + Convert.ToString(y));
            return code;
        }
        //CohenCut图形裁剪方法
        /*private void ScanLineFill1()
		{
			EdgeInfo[] edgelist = new EdgeInfo[100];
			group[PressNum] = group[0];
			int j = 0, yu = 0, yd = 1024;
			for (int i = 0; i < PressNum; i++)
			{
				if (group[i].Y > yu) yu = group[i].Y;
				if (group[i].Y < yd) yd = group[i].Y;
				if (group[i].Y != group[i + 1].Y)
				{
					if (group[i].Y > group[i + 1].Y)
					{
						edgelist[j++] = new EdgeInfo(group[i + 1].X, group[i + 1].Y, group[i].X, group[i].Y);
					}
					else
					{
						edgelist[j++] = new EdgeInfo(group[i].X, group[i].Y, group[i + 1].X, group[i + 1].Y);
					}
				}
			}
			Graphics g = CreateGraphics();
			for (int y = yd; y < yu; y++)
			{
				//AEL
				var sorted = from item in edgelist
							 where y < item.YMax && y >= item.YMin
							 orderby item.XMin, item.K
							 select item;
				int flag = 0;
				foreach(var item in sorted)
				{
					if (flag == 0)
					{
						FirstX = (int)(item.XMin + 0.5);
						flag++;
					}
					else
					{
						g.DrawLine(Pens.Blue, (int)(item.XMin + 0.5), y, FirstX - 1, y);
						flag = 0;
					}
				}
				for(int i = 0; i < j; i++)
				{
					if (y < edgelist[i].YMax - 1 && y > edgelist[i].YMin)
					{
						edgelist[i].XMin += edgelist[i].K;
					}
				}
			}
		}*/
        //ScanLineFill图形填充方法
        private void TransSymmetry1(int x1, int y1, int x2, int y2)
        {
            if (x1 == x2 && y1 == y2)
            {
                return;
            }
            //此处二点重合，跳过
            double angle;
            if (x1 == x2 && y1 < y2)
            {
                angle = 3.1415926 / 2.0;
            }
            else if (x1 == x2 && y1 > y2)
            {
                angle = 3.1415926 * 1.5;
            }
            else
            {
                angle = Math.Atan((double)(y2 - y1) / (double)(x2 - x1));
            }
            angle = angle * 180.0 / 3.1415926;//角度单位转换
            Matrix myMatrix = new Matrix();
            myMatrix.Translate(-x1, -y1);
            myMatrix.Rotate(-(float)angle, MatrixOrder.Append);
            Matrix MyM1 = new Matrix(1, 0, 0, -1, 0, 0);
            myMatrix.Multiply(MyM1, MatrixOrder.Append);
            myMatrix.Rotate((float)angle, MatrixOrder.Append);
            myMatrix.Translate(x1, y1, MatrixOrder.Append);
            Graphics g = CreateGraphics();
            g.Transform = myMatrix;
            g.DrawPolygon(Pens.Bisque, pointsgroup);

        }
        //TransSymmetry对称方法
        private void TransShear1(int x1, int y1, int x2, int y2)
        {
            if (x1 == x2 && y1 == y2)
            {
                return;
            }
            //此处二点重合，跳过
            double angle;
            if (x1 == x2 && y1 < y2)
            {
                angle = 3.1415926 / 2.0;
            }
            else if (x1 == x2 && y1 > y2)
            {
                angle = 3.1415926 * 1.5;
            }
            else
            {
                angle = Math.Atan((double)(y2 - y1) / (double)(x2 - x1));
            }
            angle = angle * 180.0 / 3.1415926;//角度单位转换
            Matrix myMatrix = new Matrix();
            myMatrix.Translate(-x1, -y1);
            myMatrix.Rotate(-(float)angle, MatrixOrder.Append);
            myMatrix.Shear((float)1.0, 0, MatrixOrder.Append);
            myMatrix.Rotate((float)angle, MatrixOrder.Append);
            myMatrix.Translate(x1, y1, MatrixOrder.Append);
            Graphics g = CreateGraphics();
            g.Transform = myMatrix;
            g.DrawPolygon(Pens.BlueViolet, pointsgroup);
        }

        /*-- 图形事件 --*/
        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //退出功能键
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics g = CreateGraphics();
            Pen MyPen = new Pen(Color.Red, 1);
            if (MenuID == 1 || MenuID == 2 || MenuID == 3)
            {
                if (PressNum == 0)
                {
                    FirstX = e.X; FirstY = e.Y;
                    OldX = e.X; OldY = e.Y;
                }
                else
                {
                    if (MenuID == 1)
                        DDALine1(FirstX, FirstY, e.X, e.Y);
                    if (MenuID == 2)
                        MidLine1(FirstX, FirstY, e.X, e.Y);
                    if (MenuID == 3)
                        BresenhamLine1(FirstX, FirstY, e.X, e.Y);
                }
                PressNum++;
                if (PressNum >= 2) PressNum = 0;
            }
            if (MenuID == 5)
            {
                if (PressNum == 0)
                {
                    FirstX = e.X; FirstY = e.Y;
                }
                else
                {
                    if (FirstX == e.X && FirstY == e.Y)
                        return;
                    BresenhamCircle1(FirstX, FirstY, e.X, e.Y);
                }
                PressNum++;
                if (PressNum >= 2) PressNum = 0;
            }
            if (MenuID == 7 || MenuID == 8)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Pen MyPen2 = new Pen(Color.Black, 1);
                    if (PointNum == 0)
                    {
                        OldX = e.X; OldY = e.Y;
                    }
                    else
                    {
                        g.DrawLine(MyPen2, OldX, OldY, e.X, e.Y);
                        OldX = e.X; OldY = e.Y;
                    }
                    group[PointNum].X = e.X;
                    group[PointNum++].Y = e.Y;
                    g.DrawLine(Pens.Black, e.X - 5, e.Y, e.X + 5, e.Y);
                    g.DrawLine(Pens.Black, e.X, e.Y - 5, e.X, e.Y + 5);
                    PressNum = 1;
                }
                if (e.Button == MouseButtons.Right && PointNum > 3)
                {
                    if (MenuID == 7)
                    {
                        Bezier1(1);
                        MenuID = 107;
                    }
                    if (MenuID == 8)
                    {
                        BSample1(1);
                        MenuID = 108;
                    }
                    PressNum = 0;
                }
            }
            if (MenuID == 11)
            {
                if (PressNum == 0)
                {
                    FirstX = e.X; FirstY = e.Y;
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        pointsgroup[i].X += e.X - FirstX;
                        pointsgroup[i].Y += e.Y - FirstY;
                    }
                    g.DrawPolygon(Pens.Blue, pointsgroup);
                }
                PressNum++;
                if (PressNum >= 2) PressNum = 0;
            }
            if (MenuID == 12)
            {
                if (PressNum == 0)
                {
                    FirstX = e.X; FirstY = e.Y;
                }
                else
                {
                    double a;
                    if (e.X == FirstX && e.Y == FirstY)
                        return;
                    if (e.X == FirstX && e.Y > FirstY)
                        a = 3.1415926 / 2.0;
                    else if (e.X == FirstX && e.Y < FirstY)
                        a = 3.1415926 / 2.0 * 3.0;
                    else
                        a = Math.Atan((double)(e.Y - FirstY) / (double)(e.X - FirstX));
                    a = a / 3.1415926 * 180.0;
                    int x0 = 150, y0 = 150;
                    Matrix myMatrix = new Matrix();
                    myMatrix.Translate(-x0, -y0);
                    myMatrix.Rotate((float)a, MatrixOrder.Append);
                    myMatrix.Translate(x0, y0, MatrixOrder.Append);
                    g.Transform = myMatrix;
                    g.DrawPolygon(Pens.Blue, pointsgroup);
                }
                PressNum++;
                if (PressNum >= 2) PressNum = 0;
            }
            if (MenuID == 14 || MenuID == 15)
            {
                if (PressNum == 0)
                {
                    FirstX = e.X; FirstY = e.Y;
                }
                else
                {
                    if (MenuID == 14)
                    {
                        g.Clear(BackColor1);
                        g.DrawLine(Pens.CadetBlue, FirstX, FirstY, e.X, e.Y);
                        pointsgroup[0] = new Point(100, 100);
                        pointsgroup[1] = new Point(200, 100);
                        pointsgroup[2] = new Point(200, 200);
                        pointsgroup[3] = new Point(100, 200);
                        g.DrawPolygon(Pens.Red, pointsgroup);
                        TransSymmetry1(FirstX, FirstY, e.X, e.Y);
                    }
                    if (MenuID == 15)
                    {
                        g.Clear(BackColor1);
                        pointsgroup[0] = new Point(100, 100);
                        pointsgroup[1] = new Point(200, 100);
                        pointsgroup[2] = new Point(200, 200);
                        pointsgroup[3] = new Point(100, 200);
                        g.DrawPolygon(Pens.Red, pointsgroup);
                        TransShear1(FirstX, FirstY, e.X, e.Y);
                    }
                }
                PressNum++;
                if (PressNum >= 2) PressNum = 0;
            }
            if (MenuID == 21) //Cohen裁剪算法
            {
                if (PressNum == 0)  //保留第一点
                {
                    FirstX = e.X; FirstY = e.Y; PressNum++;
                }
                else //第二点，调用裁减函数
                {
                    CohenCut1(FirstX, FirstY, e.X, e.Y); //CohenCut裁剪
                    PressNum = 0; //清零，为下一次裁剪做准备
                }
            }
            if (MenuID == 24 || MenuID == 31)
            {
                //Graphics g = CreateGraphics();
                if (e.Button == MouseButtons.Left)
                {
                    group[PressNum].X = e.X;
                    group[PressNum].Y = e.Y;
                    if (PressNum > 0)
                    {
                        g.DrawLine(Pens.Red, group[PressNum - 1], group[PressNum]);
                    }
                    PressNum++;
                }
                if (e.Button == MouseButtons.Right)
                {
                    g.DrawLine(Pens.Red, group[PressNum - 1], group[0]);
                    if (MenuID == 31)
                        //ScanLineFill1();
                    if (MenuID == 24)
                        WindowCut1();
                    PressNum = 0;
                }
            }
            if (MenuID == 107 || MenuID == 108)
            {
                if (e.Button == MouseButtons.Left && PressNum == 0)
                {
                    for (int i = 0; i < PointNum; i++)
                    {
                        if ((e.X >= group[i].X - 5) && (e.X <= group[i].X + 5) && (e.Y >= group[i].Y - 5) && (e.Y <= group[i].Y + 5))
                        {
                            SaveNumber = i;
                            PressNum = 1;
                        }
                    }
                }
                if (e.Button == MouseButtons.Right && PointNum > 3)
                {
                    PressNum = 0;
                }
            }
        }
        //预定义画布
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Graphics g = CreateGraphics();
            Pen BackPen = new Pen(BackColor1, 1);
            Pen MyPen = new Pen(ForeColor1, 1);
            if ((MenuID == 1 || MenuID == 2 || MenuID == 3) && PressNum == 1)
            {
                if (!(e.X == OldX && e.Y == OldY))
                {
                    g.DrawLine(BackPen, FirstX, FirstY, OldX, OldY);
                    g.DrawLine(MyPen, FirstX, FirstY, e.X, e.Y);
                    OldX = e.X;
                    OldY = e.Y;
                }
            }
            if (MenuID == 5 && PressNum == 1)
            {
                if (!(e.X == OldX && e.Y == OldY))
                {
                    double r = Math.Sqrt((FirstX - OldX) * (FirstX - OldX) + (FirstY - OldY) * (FirstY - OldY));//求圆半径
                    int r1 = (int)(r + 0.5);//取整
                    g.DrawEllipse(BackPen, FirstX - r1, FirstY - r1, 2 * r1, 2 * r1);//擦除旧圆
                    r = Math.Sqrt((FirstX - e.X) * (FirstX - e.X) + (FirstY - e.Y) * (FirstY - e.Y));//求圆半径
                    r1 = (int)(r + 0.5);//取整
                    g.DrawEllipse(MyPen, FirstX - r1, FirstY - r1, 2 * r1, 2 * r1);//画新圆
                    OldX = e.X;
                    OldY = e.Y;
                }
            }
            if ((MenuID == 31 || MenuID == 24) && PressNum > 0)
            {
                if (!(e.X == OldX && e.Y == OldY))
                {
                    g.DrawLine(BackPen, group[PressNum - 1].X, group[PressNum - 1].Y, OldX, OldY);
                    g.DrawLine(MyPen, group[PressNum - 1].X, group[PressNum - 1].Y, e.X, e.Y);
                    OldX = e.X;
                    OldY = e.Y;
                }
            }
            if ((MenuID == 107 || MenuID == 108) && PressNum > 0)
            {
                if (!(group[SaveNumber].X == e.X && group[SaveNumber].Y == e.Y))
                {
                    //Graphics g = CreateGraphics();
                    g.DrawLine(BackPen, group[SaveNumber].X - 5, group[SaveNumber].Y, group[SaveNumber].X + 5, group[SaveNumber].Y);
                    g.DrawLine(BackPen, group[SaveNumber].X, group[SaveNumber].Y - 5, group[SaveNumber].X, group[SaveNumber].Y + 5);
                    if (SaveNumber > 0)
                    {
                        g.DrawLine(BackPen, group[SaveNumber - 1].X, group[SaveNumber - 1].Y, group[SaveNumber].X, group[SaveNumber].Y);
                        g.DrawLine(MyPen, group[SaveNumber - 1].X, group[SaveNumber - 1].Y, e.X, e.Y);
                    }
                    if (SaveNumber <= PointNum - 2)
                    {
                        g.DrawLine(BackPen, group[SaveNumber].X, group[SaveNumber].Y, group[SaveNumber + 1].X, group[SaveNumber + 1].Y);
                        g.DrawLine(MyPen, group[SaveNumber + 1].X, group[SaveNumber + 1].Y, e.X, e.Y);
                    }
                    if (MenuID == 107)
                        Bezier1(0);
                    if (MenuID == 108)
                        BSample1(0);
                    g.DrawLine(MyPen, e.X - 5, e.Y, e.X + 5, e.Y);
                    g.DrawLine(MyPen, e.X, e.Y - 5, e.X, e.Y + 5);
                    group[SaveNumber].X = e.X;
                    group[SaveNumber].Y = e.Y;
                    if (MenuID == 107)
                        Bezier1(1);
                    if (MenuID == 108)
                        BSample1(1);
                }
            }
        }
        //确认绘图终点(橡皮筋)
        private void DDALine_Click(object sender, EventArgs e)
        {
            MenuID = 1; PressNum = 0;
            Graphics g = CreateGraphics();
            g.Clear(BackColor1);
        }
        //DDA直线预定义画布

        private void Form1_Load(object sender, EventArgs e)
        {
            MainPicBox.Visible = false;
        }
        //窗体加载时设置窗体隐藏
        private void MainPicBox_Click(object sender, EventArgs e)
        {
            is_mainpicbox_vis = !is_mainpicbox_vis;
            MainPicBox.Visible = is_mainpicbox_vis;
            label1.Visible = is_mainpicbox_vis;
        }
        private void Label1_Click(object sender, EventArgs e)
        {
            is_mainpicbox_vis = !is_mainpicbox_vis;
            MainPicBox.Visible = is_mainpicbox_vis;
            label1.Visible = is_mainpicbox_vis;
        }
        //点击一次关闭画布
        private void MidLine_Click(object sender, EventArgs e)
        {
            MenuID = 2; PressNum = 0;
            Graphics g = CreateGraphics();
            g.Clear(BackColor1);
        }
        //中点直线预定义画布
        private void BresenhamCircle_Click(object sender, EventArgs e)
        {
            MenuID = 5; PressNum = 0;
            Graphics g = CreateGraphics();
            g.Clear(BackColor1);
        }
        //Bresenham画圆预定义画布
        private void TransMove_Click(object sender, EventArgs e)
        {
            MenuID = 11; PressNum = 0;
            Graphics g = CreateGraphics();
            g.Clear(BackColor1);
            pointsgroup[0] = new Point(100, 100);
            pointsgroup[1] = new Point(200, 100);
            pointsgroup[2] = new Point(200, 200);
            pointsgroup[3] = new Point(100, 200);
            g.DrawPolygon(Pens.Red, pointsgroup);
        }
        //TransMove图形平移预定义画布
        private void TransRotate_Click(object sender, EventArgs e)
        {
            MenuID = 12; PressNum = 0;
            Graphics g = CreateGraphics();
            g.Clear(BackColor1);
            pointsgroup[0] = new Point(100, 100);
            pointsgroup[1] = new Point(200, 100);
            pointsgroup[2] = new Point(200, 200);
            pointsgroup[3] = new Point(100, 200);
            g.DrawPolygon(Pens.Red, pointsgroup);
        }
        //TransRotate图形旋转预定义画布
        private void TransScale_Click(object sender, EventArgs e)
        {
            MenuID = 13;
            float xs, ys;
            MyForm myf = new MyForm();
            if (myf.ShowDialog() == DialogResult.Cancel)
            {
                myf.Close();
                return;
            }
            xs = myf.Xscale;
            ys = myf.Yscale;
            myf.Close();
            Graphics g = CreateGraphics();
            g.Clear(BackColor1);
            pointsgroup[0] = new Point(100, 100);
            pointsgroup[1] = new Point(200, 100);
            pointsgroup[2] = new Point(200, 200);
            pointsgroup[3] = new Point(100, 200);
            g.DrawPolygon(Pens.Red, pointsgroup);
            Matrix myMatrix = new Matrix();
            myMatrix.Translate(-100, -100);
            myMatrix.Scale(xs, ys, MatrixOrder.Append);
            myMatrix.Translate(100, 100, MatrixOrder.Append);
            g.Transform = myMatrix;
            g.DrawPolygon(Pens.Blue, pointsgroup);
        }
        //TransScale图形缩放预定义画布
        private void TransSymmetry_Click(object sender, EventArgs e)
        {
            MenuID = 14; PressNum = 0;
            Graphics g = CreateGraphics();
            g.Clear(BackColor1);
            pointsgroup[0] = new Point(100, 100);
            pointsgroup[1] = new Point(200, 100);
            pointsgroup[2] = new Point(200, 200);
            pointsgroup[3] = new Point(100, 200);
            g.DrawPolygon(Pens.Red, pointsgroup);
        }
        //TransSymmetry对称变换预定义画布
        private void TransShear_Click(object sender, EventArgs e)
        {
            MenuID = 15; PressNum = 0;
            Graphics g = CreateGraphics();
            g.Clear(BackColor1);
            pointsgroup[0] = new Point(100, 100);
            pointsgroup[1] = new Point(200, 100);
            pointsgroup[2] = new Point(200, 200);
            pointsgroup[3] = new Point(100, 200);
            g.DrawPolygon(Pens.Red, pointsgroup);
        }
        //TransShear错切变换预定义画布
        private void CohenCut_Click(object sender, EventArgs e)
        {
            MenuID = 21; PressNum = 0;
            Graphics g = CreateGraphics();
            g.Clear(BackColor1);
            XL = 100; XR = 400; YD = 100; YU = 400;
            pointsgroup[0] = new Point(XL, YD);
            pointsgroup[1] = new Point(XR, YD);
            pointsgroup[2] = new Point(XR, YU);
            pointsgroup[3] = new Point(XL, YU);
            g.DrawPolygon(Pens.Blue, pointsgroup);
        }
        //CohenCut预定义画布
        private void ScanLineFill_Click(object sender, EventArgs e)
        {
            MainPicBox.Visible = true;
            label1.Visible = true;
            is_polygonshp_open = false;
            //Stopwatch stopWatch = new Stopwatch();
            if (is_polygonshp_open == false)
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Multiselect = false,//该值确定是否可以选择多个文件
                    Title = "请选择Shapefile文件",
                    Filter = "Shapefile文件(*.shp)|*.shp"
                };
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    filepath_polygon = dialog.FileName;
                    is_polygonshp_open = true;
                    polygons = new PolyGons();
                    polygons.ReadShapeFile(filepath_polygon);
                    if (polygons.MH.ShapeType == 5)
                    {
                        polygons.DrawPolyGons(MainPicBox);
                        Console.WriteLine("No Problem");
                        //stopWatch.Start();
                        polygons.CheckLineInPolygonsInse();
                        Console.WriteLine("No Problem");
                        //stopWatch.Stop();
                        polygons.DrawInsePoints(MainPicBox);
                        Console.WriteLine("No Problem");
                        // Get the elapsed time as a TimeSpan value.
                        //TimeSpan ts = stopWatch.Elapsed;
                        // Format and display the TimeSpan value.
                        //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                        //Console.WriteLine("运行用时：" + elapsedTime);
                        //VerString.Text = "运行用时：" + elapsedTime + "，共绘制了" + polygons.inse_points.Count + "个交点";
                    }
                    else
                    {
                        DialogResult retry_open_polygon = MessageBox.Show("你选择的文件不是多边形文件", "请重试", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    filepath_polygon = "";
                    is_polygonshp_open = false;
                }
            }
            //is_polylineshp_open = false;
            //MenuID = 31; PressNum = 0;
            //Graphics g = CreateGraphics();
            //g.Clear(BackColor1);
        }
        //ScanLineFill预定义画布

        private void WindowCut_Click(object sender, EventArgs e)
        {
            MenuID = 24; PressNum = 0;
            Graphics g = CreateGraphics();  //创建图形设备
            g.Clear(BackColor1);          //设置背景色
            XL = 100; XR = 400; YD = 100; YU = 400;
            pointsgroup[0] = new Point(XL, YD);
            pointsgroup[1] = new Point(XR, YD);
            pointsgroup[2] = new Point(XR, YU);
            pointsgroup[3] = new Point(XL, YU);
            g.DrawPolygon(Pens.Blue, pointsgroup); //画出裁剪窗口
        }
        private void WindowCut1()           //多边形和裁剪结果都存放在group数组中
        {
            group[PressNum] = group[0];     //将第一点复制为数组最后一组
            EdgeClipping(0);
            EdgeClipping(1);
            EdgeClipping(2);
            EdgeClipping(3);        //用第一、二、三、四条窗口边进行裁剪
            Graphics g = CreateGraphics();  //创建图形设备
            XL = 100; XR = 400; YD = 100; YU = 400;
            pointsgroup[0] = new Point(XL, YD);
            pointsgroup[1] = new Point(XR, YD);
            pointsgroup[2] = new Point(XR, YU);
            pointsgroup[3] = new Point(XL, YU);
            g.DrawPolygon(Pens.Blue, pointsgroup);
            Pen MyPen = new Pen(Color.Black, 3);
            for (int i = 0; i < PressNum; i++)  //绘制裁剪多边形
                g.DrawLine(MyPen, group[i], group[i + 1]);
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Graphics g = CreateGraphics();
            Pen MyPen = new Pen(Color.White, 1);
            if (MenuID == 107 || MenuID == 108)
            {
                for (int i = 0; i < PointNum; i++)
                {
                    g.DrawLine(MyPen, group[i].X - 5, group[i].Y, group[i].X + 5, group[i].Y);
                    g.DrawLine(MyPen, group[i].X, group[i].Y - 5, group[i].X, group[i].Y + 5);
                    if (i > 0)
                    {
                        g.DrawLine(MyPen, group[i - 1].X, group[i - 1].Y, group[i].X, group[i].Y);
                    }
                }
                if (MenuID == 107)
                {
                    Bezier1(2);
                    MenuID = 7;
                }
                if (MenuID == 108)
                {
                    BSample1(2);
                    MenuID = 8;
                }
                PressNum = 0;
                PointNum = 0;
            }
        }

        private void EdgeClipping(int linecode)
        {
            float x, y;
            int n, i, number1;
            Point[] q = new Point[200]; //创建点数组存放裁剪结果
            number1 = 0;
	        if (linecode == 0)          //x=XL用窗口左边来裁剪多边形
            {
                for (n = 0; n < PressNum; n++)
                {
                    if (group[n].X < XL && group[n + 1].X < XL) { }   //外外，不输出
                    if (group[n].X >= XL && group[n + 1].X >= XL) //内内，输出后点
                    {
                        q[number1++] = group[n + 1];
                    }
                    if (group[n].X >= XL && group[n + 1].X < XL)  //内外，输出交点
                    {
                        y = group[n].Y + (float)(group[n + 1].Y - group[n].Y) / (float)(group[n + 1].X - group[n].X) * (float)(XL - group[n].X);
                        q[number1].X = XL;
                        q[number1++].Y = (int)y;
                    }
                    if (group[n].X < XL && group[n + 1].X >= XL)  //外内，输出交点、后点
                    {
                        y = group[n].Y + (float)(group[n + 1].Y - group[n].Y) / (float)(group[n + 1].X - group[n].X) * (float)(XL - group[n].X);
                        q[number1].X = XL;
                        q[number1++].Y = (int)y;
                        q[number1++] = group[n + 1];
                    }
                }
                for (i = 0; i < number1; i++)    //裁剪结果存入group数组
                {
                    group[i] = q[i];
                }
                group[number1] = q[0];
                PressNum = number1;
            }
            if (linecode == 1)              //y=YU用窗口顶边来裁剪多边形
            {
                for (n = 0; n < PressNum; n++)
                {
                    if (group[n].Y >= YU && group[n + 1].Y >= YU)   //外外，不输出
                    { }
                    if (group[n].Y < YU && group[n + 1].Y < YU)     //内内，输出后点
                    {
                        q[number1++] = group[n + 1];
                    }
                    if (group[n].Y < YU && group[n + 1].Y >= YU)    //内外，输出交点
                    {
                        x = group[n].X + (float)(group[n + 1].X - group[n].X) / (float)(group[n + 1].Y - group[n].Y) * (float)(YU - group[n].Y);
                        q[number1].X = (int)x;
                        q[number1++].Y = YU;
                    }
                    if (group[n].Y >= YU && group[n + 1].Y < YU)    //外内，输出交点、后点
                    {
                        x = group[n].X + (float)(group[n + 1].X - group[n].X) / (float)(group[n + 1].Y - group[n].Y) * (float)(YU - group[n].Y);
                        q[number1].X = (int)x;
                        q[number1++].Y = YU;
                        q[number1++] = group[n + 1];
                    }
                }
                for (i = 0; i < number1; i++)
                {
                    group[i] = q[i];
                }
                group[number1] = q[0];
                PressNum = number1;
            }
            if (linecode == 2)                      //x=XR,用窗口右边来裁剪多边形
            {
                for (n = 0; n < PressNum; n++)
                {
                    if (group[n].X >= XR && group[n + 1].X >= XR)   //外外，不输出
                    { }
                    if (group[n].X < XR && group[n + 1].X < XR) //内内，输出后点
                    {
                        q[number1++] = group[n + 1];
                    }
                    if (group[n].X < XR && group[n + 1].X >= XR)    //内外，输出交点
                    {
                        y = group[n].Y + (float)(group[n + 1].Y - group[n].Y) / (float)(group[n + 1].X - group[n].X) * (float)(XR - group[n].X);
                        q[number1].X = XR;
                        q[number1++].Y = (int)y;
                    }
                    if (group[n].X >= XR && group[n + 1].X < XR)    //外内，输出交点、后点
                    {
                        y = group[n].Y + (float)(group[n + 1].Y - group[n].Y) / (float)(group[n + 1].X - group[n].X) * (float)(XR - group[n].X);
                        q[number1].X = XR;
                        q[number1++].Y = (int)y;
                        q[number1++] = group[n + 1];
                    }
                }
                for (i = 0; i < number1; i++)
                {
                    group[i] = q[i];
                }
                group[number1] = q[0];
                PressNum = number1;
            }
            if (linecode == 3)                          //y=YD,用窗口底边来裁剪多边形
            {
                for (n = 0; n < PressNum; n++)
                {
                    if (group[n].Y < YD && group[n + 1].Y < YD) //外外，不输出
                    { }
                    if (group[n].Y >= YD && group[n + 1].Y >= YD)   //内内，输出后点
                    {
                        q[number1++] = group[n + 1];
                    }
                    if (group[n].Y >= YD && group[n + 1].Y < YD)    //内外，输出交点
                    {
                        x = group[n].X + (float)(group[n + 1].X - group[n].X) / (float)(group[n + 1].Y - group[n].Y) * (float)(YD - group[n].Y);
                        q[number1].X = (int)x;
                        q[number1++].Y = YD;
                    }
                    if(group[n].Y<YD&&group[n+1].Y>=YD)
                    {
                        x = group[n].X + (float)(group[n + 1].X - group[n].X) / (float)(group[n + 1].Y - group[n].Y) * (float)(YD - group[n].Y);
                        q[number1].X = (int)x;
                        q[number1++].Y = YD;
                        q[number1++] = group[n + 1];
                    }
                }
                for (i = 0; i < number1; i++)
                {
                    group[i] = q[i];
                }
                group[number1] = q[0];
                PressNum = number1;
            }
        }

        private void BezierCurve_Click(object sender, EventArgs e)
        {
            MenuID = 7;
            PressNum = 0;
            PointNum = 0;
            Graphics g = CreateGraphics();
            g.Clear(BackColor1);
        }
        private void Bezier1(int mode)
        {
            Point[] p = new Point[300];
            int i, j;
            i = 0; j = 0;
            p[i++] = group[j++];
            p[i++] = group[j++];
            while (j <= PointNum - 2)
            {
                p[i++] = group[j++];
                p[i].X = (group[j].X + group[j - 1].X) / 2;
                p[i++].Y = (group[j].Y + group[j - 1].Y) / 2;
                p[i++] = group[j++];
            };
            for (j = 0; j < i - 3; j += 3)
            {
                Bezier_4(mode, p[j], p[j + 1], p[j + 2], p[j + 3]);
            }
        }

        private void BSampleCurve_Click(object sender, EventArgs e)
        {
            MenuID = 8;PressNum = 0;PointNum = 0;
            Graphics g = CreateGraphics();
            g.Clear(BackColor1);
        }
        private void BSample1(int mode)
        {
            for (int i = 0; i < PointNum - 3; i++)
            {
                BSample_4(mode, group[i], group[i + 1], group[i + 2], group[i + 3]);
            }
        }
        private void BSample_4(int mode, Point p0, Point p1, Point p2, Point p3)
        {
            Graphics g = CreateGraphics();
            Point p = new Point();
            Point oldp = new Point();
            Pen MyPen = new Pen(Color.Red, 1);
            int n = 100;
            if (mode == 2)
            {
                MyPen = new Pen(Color.Red, 1);
            }
            if (mode == 1)
            {
                MyPen = new Pen(Color.Black, 1);
            }
            if (mode == 0)
            {
                MyPen = new Pen(Color.White, 1);
            }
            oldp = p1;
            double dt = 1.0 / n;
            for (double t = 0.0; t <= 1.0; t += dt)
            {
                double t1 = (1.0 - t) * (1.0 - t) * (1.0 - t);
                double t2 = 3.0 * t * t * t - 6.0 * t * t + 4.0;
                double t3 = -3.0 * t * t * t + 3.0 * t * t + 3.0 * t + 1.0;
                double t4 = t * t * t;
                p.X = (int)((t1 * p0.X + t2 * p1.X + t3 * p2.X + t4 * p3.X) / 6.0);
                p.Y = (int)((t1 * p0.Y + t2 * p1.Y + t3 * p2.Y + t4 * p3.Y) / 6.0);
                if (t > 0)
                    g.DrawLine(MyPen, oldp, p);
                oldp = p;
            }
        }

        private void ParalleProjection_Click(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();
            g.Clear(BackColor1);
            MenuID = 41;
            Projection1();
        }
        private void Projection1()
        {
            //模型数据存入数组，完成模型建立
            modegroup[0] = new Point3D(100, 100, 100);
            modegroup[1] = new Point3D(200, 100, 100);
            modegroup[2] = new Point3D(200, 200, 100);
            modegroup[3] = new Point3D(100, 200, 100);
            modegroup[4] = new Point3D(100, 100, 200);
            modegroup[5] = new Point3D(200, 100, 200);
            modegroup[6] = new Point3D(200, 200, 200);
            modegroup[7] = new Point3D(100, 200, 200);
            Point3D vp = new Point3D(50, 30, 500);
            if (MenuID == 41 || MenuID == 42)
                vp = InputProjectionDirection();//通过对话框输入投影方向参数
            for (int i = 0; i < 8; i++)
            {
                if (MenuID == 41)
                    group[i] = ParallelP(vp, modegroup[i]);
                if (MenuID == 42)
                    group[i] = PerspectiveP(vp, modegroup[i]);
                if (MenuID == 43)
                    group[i] = SimpleP(modegroup[i]);
            }
            Graphics g = CreateGraphics();
            g.DrawLine(Pens.Red, group[0], group[1]);
            g.DrawLine(Pens.Red, group[1], group[2]);
            g.DrawLine(Pens.Red, group[2], group[3]);
            g.DrawLine(Pens.Red, group[3], group[0]);
            g.DrawLine(Pens.Red, group[4], group[5]);
            g.DrawLine(Pens.Red, group[5], group[6]);
            g.DrawLine(Pens.Red, group[6], group[7]);
            g.DrawLine(Pens.Red, group[7], group[4]);
            g.DrawLine(Pens.Red, group[0], group[4]);
            g.DrawLine(Pens.Red, group[1], group[5]);
            g.DrawLine(Pens.Red, group[2], group[6]);
            g.DrawLine(Pens.Red, group[3], group[7]);
        }
        private Point3D InputProjectionDirection()
        {
            Form MyInputForm = new Form();//创建窗口
            Label label1 = new Label();//创建Label控件
            Label label2 = new Label();
            Label label3 = new Label();
            label1.Text = "X:";
            label2.Text = "Y:";
            label3.Text = "Z:";
            label1.Location = new Point(10, 10);//对Label控件进行定位
            label2.Location = new Point(10, 40);
            label3.Location = new Point(10, 70);
            label1.Width = 30;
            label2.Width = 30;
            label3.Width = 30;

            NumericUpDown numeric1 = new NumericUpDown();//创建数据控件
            NumericUpDown numeric2 = new NumericUpDown();
            NumericUpDown numeric3 = new NumericUpDown();
            numeric1.Minimum = 0; numeric1.Maximum = 500;
            numeric1.Increment = 10; numeric1.Value = 150;
            numeric2.Minimum = 0; numeric2.Maximum = 500;
            numeric2.Increment = 10; numeric2.Value = 150;
            if (MenuID == 41)
            {
                numeric3.Minimum = -800; numeric3.Maximum = -300;
                numeric3.Increment = 10; numeric3.Value = -500;
            }
            else
            {
                numeric3.Minimum = 300;numeric3.Maximum = 800;
                numeric3.Increment = 10;numeric3.Value = 500;
            }
            numeric1.Location = new Point(50, 10);
            numeric2.Location = new Point(50, 40);
            numeric3.Location = new Point(50, 70);

            Button button1 = new Button();//创建按键控件
            Button button2 = new Button();
            button1.Text = "确定";
            button1.DialogResult = DialogResult.OK;
            button2.Text = "取消";
            button2.DialogResult = DialogResult.Cancel;
            button1.Location = new Point(10, 100);
            button2.Location = new Point(button1.Width + 20, 100);
            if (MenuID == 41)
                MyInputForm.Text = "请输入透视投影方向向量";
            else
                MyInputForm.Text = "请输入透视投影中心位置";
            MyInputForm.HelpButton = true;
            MyInputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            MyInputForm.MaximizeBox = false;
            MyInputForm.MinimizeBox = false;
            MyInputForm.AcceptButton = button1;
            MyInputForm.CancelButton = button2;
            MyInputForm.StartPosition = FormStartPosition.CenterScreen;
            MyInputForm.Controls.Add(label1);
            MyInputForm.Controls.Add(label2);
            MyInputForm.Controls.Add(label3);
            MyInputForm.Controls.Add(numeric1);
            MyInputForm.Controls.Add(numeric2);
            MyInputForm.Controls.Add(numeric3);
            MyInputForm.Controls.Add(button1);
            MyInputForm.Controls.Add(button2);
            Point3D vp = new Point3D(150, 150, -500);
            if (MyInputForm.ShowDialog() == DialogResult.Cancel)
            {
                MyInputForm.Close();
                return vp;
            }
            vp = new Point3D((float)numeric1.Value, (float)numeric2.Value, (float)numeric3.Value);
            MyInputForm.Close();
            return vp;
        }
        private Point ParallelP(Point3D ViewP, Point3D ModeP)
        {
            Point p;
            int x, y;
            x = (int)(ModeP.X - ViewP.X / ViewP.Z * ModeP.Z + 0.5);
            y = (int)(ModeP.Y - ViewP.Y / ViewP.Z * ModeP.Z + 0.5);
            p = new Point(x, y);
            return p;
        }
        private Point PerspectiveP(Point3D ViewP, Point3D ModeP)
        {
            Point p;
            int x, y;
            x = (int)(ViewP.X + (ModeP.X - ViewP.X) * ViewP.Z / (ViewP.Z - ModeP.Z) + 0.5);
            y = (int)(ViewP.Y + (ModeP.Y - ViewP.Y) * ViewP.Z / (ViewP.Z - ModeP.Z) + 0.5);
            p = new Point(x, y);
            return p;
        }
        private Point SimpleP(Point3D p3d)
        {
            Point p;
            float kx, ky;
            kx = (float)0.4;ky = (float)0.3;
            p = new Point((int)(p3d.X - kx * p3d.Z + 0.5), (int)(p3d.Y - ky * p3d.Z + 0.5));
            return p;
        }

        private void PerspectiveProjection_Click(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();
            g.Clear(BackColor1);
            MenuID = 42;
            Projection1();
        }

        private void SimpleProjection_Click(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();
            g.Clear(BackColor1);
            MenuID = 43;
            Projection1();
        }

        private void SceneProjection_Click(object sender, EventArgs e)
        {
            //Graphics g = CreateGraphics();
            //g.Clear(BackColor1);
            MenuID = 44;
            Projection2();
        }
        private void Projection2()
        { //模型数据存入数组
            modegroup[0] = new Point3D(100, 100, 0);
            modegroup[1] = new Point3D(200, 100, 0);
            modegroup[2] = new Point3D(200, 200, 0);
            modegroup[3] = new Point3D(100, 200, 0);
            modegroup[4] = new Point3D(100, 100, 200);
            modegroup[5] = new Point3D(200, 100, 200);
            modegroup[6] = new Point3D(200, 200, 200);
            modegroup[7] = new Point3D(100, 200, 200);
            modegroup[8] = new Point3D(300, 100, 0);
            modegroup[9] = new Point3D(400, 100, 0);
            modegroup[10] = new Point3D(400, 200, 0);
            modegroup[11] = new Point3D(300, 200, 0);
            modegroup[12] = new Point3D(300, 100, 200);
            modegroup[13] = new Point3D(400, 100, 200);
            modegroup[14] = new Point3D(400, 200, 200);
            modegroup[15] = new Point3D(300, 200, 200);
            modegroup[16] = new Point3D(100, 300, 0);
            modegroup[17] = new Point3D(200, 300, 0);
            modegroup[18] = new Point3D(200, 400, 0);
            modegroup[19] = new Point3D(100, 400, 0);
            modegroup[20] = new Point3D(100, 300, 200);
            modegroup[21] = new Point3D(200, 300, 200);
            modegroup[22] = new Point3D(200, 400, 200);
            modegroup[23] = new Point3D(100, 400, 200);
            modegroup[24] = new Point3D(300, 300, 0);
            modegroup[25] = new Point3D(400, 300, 0);
            modegroup[26] = new Point3D(400, 400, 0);
            modegroup[27] = new Point3D(300, 400, 0);
            modegroup[28] = new Point3D(300, 300, 200);
            modegroup[29] = new Point3D(400, 300, 200);
            modegroup[30] = new Point3D(400, 400, 200);
            modegroup[31] = new Point3D(300, 400, 200);

            Point3D vp1 = new Point3D(800, 250, 0);
            Point3D vp2 = new Point3D(400, 250, 0);
            Point3D vp3 = new Point3D(-800, 0, 20);
            Point3D vp = new Point3D(800, 250, 0);
            vp1 = InputProjectionDirection2(1);
            vp2 = InputProjectionDirection2(2);
            vp3 = InputProjectionDirection2(3);
            double steps = 100;
            for (double j = 0; j < steps; j++)
            {
                vp.X = (int)((1.0 - j / steps) * vp1.X + j / steps * vp2.X);
                vp.Y = (int)((1.0 - j / steps) * vp1.Y + j / steps * vp2.X);
                vp.Z = (int)((1.0 - j / steps) * vp1.Z + j / steps * vp2.X);
                for (int i = 0; i < 32; i++)
                {
                    group[i] = ScenceP(vp, vp3, modegroup[i]);
                }
                Graphics g = CreateGraphics();
                g.Clear(Color.LightGray);
                g.DrawLine(Pens.Red, group[0], group[1]);
                g.DrawLine(Pens.Red, group[1], group[2]);
                g.DrawLine(Pens.Red, group[2], group[3]);
                g.DrawLine(Pens.Red, group[3], group[0]);
                g.DrawLine(Pens.Red, group[4], group[5]);
                g.DrawLine(Pens.Red, group[5], group[6]);
                g.DrawLine(Pens.Red, group[6], group[7]);
                g.DrawLine(Pens.Red, group[7], group[4]);
                g.DrawLine(Pens.Red, group[0], group[4]);
                g.DrawLine(Pens.Red, group[1], group[5]);
                g.DrawLine(Pens.Red, group[2], group[6]);
                g.DrawLine(Pens.Red, group[3], group[7]);
                g.DrawLine(Pens.Green, group[8], group[9]);
                g.DrawLine(Pens.Green, group[9], group[10]);
                g.DrawLine(Pens.Green, group[10], group[11]);
                g.DrawLine(Pens.Green, group[11], group[8]);
                g.DrawLine(Pens.Green, group[12], group[13]);
                g.DrawLine(Pens.Green, group[13], group[14]);
                g.DrawLine(Pens.Green, group[14], group[15]);
                g.DrawLine(Pens.Green, group[15], group[12]);
                g.DrawLine(Pens.Green, group[8], group[12]);
                g.DrawLine(Pens.Green, group[9], group[13]);
                g.DrawLine(Pens.Green, group[10], group[14]);
                g.DrawLine(Pens.Green, group[11], group[15]);
                g.DrawLine(Pens.Blue, group[16], group[17]);
                g.DrawLine(Pens.Blue, group[17], group[18]);
                g.DrawLine(Pens.Blue, group[18], group[19]);
                g.DrawLine(Pens.Blue, group[19], group[16]);
                g.DrawLine(Pens.Blue, group[20], group[20]);
                g.DrawLine(Pens.Blue, group[21], group[22]);
                g.DrawLine(Pens.Blue, group[22], group[23]);
                g.DrawLine(Pens.Blue, group[23], group[20]);
                g.DrawLine(Pens.Blue, group[16], group[20]);
                g.DrawLine(Pens.Blue, group[17], group[21]);
                g.DrawLine(Pens.Blue, group[18], group[22]);
                g.DrawLine(Pens.Blue, group[19], group[23]);
                g.DrawLine(Pens.Black, group[24], group[25]);
                g.DrawLine(Pens.Black, group[25], group[26]);
                g.DrawLine(Pens.Black, group[26], group[27]);
                g.DrawLine(Pens.Black, group[27], group[24]);
                g.DrawLine(Pens.Black, group[28], group[29]);
                g.DrawLine(Pens.Black, group[29], group[30]);
                g.DrawLine(Pens.Black, group[30], group[31]);
                g.DrawLine(Pens.Black, group[31], group[28]);
                g.DrawLine(Pens.Black, group[24], group[28]);
                g.DrawLine(Pens.Black, group[25], group[29]);
                g.DrawLine(Pens.Black, group[26], group[30]);
                g.DrawLine(Pens.Black, group[27], group[31]);
                System.Threading.Thread.Sleep(30);
            }
        }
        private Point3D InputProjectionDirection2(int mode)
        {
            Form MyInputForm = new Form();
            Label label1 = new Label();
            Label label2 = new Label();
            Label label3 = new Label();
            label1.Text = "X";
            label2.Text = "Y";
            label3.Text = "Z";
            label1.Location = new Point(10, 10);
            label2.Location = new Point(10, 40);
            label3.Location = new Point(10, 70);

            NumericUpDown numeric1 = new NumericUpDown();
            NumericUpDown numeric2 = new NumericUpDown();
            NumericUpDown numeric3 = new NumericUpDown();
            Point3D vp = new Point3D(150, 150, -500);
            if (mode == 1)
            {
                numeric1.Minimum = -600; numeric1.Maximum = 800;
                numeric1.Increment = 10; numeric1.Value = 15;
                numeric2.Minimum = -210; numeric2.Maximum = 290;
                numeric2.Increment = 5; numeric2.Value = 25;
                numeric3.Minimum = 0; numeric3.Maximum = 500;
                numeric3.Increment = 10; numeric3.Value = 35;
            }
            if (mode == 2)
            {
                numeric1.Minimum = 400; numeric1.Maximum = 500;
                numeric1.Increment = 10; numeric1.Value = 400;
                numeric2.Minimum = 210; numeric2.Maximum = 290;
                numeric2.Increment = 5; numeric2.Value = 250;
                numeric3.Minimum = 0; numeric3.Maximum = 500;
                numeric3.Increment = 10; numeric3.Value = 0;
            }
            if (mode == 3)
            {
                numeric1.Minimum = -900; numeric1.Maximum = -800;
                numeric1.Increment = 5; numeric1.Value = -800;
                numeric2.Minimum = -50; numeric2.Maximum = 50;
                numeric2.Increment = 10; numeric2.Value = 0;
                numeric3.Minimum = -100; numeric3.Maximum = 100;
                numeric3.Increment = 10; numeric3.Value = 20;
            }
            numeric1.Location = new Point(30, 10);
            numeric2.Location = new Point(30, 40);
            numeric3.Location = new Point(30, 70);
            Button button1 = new Button();
            Button button2 = new Button();
            button1.Text = "确定";
            button1.DialogResult = DialogResult.OK;
            button1.Location = new Point(10, 100);
            button2.Text = "取消";
            button2.DialogResult = DialogResult.Cancel;
            button2.Location = new Point(button1.Width + 20, 100);
            if (mode == 1)
                MyInputForm.Text = "请输入漫游起点";
            if (mode == 2)
                MyInputForm.Text = "请输入漫游终点";
            if (mode == 3)
                MyInputForm.Text = "请输入观测方向";
            MyInputForm.HelpButton = true;
            MyInputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            MyInputForm.MaximizeBox = false;
            MyInputForm.MinimizeBox = false;
            MyInputForm.AcceptButton = button1;
            MyInputForm.CancelButton = button2;
            MyInputForm.StartPosition = FormStartPosition.CenterScreen;
            MyInputForm.Controls.Add(numeric1);
            MyInputForm.Controls.Add(numeric2);
            MyInputForm.Controls.Add(numeric3);
            MyInputForm.Controls.Add(label1);
            MyInputForm.Controls.Add(label2);
            MyInputForm.Controls.Add(label3);
            MyInputForm.Controls.Add(button1);
            MyInputForm.Controls.Add(button2);
            vp = new Point3D((float)numeric1.Value, (float)numeric2.Value, (float)numeric3.Value);
            if (MyInputForm.ShowDialog() == DialogResult.Cancel)
            {
                MyInputForm.Close();
                return vp;
            }
            vp = new Point3D((float)numeric1.Value, (float)numeric2.Value, (float)numeric3.Value);
            MyInputForm.Close();
            return vp;
        }
        private Point ScenceP(Point3D ViewP, Point3D DirP, Point3D ModeP)
        {
            double a11, a12, a13, a21, a22, a23, a31, a32, a33;
            a11 = Math.Sqrt(DirP.X * DirP.X + DirP.Y * DirP.Y + DirP.Z * DirP.Z);
            a31 = DirP.X / a11; a32 = DirP.Y / a11; a33 = DirP.Z / a11;
            a12 = Math.Sqrt(a31 * a31 / (a31 * a31 + a32 * a32));
            a11 = -Math.Sqrt(a32 * a32 / (a31 * a31 + a32 * a32));
            a13 = 0;
            a23 = a11 * a32 - a12 * a31;
	        if (a23 < 0)
            {
                a23 = -a23; a12 = -a12; a11 = -a11;
            }
            a21 = a12 * a33;
            a22 = -a11 * a33;
            Point p;
            double z, x, y;
            z = a31 * (ModeP.X - ViewP.X) + a32 * (ModeP.Y - ViewP.Y) + a33 * (ModeP.Z - ViewP.Z);
            x = (100.0 * (a11 * (ModeP.X - ViewP.X) + a12 * (ModeP.Y - ViewP.Y)) / z) + 300;
            y = -(100.0 * (a21 * (ModeP.X - ViewP.X) + a22 * (ModeP.Y - ViewP.Y) + a23 * (ModeP.Z - ViewP.Z)) / z) + 500;
            p = new Point((int)x, (int)y);
            return p;
        }

        private void BresenhamLine_Click(object sender, EventArgs e)
        {
            MenuID = 3; PressNum = 0;
            Graphics g = CreateGraphics();
            g.Clear(BackColor1);
        }
        private void BresenhamLine1(int X0, int Y0, int X1, int Y1)
        {
            int x, y, dx, dy, e;
            dx = System.Math.Abs(X1 - X0);
            dy = System.Math.Abs(Y1 - Y0);
            if (System.Math.Abs(Y1 - Y0) > System.Math.Abs(X1 - X0))
            {
                dx = System.Math.Abs(Y1 - Y0);
                dy = System.Math.Abs(X1 - X0);
            }
            e = -dx;
            x = 0;
            y = 0;
            Graphics g = CreateGraphics();
            for (int i = 0; i < dx; i ++)
            {
                if (X1 >= X0 && Y1 >= Y0 && (X1 - X0) >= (Y1 - Y0))//1
                    g.DrawRectangle(Pens.Red, X0+x, Y0+y, 1, 1);
                else if (X1 > X0 && Y1 > Y0 && (X1 - X0) < (Y1 - Y0))//2
                    g.DrawRectangle(Pens.Red, X0+y, Y0+x, 1, 1);
                else if (X1 < X0 && Y1 > Y0 && (X0 - X1) < (Y1 - Y0))//3
                    g.DrawRectangle(Pens.Red, X0-y, Y0+x, 1, 1);
                else if (X1 <= X0 && Y1 >= Y0 && (X0 - X1) >= (Y1 - Y0))//4
                    g.DrawRectangle(Pens.Red, X0-x, Y0+y, 1, 1);
                else if (X1 < X0 && Y1 < Y0 && (X0 - X1) > (Y0 - Y1))//5
                    g.DrawRectangle(Pens.Red, X0-x, Y0-y, 1, 1);
                else if (X1 <= X0 && Y1 <= Y0 && (X0 - X1) <= (Y0 - Y1))//6
                    g.DrawRectangle(Pens.Red, X0-y, Y0-x, 1, 1);
                else if (X1 >= X0 && Y1 <= Y0 && (X1 - X0) <= (Y0 - Y1))//7
                    g.DrawRectangle(Pens.Red, X0+y, Y0-x, 1, 1);
                else if (X1 > X0 && Y1 < Y0 && (X1 - X0) > (Y0 - Y1))//8
                    g.DrawRectangle(Pens.Red, X0+x, Y0-y, 1, 1);
                x++;
                e = e + 2 * dy;
                if (e >= 0)
                { y++; e = e - 2 * dx; }
            }
        }

        private void MyCharacter_Click(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();
            g.Clear(BackColor1);
            MenuID = 41;
            Form3 myf = new Form3();
            if (myf.ShowDialog() == DialogResult.Cancel)
            {
                myf.Close();
                return;
            }
        }


        private void Bezier_4(int mode,Point p1,Point p2,Point p3,Point p4)
        {
            int i, n;
            Graphics g = CreateGraphics();
            Point p = new Point();
            Point oldp = new Point();
            double t1, t2, t3, t4, dt;
            Pen MyPen = new Pen(Color.Red, 1);
            n = 100;
            if(mode == 2)
            {
                MyPen = new Pen(Color.Red, 1);
            }
            if(mode == 1)
            {
                MyPen = new Pen(Color.Black, 1);
            }
            if(mode == 0)
            {
                MyPen = new Pen(Color.White, 1);
            }
            oldp = p1;
            dt = 1.0 / n;
            for (i = 1; i <= n; i++)
            {
                t1 = (1.0 - i * dt) * (1.0 - i * dt) * (1.0 - i * dt);
                t2 = i * dt * (1.0 - i * dt) * (1.0 - i * dt);
                t3 = i * dt * i * dt * (1.0 - i * dt);
                t4 = i * dt * i * dt * i * dt;
                p.X = (int)(t1 * p1.X + 3 * t2 * p2.X + 3 * t3 * p3.X + t4 * p4.X);
                p.Y = (int)(t1 * p1.Y + 3 * t2 * p2.Y + 3 * t3 * p3.Y + t4 * p4.Y);
                g.DrawLine(MyPen, oldp, p);
                oldp = p;
            }
        }
        private void Bezier_41(int mode, Point p1, Point p2, Point p3, Point p4)
        {
            Graphics g = CreateGraphics();
            Point p = new Point();
            Point oldp = new Point();
            double t, dt;
            Point[] g1 = new Point[4];
            g1[0] = p1; g1[1] = p2; g1[2] = p3; g1[3] = p4;
            Pen MyPen = new Pen(Color.Red, 1);
            int n = 100;
            if (mode == 2)
            {
                MyPen = new Pen(Color.Red, 1);
            }
            if (mode == 1)
            {
                MyPen = new Pen(Color.Black, 1);
            }
            if (mode == 0)
            {
                MyPen = new Pen(Color.White, 1);
            }
            oldp = p1;
            dt = 1.0 / n;
            for (int i = 1; i <= n; i++)
            {
                t = i * dt;
                for (int k = 3; k > 0; k--)
                {
                    for (int j = 0; j < k; j++)
                    {
                        g1[j].X = (int)((1.0 - t) * g1[j].X + t * g1[j + 1].X);
                        g1[j].Y = (int)((1.0 - t) * g1[j].Y + t * g1[j + 1].Y);
                    }
                }
                p = g1[0];
                g.DrawLine(MyPen, oldp, p);
                oldp = p;
            }
        }
    }
    struct EdgeInfo
    {
        int ymax, ymin;//Y的上下端点
        float k, xmin;//斜率倒数和X的下端点
        public int YMax { get { return ymax; } set { ymax = value; } }
        public int YMin { get { return ymin; } set { ymin = value; } }
        public float XMin { get { return xmin; } set { xmin = value; } }
        public float K { get { return k; } set { k = value; } }
        public EdgeInfo(int x1, int y1, int x2, int y2)
        {
            ymax = y2; ymin = y1; xmin = (float)x1; k = (float)(x1 - x2) / (float)(y1 - y2);
        }
    }
    //扫描线填充算法预定义结构

    struct ShapeHeader
    {
        //高字节序
        public int FileCode;    //value: 9994
        public int Unused1;
        public int Unused2;
        public int Unused3;
        public int Unused4;
        public int Unused5;
        public int FileLength;
        //低字节序
        public int Version;     //value: 1000
        public int ShapeType;
        /// 0	Null Shape
        /// 1	Point
        /// 3	PolyLine
        /// 5	Polygon
        /// 8	MultiPoint
        /// 11	PointZ
        /// 13	PolyLineZ
        /// 15	PolygonZ
        /// 18	MultiPointZ
        /// 21	PointM
        /// 23	PolyLineM
        /// 25	PolygonM
        /// 28	MultiPointM
        /// 31	MultiPatch
        public double XMin;
        public double YMin;
        public double XMax;
        public double YMax;
        public double ZMin;
        public double ZMax;
        public double MMin;
        public double MMax;
    }
    //Shapefile文件标头结构

    class VertexPolygon
    {
        public int ID;//顶点ID
        public VertexPolygon PreVertex;//前顶点
        public VertexPolygon NextVertex;//后顶点
        public double X;//点坐标X
        public double Y;//点坐标Y
        public int PolygonID;//所在面ID
        public VertexPolygon()
        {
            PreVertex = null;
            NextVertex = null;
            ID = 0;
        }
        //默认构造函数，内含前一顶点ID以及后一顶点ID，以及构造时的初始ID
    }
    //定义类Vertex(顶点)

    /*	class BasicLine
		{
		//	public int ID_line;					//线段ID
			public int PolygonID;				//线段所属面ID
			public PointF point1;				//顶点1
			public PointF point2;				//顶点2
			public BasicLine()
			{

			}
		}
		//定义线段用于交点检查
	*/
    class PolyGon
    {
        public int id;                      //面ID
        public int RecordLength;            //SHP记录长度
        public int ShapeType;               //SHP图形类型
        public double xmin;                 //SHP内X最小值
        public double ymin;                 //SHP内Y最小值
        public double xmax;                 //SHP内X最大值
        public double ymax;                 //SHP内Y最大值
        public int NumOfParts;              //SHP内面数量
        public int NumOfPoints;             //SHP内点数量
        public int[] Parts;                 //SHP内每个部分索引数组
        public List<VertexPolygon> points;          //由顶点构成的集合
                                                    //	public List<BasicLine> lines;		//由多边形基础线段构成的集合
        public PolyGon()
        {
            points = new List<VertexPolygon>();
        }
        //默认构造函数，将上面的points实例化
    }
    //定义类PolyGon

    class PolyGons
    {
        private int FirstX;
        public List<PolyGon> polygons;//由多个面构成的面组
        public List<PointF> inse_points = new List<PointF>();
        public ShapeHeader MH;
        public string inname = "";
        //用来存放IsPointInPolygon函数的数据
        int count = 0;
        int loop_time = 0;
        int check_time = 0;
        public int Count
        {
            get { return count; }
        }
        //属性Count，操作元素count，只读
        public PolyGons()
        {
            polygons = new List<PolyGon>();
            //InterSectPoints = new List<IntersectNode>();
        }
        //默认构造函数，生成由面类以及交点类构成的泛型集合
        public static UInt32 ReverseBytes(UInt32 value)
        {
            return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 | (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
        }
        ///高低字节转序
        ///该函数用于在读取SHP文件时对高字节序部分进行转序
        ///参数为一个无符号的32位整形值，假设字节内容为ABCD
        ///返回值即为DCBA
        public void ReadShapeFile(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            //高字节序
            MH.FileCode = (Int32)ReverseBytes(br.ReadUInt32());
            MH.Unused1 = (Int32)ReverseBytes(br.ReadUInt32());
            MH.Unused2 = (Int32)ReverseBytes(br.ReadUInt32());
            MH.Unused3 = (Int32)ReverseBytes(br.ReadUInt32());
            MH.Unused4 = (Int32)ReverseBytes(br.ReadUInt32());
            MH.Unused5 = (Int32)ReverseBytes(br.ReadUInt32());
            MH.FileLength = (Int32)ReverseBytes(br.ReadUInt32());
            //低字节序
            MH.Version = br.ReadInt32();
            MH.ShapeType = br.ReadInt32();
            MH.XMin = br.ReadDouble();
            MH.YMin = br.ReadDouble();
            MH.XMax = br.ReadDouble();
            MH.YMax = br.ReadDouble();
            MH.ZMin = br.ReadDouble();
            MH.ZMax = br.ReadDouble();
            MH.MMin = br.ReadDouble();
            MH.MMax = br.ReadDouble();
            try
            {
                while (true)
                {
                    PolyGon polyG = new PolyGon();
                    //====
                    polyG.id = (Int32)ReverseBytes(br.ReadUInt32());
                    polyG.RecordLength = (Int32)ReverseBytes(br.ReadUInt32());
                    polyG.ShapeType = br.ReadInt32();
                    polyG.xmin = br.ReadDouble();
                    polyG.ymin = br.ReadDouble();
                    polyG.xmax = br.ReadDouble();
                    polyG.ymax = br.ReadDouble();
                    polyG.NumOfParts = br.ReadInt32();
                    polyG.NumOfPoints = br.ReadInt32();
                    //====
                    polyG.Parts = new int[polyG.NumOfParts];//长度为NumOfParts的数组，即线的数量
                    for (int i = 0; i < polyG.NumOfParts; i++)
                    {
                        polyG.Parts[i] = br.ReadInt32();
                    }
                    //面首点数组索引位
                    VertexPolygon PreVer = null;
                    VertexPolygon VertexFirst = new VertexPolygon();
                    //====
                    VertexFirst.ID = 1;
                    VertexFirst.PolygonID = polyG.id;
                    VertexFirst.PreVertex = null;
                    VertexFirst.NextVertex = null;
                    VertexFirst.X = br.ReadDouble();
                    VertexFirst.Y = br.ReadDouble();
                    PreVer = VertexFirst;
                    //====
                    polyG.points.Add(VertexFirst);
                    for (int i = 1; i < polyG.NumOfPoints - 1; i++)
                    {
                        VertexPolygon VertexMid = new VertexPolygon();
                        //====
                        VertexMid.PreVertex = PreVer;
                        VertexMid.PreVertex.NextVertex = VertexMid;
                        VertexMid.ID = PreVer.ID + 1;
                        VertexMid.PolygonID = polyG.id;
                        VertexMid.X = br.ReadDouble();
                        VertexMid.Y = br.ReadDouble();
                        VertexMid.NextVertex = null;
                        PreVer = VertexMid;
                        //====
                        polyG.points.Add(VertexMid);
                    }
                    VertexPolygon VetLast = new VertexPolygon();
                    //====
                    VetLast.PreVertex = PreVer;
                    PreVer.NextVertex = VetLast;
                    VetLast.ID = VetLast.PreVertex.ID + 1;
                    VetLast.PolygonID = polyG.id;
                    VetLast.NextVertex = null;
                    VetLast.X = br.ReadDouble();
                    VetLast.Y = br.ReadDouble();
                    //====
                    polyG.points.Add(VetLast);
                    polygons.Add(polyG);
                    count++;
                }
            }
            catch (EndOfStreamException e)
            {
                //Console.WriteLine(e.ToString());
                //Console.WriteLine("文件读取完毕");				
            }
        }
        //读取SHP文件内容
        public void DrawPolyGons(PictureBox pb)
        {
            Graphics g = pb.CreateGraphics();
            g.Clear(Color.Gray);
            foreach (PolyGon pg in polygons)
            {
                int iSeed = 10;
                Random ro = new Random(iSeed);
                long tick = DateTime.Now.Ticks;
                Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
                int R = ran.Next(255);
                int G = ran.Next(255);
                int B = ran.Next(255);
                B = (R + G > 400) ? R + G - 400 : B;//0 : 380 - R - G;
                B = (B > 255) ? 255 : B;
                //生成随机颜色

                Pen penDrawLine = new Pen(Color.Red, 3);
                Pen penDrawPolygon = new Pen(Color.FromArgb(R, G, B), 1);
                PointF[] pointsF = new PointF[pg.points.Count];
                for (int i = 0; i < pointsF.Length; i++)
                {
                    pointsF[i].X = (float)((pg.points[i].X - MH.XMin) / (MH.XMax - MH.XMin) * pb.Width);
                    pointsF[i].Y = (float)(pb.Height - (pg.points[i].Y - MH.YMin) / (MH.YMax - MH.YMin) * pb.Height);
                }
                g.DrawLines(penDrawLine, pointsF);
                //绘制多边形面

                EdgeInfo[] edgelist = new EdgeInfo[100];
                int j = 0, yu = 0, yd = 1024;
                for (int i = 0; i < pointsF.Length - 1; i++)
                {
                    if (pointsF[i].Y > yu) yu = (int)pointsF[i].Y;
                    if (pointsF[i].Y < yd) yd = (int)pointsF[i].Y;
                    if (pointsF[i].Y != pointsF[i + 1].Y)
                    {
                        if (pointsF[i].Y > pointsF[i + 1].Y)
                        {
                            edgelist[j++] = new EdgeInfo((int)pointsF[i + 1].X, (int)pointsF[i + 1].Y, (int)pointsF[i].X, (int)pointsF[i].Y);
                        }
                        else
                        {
                            edgelist[j++] = new EdgeInfo((int)pointsF[i].X, (int)pointsF[i].Y, (int)pointsF[i + 1].X, (int)pointsF[i + 1].Y);
                        }
                    }
                }
                for (int y = yd; y < yu; y++)
                {
                    //AEL
                    var sorted = from item in edgelist
                                 where y < item.YMax && y >= item.YMin
                                 orderby item.XMin, item.K
                                 select item;
                    int flag = 0;
                    foreach (var item in sorted)
                    {
                        //FirstX = 0;
                        if (flag == 0)
                        {
                            FirstX = (int)(item.XMin + 0.5);
                            flag++;
                        }
                        else
                        {
                            g.DrawLine(penDrawPolygon, (int)(item.XMin + 0.5), y, FirstX - 1, y);
                            g.DrawLine(penDrawPolygon, (int)(item.XMin + 0.5), y, FirstX - 1, y);
                            flag = 0;
                        }
                    }
                    for (int i = 0; i < j; i++)
                    {
                        if (y < edgelist[i].YMax - 1 && y > edgelist[i].YMin)
                        {
                            edgelist[i].XMin += edgelist[i].K;
                        }
                    }
                }

                //扫描线填充
            }
        }
        //绘制多边形(边加粗，内部填充)
        public void DrawInsePoints(PictureBox pb)
        {
            Graphics g = pb.CreateGraphics();
            Pen draw_point = new Pen(Color.Red, 1);
            Brush brush = new SolidBrush(Color.LightGreen);
            PointF[] pointsF = new PointF[inse_points.Count];
            for (int i = 0; i < pointsF.Length; i++)
            {
                pointsF[i].X = (float)((inse_points[i].X - MH.XMin) / (MH.XMax - MH.XMin) * pb.Width);
                pointsF[i].Y = (float)(pb.Height - (inse_points[i].Y - MH.YMin) / (MH.YMax - MH.YMin) * pb.Height);
                g.DrawEllipse(draw_point, pointsF[i].X - 5, pointsF[i].Y - 5, 9, 9);
                g.FillEllipse(brush, pointsF[i].X - 5, pointsF[i].Y - 5, 9, 9);
                //Console.WriteLine(pointsF[i].X + " " + pointsF[i].Y);
            }
            //Console.WriteLine("共绘制了{0}个交点", inse_points.Count);
        }
        //绘制交点
        public void CheckLineInPolygonsInse()
        {
            bool Inse;
            for (int front = 0; front <= polygons.Count - 2; front++)
            {
                for (int rear = front + 1; rear <= polygons.Count - 1; rear++)
                {
                    PointF left_up = new PointF((float)polygons[front].xmin, (float)polygons[front].ymax);
                    PointF right_up = new PointF((float)polygons[front].xmax, (float)polygons[front].ymax);
                    PointF left_down = new PointF((float)polygons[front].xmin, (float)polygons[front].ymin);
                    PointF right_down = new PointF((float)polygons[front].xmax, (float)polygons[front].ymin);
                    //多边形front的对角线
                    if (polygons[front].ymax < polygons[rear].ymin
                        || polygons[front].xmax < polygons[rear].xmin
                        || polygons[rear].ymax < polygons[front].ymin
                        || polygons[rear].xmax < polygons[front].xmin)
                    {
                        //Console.WriteLine("多边形{0}与多边形{1}不可能相交", front, rear);
                        loop_time++;
                    }
                    //多边形碰撞箱检查，不相交
                    else
                    {
                        //Console.WriteLine("多边形{0}与多边形{1}可能相交", front, rear);
                        string blacklist_j = "";
                        for (int i = 0; i < polygons[front].points.Count - 1; i++)
                        {
                            for (int j = 0; j < polygons[rear].points.Count - 1; j++)
                            {
                                loop_time++;
                                if (blacklist_j.Contains(Convert.ToString(j)))
                                {
                                    //Console.WriteLine("边{0}跳过", j);
                                    continue;
                                }
                                //Console.WriteLine("开始检查多边形{0}的边{1}与多边形{2}的边{3}", front, i, rear, j);
                                PointF point1a = new PointF((float)polygons[front].points[i].X, (float)polygons[front].points[i].Y);
                                PointF point1b = new PointF((float)polygons[front].points[i + 1].X, (float)polygons[front].points[i + 1].Y);
                                PointF point2a = new PointF((float)polygons[rear].points[j].X, (float)polygons[rear].points[j].Y);
                                PointF point2b = new PointF((float)polygons[rear].points[j + 1].X, (float)polygons[rear].points[j + 1].Y);
                                if ((point2a.X > polygons[front].xmin && point2a.X < polygons[front].xmax &&
                                    point2a.Y > polygons[front].ymin && point2a.Y < polygons[front].ymax) ||
                                    //多边形rear的检查边点A在碰撞箱内
                                    (point2b.X > polygons[front].xmin && point2b.X < polygons[front].xmax &&
                                    point2b.Y > polygons[front].ymin && point2b.Y < polygons[front].ymax) ||
                                    //多边形rear的检查边点B在碰撞箱内
                                    CheckIfInse(point2a, point2b, left_up, right_down) ||
                                    //多边形rear的检查边与碰撞箱的＼对角线有交点
                                    CheckIfInse(point2a, point2b, right_up, left_down)
                                    //多边形rear的检查边与碰撞箱的／对角线有交点
                                    )
                                {
                                    Inse = CheckIfInse(point1a, point1b, point2a, point2b);
                                    if (Inse)
                                    {
                                        //Console.WriteLine("多边形{0}的边{1}与多边形{2}的边{3}相交", front, i, rear, j);
                                        CalInsePoint(point1a, point1b, point2a, point2b);
                                    }
                                    else
                                    {
                                        //Console.WriteLine("多边形{0}的边{1}与多边形{2}的边{3}不相交", front, i, rear, j);
                                    }
                                }
                                else
                                {
                                    //Console.WriteLine("多边形{0}的边{1}与多边形{2}的边{3}不相交", front, i, rear, j);
                                    //Console.WriteLine("边{0}已加入多边形{1}的黑名单", j, front);
                                    blacklist_j += Convert.ToString(j);
                                }
                            }
                        }
                        //检查所有边
                    }
                    //碰撞箱检查得交，检查线交点
                }
            }
            //多边形之间检查
            //Console.WriteLine("循环次数：{0}，检查次数：{1}", loop_time, check_time);
            ///每次检查两个多边形
            ///首先检查他们的碰撞箱，假如碰撞箱不交则跳过(节省大部分时间)
            ///然后具体检查线段相交
            ///当多边形rear的一条线段不在front内的时候跳过行列式运算，并判定不相交，该线段列入front的黑名单
        }
        ///检查多边形内线段相交
        ///1.每次检查两个多边形，分别标记为front和rear。
        ///第一层循环front从第一个多边形遍历到倒数第二个多边形，
        ///第二层循环rear从front+1开始遍历到最后一个多边形。
        ///2.第二层循环体内，首先对两个多边形的外接矩形框（下称碰撞箱）进行检查，
        ///假如二者外接矩形框相交（下称碰撞箱发生碰撞），则初步判断二者可能存在相交点，
        ///而碰撞箱未发生碰撞的情况直接跳过检查，并判定不相交。
        ///3.在两多边形碰撞箱碰撞的前提下，进行逐边检查。第三层循环遍历多边形front的每一条边，
        ///检查边记为i，第四层循环遍历多边形rear的每一条边，检查边记为j。
        ///a)首先检查rear的边j是否与多边形front的碰撞箱发生碰撞，检查方法：分别判断边j的两点坐
        ///标是否在碰撞箱内，假如有一点在碰撞箱内则该边j与多边形front的碰撞箱发生了碰撞。
        ///b)而当两点均不在碰撞箱内时再判断该边j是否与碰撞箱任意一条对角线有交点，如果有交点则
        ///边j与碰撞箱有碰撞，具体检查边i与边j是否相交。而当边j两点均不在碰撞箱内且边j与碰撞箱
        ///对角线无交点时，判定边j与多边形front任意边均无交点，后续检查跳过。
        private bool CheckIfInse(PointF a1, PointF a2, PointF b1, PointF b2)
        {
            check_time++;
            double delta = Determinant(a2.X - a1.X, b2.X - b1.X, b2.Y - b1.Y, a2.Y - a1.Y);
            if (Math.Abs(delta) <= (1e-6))
            {
                return false;
            }
            ///1.将两线段转换为向量(a2-a1),(b2-b1)，两向量叉乘结果记为delta。
            ///若delta为0，说明线段平行或者共直线。具体判别共直线的情况，两线段
            ///分别任取一点构成一条新的直线，若新直线的斜率等于旧直线斜率则共线，
            ///特别的当两条直线都垂直X坐标轴斜率不存在时则判别两直线的X值是否相等
            ///即可。delta大于零，向量2在向量1的逆时针方向，小于零在顺时针方向。
            double lamda = Determinant(b2.X - b1.X, a1.X - b1.X, a1.Y - b1.Y, b2.Y - b1.Y) / delta;
            if (lamda > 1 || lamda < 0)
            {
                return false;
            }
            ///2.再求向量(b2 - b1)和(a1 - b1)的叉乘以及向量(a2 - a1)和(a1 - b1)
            ///的叉乘，所得结果分别除delta得(a1 - b1)/(a2 - a1)和(a1 - b1)/(a2 - b1)，
            ///分别记为lamda和myu。若lamda小于0则说明向量1在向量2的某时针方向，而向量(a1 - b1)也在向量2的某时针方向，
            ///即向量1与向量(a1 - b1)在向量2同侧，此时两线段不可能有交点，判false。
            ///当lamda大于0时说明向量1与向量(a1 - b1)在向量2异侧，线段可能有交点。
            ///当lamda大于1时，(a1 - b1)长度大于(a2 - a1)长度乘向量2与向量1夹角正弦值，
            ///这时线段也无交点。当lamda小于等于1大于0时才可能有交点。
            double myu = Determinant(a2.X - a1.X, a1.X - b1.X, a1.Y - b1.Y, a2.Y - a1.Y) / delta;
            if (myu > 1 || myu < 0)
            {
                return false;
            }
            ///3.当myu小于零时说明向量(a1-b1)与(b2-b1)在向量1的不同侧，此时不可能有交点。
            ///当大于0时，如果myu大于1，情况与上面类似，无交点。myu大于0小于等于1时可能有交点。
            return true;
            ///综上所述，两线段有交点（不考虑共直线情况）需同时满足以下条件，delta不等于0，lamda和myu都在区间(0,1]内
        }
        ///跨立实验：通过两点坐标判断线段是否相交
        ///参数a1, a2, b1, b2分别为线段A, B的两端点
        ///返回值false不相交，true相交
        private static double Determinant(double v1, double v2, double v3, double v4)
        {
            return (v1 * v3 - v2 * v4);
        }
        ///计算2x2矩阵行列式
        ///2x2矩阵格式：
        ///v1	v2
        ///v4	v3
        private void CalInsePoint(PointF a1, PointF a2, PointF b1, PointF b2)
        {
            PointF inse_point = new PointF();
            if (Math.Abs(a1.X - a2.X) <= 1e-6)
            {
                inse_point.X = a1.X;
                inse_point.Y = (a1.X - b1.X) / (b1.X - b2.X) * (b1.Y - b2.Y) + b1.Y;
            }
            //线段A垂直X坐标轴(加法 4，乘法 2)
            else if (Math.Abs(b1.X - b2.X) <= 1e-6)
            {
                inse_point.X = b1.X;
                inse_point.Y = (b1.X - a1.X) / (a1.X - a2.X) * (a1.Y - a2.Y) + a1.Y;
            }
            //线段B垂直X坐标轴(加法 4，乘法 2)
            else if (Math.Abs(a1.Y - a2.Y) <= 1e-6)
            {
                inse_point.Y = a1.Y;
                inse_point.X = (a1.Y - b1.Y) / (b2.Y - b1.Y) * (b2.X - b1.X) + b1.X;
            }
            //线段A平行X坐标轴(加法 4，乘法 2)
            else if (Math.Abs(b1.Y - b2.Y) <= 1e-6)
            {
                inse_point.Y = b1.Y;
                inse_point.X = (b1.Y - a1.Y) / (a2.Y - a1.Y) * (a2.X - a1.X) + a1.X;
            }
            //线段B平行X坐标轴(加法 4，乘法 2)
            else
            {
                inse_point.X = ((a1.Y - b1.Y) + (b1.Y - b2.Y) / (b1.X - b2.X) * b1.X - (a1.Y - a2.Y) / (a1.X - a2.X) * a1.X) / ((b1.Y - b2.Y) / (b1.X - b2.X) - (a1.Y - a2.Y) / (a1.X - a2.X));
                inse_point.Y = (inse_point.X - a1.X) / (a1.X - a2.X) * (a1.Y - a2.Y) + a1.Y;
            }
            //其他一般情况(加法 16，乘法 8)
            if (Math.Abs(inse_point.X - a1.X) <= 1e-6 && Math.Abs(inse_point.Y - a1.Y) <= 1e-6 ||
                Math.Abs(inse_point.X - a2.X) <= 1e-6 && Math.Abs(inse_point.Y - a2.Y) <= 1e-6 ||
                Math.Abs(inse_point.X - b1.X) <= 1e-6 && Math.Abs(inse_point.Y - b1.Y) <= 1e-6 ||
                Math.Abs(inse_point.X - b2.X) <= 1e-6 && Math.Abs(inse_point.Y - b2.Y) <= 1e-6
            )
            {
                inse_points.Add(inse_point);
                //Console.WriteLine("直线端点，不取");
            }
            else
            {
                inse_points.Add(inse_point);
                //非端点时加一点
            }
            //Console.WriteLine(inse_point.ToString());
        }
        ///计算交点坐标
        ///通过线段两点式方程联立确定交点坐标
        ///对平行或垂直X坐标轴的线段进行特殊检查
        public void IsPointInPolygon(PointF pt)
        {
            inname = "";
            foreach (PolyGon poly in polygons)
            {
                bool c = false;//初始置否
                if (pt.X < poly.xmin || pt.X > poly.xmax || pt.Y < poly.ymin || pt.Y > poly.ymax)
                {
                    c = false;
                }
                //碰撞箱判别
                else
                {
                    int i, j, nvert;
                    nvert = poly.points.Count;
                    for (i = 0, j = nvert - 2; i < nvert - 1; j = i++)
                    {
                        if (((poly.points[i].Y > pt.Y) != (poly.points[j].Y > pt.Y)) &&
                            (pt.X < (poly.points[j].X - poly.points[i].X) * (pt.Y - poly.points[i].Y) / (poly.points[j].Y - poly.points[i].Y) + poly.points[i].X))
                            c = !c;
                    }
                }
                //扫描线交多边形偶数次则点在多边形内，否则点在多边形外
                if (c)
                {
                    inname += poly.id.ToString() + " ";
                    //Console.WriteLine("该点在多边形{0}内", poly.id);
                }
            }
        }
        ///判断点是否在多边形内
        ///参数PointF pt指定点，PolyGon poly指定一个多边形
    }
}
