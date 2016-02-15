using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;

namespace Code
{
    public partial class frmOrCode : Form
    {
        public frmOrCode()
        {
            InitializeComponent();
        }

        private void frmOrCode_Load(object sender, EventArgs e)
        {
            //
            cboEncoding.SelectedIndex = 2;
            //
            cboVersion.SelectedIndex = 6;

            //设置错误校验级别
            cboCorrectionLevel.SelectedIndex = 1;
        }
        #region 生成二维码及保存
        private void btnEncode_Click(object sender, EventArgs e)
        {
            if (txtEncodeData.Text == string.Empty)
            {
                MessageBox.Show("input words please", "提示", MessageBoxButtons.OK);
                return;
            }

            //创建二维码生成类
            QRCodeEncoder qrCoderEncoder = new QRCodeEncoder();
            //设置编码方式
            string encoding = cboEncoding.Text;
            if (encoding == "Byte")
            {
                //byte编码模式
                qrCoderEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            }
            else if (encoding == "Numeric")
            {
                //Numeric
                qrCoderEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
            }
            else if (encoding == "AlphaNumeric")
            {
                qrCoderEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            }
            try
            {
                //获取大小
                int scale = Convert.ToInt16(txtSize.Text);
                qrCoderEncoder.QRCodeScale = scale;
            }
            catch (Exception ex)
            {
                MessageBox.Show("请选择大小");
                return;
            }
            try
            {
                //获取版本号
                int version = Convert.ToInt16(cboVersion.Text);
                qrCoderEncoder.QRCodeVersion = version;
            }
            catch (Exception ex)
            {
                MessageBox.Show("选择版本号");
            }
            //设置错误校验的级别
            string errorCorrect = cboCorrectionLevel.Text;
            if (errorCorrect == "L")
                qrCoderEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            else if (errorCorrect == "M")
                qrCoderEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            else if (errorCorrect == "Q")
                qrCoderEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            else if (errorCorrect == "H")
                qrCoderEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;

            Image image;
            string data = txtEncodeData.Text;
            try
            {
                //生成二维码图片
                image = qrCoderEncoder.Encode(data);
                picEncode.Image = image;


            }
            catch
            {
                MessageBox.Show("请重新选择版本号和大小");
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //保存二维码图片
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png|All files(*.*)|*.*";
            saveFileDialog1.Title = "Save";
            saveFileDialog1.FileName = string.Empty;
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != null)
            {
                //输出文件
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();

                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        this.picEncode.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case 2:
                        this.picEncode.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case 3: this.picEncode.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case 4: this.picEncode.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                }
                fs.Close();
            }

        }
        #endregion

        private void btnOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png|All files(*.*)|*.*";
            //设置默认图片索引类型
            openFileDialog1.FilterIndex = 1;
            //控制对话框在关闭前是够恢复目录
            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.FileName = string.Empty;

            //如果选择图片并点击确定
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                picDecode.Image = new Bitmap(fileName);
            }
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            try
            {
                if(picDecode.Image == null)
                {
                    MessageBox.Show("请选择图片");
                }
                else
                {
                    //创建二维码解析对象
                    QRCodeDecoder qrCoderDecoder = new QRCodeDecoder();
                    //解析二维码图片
                    string decoderString = qrCoderDecoder.decode(new QRCodeBitmapImage(new Bitmap( picDecode.Image)));
                    txtDecodedData.Text = decoderString;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("这张图片有问题", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
