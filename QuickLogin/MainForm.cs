using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace QuickLogin
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            if (File.Exists(Global.FilePath_PWD))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(Global.FilePath_PWD, FileMode.Open, FileAccess.Read, FileShare.None);
                Global.pwds = (Dictionary<string, Global.Pair>)formatter.Deserialize(stream);
                stream.Close();
            }

            btn_refresh.Click += (_, _) =>
            {

            };
        }

        /// <summary>
        /// �ر�ʱ���������
        /// </summary>
        /// <param name="e">�ر��¼�</param>
        protected override void OnClosing(CancelEventArgs e) => SavePWD();

        /// <summary>
        /// ���������
        /// </summary>
        private void SavePWD()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Global.FilePath_PWD, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, Global.pwds);
            stream.Close();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {

            Close();
        }
    }
}