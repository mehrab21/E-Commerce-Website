using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Website.Models
{
    public class Faqs
    {
        [Key]
        public int Faq_Id { get; set; }
        public string Faq_Question { get; set; }
        public string Faq_Answer { get; set; }
    }
}
