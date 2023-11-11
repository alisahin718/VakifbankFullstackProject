using Business.Repositories.Messages;
using Business.Repositories.Service;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Model;
using DataAccess.Repositories.Contract;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.Manager
{
    public class PriceListManager : IPriceListService
    {
        private readonly IPriceListRepository _priceListRepository;

        public PriceListManager(IPriceListRepository priceListRepository)
        {
            _priceListRepository = priceListRepository;
        }

        public async Task<IResult> Add(PriceList priceList)
        {
            await _priceListRepository.Add(priceList);
            return new SuccessResult(PriceListMessages.Added);
        }

        public async Task<IResult> Update(PriceList priceList)
        {
            await _priceListRepository.Update(priceList);
            return new SuccessResult(PriceListMessages.Updated);
        }

        public async Task<IResult> Delete(PriceList priceList)
        {
            _priceListRepository.Delete(priceList);
            return new SuccessResult(PriceListMessages.Deleted);
        }

        public async Task<IDataResult<List<PriceList>>> GetList()
        {
            return new SuccessDataResult<List<PriceList>>(await _priceListRepository.GetAll());
        }

        public async Task<IDataResult<PriceList>> GetById(int id)
        {
            return new SuccessDataResult<PriceList>(await _priceListRepository.Get(p => p.Id == id));
        }

    }
}
