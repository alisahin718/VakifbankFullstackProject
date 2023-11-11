using Core.Utilities.Result.Abstract;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.Service
{
    public interface IPriceListService
    {
        Task<IResult> Add(PriceList priceList);
        Task<IResult> Update(PriceList priceList);
        Task<IResult> Delete(PriceList priceList);
        Task<IDataResult<List<PriceList>>> GetList();
        Task<IDataResult<PriceList>> GetById(int id);
    }
}
