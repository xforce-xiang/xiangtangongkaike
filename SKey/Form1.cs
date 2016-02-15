using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SKey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string str =null;
            //产生随机数
            //for (int i = 0; i < 6; i++)
            //{
            //    int rNumber = r.Next(0,10);
            //    str += rNumber;
            //}

            string textArray = "0123456789abcdefghijklmnopqrtuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < 6; i++)
			{
                str+=textArray.Substring(r.Next() % textArray.Length,1);
			}

            Bitmap bmp = new Bitmap(150, 40);
            Graphics g = Graphics.FromImage(bmp);//绘制图画
            for (int i = 0; i < 6; i++)
            {
                Point p = new Point(i * 20, 0);
                string[] fonts = { "宋体", "楷体", "微软雅黑", "黑体", "仿宋", "隶书", "华文琥珀", "行书" };
                Color[] colors = { Color.Yellow, Color.Red, Color.Blue, Color.Green, Color.GreenYellow, Color.Pink, Color.YellowGreen };
                //将随机数绘制到验证码区
                g.DrawString(str[i].ToString(),new Font(fonts[r.Next(0,8)],20,FontStyle.Bold),new SolidBrush(colors[r.Next(0,7)]),p);


            }
        }
    }
}
