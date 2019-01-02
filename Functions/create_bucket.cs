using Aliyun.OSS;
using Aliyun.OSS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cloud3
{
    public static class Create_bucket
    {
        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

        public static void CreateBucket(string bucketName)
        {
            try
            {
                client.CreateBucket(bucketName);

                Console.WriteLine("Created bucket name:{0} succeeded ", bucketName);
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error info: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                                  ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            return;
        }
    }
}
