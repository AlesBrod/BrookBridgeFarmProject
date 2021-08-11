using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class EditAndDeleteModel
    {

        public int Id { get; set; }
        
        public string Name { get; set; }

        
        public string Description { get; set; }
        
        public string Code { get; set; }
        public int Price { get; set; }
                
        public int Qty { get; set; }
        
        public string Veg { get; set; }
        public int QtyToBuy { get; set; }
        public int Summary { get; set; }
        public int SumTotal { get; set; }


    }
}
