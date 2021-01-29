using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using WindowsFormsApp1.music;
namespace WindowsFormsApp1
{
    public partial class PPP : Form
    {
        int pageNum = 1;
        int pageSize = 20;
        long total = 0;
        int allPageNum = 1;
        string content = "";

        public PPP()
        {
            InitializeComponent();
            this.label1.Text = "PPP";
        }
        private void SearchMusic()
        {
            if (this.label1.Text == "PPP")
            {
                this.label1.Text = "搜索中";
                ResponseDto responseDto = MusicController.Search(pageNum, pageSize, content);
                if (responseDto.code == 200)
                {
                    ResponseData data = responseDto.data;
                    total = data.total;
                    allPageNum = (int)total / pageSize + 1;
                    //如果有数据显示
                    List<MusicItem> list = data.list;
                    if (list.Count > 0)
                    {
                        this.listView1.Items.Clear();

                        for (int i = 0; i < list.Count; i++)
                        {
                            MusicItem item = list[i];
                            ListViewItem lt = new ListViewItem();
                            lt.SubItems[0].Text = item.rid.ToString();
                            lt.SubItems.Add(item.name == null ? "" : item.name.ToString());
                            lt.SubItems.Add(item.artist == null ? "" : item.artist.ToString());
                            lt.SubItems.Add(item.album == null ? "" : item.album.ToString());
                            lt.SubItems.Add(item.songTimeMinutes == null ? "" : item.songTimeMinutes.ToString());
                            lt.SubItems.Add(item.id.ToString());
                            listView1.Items.Add(lt);

                        }
                    }
                }
                this.label1.Text = "PPP";
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            content = this.textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //初始化
            pageNum = 1;
            pageSize = 20;
            total = 0;
            allPageNum = 1;
            this.listView1.Items.Clear();
            SearchMusic();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (pageNum > 1)
            {
                pageNum--;
                SearchMusic();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pageNum < allPageNum)
            {
                pageNum++;
                SearchMusic();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.linkLabel1.Text = "转码中...";
            ListView.SelectedIndexCollection indexes = listView1.SelectedIndices;//
            string rid = "";
            foreach (int index in indexes)
            {
                rid += listView1.Items[index].Text;//
            }
            this.linkLabel1.Text=MusicController.ridToUrl(rid);
            musicPlayer.URL = MusicController.ridToUrl(rid);
            musicPlayer.Ctlcontrols.play();
        }

        private void PPP_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(linkLabel1.Text);
            MessageBox.Show("复制成功");
        }
    }
}
