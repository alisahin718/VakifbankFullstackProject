using Core.DataAccess;
using DataAccess.Context;
using DataAccess.Repositories.Contract;
using Entities.DTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.EFCore
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly RepositoryContext _context;

        public ProductRepository(RepositoryContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<ProductListDto>> GetList()
        {
            var result = from product in _context.Products
                         select new ProductListDto
                         {
                             Id = product.Id,
                             Name = product.Name,
                             MainImageUrl = (_context.ProductImages.Where(p => p.ProductId == product.Id && p.IsMainImage == true).Count() > 0
                                            ? _context.ProductImages.Where(p => p.ProductId == product.Id && p.IsMainImage == true).Select(s => s.ImageUrl).FirstOrDefault()
                                            : "")
                         };
            return await result.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<List<ProductListDto>> GetProductList(int dealerId)
        {
            if (dealerId != 0)
            {
                var dealerRelationship = _context.DealerRelationships.Where(p => p.DealerId == dealerId).SingleOrDefault();

                var result = from product in _context.Products
                             select new ProductListDto
                             {
                                 Id = product.Id,
                                 Name = product.Name,
                                 Discount = dealerRelationship.Discount,
                                 Price = _context.PriceListDetails.Where(p => p.PriceListId == dealerRelationship.PriceListId && p.ProductId == product.Id).Count() > 0
                                        ? _context.PriceListDetails.Where(p => p.PriceListId == dealerRelationship.PriceListId && p.ProductId == product.Id).Select(s => s.Price).FirstOrDefault()
                                        : 0,
                                 MainImageUrl = (_context.ProductImages.Where(p => p.ProductId == product.Id && p.IsMainImage == true).Count() > 0
                                                ? _context.ProductImages.Where(p => p.ProductId == product.Id && p.IsMainImage == true).Select(s => s.ImageUrl).FirstOrDefault()
                                                : ""),
                                 Images = _context.ProductImages.Where(p => p.ProductId == product.Id).Select(s => s.ImageUrl).ToList()
                             };
                return await result.OrderBy(p => p.Name).ToListAsync();
            }
            else
            {
                var result = from product in _context.Products
                             select new ProductListDto
                             {
                                 Id = product.Id,
                                 Name = product.Name,
                                 Discount = 0,
                                 Price = 0,
                                 MainImageUrl = (_context.ProductImages.Where(p => p.ProductId == product.Id && p.IsMainImage == true).Count() > 0
                                                ? _context.ProductImages.Where(p => p.ProductId == product.Id && p.IsMainImage == true).Select(s => s.ImageUrl).FirstOrDefault()
                                                : ""),
                                 Images = _context.ProductImages.Where(p => p.ProductId == product.Id).Select(s => s.ImageUrl).ToList()
                             };
                return await result.OrderBy(p => p.Name).ToListAsync();
            }
        }
    }
}
