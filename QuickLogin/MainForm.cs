using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#pragma warning disable IDE0079 // ��ɾ������Ҫ�ĺ���
#pragma warning disable CS8600 // �� null �����������Ϊ null ��ֵת��Ϊ�� null ���͡�
#pragma warning disable CS8602 // �����ÿ��ܳ��ֿ����á�
#pragma warning disable CS8625 // �޷��� null ������ת��Ϊ�� null ���������͡�
#pragma warning disable CS8605 // ȡ��װ�����Ϊ null ��ֵ��

namespace QuickLogin
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// �ȼ�id��¼
        /// </summary>
        private readonly Dictionary<int, string> hotKey_id = new();

        /// <summary>
        /// �ȼ���Ϣ����
        /// </summary>
        private const int WM_HOTKEY = 0x0312;

        /// <summary>
        /// ���캯�� �� ��ʼ��
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            InitKeyEvent();

            // ��������ѱ���������������ȡ��
            if (File.Exists(Global.FilePath_PWD)) LoadPWD();

            // ����ˢ�°�ť�¼�
            btn_refresh.Click += (_, _) =>
            {
                DrawPWDTree();
            };

            // ����ʱ����
            DrawPWDTree();
        }

        /// <summary>
        /// ��ʼ�������¼�
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
        /// ����������
        /// </summary>
        private void DrawPWDTree()
        {
            TreeView tree = pwdtree;
            TreeNode root = tree.Nodes[0];
            root.Nodes.Clear();
            // ȡ��ע��ȫ���ȼ�
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
                // ע������ȼ�
                RegisterHotKey(item, index);
                ++index;
            }
            root.ExpandAll();
        }

        /// <summary>
        /// ע���ȼ�
        /// </summary>
        /// <param name="name">�ȼ�����</param>
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
        /// ������Ϣ������
        /// </summary>
        /// <param name="m">��Ϣ</param>
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
        /// �����ѱ��������
        /// </summary>
        private static void LoadPWD()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Global.FilePath_PWD, FileMode.Open, FileAccess.Read, FileShare.None);
            Global.pwds = (Dictionary<string, Global.Pair>)formatter.Deserialize(stream);
            stream.Close();
        }

        /// <summary>
        /// ���������
        /// </summary>
        private static void SavePWD()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Global.FilePath_PWD, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, Global.pwds);
            stream.Close();
        }

        /// <summary>
        /// �ر�ʱ���������
        /// </summary>
        /// <param name="e">�ر��¼�</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            foreach (int id in hotKey_id.Keys)
            {
                HotKey.UnregisterHotKey(Handle, id);
            }
            SavePWD();
        }

        /// <summary>
        /// �˳���ť�¼�
        /// </summary>
        /// <param name="sender">�˳���ť</param>
        /// <param name="e">�¼�����</param>
        private void Btn_exit_Click(object sender, EventArgs e) => Close();

        /// <summary>
        /// ��������밴ť�¼�
        /// </summary>
        /// <param name="sender">��������밴ť</param>
        /// <param name="e">��ť�¼�</param>
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

#pragma warning restore CS8605 // ȡ��װ�����Ϊ null ��ֵ��
#pragma warning restore CS8625 // �޷��� null ������ת��Ϊ�� null ���������͡�
#pragma warning restore CS8602 // �����ÿ��ܳ��ֿ����á�
#pragma warning restore CS8600 // �� null �����������Ϊ null ��ֵת��Ϊ�� null ���͡�
#pragma warning restore IDE0079 // ��ɾ������Ҫ�ĺ���
