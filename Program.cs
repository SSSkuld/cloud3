using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aliyun.OSS.Common;


namespace cloud3
{
    public class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
//        [STAThread]
        static void Main()
        {
            const string bucketName = "testmemory1";
            const string fileToUpLoad = "E:\\code\\course\\os\\1.cpp";
            const string folderName = "test/"; // 新建文件夹末尾应该加/

            //Create_bucket.CreateBucket(bucketName);

            //Uplord_file.PutObject(bucketName, fileToUpLoad);

            Create_folder.CreateEmptyFolder(bucketName, folderName);

            //if (Does_file_exist.DoesObjectExist(bucketName, "1.cpp") == true) // 存在
            //{
            //    Delete_file.DeleteObject(bucketName, "1.cpp");
            //    //Console.WriteLine("ture");
            //}
            //else // 不存在
            //{
            //    //Console.WriteLine("false");
            //}
            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            */
        }

    }
}
