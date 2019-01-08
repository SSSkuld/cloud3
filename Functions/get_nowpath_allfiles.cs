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
    public class Get_nowpath_allfiles
    {
        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);
        public List<FNode> list;

        public Get_nowpath_allfiles()
        {
            list = new List<FNode>();
            list.Clear();
        }

        public void getNowPathAllFiles(string bucketName, string prefix)
        {
            try
            {
                list.Clear();
                var keys = new List<string>();
                ObjectListing result = null;
                string nextMarker = string.Empty;
                do
                {
                    var listObjectsRequest = new ListObjectsRequest(bucketName)
                    {
                        Marker = nextMarker,
                        MaxKeys = 100,
                        Prefix = prefix,
                    };
                    result = client.ListObjects(listObjectsRequest);
                    foreach (var summary in result.ObjectSummaries)
                    {
                        keys.Add(summary.Key);

                        FNode newnode = new FNode(summary.Key, summary.Key.Contains(".")); // 1 = file  0 = folder
                        list.Add(newnode);
                    }
                    nextMarker = result.NextMarker;
                } while (result.IsTruncated);
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
