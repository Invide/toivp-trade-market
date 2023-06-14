using System.Data;
using System.Text;

namespace TOIVP
{
    public partial class FormExR : Form
    {
        public FormExR()
        {
            InitializeComponent();
            ExRRefresh();
        }

        private void ExRRefresh()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string url = "http://www.cbr.ru/scripts/XML_daily.asp";
            DataSet ds = new DataSet();
            ds.ReadXml(url);
            DataTable currency = ds.Tables["Valute"];
            foreach (DataRow row in currency.Rows)
            {
                if (row["CharCode"].ToString() == "USD")
                {
                    double usdtorub = Convert.ToDouble(row["Value"]);
                    labelUsdToRub.Text = "1 $ (USD) = " + usdtorub.ToString() + " ₽";
                }
                if (row["CharCode"].ToString() == "EUR")
                {
                    double eurtorub = Convert.ToDouble(row["Value"]);
                    labelEurToRub.Text = "1 € (EUR) = " + eurtorub.ToString() + " ₽";
                }
                if (row["CharCode"].ToString() == "TRY")
                {
                    double trytorub = Convert.ToDouble(row["Value"]);
                    labelTryToRub.Text = "1 ₺ (TRY) = " + trytorub.ToString() + " ₽";
                }
                if (row["CharCode"].ToString() == "JPY")
                {
                    double jpytorub = Convert.ToDouble(row["Value"]);
                    labelJpyToRub.Text = "1 ¥ (JPY) = " + jpytorub.ToString() + " ₽";
                }
                if (row["CharCode"].ToString() == "UAH")
                {
                    double uahtorub = Convert.ToDouble(row["Value"]);
                    labelUahToRub.Text = "1 ₴ (UAH) = " + uahtorub.ToString() + " ₽";
                }
                if (row["CharCode"].ToString() == "INR")
                {
                    double inrtorub = Convert.ToDouble(row["Value"]);
                    labelInrToRub.Text = "1 ₹ (INR) = " + inrtorub.ToString() + " ₽";
                }
                if (row["CharCode"].ToString() == "CNY")
                {
                    double cnytorub = Convert.ToDouble(row["Value"]);
                    labelCnyToRub.Text = "1 ¥ (CNY) = " + cnytorub.ToString() + " ₽";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExRRefresh();
        }
    }
}
