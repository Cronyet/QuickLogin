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
        }

        /// <summary>
        /// ����������
        /// </summary>
        private void DrawPWDTree()
        {

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
        /// �ر�ʱ���������
        /// </summary>
        /// <param name="e">�ر��¼�</param>
        protected override void OnClosing(CancelEventArgs e) => SavePWD();

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
        /// �˳���ť�¼�
        /// </summary>
        /// <param name="sender">�˳���ť</param>
        /// <param name="e">�¼�����</param>
        private void Btn_exit_Click(object sender, EventArgs e) => Close();
    }
}