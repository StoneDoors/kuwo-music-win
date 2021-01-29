using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.music
{
    class MusicItem
    {
        public long id { set; get; }
        public String pic120 { set; get; } //缩略图
        public String name { set; get; } //歌曲名称
        public String artist { set; get; } //歌手名称
        public String album { set; get; } //专辑名称
        public String songTimeMinutes { set; get; } //时长
        public String rid { set; get; } //下载地址代码
        public String downUrl { set; get; } //下载地址
        public String playFlag { set; get; }//
    }
}
