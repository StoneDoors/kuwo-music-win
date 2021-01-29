using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.utils
{
    class NetUtils
    {
        /// <summary>
        /// 指定Url地址使用Get 方式获取全部字符串
        /// </summary>
        /// <param name="url">请求链接地址</param>
        /// <returns></returns>
        public static string Get(string url)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Timeout = 600000;//设置超时时间
            SetHeaderValue(req.Headers, "Connection", "keep-alive");
            req.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; QQWubi 133; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; CIBA; InfoPath.2)";
            Stream stream = null;
         
            try
            {
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                stream = resp.GetResponseStream();
                //获取内容
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="dic">请求参数定义</param>
        /// <returns></returns>
        public static string Get(string url, Dictionary<string, string> dic)
        {
            #region 创建Web访问对象httpwebrequest
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";//获取货币设置请求的方法
            myRequest.Accept = "*/*";
            foreach (var item in dic)
            {
                SetHeaderValue(myRequest.Headers, item.Key, item.Value);
            }
            myRequest.Timeout = 600000;//设置超时时间
            myRequest.ReadWriteTimeout = 600000;
            SetHeaderValue(myRequest.Headers, "Connection", "keep-alive");
            myRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; QQWubi 133; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; CIBA; InfoPath.2)";
            #endregion

            #region 通过Web访问对象获取响应内容
            String result = "";
            Stream stream = null;
            try
            {
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                stream = myResponse.GetResponseStream();
                //获取内容，从特定的流获取数据
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            #endregion
            return result;
        }

        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }
    }
}
