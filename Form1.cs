using Exusiai.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exusiai
{
    public partial class Form1 : Form
    {
        TextBoxFormat tbFormat;
        List<string> SecretMsg;
        Random random;
        public Form1()
        {
            InitializeComponent();
            SecretMsg = new List<string>();
            SecretMsg.Add("老板，安排点差事给我们吧~");
            SecretMsg.Add("老板，不办个派对吗？来点嘻哈，再来点烤苹果派~完美的人生可是不能停止聚会的！No party，No life!");
            SecretMsg.Add("子弹上膛，准备万全★老板，今天送点什么呀！");
            SecretMsg.Add("有人认为只要有钱什么都能做到，不过在我这里可是行不通的哦。如果我要那个人安息，他有多少钱都没有用~");
            SecretMsg.Add("第一个愿望！请送我八把铳当礼物！我们天使都有自己的守护铳，但只有一把可不够看！");
            SecretMsg.Add("第二个愿望……找个人把我头上这盏日光灯管关掉！");
            SecretMsg.Add("德克萨斯那家伙能活得这么潇洒，可多亏有我罩着她，这不是明摆着的事嘛~");
            SecretMsg.Add("如果您见到一名长着漆黑的角，散发着不祥气息的天使，请定替我转告她:我从来没有忘记过她。");
            SecretMsg.Add("老板，咱们企鹅物流还是挺不错的吧？要不要来当当我们的老大？诶嘿嘿~开玩笑的~");
            SecretMsg.Add("……主啊，这个人也是我们要拯救的吗？");
            SecretMsg.Add("口令是“企鹅帝国万岁”，你就是雇主吗？叫我能天使。我和那个冷淡的鲁珀人可不一样，你要是想找点有趣的事做，随时都可以来叫我！");
            SecretMsg.Add("我喜欢这种感觉！");
            SecretMsg.Add("老板，多谢提拔~");
            SecretMsg.Add("老板……不，义人，以手中的这把铳起誓，我将守护您的生命直至万物终结之日。");
            SecretMsg.Add("小组作战，我熟得很~");
            SecretMsg.Add("没有比我更适合当队长的人！您真是别具慧眼！");
            SecretMsg.Add("出发！让我们像风暴一样碾过去！");
            SecretMsg.Add("我帮你们预定了地狱黄金地段的房产，请放心！");
            SecretMsg.Add("好！");
            SecretMsg.Add("轮到我出场了吗？");
            SecretMsg.Add("跟我来！");
            SecretMsg.Add("让我来制造点混乱。");
            SecretMsg.Add("天意！");
            SecretMsg.Add("摇滚！");
            SecretMsg.Add("弹幕！");
            SecretMsg.Add("苹果派！");
            SecretMsg.Add("好！就照这个劲头向前冲！");
            SecretMsg.Add("愿我的弹雨能熄灭你们的苦痛。");
            SecretMsg.Add("胜利的讯息即是甜蜜的福音。好~回去喝一杯吧！");
            SecretMsg.Add("铳有卡壳的时候，人生也是如此，别介意别介意~");
            SecretMsg.Add("我喜欢这个地方！");
            SecretMsg.Add("哟！");
            SecretMsg.Add("老板！来试试我的武器吗？");
            SecretMsg.Add("哟，老板！");
            random = new Random();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.notifyIcon1.Visible = false;
            System.Environment.Exit(0);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("2019 LenShang https://space.bilibili.com/573429");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.Visible = false;
            this.notifyIcon1.BalloonTipText = "我在这里!";
            this.notifyIcon1.ShowBalloonTip(5);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tbFormat = new TextBoxFormat(this.textBox1, 10);
            this.notifyIcon1.BalloonTipText = "Api Start On "+Config.Get().ApiHost;
            this.notifyIcon1.ShowBalloonTip(5);

            this.tbFormat.WriteLine("Api Start On " + Config.Get().ApiHost);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            int i = this.random.Next(0,this.SecretMsg.Count-1);
            this.tbFormat.WriteLine(this.SecretMsg[i]);
        }
    }
}
