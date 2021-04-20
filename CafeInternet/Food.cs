using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeInternet
{
    class Food
    {
        public string name { get; set; }
        public float price { get; set; }
        public int quantity { get; set; }
        public string image { get; set; }
        public DateTime receipt_date { get; set; }
        public int food_type_id { get; set; }
    }
}
