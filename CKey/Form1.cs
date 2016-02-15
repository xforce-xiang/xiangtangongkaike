using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CKey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string txt = "";
        public static object[] CreateCode(int strength)
        {
            string[] r = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", };
            Random rnd = new Random();
            object[] bytes = new object[strength];
            for (int i = 0; i < strength; i++)
            {
                //区位码第一位
                int r1 = rnd.Next(11, 14);
                string str_1 = r[r1].Trim();

                //避免产生相同的汉字
                rnd = new Random(r1 * unchecked((int)DateTime.Now.Ticks) + i);

                //区位码第二位
                int r2;
                if (r1 == 13)
                {
                    r2 = rnd.Next(0, 7);
                }
                else
                {
                    r2 = rnd.Next(0, 16);
                }
                string str_2 = r[r2].Trim();
                rnd = new Random(r2 * unchecked((int)DateTime.Now.Ticks) + i);
                //区位码第三位
                int r3 = rnd.Next(10, 16);
                string str_3 = r[r3].Trim();
                rnd = new Random(r3 * unchecked((int)DateTime.Now.Ticks) + i);
                //区位码第四位
                int r4;
                if (r3 == 10)
                {
                    r4 = rnd.Next(1, 16);
                }
                else if (r3 == 15)
                {
                    r4 = rnd.Next(0, 15);
                }
                else
                {
                    r4 = rnd.Next(0, 16);
                }
                string str_4 = r[r4].Trim();
                //定义两个临时变量存储产生的随机汉字区位码
                byte byte1 = Convert.ToByte(str_1 + str_2, 16);
                byte byte2 = Convert.ToByte(str_3 + str_4, 16);
                //降临时变量的汉字区位码存储到字节数组
                byte[] str_r = new byte[] { byte1, byte2 };
                //将字节数组存储到object数组中
                bytes.SetValue(str_r, i);
            }

            return bytes;
        }

        private void CreatImage()
        {
            //GB2312
            Encoding gb = Encoding.GetEncoding("gb2312");
            //调用产生的随机汉字编码
            object[] bytes = CreateCode(4);
            //解码
            string str1 = gb.GetString((byte[])Convert.ChangeType(bytes[0], typeof(byte[])));
            string str2 = gb.GetString((byte[])Convert.ChangeType(bytes[1], typeof(byte[])));
            string str3 = gb.GetString((byte[])Convert.ChangeType(bytes[2], typeof(byte[])));
            string str4 = gb.GetString((byte[])Convert.ChangeType(bytes[3], typeof(byte[])));
            txt = str1 + str2 + str3 + str4;//拼接解码汉字
            //MessageBox.Show(str1);
            //判断txt是否获取到汉字
            if(txt == null|| txt == string.Empty)
            {
                return;
            }

            Bitmap image = new Bitmap((int)Math.Ceiling((txt.Length*20.5)),22);
            Graphics g = Graphics.FromImage(image);
            try
            {
                Random random = new Random();

                g.Clear(Color.White);
                for (int i = 0; i < 10; i++)
                {
                    Point p1 = new Point(random.Next(image.Width), random.Next(image.Height));
                    Point p2 = new Point(random.Next(image.Width), random.Next(image.Height));
                    g.DrawLine(new Pen(Color.Blue), p1, p2);
                }
                //for (int i = 0; i < length; i++)
                //{
                    
                //}

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreatImage();

        }

    }
}
