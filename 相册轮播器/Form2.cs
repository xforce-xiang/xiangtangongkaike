using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 相册轮播器
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public ArrayList fsi = new ArrayList();
        public int mytimer;
        public string picPath;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            //关闭窗体的两种方式
            this.Close();//退出当前窗体

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Interval = mytimer;
            timer1.Start();
        }

        int MM = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (MM < fsi.Count)
            {
                if (picPath.Length == 3)
                {
                    pictureBox2.Image = Image.FromFile(picPath + fsi[MM].ToString());//获取图片路径
                }
                else
                {
                    pictureBox2.Image = Image.FromFile(picPath + "\\" + fsi[MM]);
                }
                MM++;
                if(MM >=fsi.Count)
                {
                    MM = 0;
                }
            }
           
            
        }
    }
}
