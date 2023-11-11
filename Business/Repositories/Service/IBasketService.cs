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
    public interface IBasketService
    {
        Task<IResult> Add(Basket basket);
        Task<IResult> Update(Basket basket);
        Task<IResult> Delete(Basket basket);
        Task<IDataResult<List<Basket>>> GetList();
        Task<List<Basket>> GetListByProductId(int productId);
        Task<IDataResult<List<BasketListDto>>> GetListByDealerId(int dealerId);
        Task<IDataResult<Basket>> GetById(int id);
    }
}
