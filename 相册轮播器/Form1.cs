using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 相册轮播器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public bool Pflag;
        ArrayList al = new ArrayList(); //动态数组
        FileSystemInfo[] fsinfo; //可以存储目录
        int flag = 0;
        int MM = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            cbbShowType.SelectedIndex = 0;
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                al.Clear();
                listBox1.Items.Clear();
                txtPicPath.Text = folderBrowserDialog1.SelectedPath;//获取选定的路径
                DirectoryInfo di = new DirectoryInfo(txtPicPath.Text);
                fsinfo = di.GetFileSystemInfos();//返回目录的所有文件

                for (int i = 0; i < fsinfo.Length; i++)
                {
                    string filename = fsinfo[i].ToString();

                    string filetype = filename.Substring(filename.LastIndexOf(".") + 1, filename.Length - filename.LastIndexOf(".") - 1);
                    filetype = filetype.ToLower();//转换为小写
                    if (filetype == "jpg" || filetype == "png" || filetype == "gif" || filetype == "bmp" || filetype == "jpeg")
                    {
                        //将图片放入listbox
                        listBox1.Items.Add(fsinfo[i].ToString());
                        //将图片放入动态数组
                        al.Add(fsinfo[i].ToString());
                        flag++;
                    }
                }
                listBox1.SetSelected(0, true);
                listBox1.Focus();
                tssltotle.Text = "共有" + flag + "张图片";
                Pflag = true;
            }
            //test = txtPicPath.Text.Trim().Length;
            button2.Enabled = true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //获取路径
            string picpath = txtPicPath.Text + "\\" + listBox1.SelectedItem.ToString();
            tssltotle.Text="当前第"+ Convert.ToString(listBox1.SelectedIndex+1)+"张图片|图片位置";

            pictureBox1.Image = Image.FromFile(picpath);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(Pflag)
            {
                if(txtTime.Text != "")
                {
                    if(cbbShowType.SelectedIndex == 1)
                    {
                        timer1.Interval = int.Parse(txtTime.Text.Trim());
                        timer1.Start();
                        button2.Enabled = true;
                    }
                    else
                    {
                        Form2 frm2 = new Form2();
                        frm2.fsi = al;
                        frm2.picPath = txtPicPath.Text.Trim();
                        frm2.mytimer = int.Parse(txtTime.Text.Trim());
                        frm2.ShowDialog();//显示成窗口界面
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            button2.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            txtPicPath.Text = "";
            tssltotle.Text = "";
            pictureBox1.Image = null;
            Pflag = false;
            timer1.Stop();
            button2.Enabled = false;



        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(MM<listBox1.Items.Count)
            {
                if(txtPicPath.Text.Trim().Length == 3)
                {
                    pictureBox1.Image = Image.FromFile(txtPicPath.Text.Trim() + listBox1.Items[MM].ToString());
                    listBox1.SetSelected(MM, true);
                }
                else
                {
                    pictureBox1.Image = Image.FromFile(txtPicPath.Text.Trim()+"\\"+listBox1.Items[MM].ToString());
                    listBox1.SetSelected(MM,true);
                } 
                MM++;
                if(MM >= listBox1.Items.Count)
                {
                    MM = 0;
                }
            }
           
        }

        private void txtTime_KeyDown(object sender, KeyEventArgs e)
        {
            if(txtPicPath.Text != "")
            {
                if (txtTime.Text.Trim().Substring(0,1) == "0")
                {
                    txtTime.Text = txtTime.Text.Substring(1, txtTime.Text.Length - 1);//从文本1开始
                }
            }
                
        }

        private void txtTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.KeyChar 获取键盘输入的字符
            if(!(e.KeyChar<='9'&&e.KeyChar>='0')&&e.KeyChar!='\r'&&e.KeyChar!='\b')
            {
                //Handle 获取或者设置一个值，该值指示是否处理过KeyPress事件
                e.Handled = true;//不处理从事件                
            }
        }

        private void cbbShowType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbShowType.SelectedIndex == 0)//原始图像大小
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            }
            else if(cbbShowType.SelectedIndex == 1)//适应窗口大小
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }


    }
}
