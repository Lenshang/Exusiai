using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exusiai.Utils
{
    public class TextBoxFormat
    {
        private TextBox tbControl = null;
        private List<BoxMessage> Messages = null;
        public int MaxLine { get; set; }
        /// <summary>
        /// 初始化一个带格式的文本框
        /// </summary>
        /// <param name="_tbControl">文本框控件</param>
        /// <param name="_MaxLine">最大行数</param>
        public TextBoxFormat(TextBox _tbControl, int _MaxLine = 80)
        {
            tbControl = _tbControl;
            MaxLine = _MaxLine;
            Messages = new List<BoxMessage>();
            _tbControl.Font = new System.Drawing.Font("Microsoft YaHei", 8);
        }
        private void RefreshTextBox()
        {
            InvokeOnUiThreadIfRequired(() => {
                tbControl.Text = "";
                FormatMessage();
                tbControl.SelectionStart = tbControl.TextLength;
                tbControl.ScrollToCaret();
            });
        }
        private void InvokeOnUiThreadIfRequired(Action action)
        {
            if (tbControl.InvokeRequired)
            {
                tbControl.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }
        /// <summary>
        /// 将格式化文本写入TextBox
        /// </summary>
        private void FormatMessage()
        {
            List<string> _TextLines = new List<string>();
            foreach (var message in Messages)
            {
                if (message == null) continue;
                _TextLines.Add($"[{message.date.ToString()}]: {message.Content}");
            }

            tbControl.Lines = _TextLines.ToArray();
        }
        /// <summary>
        /// 显示一条信息（行）
        /// </summary>
        /// <param name="message"></param>
        public void WriteLine(string message)
        {
            if (Messages.Count >= MaxLine)
            {
                Messages.Reverse();
                Messages.RemoveRange(MaxLine - 1, Messages.Count - (MaxLine - 1));
                Messages.Reverse();
            }
            Messages.Add(new BoxMessage() { date = DateTime.Now, Content = message });
            RefreshTextBox();//刷新文本框
        }
    }
    class BoxMessage
    {
        public DateTime date { get; set; }
        public string Content { get; set; }
    }
}
