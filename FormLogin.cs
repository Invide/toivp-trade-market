namespace TOIVP
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isFound = false;
            Form1 form1 = this.Owner as Form1;
            using StreamReader sr = new StreamReader("accounts.csv");
            String line;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                string[] a = line.Split(',');
                if (a[1] == textBox1.Text && a[2] == textBox2.Text)
                {
                    form1.accountId = Convert.ToInt32(a[0]);
                    form1.accountName = a[1];
                    form1.accountCurrency = a[3];
                    form1.SuccessfullLogin();
                    this.Close();

                    isFound = true;
                }
                if (a[1] == textBox1.Text && a[2] != textBox2.Text)
                {
                    MessageBox.Show("Неправильный пароль");
                    isFound = true;
                }
            }
            if (!isFound){
                MessageBox.Show("Аккаунт с таким именем не найден");
            }
            sr.Close();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
