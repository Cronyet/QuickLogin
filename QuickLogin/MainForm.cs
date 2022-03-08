using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace QuickLogin
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            string fn = $"{Global.WorkBase}\\pwd.dat";
            if (File.Exists(fn))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(fn, FileMode.Open, FileAccess.Read, FileShare.None);
                Global.pwds = (Dictionary<string, Global.Pair>)formatter.Deserialize(stream);
                stream.Close();
            }

            btn_refresh.Click += (_, _) =>
            {

            };
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}