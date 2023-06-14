namespace TOIVP
{
    public partial class FormProfile : Form
    {
        public FormProfile()
        {
            InitializeComponent();
        }

        private void FormProfile_Load(object sender, EventArgs e)
        {
            Form1 form1 = this.Owner as Form1;
            string accountName = form1.accountName;
            label2.Text = label2.Text + " " + accountName;
            string accountCurrency = form1.accountCurrency;
            label3.Text = label3.Text + " " + accountCurrency;

            using StreamReader sr = new StreamReader("operations.csv");
            String line;
            while (!sr.EndOfStream){
                line = sr.ReadLine();
                string[] a = line.Split(',');
                if (a[1] == accountName || a[2] == accountName)
                {
                    dataGridView1.Rows.Add(a);
                }
            }
        }
    }
}
