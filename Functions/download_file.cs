using Aliyun.OSS;
using Aliyun.OSS.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace cloud3
{
    class Download_file
    {

        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

        static AutoResetEvent _event = new AutoResetEvent(false);


        public static void GetObject(string bucketName, string fileName, string path)
        {
            try
            {
                var result = client.GetObject(bucketName, fileName);

                string file_name = "";
                for (int i = fileName.Length - 1; i >= 0; i --)
                {
                    if (fileName[i] == '/')
                    {
                        file_name = fileName.Substring(i + 1);
                        break;
                    }
                }
                if (file_name == "") file_name = fileName;


                using (var requestStream = result.Content)
                {
                    using (var fs = File.Open(path + "\\" + file_name, FileMode.OpenOrCreate))
                    {
                        int length = 4 * 1024;
                        var buf = new byte[length];
                        do
                        {
                            length = requestStream.Read(buf, 0, length);
                            fs.Write(buf, 0, length);
                        } while (length != 0);
                    }
                }

                Console.WriteLine("Get object succeeded");
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
