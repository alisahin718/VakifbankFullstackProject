using Core.Utilities.Result.Abstract;
using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.Service
{
    public interface IPriceListDetailService
    {
        Task<IResult> Add(PriceListDetail priceListDetail);
        Task<IResult> Update(PriceListDetail priceListDetail);
        Task<IResult> Delete(PriceListDetail priceListDetail);
        Task<IDataResult<List<PriceListDetail>>> GetList();
        Task<IDataResult<List<PriceListDetailDto>>> GetListDto(int priceListId);
        Task<List<PriceListDetail>> GetListByProductId(int productId);
        Task<IDataResult<PriceListDetail>> GetById(int id);
    }
}
