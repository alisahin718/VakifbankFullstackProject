using Core.DataAccess;
using DataAccess.Context;
using DataAccess.Repositories.Contract;
using Entities.DTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.EFCore
{
    public class BasketRepository : RepositoryBase<Basket>, IBasketRepository
    {
        private readonly RepositoryContext _context;

        public BasketRepository(RepositoryContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<BasketListDto>> GetListByDealerId(int dealerId)
        {
            var result = from basket in _context.Baskets.Where(p => p.DealerId == dealerId)
                         join product in _context.Products on basket.ProductId equals product.Id
                         select new BasketListDto
                         {
                             Id = basket.Id,
                             DealerId = basket.DealerId,
                             ProductId = basket.ProductId,
                             ProductName = product.Name,
                             Price = basket.Price,
                             Quantity = basket.Quantity,
                             Total = basket.Price * basket.Quantity
                         };
            return await result.ToListAsync();
        }
    }
}
