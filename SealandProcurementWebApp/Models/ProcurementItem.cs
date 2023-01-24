using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SealandProcurementWebApp.Models
{
    public class ProcurementItem
    {
        public int Id { get; set; }
        public string CompName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string HireTime { get; set; }
        public string Source { get; set; }
        public string Date { get; set; }

        public ProcurementItem()
        {

        }
    }
}
