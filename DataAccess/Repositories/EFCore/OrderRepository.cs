using Core.DataAccess;
using DataAccess.Context;
using DataAccess.Repositories.Contract;
using Entities.DTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.EFCore
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public readonly RepositoryContext _context;
        public OrderRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<OrderDto>> GetListDto()
        {
            var result = from order in _context.Orders
                         join dealer in _context.Dealers on order.DealerId equals dealer.Id
                         select new OrderDto
                         {
                             Id = order.Id,
                             DealerId = order.DealerId,
                             DealerName = dealer.Name,
                             Date = order.Date,
                             OrderNumber = order.OrderNumber,
                             Status = order.Status,
                             Quantity = _context.OrderDetails.Where(p => p.OrderId == order.Id).Sum(s => s.Quantity),
                             Total = _context.OrderDetails.Where(p => p.OrderId == order.Id).Sum(s => s.Price) * _context.OrderDetails.Where(p => p.OrderId == order.Id).Sum(s => s.Quantity)
                         };
            return await result.OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task<List<OrderDto>> GetListByDealerIdDto(int dealerId)
        {
            var result = from order in _context.Orders.Where(p => p.DealerId == dealerId)
                         join dealer in _context.Dealers on order.DealerId equals dealer.Id
                         select new OrderDto
                         {
                             Id = order.Id,
                             DealerId = order.DealerId,
                             DealerName = dealer.Name,
                             Date = order.Date,
                             OrderNumber = order.OrderNumber,
                             Status = order.Status,
                             Quantity = _context.OrderDetails.Where(p => p.OrderId == order.Id).Sum(s => s.Quantity),
                             Total = _context.OrderDetails.Where(p => p.OrderId == order.Id).Sum(s => s.Price) * _context.OrderDetails.Where(p => p.OrderId == order.Id).Sum(s => s.Quantity)
                         };
            return await result.OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task<OrderDto> GetByIdDto(int id)
        {
            var result = from order in _context.Orders.Where(p => p.Id == id)
                         join dealer in _context.Dealers on order.DealerId equals dealer.Id
                         select new OrderDto
                         {
                             Id = order.Id,
                             DealerId = order.DealerId,
                             DealerName = dealer.Name,
                             Date = order.Date,
                             OrderNumber = order.OrderNumber,
                             Status = order.Status,
                             Quantity = _context.OrderDetails.Where(p => p.OrderId == order.Id).Sum(s => s.Quantity),
                             Total = _context.OrderDetails.Where(p => p.OrderId == order.Id).Sum(s => s.Price) * _context.OrderDetails.Where(p => p.OrderId == order.Id).Sum(s => s.Quantity)
                         };
            return await result.FirstOrDefaultAsync();
        }

        public string GetOrderNumber()
        {
            var findLastOrder = _context.Orders.OrderBy(p => p.Id).LastOrDefault();

            if (findLastOrder == null)
            {
                return "SP00000000000001";
            }

            string findLastOrderNumber = findLastOrder.OrderNumber;
            findLastOrderNumber = findLastOrderNumber.Substring(2, 14);
            int orderNumberInt = Convert.ToInt16(findLastOrderNumber);
            orderNumberInt++;
            string newOrderNumber = orderNumberInt.ToString();

            for (int i = newOrderNumber.Length; i < 14; i++)
            {
                newOrderNumber = "0" + newOrderNumber;
            }

            newOrderNumber = "SP" + newOrderNumber;
            return newOrderNumber;
        }
    }
}
