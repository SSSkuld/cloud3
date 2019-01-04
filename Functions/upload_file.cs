using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aliyun.OSS;
using Aliyun.OSS.Common;
using System.Threading;
using System.Security.Cryptography;
using System.IO;
using Aliyun.OSS.Util;

namespace cloud3
{
    public static class Upload_file
    {
        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

        //static string fileToUpload = Config.FileToUpload;

        /// <summary>
        /// sample for put object to oss
        /// </summary>

        // 上传文件，自带.后缀名，无法上传文件夹
        public static void PutObjectFromFile(string bucketName, string key, string fileToUpload)
        {
            // debug
            try
            {
                client.PutObject(bucketName, key, fileToUpload);
                Console.WriteLine("Put object:{0} succeeded", key);
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }
        }

    }
}
