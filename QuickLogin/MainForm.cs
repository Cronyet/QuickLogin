using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#pragma warning disable IDE0079 // 请删除不必要的忽略
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
#pragma warning disable CS8602 // 解引用可能出现空引用。
#pragma warning disable CS8625 // 无法将 null 字面量转换为非 null 的引用类型。
#pragma warning disable CS8605 // 取消装箱可能为 null 的值。

namespace QuickLogin
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 热键id记录
        /// </summary>
        private readonly Dictionary<int, string> hotKey_id = new();

        /// <summary>
        /// 热键消息定量
        /// </summary>
        private const int WM_HOTKEY = 0x0312;

        /// <summary>
        /// 构造函数 并 初始化
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            InitKeyEvent();

            // 如果存在已保存的密码数据则读取他
            if (File.Exists(Global.FilePath_PWD)) LoadPWD();

            // 设置刷新按钮事件
            btn_refresh.Click += (_, _) =>
            {
                DrawPWDTree();
            };

            // 启动时绘制
            DrawPWDTree();
        }

        /// <summary>
        /// 初始化键盘事件
        /// </summary>
        private void InitKeyEvent()
        {
            KeyDown += (_, e) =>
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (pwdtree.SelectedNode.Tag?.ToString() != "root_node")
                        {
                            Global.pwds.Remove(pwdtree.SelectedNode.Text);
                            DrawPWDTree();
                        }
                        break;
                    case Keys.Space:
                        Clipboard.SetText(Global.pwds[pwdtree.SelectedNode.Text].pwd);
                        break;
                }
            };
        }

        /// <summary>
        /// 绘制密码树
        /// </summary>
        private void DrawPWDTree()
        {
            TreeView tree = pwdtree;
            TreeNode root = tree.Nodes[0];
            root.Nodes.Clear();
            // 取消注册全部热键
            foreach (int id in hotKey_id.Keys)
                HotKey.UnregisterHotKey(Handle, id);
            hotKey_id.Clear();
            int index = 1;
            foreach (string item in Global.pwds.Keys)
            {
                Global.Pair pair = Global.pwds[item];
                TreeNode node = new()
                {
                    Text = item,
                    ToolTipText = pair.keys
                };
                root.Nodes.Add(node);
                // 注册相关热键
                RegisterHotKey(item, index);
                ++index;
            }
            root.ExpandAll();
        }

        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="name">热键名称</param>
        private void RegisterHotKey(string name, int id)
        {
            string[] keys = Global.pwds[name].keys.Split('+');
            bool[] Modifiers = new bool[3] { false, false, false };
            Keys key = Keys.None;
            foreach (string item in keys)
            {
                switch (item)
                {
                    case "Ctrl": Modifiers[0] = true; break;
                    case "Shift": Modifiers[1] = true; break;
                    case "Alt": Modifiers[2] = true; break;
                    default: key = (Keys)new KeysConverter().ConvertFromString(item); break;
                }
            }
            hotKey_id.Add(id, name);
            HotKey.RegisterHotKey(Handle, id,
                (Modifiers[0] ? HotKey.KeyModifiers.Ctrl : HotKey.KeyModifiers.None) |
                (Modifiers[1] ? HotKey.KeyModifiers.Shift : HotKey.KeyModifiers.None) |
                (Modifiers[2] ? HotKey.KeyModifiers.Alt : HotKey.KeyModifiers.None)
                , key);
        }

        /// <summary>
        /// 重载消息处理函数
        /// </summary>
        /// <param name="m">消息</param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    SendKeys.SendPwd(Global.pwds[hotKey_id[m.WParam.ToInt32()]].pwd);
                    break;
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// 加载已保存的密码
        /// </summary>
        private static void LoadPWD()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Global.FilePath_PWD, FileMode.Open, FileAccess.Read, FileShare.None);
            Global.pwds = (Dictionary<string, Global.Pair>)formatter.Deserialize(stream);
            stream.Close();
        }

        /// <summary>
        /// 保存密码对
        /// </summary>
        private static void SavePWD()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Global.FilePath_PWD, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, Global.pwds);
            stream.Close();
        }

        /// <summary>
        /// 关闭时保存密码对
        /// </summary>
        /// <param name="e">关闭事件</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            foreach (int id in hotKey_id.Keys)
            {
                HotKey.UnregisterHotKey(Handle, id);
            }
            SavePWD();
        }

        /// <summary>
        /// 退出按钮事件
        /// </summary>
        /// <param name="sender">退出按钮</param>
        /// <param name="e">事件参数</param>
        private void Btn_exit_Click(object sender, EventArgs e) => Close();

        /// <summary>
        /// 添加新密码按钮事件
        /// </summary>
        /// <param name="sender">添加新密码按钮</param>
        /// <param name="e">按钮事件</param>
        private void Btn_add_Click(object sender, EventArgs e)
        {
            string name = "", pwd = "", key = "";
            while (name.Equals(""))
            {
                string? name_tmp = new InputForm("Add new pwd", "Name: ").ShowTopMost(this);
                if (name_tmp == null) goto draw;
                else name = name_tmp;
            }
            while (pwd.Equals(""))
            {
                string? pwd_tmp = new InputForm("Add new pwd", "Password: ").ShowTopMost(this);
                if (pwd_tmp == null) goto draw;
                else pwd = pwd_tmp;
            }
            while (key.Equals(""))
            {
                string? key_tmp = new InputForm("Add new pwd", "Key: ",
                    type: InputForm.InputType.Keyboard).ShowTopMost(this);
                if (key_tmp == null) goto draw;
                else key = key_tmp;
            }
            Global.pwds.Add(name, new Global.Pair()
            {
                pwd = pwd,
                keys = key
            });
        draw: DrawPWDTree();
        }
    }
}

#pragma warning restore CS8605 // 取消装箱可能为 null 的值。
#pragma warning restore CS8625 // 无法将 null 字面量转换为非 null 的引用类型。
#pragma warning restore CS8602 // 解引用可能出现空引用。
#pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
#pragma warning restore IDE0079 // 请删除不必要的忽略
