using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Website.Models
{
    public class Product
    {
        [Key]
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_Price { get; set; }
        public string Product_Description { get; set; }
        public string Product_Image { get; set; }
        public int Cat_Id { get; set; }
        public Category category { get; set; }
    }
}
