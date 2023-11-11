using Business.Repositories.Messages;
using Business.Repositories.Service;
using Core.Utilities.Business;
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
    public class PriceListDetailManager : IPriceListDetailService
    {
        private readonly IPriceListDetailRepository _priceListDetailRepository;

        public PriceListDetailManager(IPriceListDetailRepository priceListDetailRepository)
        {
            _priceListDetailRepository = priceListDetailRepository;
        }

        public async Task<IResult> Add(PriceListDetail priceListDetail)
        {
            IResult result = BusinessRules.Run(
                await CheckIfProductExist(priceListDetail)
                );

            if (result != null)
            {
                return result;
            }

            await _priceListDetailRepository.Add(priceListDetail);
            return new SuccessResult(PriceListDetailMessages.Added);
        }

        public async Task<IResult> Update(PriceListDetail priceListDetail)
        {
            await _priceListDetailRepository.Update(priceListDetail);
            return new SuccessResult(PriceListDetailMessages.Updated);
        }

        public async Task<IResult> Delete(PriceListDetail priceListDetail)
        {
            await _priceListDetailRepository.Delete(priceListDetail);
            return new SuccessResult(PriceListDetailMessages.Deleted);
        }

        public async Task<IDataResult<List<PriceListDetail>>> GetList()
        {
            return new SuccessDataResult<List<PriceListDetail>>(await _priceListDetailRepository.GetAll());
        }

        public async Task<IDataResult<List<PriceListDetailDto>>> GetListDto(int priceListId)
        {
            return new SuccessDataResult<List<PriceListDetailDto>>(await _priceListDetailRepository.GetListDto(priceListId));
        }

        public async Task<IDataResult<PriceListDetail>> GetById(int id)
        {
            return new SuccessDataResult<PriceListDetail>(await _priceListDetailRepository.Get(p => p.Id == id));
        }

        public async Task<List<PriceListDetail>> GetListByProductId(int productId)
        {
            return await _priceListDetailRepository.GetAll(p => p.ProductId == productId);
        }

        public async Task<IResult> CheckIfProductExist(PriceListDetail priceListDetail)
        {
            var result = await _priceListDetailRepository.Get(p => p.PriceListId == priceListDetail.PriceListId && p.ProductId == priceListDetail.ProductId);
            if (result != null)
            {
                return new ErrorResult("Bu ürün daha önce fiyat listesine eklenmiş!");
            }
            return new SuccessResult();
        }
    }
}
