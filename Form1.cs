using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e) //打开锁屏设置
        {
            try
            { 
                System.Diagnostics.Process.Start("ms-settings:lockscreen");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text);
            }

        }

        private void BtnDesktop_Click(object sender, EventArgs e)//打开桌面设置
        {
            try
            { System.Diagnostics.Process.Start("ms-settings:personalization-background");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text);
            }
        }


        private void BtnPath_Click(object sender, EventArgs e)//指定路径
        {
            try
            {
                FolderBrowserDialog fb = new FolderBrowserDialog();

                fb.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                fb.ShowNewFolderButton = true;
                fb.ShowDialog();
                txtPath.Text = fb.SelectedPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text);
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path= Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            txtPath.Text = path;

        }

        private void BtnStart_Click(object sender, EventArgs e)//开始保存图片
        {
            
            string orgPic = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                @"\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets\";

            if (!Directory.Exists(orgPic))
            {
                MessageBox.Show("锁屏图片存放文件夹不存在,请检查系统版本或升级本程序!",this.Text);
                return;
            }
            if(!Directory.Exists(txtPath.Text))
            {
                try
                {
                    Directory.CreateDirectory(txtPath.Text);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text);
                    return;
                }
                
            }

            string[] files = Directory.GetFiles(orgPic);
            int i = 0;
            foreach (string file in files)
            {
                int w = Image.FromFile(file).Width;
                Console.WriteLine(Image.FromFile(file).Width.ToString());
                

                if (w >= (int)nudWidth.Value)
                {
                    //MessageBox.Show(w.ToString());
                    
                    
                    FileInfo f = new FileInfo(file);
                    string newFile=txtPath.Text+"\\"+f.Name+ ".jpg";
                    File.Copy(file, newFile, true);
                    i++;
                }

                
            }
            string msg = string.Format("已有{0}个文件被复制.", i);
            MessageBox.Show(msg, this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
            try
            {
                System.Diagnostics.Process.Start(txtPath.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text);
            }
        }

        private void Label1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string orgPic = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                        @"\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets\";
                System.Diagnostics.Process.Start(orgPic);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text);
            }
        }

        private void Label2_DoubleClick(object sender, EventArgs e)
        {
            int w=Screen.PrimaryScreen.Bounds.Width;
            nudWidth.Value = w;

        }
    }


}
