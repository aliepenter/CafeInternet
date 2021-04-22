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
    public partial class frmViewer : Form
    {
        public int check;
        DataClasses1DataContext dc = new DataClasses1DataContext();
        public frmViewer()
        {
            InitializeComponent();
        }
        private void frmViewer_Load(object sender, EventArgs e)
        {
            //if (check == 1)
            //{
            //    crpFood c = new crpFood();
            //    var food = dc.foods.Select(f => new
            //    {
            //        Entity_id = f.entity_id,
            //        Name = f.name,
            //        Price = f.price,
            //        Quantity = f.quantity,
            //        Food_type_id = f.food_type_id == 1 ? "Foods" : f.food_type_id == 2 ? "Bottled drinks" : "Concoction drinks"
            //    }).ToList();
            //    c.SetDataSource(food);
            //    this.crpView.ReportSource = c;
            //}
            //else if (check == 2)
            //{
            //    crpComputer c = new crpComputer();
            //    var com = dc.computers.Select(f => new
            //    {
            //        Entity_id = f.entity_id,
            //        Name = f.name,
            //        Status = f.status ==1 ?"Good" :"Break",
            //        Tolal_used_time = f.total_used_time,
            //        Profit = f.profit,
            //        Area_Id  = f.area_id
            //    }).ToList();
            //    c.SetDataSource(com);
            //    this.crpView.ReportSource = c;
            //}
            //else
            //{
            //    crpArea c = new crpArea();
            //    var ar = dc.areas.Select(f => new
            //    {
            //        Entity_id = f.entity_id,
            //        Name = f.name,
            //        Price = f.price
            //    }).ToList();
            //    c.SetDataSource(ar);
            //    this.crpView.ReportSource = c;
            //}
        }
    }
}
