namespace QuickLogin
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            

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