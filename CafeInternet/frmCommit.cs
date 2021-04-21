using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeInternet
{
    public partial class frmCommit : Form
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();
        public frmCommit()
        {
            InitializeComponent();
        }
        private void DisplayReport()
        {
            
            var re = from f in dc.reports
                       select new
                       {
                           Id = f.entity_id,
                           Day = f.date.ToShortDateString(),
                           Time = f.time.ToLongTimeString(),
                           Content = f.information,
                           Performer = f.performer,
                           Activity = f.activity,
                           Category = f.type == 1 ? "Food" : f.type == 2 ? "Computer" : "Area",
                       };
            //hiển thị lên lưới
            dgvCommit.DataSource = re;

            //DisplayFoodDetail();
        }
        private void frmCommit_Load(object sender, EventArgs e)
        {
            DisplayReport();
        }
    }
}
