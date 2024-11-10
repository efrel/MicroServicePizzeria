using Domain.MSPizzeria.Entity.Models.v1;
using Infrastructure.MSPizzeria.Data;
using Infrastructure.MSPizzeria.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MSPizzeria.Repository;

public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;

        public OrderRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Order> GetByIdAsync(int id, bool includeDetails = true)
        {
            IQueryable<Order> query = _context.Orders;

            if (includeDetails)
            {
                query = query
                    .Include(o => o.Customer)
                    .Include(o => o.Address)
                    .Include(o => o.Details)
                        .ThenInclude(d => d.Product);
            }

            return await query.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync(bool includeDetails = true)
        {
            IQueryable<Order> query = _context.Orders;

            if (includeDetails)
            {
                query = query
                    .Include(o => o.Customer)
                    .Include(o => o.Address)
                    .Include(o => o.Details)
                        .ThenInclude(d => d.Product);
            }

            return await query.OrderByDescending(o => o.OrderDate).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Orders
                .Include(o => o.Details)
                    .ThenInclude(d => d.Product)
                .Where(o => o.CustomerId == customerId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByStatusAsync(string status)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Details)
                    .ThenInclude(d => d.Product)
                .Where(o => o.OrderStatus == status)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<Order> CreateAsync(Order order)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            
            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    order.OrderDate = DateTime.UtcNow;
                    order.OrderStatus = "Pending";

                    // Calculate total from details
                    order.TotalAmount = order.Details.Sum(d => d.Quantity * d.UnitPrice);

                    await _context.Orders.AddAsync(order);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return order;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            
            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var existingOrder = await _context.Orders
                        .Include(o => o.Details)
                        .FirstOrDefaultAsync(o => o.Id == order.Id);

                    if (existingOrder == null) return null;

                    // Update order header
                    existingOrder.OrderStatus = order.OrderStatus;
                    existingOrder.PaymentMethod = order.PaymentMethod;
                    existingOrder.Notes = order.Notes;

                    // Remove deleted details
                    var detailsToRemove = existingOrder.Details
                        .Where(d => !order.Details.Any(od => od.Id == d.Id))
                        .ToList();

                    foreach (var detail in detailsToRemove)
                    {
                        _context.OrderDetails.Remove(detail);
                    }

                    // Update or add new details
                    foreach (var detail in order.Details)
                    {
                        var existingDetail = existingOrder.Details
                            .FirstOrDefault(d => d.Id == detail.Id);

                        if (existingDetail != null)
                        {
                            existingDetail.Quantity = detail.Quantity;
                            existingDetail.UnitPrice = detail.UnitPrice;
                            existingDetail.Notes = detail.Notes;
                        }
                        else
                        {
                            existingOrder.Details.Add(new OrderDetail
                            {
                                ProductId = detail.ProductId,
                                Quantity = detail.Quantity,
                                UnitPrice = detail.UnitPrice,
                                Notes = detail.Notes
                            });
                        }
                    }

                    // Recalculate total
                    existingOrder.TotalAmount = existingOrder.Details.Sum(d => d.Quantity * d.UnitPrice);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return existingOrder;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var order = await _context.Orders
                    .Include(o => o.Details)
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (order == null) return false;

                _context.OrderDetails.RemoveRange(order.Details);
                _context.Orders.Remove(order);
                
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Orders.AnyAsync(o => o.Id == id);
        }
    }