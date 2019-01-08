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
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());



            const string bucketName = "testmemory1";
            //const string fileToUpLoad = "E:\\code\\course\\os\\1.cpp";
            const string folderName = "test/123/"; // 新建文件夹末尾应该加/
                                                   //const string fileName = "test/1.cpp";


            //Get_nowpath_allfiles x = new Get_nowpath_allfiles();
            //x.getNowPathAllFiles(bucketName, "");

            //foreach (var i in x.list)
            //{
            //    Console.WriteLine(i.name);
            //}



            //Create_bucket.CreateBucket(bucketName);

            //Upload_file.PutObjectFromFile(bucketName, fileName, fileToUpLoad);

            //Create_folder.CreateEmptyFolder(bucketName, folderName);

            //Download_file.GetObject(bucketName, "roottt/模板.docx");


            //Get_nowpath_allfiles p;



            //Get_nowpath_allfiles.getNowPathAllFiles(bucketName, "test");

            //foreach (FNode x in Get_nowpath_allfiles.list)
            //{
            //    Console.WriteLine(x.name);
            //    Console.WriteLine(x.is_file);
            //}


            //List_bucket.ListBucket(bucketName);

            //Does_file_exist.DoesObjectExist(bucketName, fileName);

            //Delete_file.DeleteObject(bucketName, folderName);

            //if (Does_file_exist.DoesObjectExist(bucketName, fileName) == true) // 存在
            //{
            //    //Console.WriteLine("ture");
            //}
            //else // 不存在
            //{
            //    //Console.WriteLine("false");
            //}
        }

    }
}
