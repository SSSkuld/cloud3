using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aliyun.OSS.Common;

namespace cloud3
{
    internal class Config
    {
        public static string AccessKeyId = "LTAI2EBx4neKsJHr";

        public static string AccessKeySecret = "FWPS0aZKK2wRG0TNUg5BXAX3YNrnC6";

        public static string Endpoint = "http://oss-cn-beijing.aliyuncs.com";

        public static string DirToDownload = "E:\\资料\\大三上\\云计算\\download";

        public static string private_bucket_name = "testmemory1";

        public static string public_bucket_name = "publicmemory";

        /*

        public static string FileToUpload = "<your local file to upload>";

        public static string BigFileToUpload = "<your local big file to upload>";
        public static string ImageFileToUpload = "<your local image file to upload>";
        public static string CallbackServer = "<your callback server uri>";
        */
    }
}
