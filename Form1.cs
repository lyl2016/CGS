using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//引用库

namespace _1801080101刘永麟
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }//窗体初始化
		/*-- 定义变量 --*/
        Color BackColor1 = Color.White;
        Color ForeColor1 = Color.Black;
        public int MenuID, PressNum, FirstX, FirstY, OldX, OldY;
		public int R, XL, XR, YU, YD;
		Point[] group = new Point[100];
		Point[] pointsgroup = new Point[4];
		public struct EdgeInfo
		{
			int ymax, ymin;//Y的上下端点
			float k, xmin;//斜率倒数和X的下端点
			public int YMax { get { return ymax; } set { ymax = value; } }
			public int YMin { get { return ymin; } set { ymin = value; } }
			public float XMin { get { return xmin; } set { xmin = value; } }
			public float K { get { return k; } set { k = value; } }
			public EdgeInfo(int x1,int y1,int x2,int y2)
			{
				ymax = y2;ymin = y1;xmin = (float)x1;k = (float)(x1 - x2) / (float)(y1 - y2);
			}
		}//扫描线填充算法预定义结构
		//设置预定义变量

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
			x = 0;y = r;d = 3 - 2 * r;
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
				if(d<0||d==0)
				{
					d = d + 4 * x + 6;
				}
				else
				{
					y -= 1;d = d + 4 * (x - y) + 10;
				}
			}
		}
		//Bresenham画圆绘图方法
		private void CohenCut1(int x1,int y1,int x2,int y2)
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
		private int encode(int x,int y)
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
		private void ScanLineFill1()
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
		}
		//ScanLineFill图形填充方法

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
            if (MenuID == 1||MenuID == 2)
            {
				g.Clear(BackColor1);
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
                }
                PressNum++;
                if(PressNum>=2)
                    PressNum = 0;
            }
			if (MenuID == 5)
			{
				g.Clear(BackColor1);
				if (PressNum == 0)
				{
					FirstX = e.X;FirstY = e.Y;
				}
				else
				{
					if(FirstX == e.X&&FirstY == e.Y)
					{
						return;
					}										
					BresenhamCircle1(FirstX, FirstY, e.X, e.Y);					
				}
				PressNum++;
				if (PressNum >= 2)
					PressNum = 0;
			}
			if (MenuID == 11)
			{
				if (PressNum == 0)
				{
					FirstX = e.X;FirstY = e.Y;
				}
				else
				{
					for(int i = 0; i < 4; i++)
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
					FirstX = e.X;FirstY = e.Y;
				}
				else
				{
					double a;
					if (e.X == FirstX && e.Y == FirstY) return;
					if (e.X == FirstX && e.Y > FirstY) a = 3.1415926 / 2.0;
					else if (e.X == FirstX && e.Y < FirstY) a = 3.1415926 / 2.0 * 3.0;
					else a = Math.Atan((double)(e.Y - FirstY) / (double)(e.X - FirstX));
					a = a / 3.1415926 * 180.0;
					int x0 = 150, y0 = 150;
					Matrix myMatrix = new Matrix();
					myMatrix.Translate(-x0, -y0);
					myMatrix.Rotate((float)a, MatrixOrder.Append);
					myMatrix.Translate(x0, y0, MatrixOrder.Append);
					//Graphics g = CreateGraphics();
					g.Transform = myMatrix;
					g.DrawPolygon(Pens.Blue, pointsgroup);
				}
				PressNum++;
				if (PressNum >= 2) PressNum = 0;
			}
			if (MenuID == 21)
			{
				//g.Clear(BackColor1);
				if (PressNum == 0)
				{
					FirstX = e.X;FirstY = e.Y;PressNum++;
				}
				else
				{
					CohenCut1(FirstX, FirstY, e.X, e.Y);
					PressNum = 0;
				}
			}
			if (MenuID == 31)
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
					ScanLineFill1();
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
			if (MenuID == 1 || MenuID == 2 && PressNum == 1)
			{
				if (!(e.X == OldX && e.Y == OldY))
				{
					g.DrawLine(BackPen, FirstX, FirstY, e.X, e.Y);
					g.DrawLine(MyPen, FirstX, FirstY, e.X, e.Y);
					//DDALine1(FirstX, FirstY, e.X, e.Y);
					OldX = e.X;
					OldY = e.Y;
				}
			}
			if (MenuID == 5)
			{
				if (!(FirstX == OldX && FirstY == OldY))
				{
					double r = Math.Sqrt((FirstX - OldX) * (FirstX - OldX) + (FirstY - OldY) * (FirstY - OldY));//求圆半径
					int r1 = (int)(r + 0.5);
					g.DrawEllipse(BackPen, FirstX - r1, FirstY - r1, 2 * r1, 2 * r1);
					r = Math.Sqrt((FirstX - e.X) * (FirstX - e.X) + (FirstY - e.Y) * (FirstY - e.Y));//求圆半径
					r1 = (int)(r + 0.5);
					g.DrawEllipse(MyPen, FirstX - r1, FirstY - r1, 2 * r1, 2 * r1);
					OldX = e.X;
					OldY = e.Y;
				}
			}
			if (MenuID == 31 && PressNum > 0)
			{
				if (!(e.X == OldX && e.Y == OldY))
				{
					g.DrawLine(BackPen, group[PressNum - 1].X, group[PressNum - 1].Y, OldX, OldY);
					g.DrawLine(MyPen, group[PressNum - 1].X, group[PressNum - 1].Y, e.X, e.Y);
					OldX = e.X;
					OldY = e.Y;
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
			MenuID = 11;PressNum = 0;
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
			MenuID = 12;PressNum = 0;
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
		private void CohenCut_Click(object sender, EventArgs e)
		{
			MenuID = 21;PressNum = 0;
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
			MenuID = 31; PressNum = 0;
			Graphics g = CreateGraphics();
			g.Clear(BackColor1);
		}
		//ScanLineFill预定义画布
	}
}
