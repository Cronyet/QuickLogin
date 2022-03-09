using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
#pragma warning disable CS8602 // 解引用可能出现空引用。

namespace QuickLogin
{
    public partial class MainForm : Form
    {
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
            foreach (string item in Global.pwds.Keys)
            {
                Global.Pair pair = Global.pwds[item];
                TreeNode node = new()
                {
                    Text = item,
                    ToolTipText = pair.keys
                };
                root.Nodes.Add(node);
            }
            root.ExpandAll();
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
        protected override void OnClosing(CancelEventArgs e) => SavePWD();

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
            draw:  DrawPWDTree();
        }
    }
}

#pragma warning restore CS8602 // 解引用可能出现空引用。
#pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
