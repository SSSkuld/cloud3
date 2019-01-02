using Aliyun.OSS;
using Aliyun.OSS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cloud3
{
    public static class Does_file_exist
    {
        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

        // 判断文件是否存在，传入bucket与文件名
        public static bool DoesObjectExist(string bucketName, string fileName)
        {
            try
            {
                var exist = client.DoesObjectExist(bucketName, fileName);
                Console.WriteLine("exist ? " + exist);
                return exist;
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
                return false;
            }
        }
  
    }
}
