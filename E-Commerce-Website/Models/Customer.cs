﻿using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Website.Models
{
    public class Customer
    {
        [Key]
        public int Customer_Id { get; set; }
        public string Customer_Name { get; set; }
        public string? Customer_Phone { get; set; }
        public string Customer_Email { get; set; }
        public string Customer_Password { get; set; }
        public string? Customer_Gender { get; set; }
        public string? Customer_Country { get; set; }
        public string? Customer_City { get; set; }
        public string? Customer_Address { get; set; }
        public string? Customer_Image { get; set; }
    }
}
