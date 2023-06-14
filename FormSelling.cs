namespace TOIVP
{
    public partial class FormSelling : Form
    {
        string productArticle = "";
        int productPrice = 0;
        int productAmount = 0;

        public FormSelling()
        {
            InitializeComponent();

        }

        private void FormSelling_Load(object sender, EventArgs e)
        {
            label4.Text = "Итого: " + Convert.ToString(productPrice * productAmount);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            productArticle = textBox1.Text;
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                productPrice = Convert.ToInt32(textBox2.Text);
            }
            label4.Text = "Итого: " + Convert.ToString(productPrice * productAmount);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                productAmount = Convert.ToInt32(textBox3.Text);
                label4.Text = "Итого: " + Convert.ToString(productPrice * productAmount);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = this.Owner as Form1;
            int id = form1.lots.Count;
            List<int> ids = new List<int>();
            for (int i = 0; i < form1.lots.Count; i++)
            {
                ids.Add(form1.lots[i].id);
                ids.Sort();
            }
            for (int i = 0; i < form1.lots.Count; i++)
            {
                if (ids[i] != i)
                {
                    id = i;
                    break;
                }
            }
            form1.CreateLot(id, productArticle, form1.accountName, productPrice, form1.accountCurrency, productAmount);
            string str = Convert.ToString(id) + ',' + productArticle + ',' + form1.accountName + ',' + Convert.ToString(productPrice) + ',' + form1.accountCurrency + ',' + Convert.ToString(productAmount);
            using (var sw = new StreamWriter("market.csv", append: true))
            {
                sw.WriteLine(str);
            }
            using (var sw = new StreamWriter("operations.csv", append: true))
            {
                DateTime dateTime = DateTime.Now;
                sw.WriteLine("Выставление," + form1.accountName+",," + Convert.ToString(productPrice) + "," + Convert.ToString(form1.accountCurrency) + "," + Convert.ToString(productAmount) + "," + Convert.ToString(dateTime.ToLocalTime()).Split(' ')[0] + "," + Convert.ToString(dateTime.ToLocalTime()).Split(' ')[1]);
            }
            MessageBox.Show("Лот выставлен на продажу");
            form1.ReadMarketData();
            button1.Enabled = false;
        }
    }
}
