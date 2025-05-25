using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Website.Models
{
    public class Cart
    {
        [Key]
        public int Cart_Id { get; set; }
        public int Prod_Id { get; set; }
        public int Cust_Id { get; set; }
        public int Product_Quantity { get; set; }
        public int Cart_Status { get; set; }
        [ForeignKey("Prod_Id")]
        public Product products { get; set; }
        [ForeignKey("Cust_Id")]
        public Customer customers { get; set; }
    }
}
