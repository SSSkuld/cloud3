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
            const string fileName = "test/1.cpp";

            //Create_bucket.CreateBucket(bucketName);

            //Upload_file.PutObjectFromFile(bucketName, fileName, fileToUpLoad);

            //Create_folder.CreateEmptyFolder(bucketName, folderName);

            //Download_file.GetObject(bucketName, fileName);

            //List_bucket.ListBucket(bucketName);

            Does_file_exist.DoesObjectExist(bucketName, fileName);

            //if (Does_file_exist.DoesObjectExist(bucketName, fileName) == true) // 存在
            //{
            Delete_file.DeleteObject(bucketName, fileName);
            //    //Console.WriteLine("ture");
            //}
            //else // 不存在
            //{
            //    //Console.WriteLine("false");
            //}
        }

    }
}
