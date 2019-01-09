using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Aliyun.OSS;
using Aliyun.OSS.Common;

namespace cloud3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lastCount = 0;
            fileCount = 0;
            path = "";
            now_bucket = Config.private_bucket_name;
            show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void show()
        {
            choose.Clear();
            //显示路径
            pathLabel.AutoSize = true;
            pathLabel.Size = new System.Drawing.Size(716, 31);
            pathLabel.TabIndex = 11;
            pathLabel.Text = path;
            pathLabel.Font = new System.Drawing.Font("微软雅黑", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            pathLabel.Location = new System.Drawing.Point(200, 210);
            
            //显示文件
            PictureBox filePicBox;
            Label fileLable;
            if (lastCount != 0) //如果上一次没有文件存在 则不用回收上一次显示的
            {
                for(int j = 0; j < lastCount; j++)
                {
                    this.Controls.Remove(allFilePicBox[j]);
                    this.Controls.Remove(allFileLabel[j]);
                }
            }

            Get_nowpath_allfiles x = new Get_nowpath_allfiles();
            x.getNowPathAllFiles(now_bucket, path);
            fileCount = x.list.Count;

            int std_num = 0;
            for (int j = 0; j < path.Length; j++)
                if (path[j] == '/')
                    std_num += 1;

            lastCount = fileCount;
            int i = 0;

            foreach (FNode s in x.list)
            {
                int temp_num = 0;
                for (int j = 0; j < s.name.Length; j++)
                    if (s.name[j] == '/')
                        temp_num += 1;
                if (s.is_file == false) temp_num -= 1;
                if (temp_num != std_num)
                {
                    continue; // 不符合的跳过不处理
                }

                //显示图片
                allFilePicBox[i] = new PictureBox();
                filePicBox = allFilePicBox[i];
                if (s.is_file == false)
                    filePicBox.BackgroundImage = global::cloud3.Properties.Resources.文件夹;
                else filePicBox.BackgroundImage = global::cloud3.Properties.Resources.文件;
                filePicBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                filePicBox.Name = s.name;
                if (i<=5)
                    filePicBox.Location= new System.Drawing.Point(200+i*100, 260);
                else
                    filePicBox.Location = new System.Drawing.Point(200 + (i-6) * 100, 260+100);
                filePicBox.Size = new System.Drawing.Size(60, 50);
                this.Controls.Add(filePicBox);

                filePicBox.Click += new EventHandler(filePicBox_clicked);
                filePicBox.DoubleClick+= new EventHandler(filePicBox_doubleClicked);


                //显示文件名 
                allFileLabel[i] = new Label();
                fileLable = allFileLabel[i];
                fileLable.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                if(i<=5)
                    fileLable.Location = new System.Drawing.Point(200 + i * 100, 330);
                else
                    fileLable.Location = new System.Drawing.Point(200 + (i - 6) * 100, 330+100);
                string new_name = "";
                for (int j = s.name.Length - 2; j >= 0; j--)
                    if (s.name[j] == '/')
                    {
                        new_name = s.name.Substring(j+1);
                        break;
                    }
                if (new_name == "") new_name = s.name;
                fileLable.Text = new_name;
                this.Controls.Add(fileLable);
                i++;
            }

        }
        private string now_bucket;
        private string path;   // 当前路径
        private List<string> fileName = new List<string>();  //要显示的文件名列表
        private PictureBox[] allFilePicBox = new PictureBox[20];
        private Label[] allFileLabel = new Label[20];
        int fileCount; //要显示的文件总数  
        int lastCount;//上一次显示的文件总数 
        private List<string> choose = new List<string>();
        private bool isClicked;
        private DateTime clickTime;
        private string clickedName;


        private void filePicBox_clicked(object sender, EventArgs e)
        {
            PictureBox filePicBox=sender as PictureBox;
            if (choose.Contains(filePicBox.Name))
            {
                choose.Remove(filePicBox.Name);
                filePicBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            }
            else
            {
                choose.Add(filePicBox.Name);
                filePicBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            }
            if (isClicked)
            {
                TimeSpan span = DateTime.Now - clickTime;
                if (span.Milliseconds < SystemInformation.DoubleClickTime && filePicBox.Name == clickedName)
                {
                    filePicBox_doubleClicked(sender, e);
                    isClicked = false;
                }
                else
                {
                    isClicked = true;
                    clickTime = DateTime.Now;
                    clickedName = filePicBox.Name;
                }
            }
            else
            {
                isClicked = true;
                clickTime = DateTime.Now;
                clickedName = filePicBox.Name;
            }


        }
        private void filePicBox_doubleClicked(object sender, EventArgs e)
            //双击文件夹后  转入新的path 
        {
            //一顿操作
            PictureBox filePicBox = sender as PictureBox;

            path = filePicBox.Name;

            show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //上传
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;

                string key = "";
                for (int i = file.Length-1; i >= 0; i --)
                {
                    if (file[i] == '\\')
                    {
                        key = path + file.Substring(i + 1);
                        break;
                    }
                }

                Console.WriteLine(key);

                Upload_file.PutObjectFromFile(now_bucket, file, key);
            }
            show();
        }
        private void downloadButton_Click(object sender, EventArgs e)
        {
            //下载

            foreach (string s in choose)
            {
                Console.WriteLine(s);
            }

            FolderBrowserDialog folder_dialog = new FolderBrowserDialog();
            folder_dialog.ShowDialog();
            if (folder_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folder_path = folder_dialog.SelectedPath;

                Console.WriteLine(folder_path);
                
                foreach (string s in choose)
                {
                    Download_file.GetObject(now_bucket, s, folder_path);
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            //删除 
            int delete = choose.Count;

            for (int i = 0; i < delete; i ++)
            {
                if (now_bucket == Config.recycled_bucket_name)
                    Delete_file.DeleteObject(now_bucket, choose[i]);
                else
                {
                    Copy_bucket_to_bucket.CopyObject(now_bucket, choose[i], Config.recycled_bucket_name, choose[i]);
                    Delete_file.DeleteObject(now_bucket, choose[i]);
                }
            }

            for (int i = 0; i < delete; i++)
            {
                fileName.Remove(choose[0]);
                choose.Remove(choose[0]);
            }
            fileCount -= delete;
            show();
        }

        private void createButton_Click(object sender, EventArgs e)
            //新建文件夹
        {
            string str = Interaction.InputBox("请输入新创建空间名称", "创建新空间", "");
            string folder_path = path + str;
          
            folder_path += "/";
            Create_folder.CreateEmptyFolder(now_bucket, folder_path);
            show();
        }

        private void moveButton_Click(object sender, EventArgs e)
            //移动至公共子空间
        {
            int delete = choose.Count;

            for (int i = 0; i < delete; i++)
            {
                if (now_bucket == Config.public_bucket_name)
                    continue;
                else
                {
                    Copy_bucket_to_bucket.CopyObject(now_bucket, choose[i], Config.public_bucket_name, choose[i]);
                    Delete_file.DeleteObject(now_bucket, choose[i]);
                }
            }

            for (int i = 0; i < delete; i++)
            {
                fileName.Remove(choose[0]);
                choose.Remove(choose[0]);
            }
            fileCount -= delete;
            show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            now_bucket = Config.private_bucket_name;
            path = "";
            show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            now_bucket = Config.public_bucket_name;
            path = "";
            show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            now_bucket = Config.recycled_bucket_name;
            path = "";
            show();
        }

        private void pathLabel_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            int len = path.Length;
            if (len == 0) return;
            //Console.WriteLine("old path : " + path);
            int if_find = 0;
            for (int i = len-2; i >= 0; i --)
            {
                if (path[i] == '/')
                {
                    if_find = 1;
                    path = path.Remove(i + 1);
                    break;
                }
            }
            if (if_find == 0) path = "";
            //Console.WriteLine("new path : " + path);
            show();
        }
    }
}
