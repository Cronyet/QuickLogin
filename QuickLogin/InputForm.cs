namespace QuickLogin
{
    public partial class InputForm : Form
    {
        private bool IsConfirmed = false;

        /// <summary>
        /// 输入框
        /// </summary>
        /// <param name="header">抬头</param>
        /// <param name="tip">提示</param>
        /// <param name="cancel_text">取消按钮文字</param>
        /// <param name="confirm_text">确认按钮文字</param>
        public InputForm(string header = "InputForm", string tip = "input",
            string cancel_text = "Cancel", string confirm_text = "Confirm")
        {
            InitializeComponent();
            cancel.Text = cancel_text;
            confirm.Text = confirm_text;
            label.Text = tip;
            Text = header;
            TopMost = true;
        }

        /// <summary>
        /// 弹出输入框
        /// </summary>
        /// <param name="owner">所有者</param>
        /// <returns>输入的内容</returns>
        internal string ShowTopMost(Form? owner)
        {
            CheckForIllegalCrossThreadCalls = false; // 取消跨线程 UI 操作检查
            new Thread(() =>
            {
                Thread.Sleep(10);
                if (owner != null)
                {
                    SetDesktopLocation(owner.DesktopLocation.X + (owner.PreferredSize.Width
                        - PreferredSize.Width) / 2, owner.DesktopLocation.Y +
                        (owner.PreferredSize.Height - PreferredSize.Height) / 2);
                }
            }).Start();
            ShowDialog();
            CheckForIllegalCrossThreadCalls = true; // 启用跨线程 UI 操作检查
            return IsConfirmed ? input.Text : null;
        }

        /// <summary>
        /// 确认按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Confirm_Click(object sender, EventArgs e)
        {
            IsConfirmed = true;
            Close();
        }
    }
}
