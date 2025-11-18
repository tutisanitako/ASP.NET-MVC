using Microsoft.AspNetCore.Mvc;
using Lecture10.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;

namespace Lecture10.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        // Inject IWebHostEnvironment for file handling
        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        private static List<Customer> customers = new List<Customer>
        {
            new Customer { Id = 1, Name = "Taylor Otwell", Code = "001", Address = "123 Laravel St", Phone = "555-0001", IsAdmin = true, Has2FA = false, ImagePath = "/images/products/img1.jpg" },
            new Customer { Id = 2, Name = "David Hemphill", Code = "002", Address = "456 Vue Ave", Phone = "555-0002", IsAdmin = true, Has2FA = true, ImagePath = "/images/products/img2.jpg" },
            new Customer { Id = 3, Name = "Mohamed Said", Code = "003", Address = "789 PHP Blvd", Phone = "555-0003", IsAdmin = false, Has2FA = true, ImagePath = "/images/products/img3.jpg" },
            new Customer { Id = 4, Name = "Ian Landsman", Code = "004", Address = "321 Code Rd", Phone = "555-0004", IsAdmin = false, Has2FA = true, ImagePath = "/images/products/img4.jpg" },
            new Customer { Id = 5, Name = "Dries Vints", Code = "005", Address = "654 Dev Lane", Phone = "555-0005", IsAdmin = false, Has2FA = true, ImagePath = "/images/products/img5.jpg" },
            new Customer { Id = 6, Name = "Jess Archer", Code = "006", Address = "987 Test Dr", Phone = "555-0006", IsAdmin = false, Has2FA = true, ImagePath = "/images/products/img6.jpg" },
            new Customer { Id = 7, Name = "Mior Zaki", Code = "007", Address = "147 Web Way", Phone = "555-0007", IsAdmin = true, Has2FA = true, ImagePath = "/images/products/img7.jpg" }
        };

        public IActionResult Index()
        {
            ViewBag.Title = "Customer Management System";
            return View(customers);
        }

        // NEW: Helper method to upload image
        private string UploadImage(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");

                // Create directory if it doesn't exist
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generate unique filename
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }

                return "/images/products/" + uniqueFileName;
            }
            return null;
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Id = customers.Any() ? customers.Max(c => c.Id) + 1 : 1;
                customer.IsAdmin = true;
                customer.Has2FA = true;

                // NEW: Handle image upload
                if (customer.ImageFile != null)
                {
                    customer.ImagePath = UploadImage(customer.ImageFile);
                }
                else
                {
                    customer.ImagePath = "/images/products/default.jpg";
                }

                customers.Add(customer);
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Customer Management System";
            return View("Index", customers);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                // Optional: Delete image file from server
                if (!string.IsNullOrEmpty(customer.ImagePath) && customer.ImagePath != "/images/products/default.jpg")
                {
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, customer.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                customers.Remove(customer);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Edit Customer";
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var existing = customers.FirstOrDefault(c => c.Id == customer.Id);
                if (existing != null)
                {
                    existing.Name = customer.Name;
                    existing.Code = customer.Code;
                    existing.Address = customer.Address;
                    existing.Phone = customer.Phone;

                    // Only update image if a new one was uploaded
                    if (customer.ImageFile != null && customer.ImageFile.Length > 0)
                    {
                        // Optional: Delete old image
                        if (!string.IsNullOrEmpty(existing.ImagePath) && existing.ImagePath != "default.jpg")
                        {
                            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", existing.ImagePath);
                            if (System.IO.File.Exists(oldPath))
                                System.IO.File.Delete(oldPath);
                        }

                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(customer.ImageFile.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await customer.ImageFile.CopyToAsync(stream);
                        }

                        existing.ImagePath = fileName;
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Edit Customer";
            return View("Edit", customer);
        }

        [HttpPost]
        public IActionResult ToggleAdmin(int id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                customer.IsAdmin = !customer.IsAdmin;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Toggle2FA(int id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                customer.Has2FA = !customer.Has2FA;
            }
            return RedirectToAction("Index");
        }
    }
}