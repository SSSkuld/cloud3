using Aliyun.OSS;
using Aliyun.OSS.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cloud3
{
    // 新建文件夹末尾要加/
    public static class Create_folder
    {
        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

        public static void CreateEmptyFolder(string bucketName, string folderName)
        {
            // Note: key treats as a folder and must end with slash.
            try
            {
                // put object with zero bytes stream.
                using (MemoryStream memStream = new MemoryStream())
                {
                    client.PutObject(bucketName, folderName, memStream);
                    Console.WriteLine("Create dir:{0} succeeded", folderName);
                }
            }
            catch (OssException ex)
            {
                Console.WriteLine("CreateBucket Failed with error info: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
        }

    }
}
