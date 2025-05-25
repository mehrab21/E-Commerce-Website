using E_Commerce_Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Website.Controllers
{
    public class AdminController : Controller
    {
       
        private readonly myContext _context;
        private readonly IWebHostEnvironment _env;
        public AdminController(myContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            string check = HttpContext.Session.GetString("admin_session");
            if(check != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login");
            }
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string adminEmail, string adminPassword)
        {
            var row = _context.tbl_Admin.FirstOrDefault(a => a.Admin_Email == adminEmail);
            if(row != null && row.Admin_Password == adminPassword)
            {
                HttpContext.Session.SetString("admin_session", row.Admin_Id.ToString());
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Invalid Email or Password";
                return View();
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("admin_session");
            return RedirectToAction("Login");
        }
        public IActionResult Profile()
        {
           var adminid= HttpContext.Session.GetString("admin_session");
            if (string.IsNullOrEmpty(adminid))
            {
                // Handle the missing session (e.g., redirect to login)
                return RedirectToAction("Login", "Admin");
            }
            var row = _context.tbl_Admin.Where(a => a.Admin_Id == int.Parse(adminid)).ToList();

            return View(row);
        }
        [HttpPost]
        public IActionResult profile(Admin admin)
        {
            _context.tbl_Admin.Update(admin);
            _context.SaveChanges();
            ViewBag.Message = "Successful";
            return RedirectToAction("Profile");
        }
        public IActionResult ChangeProfileImage( IFormFile admin_image, Admin admin)
        {
            string image = Path.Combine(_env.WebRootPath, "admin_image", admin_image.FileName);
            FileStream fs = new FileStream(image, FileMode.Create);
            admin_image.CopyTo(fs);
            admin.Admin_Image = admin_image.FileName;
            _context.tbl_Admin.Update(admin);
            _context.SaveChanges();
            return RedirectToAction("Profile");
        }

        public IActionResult fetchCustomer()
        {
            return View(_context.tbl_Customer.ToList());
        }

        public IActionResult CustomerDetails(int id)
        {
            return View(_context.tbl_Customer.FirstOrDefault(a => a.Customer_Id==id));
        }

        public IActionResult CustomerEdit(int id)
        {
            return View(_context.tbl_Customer.Find(id));
        }
        [HttpPost]
        public IActionResult CustomerEdit(Customer customer, IFormFile Customer_Image)
        {
            string image = Path.Combine(_env.WebRootPath, "Customer_images", Customer_Image.FileName);
            FileStream fs = new FileStream(image, FileMode.Create);
            Customer_Image.CopyTo(fs);
            customer.Customer_Image = Customer_Image.FileName;
            _context.tbl_Customer.Update(customer);
            _context.SaveChanges();
            return RedirectToAction("fetchCustomer");
        }
        public IActionResult DeleteParmission(int id)
        {
            return View(_context.tbl_Customer.FirstOrDefault(a => a.Customer_Id == id));
        }
        public IActionResult CustomerDelete(int id)
        {
            var row = _context.tbl_Customer.FirstOrDefault(a => a.Customer_Id == id);
            _context.tbl_Customer.Remove(row);
            _context.SaveChanges();
            return RedirectToAction("fetchCustomer");
        }
        
        public IActionResult fetchCategory()
        {
            return View(_context.tbl_Category.ToList());
        }
        public IActionResult addcategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult addcategory(Category category)
        {
            _context.tbl_Category.Add(category);
            _context.SaveChanges();
            return RedirectToAction("fetchCategory");
        }
        public IActionResult UpdateCategory(int id)
        {
            return View(_context.tbl_Category.Find(id));
        }
        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            _context.tbl_Category.Update(category);
            _context.SaveChanges();
            return RedirectToAction("fetchCategory");
        }
        public IActionResult DeleteParmissionCategory(int id)
        {
            return View(_context.tbl_Category.FirstOrDefault(a => a.Category_Id == id));
        }
        public IActionResult DeleteCategory(int id)
        {
            var row = _context.tbl_Category.Find(id);
            _context.tbl_Category.Remove(row);
            _context.SaveChanges();
            return RedirectToAction("fetchCategory");
        }

        public IActionResult fetchProduct()
        {
            return View(_context.tbl_Product.ToList());
        }
        public IActionResult addProduct()
        {
            List<Category> categories = _context.tbl_Category.ToList();
            ViewData["category"]= categories; 
            return View();
        }
        [HttpPost]
        public IActionResult addProduct(Product product, IFormFile Product_Image) 
        {
            
            string image = Path.GetFileName(Product_Image.FileName);
            string imagepath = Path.Combine(_env.WebRootPath,"Product_Images",image);
            FileStream fs = new FileStream(imagepath,FileMode.Create);
            Product_Image.CopyTo(fs);
            product.Product_Image = image;
            _context.tbl_Product.Add(product);
            _context.SaveChanges();
            return RedirectToAction("fetchProduct");
        }
        public IActionResult ProductDetails(int id)
        {
            return View(_context.tbl_Product.Include(p => p.category).FirstOrDefault(a => a.Product_Id==id));
        }
        public IActionResult DeleteParmissionProduct(int id)
        {
            return View(_context.tbl_Product.FirstOrDefault(p => p.Product_Id ==id));
        }
        public IActionResult deleteProduct(int id)
        {
            var row = _context.tbl_Product.Find(id);
            _context.tbl_Product.Remove(row);
            _context.SaveChanges();
            return RedirectToAction("fetchProduct");
        }
        public IActionResult UpdateProduct(int id)
        {
            List<Category> categories = _context.tbl_Category.ToList();
            ViewData["category"] = categories;
            var product = _context.tbl_Product.Find(id);
            ViewBag.selectedCategoryid = product.Cat_Id;
            return View(product);
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
           
            _context.tbl_Product.Update(product);
            _context.SaveChanges();
            return RedirectToAction("fetchProduct");
        }
        public IActionResult ChangeProductImage(IFormFile Product_Image, Product product)
        {
            string image = Path.Combine(_env.WebRootPath, "Product_Images", Product_Image.FileName);
            FileStream fs = new FileStream(image, FileMode.Create);
            Product_Image.CopyTo(fs);
            product.Product_Image = Product_Image.FileName;
            _context.tbl_Product.Update(product);
            _context.SaveChanges();
            return RedirectToAction("fetchProduct");
        }
        public IActionResult feedback()
        {
           
            return View(_context.tbl_Feedbacks.ToList());
        }
        public IActionResult DeleteParmissionFeed(int id)
        {
            return View(_context.tbl_Feedbacks.FirstOrDefault(f => f.Feedback_Id == id));
        }
        public IActionResult DeleteFeedback(int id)
        {
            var row = _context.tbl_Feedbacks.Find(id);
            _context.tbl_Feedbacks.Remove(row);
            _context.SaveChanges();
            return RedirectToAction("feedback");
        }
        public IActionResult fetchCart()
        {
           var cart = _context.tbl_Cart.Include(c => c.products).Include(c => c.customers).ToList();
            return View(cart);
        }
        public IActionResult CartDetails(int id)
        {
            return View(_context.tbl_Cart.FirstOrDefault(c => c.Cart_Id == id));
        }
        public IActionResult DeleteParmissionCart(int id)
        {
            return View(_context.tbl_Cart.FirstOrDefault(c => c.Cart_Id == id));
        }
        public IActionResult DeleteCart(int id)
        {
            var row = _context.tbl_Cart.Find(id);
            _context.tbl_Cart.Remove(row);
            _context.SaveChanges();
            return RedirectToAction("fetchCart");
        }
        public IActionResult CartEdit(int id)
        {
            return View(_context.tbl_Cart.Find(id));
        }
        [HttpPost]
        public IActionResult CartEdit(int Cart_Status, Cart cart)
        {
            cart.Cart_Status = Cart_Status;
            _context.tbl_Cart.Update(cart);
            _context.SaveChanges();
            return RedirectToAction("fetchCart");
        }



    }

}
 