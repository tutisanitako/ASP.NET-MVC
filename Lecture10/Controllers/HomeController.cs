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

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        // ✅ MUST BE STATIC to persist
        private static List<Customer> customers = new List<Customer>
        {
            new Customer { Id = 1, Name = "Taylor Otwell", Code = "001", Address = "123 Laravel St", Phone = "555-0001", IsAdmin = true, Has2FA = false, ImagePath = null },
            new Customer { Id = 2, Name = "David Hemphill", Code = "002", Address = "456 Vue Ave", Phone = "555-0002", IsAdmin = true, Has2FA = true, ImagePath = null },
            new Customer { Id = 3, Name = "Mohamed Said", Code = "003", Address = "789 PHP Blvd", Phone = "555-0003", IsAdmin = false, Has2FA = true, ImagePath = null },
            new Customer { Id = 4, Name = "Ian Landsman", Code = "004", Address = "321 Code Rd", Phone = "555-0004", IsAdmin = false, Has2FA = true, ImagePath = null },
            new Customer { Id = 5, Name = "Dries Vints", Code = "005", Address = "654 Dev Lane", Phone = "555-0005", IsAdmin = false, Has2FA = true, ImagePath = null },
            new Customer { Id = 6, Name = "Jess Archer", Code = "006", Address = "987 Test Dr", Phone = "555-0006", IsAdmin = false, Has2FA = true, ImagePath = null },
            new Customer { Id = 7, Name = "Mior Zaki", Code = "007", Address = "147 Web Way", Phone = "555-0007", IsAdmin = true, Has2FA = true, ImagePath = null }
        };

        public IActionResult Index()
        {
            ViewBag.Title = "Customer Management System";
            return View(customers);
        }

        [HttpPost]
        public IActionResult Create(string Name, string Code, string Address, string Phone, IFormFile ImageFile)
        {
            try
            {
                // Create new customer
                var customer = new Customer
                {
                    Id = customers.Any() ? customers.Max(c => c.Id) + 1 : 1,
                    Name = Name,
                    Code = Code,
                    Address = Address,
                    Phone = Phone,
                    IsAdmin = true,
                    Has2FA = true,
                    ImagePath = null
                };

                // Handle image upload if provided
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    customer.ImagePath = SaveUploadedImage(ImageFile);
                }

                // Add to list
                customers.Add(customer);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log error
                ViewBag.Error = ex.Message;
                ViewBag.Title = "Customer Management System";
                return View("Index", customers);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                if (!string.IsNullOrEmpty(customer.ImagePath))
                {
                    DeleteImageFile(customer.ImagePath);
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
        public IActionResult Update(int Id, string Name, string Code, string Address, string Phone, IFormFile ImageFile)
        {
            try
            {
                var existingCustomer = customers.FirstOrDefault(c => c.Id == Id);
                if (existingCustomer != null)
                {
                    existingCustomer.Name = Name;
                    existingCustomer.Code = Code;
                    existingCustomer.Address = Address;
                    existingCustomer.Phone = Phone;

                    // Handle image upload
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(existingCustomer.ImagePath))
                        {
                            DeleteImageFile(existingCustomer.ImagePath);
                        }
                        existingCustomer.ImagePath = SaveUploadedImage(ImageFile);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Title = "Edit Customer";
                var customer = customers.FirstOrDefault(c => c.Id == Id);
                return View("Edit", customer);
            }
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

        private string SaveUploadedImage(IFormFile imageFile)
        {
            try
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }

                return "/images/products/" + uniqueFileName;
            }
            catch
            {
                return null;
            }
        }

        private void DeleteImageFile(string imagePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(imagePath))
                {
                    string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath.TrimStart('/'));
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
            }
            catch
            {
                // Ignore errors
            }
        }
    }
}