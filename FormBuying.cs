namespace TOIVP
{
    public partial class FormBuying : Form
    {
        int lid;
        int stnum;
        int maxquantity;
        int quantity;
        string lcurrency;
        decimal lprice;
        string larticle;
        string lseller;
        public FormBuying()
        {
            InitializeComponent();
        }

        private void FormBuying_Load(object sender, EventArgs e)
        {

        }
        public void ChangeForm(int id, string article, string seller, string currency, decimal price, int maxq)
        {
            lid = id;
            larticle = article;
            label2.Text += article;
            lseller = seller;
            lcurrency = currency;
            label3.Text += seller;
            lprice = price;
            label6.Text += price;
            maxquantity = maxq;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (Convert.ToInt32(textBox1.Text) > maxquantity)
                {
                    quantity = maxquantity;
                    textBox1.Text = Convert.ToString(quantity);
                }
                else
                {
                    quantity = Convert.ToInt32(textBox1.Text);
                    label4.Text = "Итоговая стоимость: " + Convert.ToString(quantity * lprice);
                }
                
            }
            if (textBox1.Text == "")
            {
                quantity = 0;
                label4.Text = "Итоговая стоимость: 0";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = this.Owner as Form1;
            for (int i = 0; i < form1.lots.Count; i++)
            {    
                if (form1.lots[i].id == lid)
                {
                    stnum = i;
                    if (form1.lots[i].quantity - quantity == 0)
                    {
                        form1.lots.RemoveAt(i);
                    }
                    else
                    {
                        form1.lots[i].quantity -= quantity;
                    }
                }
            }
            using (var sw = new StreamWriter("market.csv"))
            {
                foreach (Lot lot in form1.lots)
                {
                    string str = Convert.ToString(lot.id) + ',' + lot.article + ',' + lot.seller + ',' + Convert.ToString(lot.price) + ',' + lot.currency + ',' + Convert.ToString(lot.quantity);
                    sw.WriteLine(str);
                }
                
            }
            DateTime dateTime = DateTime.Now;
            using (var sw = new StreamWriter("operations.csv", append: true))
            {
                sw.WriteLine("Покупка," + form1.accountName + "," + lseller + "," + Convert.ToString(lprice) + "," + lcurrency + "," + Convert.ToString(quantity) + "," + Convert.ToString(dateTime.ToLocalTime()).Split(' ')[0] + "," + Convert.ToString(dateTime.ToLocalTime()).Split(' ')[1]);
            }
            MessageBox.Show("Подробности в отчете", "Лот успешно куплен");
            Dictionary<string, string> reportSubs = new Dictionary<string, string>();
            reportSubs.Add("[ARTICLE]", larticle);
            reportSubs.Add("[SELLER]", lseller);
            reportSubs.Add("[BUYER]", form1.accountName);
            reportSubs.Add("[PRICE]", Convert.ToString(lprice));
            reportSubs.Add("[CURRENCY]", form1.accountCurrency);
            reportSubs.Add("[AMOUNT]", Convert.ToString(quantity));
            reportSubs.Add("[DATE]", Convert.ToString(dateTime.ToLocalTime()).Split(' ')[0]);
            reportSubs.Add("[TIME]", Convert.ToString(dateTime.ToLocalTime()).Split(' ')[1]);
            StreamReader sr = new StreamReader("report_orig.txt");
            StreamWriter sp = new StreamWriter("report.txt", false);
            String line = "";
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                foreach (KeyValuePair<string, string> v in reportSubs)
                {
                    line = line.Replace(v.Key, v.Value);
                }
                sp.WriteLine(line);
            }
            sr.Close();
            sp.Close();

            form1.ReadMarketData();
            button1.Enabled = false;
        }
    }
}
