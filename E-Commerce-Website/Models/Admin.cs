using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Website.Models
{
    public class Admin
    {
        [Key]
        public int Admin_Id { get; set; }
        public string Admin_Name { get; set; }
        public string Admin_Email { get; set; }
        public string Admin_Password { get; set; }
        public string Admin_Image { get; set; }
    }
}