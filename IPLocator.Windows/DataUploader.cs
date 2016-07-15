using IPLocator.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPLocator.Windows
{
    public partial class DataUploader : Form
    {
        string ip4Path = string.Empty;
        string cityLocPath = string.Empty;
        public DataUploader()
        {
            InitializeComponent();
        }

        private void btn_uploadInDB_Click(object sender, EventArgs e)
        {
            string consString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
            CSVParser parser = new CSVParser(new string[]{ip4Path,cityLocPath },consString, Convert.ToInt32(ConfigurationManager.AppSettings["BatchSize"]));
            parser.ReadUploadInDB();
            MessageBox.Show("database udpated successfully");
        }

        private void btn_IP4Browse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDg.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                ip4Path = openFileDg.FileName;
                txtIP4.Text = ip4Path;                
            }
        }

        private void btn_CityBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDg.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                cityLocPath = openFileDg.FileName;
                txtCityLocation.Text = cityLocPath;
            }
        }
    }
}
