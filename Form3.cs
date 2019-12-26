using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThirdGroup
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public enum DataMode { hanzi, zimu }
        public enum Sizeoffont { 四八 };
        private int[] hanzigroup = { 288 };
        private int[] charsize = { 48 };
        public Sizeoffont Style
        {
            get
            { 
                return Sizeoffont.四八;
            }
        }
        public Bitmap GetCharBMP(string str, int size)
        {
            StringFormat sf = new StringFormat(); //设置输出格式
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Near;
            Bitmap bmp = new Bitmap(size, size); //新建位图变量
            Graphics g = Graphics.FromImage(bmp);
            g.DrawString(str, new Font("幼圆", size * 3 / 4), Brushes.LightBlue, new Rectangle(0, 0, size, size), sf); //输出字符转出的位图
            return bmp;
        }
        //此部分完成字符转位图，并将位图输出
        StringBuilder stringBuilder1 = new StringBuilder();
        StringBuilder stringBuilder2 = new StringBuilder();
        public void CreateCharSetFile(string filePath, int size)
        {
            int k = 0;
            int count = 0;
            byte[] data = new byte[size];
            byte temp = 0;

            foreach (char ch in filePath)
            {
                int mode = (int)ch;
                DataMode dataMode;
                if (0x4e00 <= mode && mode <= 0x9fa5)//汉字字符集
                    dataMode = DataMode.hanzi;
                else
                    dataMode = DataMode.zimu;//其他字符
                Bitmap bmp = GetCharBMP(ch.ToString(), charsize[(int)Style]); //获取待分析的字符位图
                for (int i = 0; i < bmp.Height; i++)
                {
                    for (int j = 0; j < bmp.Width; j++)
                    {
                        if (bmp.GetPixel(j, i) == Color.FromArgb(0, 0, 0))// 以下几行根据点阵格式计算它的十六进制并写入
                            temp += (byte)Math.Pow(2, (7 - j % 8));//取模长大小
                        if (j % 8 == 7)
                        {
                            count++;
                            if (temp.ToString("x").Length == 2)
                                stringBuilder1.Append("0x" + temp.ToString("x"));
                            else stringBuilder1.Append("0x0" + temp.ToString("x"));
                            stringBuilder1.Append(",");

                            data[i * (bmp.Width / 8) + k] = temp;
                            temp = 0;
                            if (count == 4)
                            {
                                stringBuilder1.Append("/r/n");//控制每行输出字节数
                                count = 0;
                            }
                            k++;
                        }
                    }
                    k = 0;
                }//行列输出字符
                stringBuilder1.Append("/r/n");//输出一个字符换行
                for (int l = 0; l < bmp.Height; l++)
                {                
                    for (int m = 0; m < bmp.Width / 8; m++)
                    {
                        byte btCode = data[l * (bmp.Width / 8) + m];
                        //按字节输出每点的数据。
                        for (int n = 0; n < 8; n++)
                        {
                            if ((btCode & (0x80 >> n)) != 0)
                            {
                                stringBuilder2.Append("1");
                            }
                            else
                            {
                                stringBuilder2.Append("0");
                            }
                        }
                    }
                    stringBuilder2.Append("/r/n");
                }
            }
            //sb.Append("};");
            //sw.Close();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            stringBuilder1.Remove(0, stringBuilder1.Length);
            stringBuilder2.Remove(0, stringBuilder2.Length);
            picBox.SizeMode = PictureBoxSizeMode.Zoom;
            string asd = comboBox1.Text;
            picBox.Image = GetCharBMP(asd, charsize[(int)Style]);
            CreateCharSetFile(asd, hanzigroup[(int)Style]);
            textBox1.Text = stringBuilder1.ToString();
            textBox1.Text += stringBuilder2.ToString();
        }
    }
}
