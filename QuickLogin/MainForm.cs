using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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
            string name = new InputForm("Add new pwd", "Name: ").ShowTopMost(this);
            string pwd = new InputForm("Add new pwd", "Password: ").ShowTopMost(this);
            Global.pwds.Add(name ?? "", new Global.Pair()
            {
                pwd = pwd ?? "",
            });
            DrawPWDTree();
        }
    }
}