using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models
{
    public class ShoppingListModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Product Name ")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Product Description ")]
        public string Description { get; set; }
        [Required]
        [DisplayName("Identification Code ")]
        public string Code { get; set; }
        [Range(1, 999)]
        [DisplayName("Price (each) ")]
        public int Price { get; set; }
        [Range(1, 999)]
        [DisplayName("Available Quantity ")]
        public int Qty { get; set; }
        [Required]
        [DisplayName("Suitable for Vegeterians? ")]
        public string Veg { get; set; }
        [Range(1, 999)]
        [DisplayName("Required Quantity ")]
        public int QtyToBuy { get; set; }
        [DisplayName("Total Price For Product")]
        public int Summary { get; set; }
        [DisplayName("Total Price")]
        public int SumTotal { get; set; }
    }
}