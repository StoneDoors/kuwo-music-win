using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;
using WindowsFormsApp1.utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp1.music
{
    class MusicController
    {
        /**
    * @Description  列表查询
    * @Date 2020/10/21 14:41
    * @Created by 石昌鹏
    */
        public static ResponseDto Search(int pageNum, int pageSize, String content)
        {
            ResponseDto responseDto = new ResponseDto();
            //判断是否缺少查询
            if (content == null || content.Equals(""))
            {
                responseDto.code=400;
                return responseDto;
            }
            //拼凑访问路径
            StringBuilder base_url = new StringBuilder("http://www.kuwo.cn/api/www/search/searchMusicBykeyWord?");
            base_url.Append("key=" + UrlEncode(content));
            base_url.Append("&pn=" + pageNum);
            base_url.Append("&rn=" + pageSize);
            base_url.Append("&httpsStatus=1");
            base_url.Append("&reqId=cc337fa0-e856-11ea-8e2d-ab61b365fb50");
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("Cookie", "_ga=GA1.2.1083049585.1590317697; _gid=GA1.2.2053211683.1598526974; _gat=1; Hm_lvt_cdb524f42f0ce19b169a8071123a4797=1597491567,1598094297,1598096480,1598526974; Hm_lpvt_cdb524f42f0ce19b169a8071123a4797=1598526974; kw_token=HYZQI4KPK3P");
            header.Add("Referer", "http://www.kuwo.cn/search/list?key=%E5%91%A8%E6%9D%B0%E4%BC%A6");
            header.Add("CSRF", "HYZQI4KPK3P");
            String result = NetUtils.Get(base_url.ToString(), header);
            responseDto = DeserializeObject<ResponseDto>(result);
            if (responseDto.code != 200)
            {
                return responseDto;
            }
            if (responseDto.data != null)
            {
                if (responseDto.data.list != null && responseDto.data.list.Count > 0)
                {
                    //查找下载地址
                    int i = 1;
                    foreach (var item in responseDto.data.list)
                    {
                        item.id=i;
                       // item.downUrl=ridToUrl(item.rid);
                        item.playFlag="2";
                        i++;
                    }
                }
            }
            return responseDto;

        }
        public static string UrlEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return (sb.ToString());
        }
        public static string ridToUrl(String rid)
        {
            string url = "";
            if (rid != null && !rid.Equals(""))
            {
                String api_music = "http://www.kuwo.cn/url?format=mp3&rid=" + rid + "&response=url&type=convert_url3"
                    + "&br=128kmp3&from=web&t=1598528574799&httpsStatus=1"
                    + "&reqId=72259df1-e85a-11ea-a367-b5a64c5660e5";
                String s = NetUtils.Get(api_music);
                Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(s);
                foreach (var item in dic)
                {
                    if (item.Key.Equals("url"))
                    {
                        url = item.Value;
                    }
                }
            }
            return url;
        }
        public static T DeserializeObject<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

    }
}
