using Core.DataAccess;
using DataAccess.Context;
using DataAccess.Repositories.Contract;
using Entities.DTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.EFCore
{
    public class PriceListDetailRepository : RepositoryBase<PriceListDetail>, IPriceListDetailRepository
    {
        private readonly RepositoryContext _context;

        public PriceListDetailRepository(RepositoryContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<PriceListDetailDto>> GetListDto(int priceListId)
        {
            var result = from priceListDetail in _context.PriceListDetails.Where(p => p.PriceListId == priceListId)
                         join product in _context.Products on priceListDetail.ProductId equals product.Id
                         select new PriceListDetailDto
                         {
                             Id = priceListDetail.Id,
                             Price = priceListDetail.Price,
                             PriceListId = priceListDetail.PriceListId,
                             ProductId = priceListDetail.ProductId,
                             ProductName = product.Name
                         };
            return await result.OrderBy(p => p.ProductName).ToListAsync();
        }
    }
}
