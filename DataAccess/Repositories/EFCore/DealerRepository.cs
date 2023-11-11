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
    public class DealerRepository : RepositoryBase<Dealer>, IDealerRepository
    {
        private readonly RepositoryContext _context;

        public DealerRepository(RepositoryContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<DealerDto> GetDto(int id)
        {
            var result = from dealer in _context.Dealers.Where(p => p.Id == id)
                         select new DealerDto
                         {
                             Id = dealer.Id,
                             Email = dealer.Email,
                             Name = dealer.Name,
                             Password = dealer.Password,
                             Discount =
                             (_context.DealerRelationships.Where(p => p.DealerId == dealer.Id) != null
                             ? _context.DealerRelationships.Where(p => p.DealerId == dealer.Id).Select(s => s.Discount).FirstOrDefault()
                            : 0),
                             PriceListId =
                             (_context.DealerRelationships.Where(p => p.DealerId == dealer.Id) != null
                            ? _context.DealerRelationships.Where(p => p.DealerId == dealer.Id).Select(s => s.PriceListId).FirstOrDefault()
                            : 0),
                             PriceListName =
                             (_context.DealerRelationships.Where(p => p.DealerId == dealer.Id) != null
                             ? _context.PriceLists.Where(p => p.Id == (_context.DealerRelationships.Where(p => p.DealerId == dealer.Id).Select(s => s.PriceListId).FirstOrDefault())).Select(s => s.Name).FirstOrDefault()
                             : "")
                         };
            return await result.FirstOrDefaultAsync();
        }

        public async Task<List<DealerDto>> GetListDto()
        {
            var result = from dealer in _context.Dealers
                         select new DealerDto
                         {
                             Id = dealer.Id,
                             Email = dealer.Email,
                             Name = dealer.Name,
                             Password = dealer.Password,
                             Discount =
                             (_context.DealerRelationships.Where(p => p.DealerId == dealer.Id) != null
                             ? _context.DealerRelationships.Where(p => p.DealerId == dealer.Id).Select(s => s.Discount).FirstOrDefault()
                            : 0),
                             PriceListId =
                             (_context.DealerRelationships.Where(p => p.DealerId == dealer.Id) != null
                            ? _context.DealerRelationships.Where(p => p.DealerId == dealer.Id).Select(s => s.PriceListId).FirstOrDefault()
                            : 0),
                             PriceListName =
                             (_context.DealerRelationships.Where(p => p.DealerId == dealer.Id) != null
                             ? _context.PriceLists.Where(p => p.Id == (_context.DealerRelationships.Where(p => p.DealerId == dealer.Id).Select(s => s.PriceListId).FirstOrDefault())).Select(s => s.Name).FirstOrDefault()
                             : "")
                         };
            return await result.OrderBy(p => p.Name).ToListAsync();
        }
    }
}
