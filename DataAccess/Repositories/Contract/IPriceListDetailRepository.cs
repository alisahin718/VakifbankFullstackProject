using Core.DataAccess;
using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Contract
{
    public interface IPriceListDetailRepository : IRepositoryBase<PriceListDetail>
    {
        Task<List<PriceListDetailDto>> GetListDto(int priceListId);
    }
}
