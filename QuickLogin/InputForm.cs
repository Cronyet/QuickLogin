using System.ComponentModel;

namespace QuickLogin
{
    public partial class InputForm : Form
    {
        private bool IsCancled = false;

        public enum InputType
        {
            Text = 0, Keyboard = 1
        }

        private readonly InputType inputType;

        /// <summary>
        /// 输入框
        /// </summary>
        /// <param name="header">抬头</param>
        /// <param name="tip">提示</param>
        /// <param name="cancel_text">取消按钮文字</param>
        /// <param name="confirm_text">确认按钮文字</param>
        public InputForm(string header = "InputForm", string tip = "input",
            string cancel_text = "Cancel", string confirm_text = "Confirm",
            InputType type = InputType.Text)
        {
            InitializeComponent();
            cancel.Text = cancel_text;
            confirm.Text = confirm_text;
            label.Text = tip;
            Text = header;
            TopMost = true;
            inputType = type;
            input.Text = "";
            switch (type)
            {
                case InputType.Keyboard:
                    input.ReadOnly = true;
                    KeyDown += (_, e) =>
                    {
                        string rst = "";
                        if (e.Control) rst += "Ctrl+";
                        if (e.Shift) rst += "Shift+";
                        if (e.Alt) rst += "Alt+";
                        rst += e.KeyCode.ToString();
                        input.Text = rst;
                    };
                    break;
            }
        }

        /// <summary>
        /// 重载关闭函数
        /// </summary>
        /// <param name="e">关闭事件</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (!IsCancled)
                if (inputType == InputType.Keyboard && (!CheckKey()))
                {
                    MessageBox.Show("Hotkey is invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
        }

        /// <summary>
        /// 弹出输入框
        /// </summary>
        /// <param name="owner">所有者</param>
        /// <returns>输入的内容</returns>
        internal string? ShowTopMost(Form? owner)
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
                CheckForIllegalCrossThreadCalls = true; // 启用跨线程 UI 操作检查
            }).Start();
            ShowDialog();
            return IsCancled ? null : input.Text;
        }

        private static readonly string[] InvalidKey = new string[3]
        { "ControlKey", "ShiftKey", "Menu" };

        /// <summary>
        /// 检查按键是否合法
        /// </summary>
        /// <returns>是否合法</returns>
        private bool CheckKey()
        {
            bool pass = true;
            foreach (string item in InvalidKey)
                if (input.Text.IndexOf(item) != -1)
                {
                    pass = false;
                    break;
                }
            if (input.Text.Equals("") || input.Text.Equals(null)) pass = false;
            return pass;
        }

        /// <summary>
        /// 确认按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Confirm_Click(object sender, EventArgs e)
        {
            if (inputType == InputType.Keyboard && (!CheckKey()))
            {
                MessageBox.Show("Hotkey is invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else Close();
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            IsCancled = true;
            Close();
        }
    }
}
