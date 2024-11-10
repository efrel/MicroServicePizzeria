using Domain.MSPizzeria.Entity.Models.v1;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MSPizzeria.Data.Configuration;

public static class SeedData
{
    public static void ConfigureSeedData(ModelBuilder builder)
    {
        // Seed Products
        builder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Pepperoni Pizza",
                Description = "Classic pepperoni pizza with mozzarella cheese",
                Price = 14.99M,
                IsAvailable = true,
                Category = "Pizzas",
                ImageUrl = "/images/pepperoni-pizza.jpg",
                CreatedAt = DateTime.Parse("2024-01-01")
            },
            new Product
            {
                Id = 2,
                Name = "Margherita Pizza",
                Description = "Traditional pizza with tomato, mozzarella, and basil",
                Price = 12.99M,
                IsAvailable = true,
                Category = "Pizzas",
                ImageUrl = "/images/margherita-pizza.jpg",
                CreatedAt = DateTime.Parse("2024-01-01")
            },
            new Product
            {
                Id = 3,
                Name = "Hawaiian Pizza",
                Description = "Pizza with ham, pineapple, and mozzarella cheese",
                Price = 15.99M,
                IsAvailable = true,
                Category = "Pizzas",
                ImageUrl = "/images/hawaiian-pizza.jpg",
                CreatedAt = DateTime.Parse("2024-01-01")
            },
            new Product
            {
                Id = 4,
                Name = "Coca-Cola",
                Description = "600ml bottle",
                Price = 2.50M,
                IsAvailable = true,
                Category = "Drinks",
                ImageUrl = "/images/coca-cola.jpg",
                CreatedAt = DateTime.Parse("2024-01-01")
            },
            new Product
            {
                Id = 5,
                Name = "Garlic Bread",
                Description = "Fresh baked garlic bread with herbs",
                Price = 4.99M,
                IsAvailable = true,
                Category = "Sides",
                ImageUrl = "/images/garlic-bread.jpg",
                CreatedAt = DateTime.Parse("2024-01-01")
            }
        );

        // Seed Customers
        builder.Entity<Customer>().HasData(
            new Customer
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@email.com",
                Phone = "1234567890",
                RegisterDate = DateTime.Parse("2024-01-01"),
                IsActive = true
            },
            new Customer
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@email.com",
                Phone = "9876543210",
                RegisterDate = DateTime.Parse("2024-01-01"),
                IsActive = true
            },
            new Customer
            {
                Id = 3,
                FirstName = "Robert",
                LastName = "Johnson",
                Email = "robert.j@email.com",
                Phone = "5555555555",
                RegisterDate = DateTime.Parse("2024-01-01"),
                IsActive = true
            }
        );

        // Seed Addresses
        builder.Entity<Address>().HasData(
            new Address
            {
                Id = 1,
                CustomerId = 1,
                Street = "123 Main St",
                ExteriorNumber = "45A",
                InteriorNumber = "2B",
                Neighborhood = "Downtown",
                City = "Springfield",
                State = "IL",
                ZipCode = "62701",
                Reference = "Next to the park",
                IsMain = true
            },
            new Address
            {
                Id = 2,
                CustomerId = 2,
                Street = "456 Oak Avenue",
                ExteriorNumber = "78",
                InteriorNumber = "",
                Neighborhood = "West Side",
                City = "Springfield",
                State = "IL",
                ZipCode = "62702",
                Reference = "White house with blue roof",
                IsMain = true
            },
            new Address
            {
                Id = 3,
                CustomerId = 3,
                Street = "789 Pine Road",
                ExteriorNumber = "100",
                InteriorNumber = "4C",
                Neighborhood = "East Side",
                City = "Springfield",
                State = "IL",
                ZipCode = "62703",
                Reference = "Near the shopping mall",
                IsMain = true
            }
        );

        // Seed Orders
        builder.Entity<Order>().HasData(
            new Order
            {
                Id = 1,
                CustomerId = 1,
                AddressId = 1,
                OrderDate = DateTime.Parse("2024-01-15 18:30:00"),
                OrderStatus = "Delivered",
                TotalAmount = 32.48M,
                PaymentMethod = "Credit Card",
                Notes = "Please deliver to the back door"
            },
            new Order
            {
                Id = 2,
                CustomerId = 2,
                AddressId = 2,
                OrderDate = DateTime.Parse("2024-01-15 19:15:00"),
                OrderStatus = "In Progress",
                TotalAmount = 45.97M,
                PaymentMethod = "Cash",
                Notes = "Extra napkins please"
            },
            new Order
            {
                Id = 3,
                CustomerId = 3,
                AddressId = 3,
                OrderDate = DateTime.Parse("2024-01-15 20:00:00"),
                OrderStatus = "Pending",
                TotalAmount = 28.98M,
                PaymentMethod = "Debit Card",
                Notes = "Ring the doorbell twice"
            }
        );

        // Seed OrderDetails
        builder.Entity<OrderDetail>().HasData(
            // Order 1 Details
            new OrderDetail
            {
                Id = 1,
                OrderId = 1,
                ProductId = 1,
                Quantity = 1,
                UnitPrice = 14.99M,
                Subtotal = 14.99M,
                Notes = "Extra cheese"
            },
            new OrderDetail
            {
                Id = 2,
                OrderId = 1,
                ProductId = 4,
                Quantity = 2,
                UnitPrice = 2.50M,
                Subtotal = 5.00M,
                Notes = ""
            },
            // Order 2 Details
            new OrderDetail
            {
                Id = 3,
                OrderId = 2,
                ProductId = 2,
                Quantity = 2,
                UnitPrice = 12.99M,
                Subtotal = 25.98M,
                Notes = "Well done"
            },
            new OrderDetail
            {
                Id = 4,
                OrderId = 2,
                ProductId = 5,
                Quantity = 1,
                UnitPrice = 4.99M,
                Subtotal = 4.99M,
                Notes = ""
            },
            // Order 3 Details
            new OrderDetail
            {
                Id = 5,
                OrderId = 3,
                ProductId = 3,
                Quantity = 1,
                UnitPrice = 15.99M,
                Subtotal = 15.99M,
                Notes = "No pineapple"
            }
        );
    }
}