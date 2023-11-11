using Core.DataAccess;
using DataAccess.Context;
using DataAccess.Repositories.Contract;
using Entities.DTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.EFCore
{
    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public readonly RepositoryContext _context;
        public OrderDetailRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<OrderDetailDto>> GetListDto(int orderId)
        {
            var result = from orderDetail in _context.OrderDetails.Where(p => p.OrderId == orderId)
                         join product in _context.Products on orderDetail.ProductId equals product.Id
                         select new OrderDetailDto
                         {
                             Id = orderDetail.Id,
                             OrderId = orderDetail.OrderId,
                             Price = orderDetail.Price,
                             ProductId = orderDetail.ProductId,
                             ProductName = product.Name,
                             Quantity = orderDetail.Quantity,
                             Total = orderDetail.Quantity * orderDetail.Price
                         };
            return await result.ToListAsync();
        }
    }
}
