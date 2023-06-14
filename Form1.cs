namespace TOIVP
{
    public partial class Form1 : Form
    {
        int secSinceLastUpdate = 0;
        public int accountId = -1;
        public string accountName = "";
        public string accountCurrency = "";
        public List<Lot> lots = new List<Lot>();
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(Timer1_Tick);
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AccShowToolStripMenuItem.Enabled = false;
            ReadMarketData();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            secSinceLastUpdate += 1;
            if (secSinceLastUpdate == 10)
            {
                secSinceLastUpdate = 0;
                ReadMarketData();
            }
            labelTitle.Text = "����� ��������� " + secSinceLastUpdate + " ���. �����";
        }

        private void курсыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormExR fExR = new FormExR();
            fExR.ShowDialog();
            toolStripStatusLabel1.Text = "����������� ����� �����";
        }

        private void AccLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Owner = this;
            formLogin.ShowDialog();
        }

        private void AccShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProfile formProfile = new FormProfile(); 
            formProfile.Owner = this;
            formProfile.ShowDialog();
            toolStripStatusLabel1.Text = "���������� �������";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormSelling formSelling = new FormSelling();
            formSelling.Owner = this;
            formSelling.ShowDialog();
            toolStripStatusLabel1.Text = "����������� ������ �� �������";
        }

        public void ReadMarketData()
        {
            dataGridView1.Rows.Clear();
            using StreamReader sr = new StreamReader("market.csv");
            String line;
            lots = new List<Lot>();
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                string[] a = line.Split(',');
                CreateLot(Convert.ToInt32(a[0]),a[1],a[2],Convert.ToInt32(a[3]),a[4],Convert.ToInt32(a[5]));
                dataGridView1.Rows.Add(a[1], a[2], a[3], a[4], a[5], "������");
            }
            sr.Close();
        }
        public void SuccessfullLogin()
        {
            toolStripStatusLabel1.Text = "�������� ���� � �������";
            AccLoginToolStripMenuItem.Text = "������� �������";
            AccShowToolStripMenuItem.Enabled = true;
            button1.Enabled = true;
        }
        public void CreateLot(int id, string article, string seller, int price, string currency, int quantity)
        {
            lots.Add(new Lot(id, article, seller, price, currency, quantity));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Buy")
            {
                FormBuying formBuying = new FormBuying();
                formBuying.Owner = this;
                formBuying.ChangeForm(lots[e.RowIndex].id, lots[e.RowIndex].article, lots[e.RowIndex].seller, lots[e.RowIndex].currency, lots[e.RowIndex].price, lots[e.RowIndex].quantity);
                formBuying.ShowDialog();
                toolStripStatusLabel1.Text = "������������ ������";
            }
        }
    }

    public class Lot
    {
        public int id;

    public Lot(int id, string article, string seller, int price, string currency, int quantity)
    {
        this.id = id;
        this.article = article;
        this.seller = seller;
        this.price = price;
        this.currency = currency;
        this.quantity = quantity;
    }

    public string article {get; set;}
        public string seller { get; set; }
        public decimal price { get; set; }
        public string currency { get; set; }
        public int quantity { get; set; }
    }
}