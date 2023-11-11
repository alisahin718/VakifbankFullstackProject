using Business.Repositories.BasketRepository.Constants;
using Business.Repositories.Service;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Model;
using DataAccess.Repositories.Contract;
using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.Manager
{
    public class BasketManager : IBasketService
    {
        private readonly IBasketRepository _basket;
        private readonly ILoggerService _logger;

        public BasketManager(IBasketRepository basket, ILoggerService logger)
        {
            _basket = basket;
            _logger = logger;
        }

        public async Task<IResult> Add(Basket basket)
        {
            await _basket.Add(basket);
            return new SuccessResult(BasketMessages.Added);
        }

        

        public async Task<IResult> Update(Basket basket)
        {
            await _basket.Update(basket);
            return new SuccessResult(BasketMessages.Updated);
        }


        public async Task<IResult> Delete(Basket basket)
        {
            await _basket.Delete(basket);
            return new SuccessResult(BasketMessages.Deleted);
        }

        public async Task<IDataResult<List<Basket>>> GetList()
        {
            return new SuccessDataResult<List<Basket>>(await _basket.GetAll());
        }

        public async Task<IDataResult<List<BasketListDto>>> GetListByDealerId(int dealerId)
        {
            return new SuccessDataResult<List<BasketListDto>>(await _basket.GetListByDealerId(dealerId));
        }

        public async Task<IDataResult<Basket>> GetById(int id)
        {
            return new SuccessDataResult<Basket>(await _basket.Get(p => p.Id == id));
        }

        public async Task<List<Basket>> GetListByProductId(int productId)
        {
            return await _basket.GetAll(p => p.ProductId == productId);
        }
    }
}
