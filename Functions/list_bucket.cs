using Aliyun.OSS;
using Aliyun.OSS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace cloud3
{
    class List_bucket
    {
        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

        static AutoResetEvent _event = new AutoResetEvent(false);


        public static void ListBucket(string bucketName, List<String> now_list)
        {
            try
            {
                var result = client.ListObjects(bucketName);

                Console.WriteLine("List objects of bucket:{0} succeeded ", bucketName);
                foreach (var summary in result.ObjectSummaries)
                {
                    Console.WriteLine(summary.Key);
                }

                Console.WriteLine("List objects of bucket:{0} succeeded, is list all objects ? {1}", bucketName, !result.IsTruncated);
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
