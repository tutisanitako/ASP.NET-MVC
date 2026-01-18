using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Check if already seeded
            if (await context.Users.AnyAsync() || await context.Products.AnyAsync())
                return;

            // Seed Admin User
            var adminUser = new User
            {
                Email = "admin@shophub.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                FullName = "Admin User",
                IsAdmin = true,
                CreatedAt = DateTime.Now.AddMonths(-6)
            };
            context.Users.Add(adminUser);

            // Seed Regular Users
            var users = new List<User>
            {
                new User
                {
                    Email = "john.doe@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"),
                    FullName = "John Doe",
                    PhoneNumber = "+1234567890",
                    IsAdmin = false,
                    CreatedAt = DateTime.Now.AddMonths(-3)
                },
                new User
                {
                    Email = "jane.smith@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"),
                    FullName = "Jane Smith",
                    PhoneNumber = "+1234567891",
                    IsAdmin = false,
                    CreatedAt = DateTime.Now.AddMonths(-2)
                },
                new User
                {
                    Email = "mike.johnson@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"),
                    FullName = "Mike Johnson",
                    PhoneNumber = "+1234567892",
                    IsAdmin = false,
                    CreatedAt = DateTime.Now.AddMonths(-4)
                },
                new User
                {
                    Email = "sarah.williams@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"),
                    FullName = "Sarah Williams",
                    PhoneNumber = "+1234567893",
                    IsAdmin = false,
                    CreatedAt = DateTime.Now.AddMonths(-1)
                },
                new User
                {
                    Email = "david.brown@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"),
                    FullName = "David Brown",
                    PhoneNumber = "+1234567894",
                    IsAdmin = false,
                    CreatedAt = DateTime.Now.AddDays(-15)
                }
            };
            context.Users.AddRange(users);
            await context.SaveChangesAsync();

            // Seed 20 Products
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Wireless Headphones Pro",
                    Description = "Premium noise-cancelling wireless headphones with 40-hour battery life",
                    Price = 299.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=500",
                    Stock = 45,
                    Category = "Electronics",
                    CreatedAt = DateTime.Now.AddMonths(-2)
                },
                new Product
                {
                    Name = "Smart Watch Series 5",
                    Description = "Advanced fitness tracking smartwatch with heart rate monitor and GPS",
                    Price = 399.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=500",
                    Stock = 30,
                    Category = "Electronics",
                    CreatedAt = DateTime.Now.AddMonths(-2)
                },
                new Product
                {
                    Name = "Laptop Stand Aluminum",
                    Description = "Ergonomic aluminum laptop stand for better posture and comfort",
                    Price = 49.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1527864550417-7fd91fc51a46?w=500",
                    Stock = 100,
                    Category = "Accessories",
                    CreatedAt = DateTime.Now.AddMonths(-1)
                },
                new Product
                {
                    Name = "Mechanical Keyboard RGB",
                    Description = "Gaming mechanical keyboard with RGB backlight and cherry switches",
                    Price = 149.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=500",
                    Stock = 60,
                    Category = "Electronics",
                    CreatedAt = DateTime.Now.AddMonths(-1)
                },
                new Product
                {
                    Name = "Wireless Mouse Pro",
                    Description = "Precision wireless mouse with ergonomic design and long battery life",
                    Price = 79.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1527814050087-3793815479db?w=500",
                    Stock = 85,
                    Category = "Electronics",
                    CreatedAt = DateTime.Now.AddMonths(-1)
                },
                new Product
                {
                    Name = "USB-C Hub 7-in-1",
                    Description = "Multi-port USB-C hub with HDMI, USB 3.0, and SD card reader",
                    Price = 39.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1625948515291-69613efd103f?w=500",
                    Stock = 120,
                    Category = "Accessories",
                    CreatedAt = DateTime.Now.AddDays(-45)
                },
                new Product
                {
                    Name = "Portable SSD 1TB",
                    Description = "High-speed portable SSD with USB 3.2 Gen 2 interface",
                    Price = 129.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1531492746076-161ca9bcad58?w=500",
                    Stock = 50,
                    Category = "Storage",
                    CreatedAt = DateTime.Now.AddDays(-40)
                },
                new Product
                {
                    Name = "Webcam 4K Ultra HD",
                    Description = "Professional 4K webcam with auto-focus and built-in microphone",
                    Price = 199.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1614624532983-4ce03382d63d?w=500",
                    Stock = 35,
                    Category = "Electronics",
                    CreatedAt = DateTime.Now.AddDays(-35)
                },
                new Product
                {
                    Name = "Desk Lamp LED Smart",
                    Description = "Smart LED desk lamp with wireless charging and touch controls",
                    Price = 69.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1507473885765-e6ed057f782c?w=500",
                    Stock = 75,
                    Category = "Home Office",
                    CreatedAt = DateTime.Now.AddDays(-30)
                },
                new Product
                {
                    Name = "Monitor 27\" 4K IPS",
                    Description = "27-inch 4K IPS monitor with HDR support and USB-C connectivity",
                    Price = 549.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?w=500",
                    Stock = 25,
                    Category = "Electronics",
                    CreatedAt = DateTime.Now.AddDays(-25)
                },
                new Product
                {
                    Name = "Bluetooth Speaker Waterproof",
                    Description = "Portable waterproof Bluetooth speaker with 20-hour battery life",
                    Price = 89.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?w=500",
                    Stock = 90,
                    Category = "Electronics",
                    CreatedAt = DateTime.Now.AddDays(-20)
                },
                new Product
                {
                    Name = "Phone Stand Adjustable",
                    Description = "Adjustable phone stand for desk with anti-slip base",
                    Price = 19.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1601784551446-20c9e07cdbdb?w=500",
                    Stock = 150,
                    Category = "Accessories",
                    CreatedAt = DateTime.Now.AddDays(-15)
                },
                new Product
                {
                    Name = "Cable Organizer Set",
                    Description = "Cable management organizer set for clean desk setup",
                    Price = 24.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=500",
                    Stock = 200,
                    Category = "Accessories",
                    CreatedAt = DateTime.Now.AddDays(-10)
                },
                new Product
                {
                    Name = "Gaming Chair Ergonomic",
                    Description = "Ergonomic gaming chair with lumbar support and adjustable armrests",
                    Price = 349.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1598550476439-6847785fcea6?w=500",
                    Stock = 20,
                    Category = "Furniture",
                    CreatedAt = DateTime.Now.AddDays(-8)
                },
                new Product
                {
                    Name = "Drawing Tablet Pro",
                    Description = "Professional drawing tablet with pressure sensitivity and tilt support",
                    Price = 279.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1593640495253-23196b27a87f?w=500",
                    Stock = 40,
                    Category = "Electronics",
                    CreatedAt = DateTime.Now.AddDays(-5)
                },
                new Product
                {
                    Name = "Microphone USB Condenser",
                    Description = "Professional USB condenser microphone for streaming and recording",
                    Price = 129.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1590602847861-f357a9332bbc?w=500",
                    Stock = 55,
                    Category = "Electronics",
                    CreatedAt = DateTime.Now.AddDays(-3)
                },
                new Product
                {
                    Name = "Desk Mat XXL",
                    Description = "Extra large desk mat with waterproof surface and anti-slip base",
                    Price = 29.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1595044426077-d36d9236d54a?w=500",
                    Stock = 110,
                    Category = "Accessories",
                    CreatedAt = DateTime.Now.AddDays(-2)
                },
                new Product
                {
                    Name = "Ring Light 12\"",
                    Description = "12-inch ring light with tripod stand for photography and video",
                    Price = 59.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1589939705384-5185137a7f0f?w=500",
                    Stock = 70,
                    Category = "Electronics",
                    CreatedAt = DateTime.Now.AddDays(-1)
                },
                new Product
                {
                    Name = "External Hard Drive 2TB",
                    Description = "Portable external hard drive with USB 3.0 and backup software",
                    Price = 89.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1597872200969-2b65d56bd16b?w=500",
                    Stock = 65,
                    Category = "Storage",
                    CreatedAt = DateTime.Now.AddHours(-12)
                },
                new Product
                {
                    Name = "Power Bank 20000mAh",
                    Description = "High-capacity power bank with fast charging and dual USB ports",
                    Price = 44.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1609091839311-d5365f9ff1c5?w=500",
                    Stock = 95,
                    Category = "Accessories",
                    CreatedAt = DateTime.Now.AddHours(-6)
                }
            };
            context.Products.AddRange(products);
            await context.SaveChangesAsync();

            // Seed 10 Orders
            var random = new Random();
            var statuses = new[] { "Pending", "Processing", "Shipped", "Delivered" };

            for (int i = 0; i < 10; i++)
            {
                var user = users[random.Next(users.Count)];
                var orderDate = DateTime.Now.AddDays(-random.Next(1, 60));
                var orderNumber = $"ORD-{orderDate:yyyyMMdd}-{1000 + i}";
                var trackingCode = Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper();

                var order = new Order
                {
                    OrderNumber = orderNumber,
                    TrackingCode = trackingCode,
                    CustomerName = user.FullName,
                    CustomerEmail = user.Email,
                    Address = $"{random.Next(100, 999)} Main St, City {random.Next(1, 10)}, ST {random.Next(10000, 99999)}",
                    Phone = user.PhoneNumber ?? "+1234567890",
                    OrderDate = orderDate,
                    Status = statuses[random.Next(statuses.Length)],
                    UserId = user.Id,
                    OrderItems = new List<OrderItem>()
                };

                // Add 1-4 random products to each order
                var itemCount = random.Next(1, 5);
                var selectedProducts = products.OrderBy(x => random.Next()).Take(itemCount).ToList();

                foreach (var product in selectedProducts)
                {
                    var quantity = random.Next(1, 4);
                    order.OrderItems.Add(new OrderItem
                    {
                        ProductId = product.Id,
                        Quantity = quantity,
                        Price = product.Price
                    });
                }

                order.TotalAmount = order.OrderItems.Sum(oi => oi.Price * oi.Quantity);
                context.Orders.Add(order);
            }

            await context.SaveChangesAsync();
        }
    }
}