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
        public enum geshisize { 十六, 二十四, 三十二, 四十八 };
        private int[] hanzishuzu = { 32, 72, 128, 288 };
        private int[] hanzisize = { 16, 24, 32, 48 };
        public geshisize geshi
        {
            get
            {
                if (radioButton_16.Checked) return geshisize.十六;
                else if (radioButton_24.Checked) return geshisize.二十四;
                else if (radioButton_32.Checked) return geshisize.三十二;
                else return geshisize.四十八;
            }
        }
        public Bitmap GetCharBMP(string str, int size)
        {
            StringFormat sf = new StringFormat(); // 设置格式
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Near;
            Bitmap bmp = new Bitmap(size, size); // 新建位图变量
            Graphics g = Graphics.FromImage(bmp);
            g.DrawString(str, new Font("宋体", size * 3 / 4), Brushes.Black, new Rectangle(0, 0, size, size), sf); // 向图像变量输出字符
            return bmp;
        }
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
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
                if (0x4e00 <= mode && mode <= 0x9fa5)
                    dataMode = DataMode.hanzi;
                else
                    dataMode = DataMode.zimu;
                Bitmap bmp = GetCharBMP(ch.ToString(), hanzisize[(int)geshi]); // 获取待分析的字符位图

                for (int i = 0; i < bmp.Height; i++)
                {
                    for (int j = 0; j < bmp.Width; j++)
                    {
                        if (bmp.GetPixel(j, i) == Color.FromArgb(0, 0, 0))// 以下几行根据点阵格式计算它的十六进制并写入
                            temp += (byte)Math.Pow(2, (7 - j % 8));//横向取模大~小
                        if (j % 8 == 7)
                        {
                            count++;
                            if (temp.ToString("x").Length == 2)
                                sb.Append("0x" + temp.ToString("x"));
                            else sb.Append("0x0" + temp.ToString("x"));
                            //if (!(j == bmp.Width - 1 && i == bmp.Height - 1 && ch == filePath[filePath.Length - 1]))
                            sb.Append(",");

                            data[i * (bmp.Width / 8) + k] = temp;
                            temp = 0;
                            if (count == 4)
                            {
                                sb.Append("/r/n");//控制每行输出字节数
                                count = 0;
                            }
                            k++;
                        }
                    }
                    k = 0;
                }
                sb.Append("/r/n");//输出一个字符换行
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
                                sb2.Append("1");
                            }
                            else
                            {
                                sb2.Append("0");
                            }
                        }
                    }
                    sb2.Append("/r/n");
                }
            }
            //sb.Append("};");
            //sw.Close();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            sb.Remove(0, sb.Length);
            sb2.Remove(0, sb2.Length);
            picBox.SizeMode = PictureBoxSizeMode.Zoom;
            string asd = comboBox1.Text;
            picBox.Image = GetCharBMP(asd, hanzisize[(int)geshi]);
            CreateCharSetFile(asd, hanzishuzu[(int)geshi]);
            textBox1.Text = sb.ToString();
            textBox1.Text += sb2.ToString();
        }
    }
}
