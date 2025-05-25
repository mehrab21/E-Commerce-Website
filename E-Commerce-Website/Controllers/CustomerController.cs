using E_Commerce_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Website.Controllers
{
    public class CustomerController : Controller
    {
        private myContext _context;
        private IWebHostEnvironment _env;
        public CustomerController(myContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Category> categories = _context.tbl_Category.ToList();
            ViewData["category"] = categories;
            List<Product> products = _context.tbl_Product.ToList();
            ViewData["product"] = products;
            ViewBag.sessioncheck = HttpContext.Session.GetString("customer_session");
            return View();
        }
        public IActionResult customerLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult customerLogin(string CustomerEmail, string CustomerPassword)
        {
            var row = _context.tbl_Customer.FirstOrDefault(c => c.Customer_Email == CustomerEmail);
            if (row != null && row.Customer_Password == CustomerPassword)
            {
                HttpContext.Session.SetString("customer_session", row.Customer_Id.ToString());
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.Message = "Invalid Email or Password";
                return View();
            }
        }
        public IActionResult CustomerLogout()
        {
            HttpContext.Session.Remove("customer_session");
            return RedirectToAction("index");
        }

        public IActionResult CustomerRegistration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CustomerRegistration(Customer customer)
        {
            _context.tbl_Customer.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("customerLogin");
        }
        public IActionResult CustomerProfile()
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("customer_session")))
            {
                return RedirectToAction("CustomerLogin");
            }
            else
            {
                List<Category> categories = _context.tbl_Category.ToList();
                ViewData["category"] = categories;
                var customerid = HttpContext.Session.GetString("customer_session");
                var row = _context.tbl_Customer.Where(c => c.Customer_Id == int.Parse(customerid)).ToList();
                return View(row);
            }
        }
        public IActionResult UpdateCustomerProfile(Customer customer)
        {
            _context.tbl_Customer.Update(customer);
            _context.SaveChanges();
            return RedirectToAction("CustomerProfile");
        }
        [HttpPost]
        public IActionResult ChangeCustomerProfile(IFormFile Customer_Image, Customer customer)
        {
            string image = Path.Combine(_env.WebRootPath, "Customer_images", Customer_Image.FileName);
            FileStream fs = new FileStream(image, FileMode.Create);
            Customer_Image.CopyTo(fs);
            customer.Customer_Image = Customer_Image.FileName;
            _context.tbl_Customer.Update(customer);
            _context.SaveChanges();
            return RedirectToAction("CustomerProfile");
        }
        public IActionResult Feedback()
        {
            List<Category> categories = _context.tbl_Category.ToList();
            ViewData["category"] = categories;
            return View();
        }
        [HttpPost]
        public IActionResult Feedback(Feedback feedback)
        {
            TempData["viewmessage"] = "thanks for your feedback";
            _context.tbl_Feedbacks.Add(feedback);
            _context.SaveChanges();
            return RedirectToAction("Feedback");
        }
        public IActionResult fetchallproduct()
        {
            List<Category> categories = _context.tbl_Category.ToList();
            ViewData["category"] = categories;
            List<Product> products = _context.tbl_Product.ToList();
            ViewData["product"] = products;
            return View();
        }
        public IActionResult ProductDetails(int id)
        {
            List<Category> categories = _context.tbl_Category.ToList();
            ViewData["category"] = categories;
            var row = _context.tbl_Product.Where(p => p.Product_Id == id).ToList();

            return View(row);
        }
        public IActionResult AddToCart(int Prod_Id, Cart cart)
        {
            string islogin = HttpContext.Session.GetString("customer_session");
            if (islogin == null)
            {
                return RedirectToAction("customerLogin");
            }
            else
            {
                cart.Cust_Id = int.Parse(islogin);
                cart.Prod_Id = Prod_Id;
                cart.Cart_Status = 0;
                cart.Product_Quantity = 1;
                _context.tbl_Cart.Add(cart);
                _context.SaveChanges();
                TempData["cartmessage"] = "successfully Product add To cart";
                return RedirectToAction("fetchallproduct");
            }
        }
        public IActionResult fetchCart()
        {
            List<Category> categories = _context.tbl_Category.ToList();
            ViewData["category"] = categories;
            var cart = _context.tbl_Cart.Include(c => c.products).Include(c => c.customers).ToList();
            return View(cart);
        }
        public IActionResult AboutUs()
        {
            List<Category> categories = _context.tbl_Category.ToList();
            ViewData["category"] = categories;
            return View();
        }
        [HttpGet]
        public IActionResult Search(string q)
        {
            List<Category> categories = _context.tbl_Category.ToList();
            ViewData["category"] = categories;
            var results = _context.tbl_Product
                .Where(p =>
                    p.Product_Name.Contains(q) ||
                    p.Product_Description.Contains(q) ||
                    p.Product_Price.ToString().Contains(q) // Assuming navigation property
                )
                .ToList();

            return View("Search", results);
        }

    }
}