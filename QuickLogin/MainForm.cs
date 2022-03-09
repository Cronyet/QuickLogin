using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#pragma warning disable CS8600 // �� null �����������Ϊ null ��ֵת��Ϊ�� null ���͡�
#pragma warning disable CS8602 // �����ÿ��ܳ��ֿ����á�

namespace QuickLogin
{
    public partial class MainForm : Form
    {
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
        protected override void OnClosing(CancelEventArgs e) => SavePWD();

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
            draw:  DrawPWDTree();
        }
    }
}

#pragma warning restore CS8602 // �����ÿ��ܳ��ֿ����á�
#pragma warning restore CS8600 // �� null �����������Ϊ null ��ֵת��Ϊ�� null ���͡�
