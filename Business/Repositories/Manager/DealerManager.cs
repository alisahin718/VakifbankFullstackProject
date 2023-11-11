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
    public class DealerManager : IDealerService
    {
        private readonly IDealerRepository _context;
        private readonly IDealerRelationshipService _dealerRelationshipService;
        private readonly IOrderService _orderService;

        public DealerManager(IDealerRepository context, IDealerRelationshipService dealerRelationshipService, IOrderService orderService)
        {
            _context = context;
            _dealerRelationshipService = dealerRelationshipService;
            _orderService = orderService;
        }

        public async Task<IResult> Add(DealerRegisterDto dealerRegisterDto)
        {

            IResult result = BusinessRules.Run(
            await CheckIfEmailExists(dealerRegisterDto.Email)
    );

            if (result != null)
            {
                return result;
            }
                Dealer dealer = new Dealer()
            {
                Id = 0,
                Email = dealerRegisterDto.Email,
                Name = dealerRegisterDto.Name,
                Password = dealerRegisterDto.Password,
            };

            await _context.Add(dealer);
            return new SuccessResult(DealerMessage.Added);
        }

        public async Task<IResult> Update(Dealer dealer)
        {
            await _context.Update(dealer);
            return new SuccessResult(DealerMessage.Updated);
        }

        public async Task<IResult> Delete(Dealer dealer)
        {
            IResult result = BusinessRules.Run(
                await CheckIfDealerOrderExist(dealer.Id));

            if (result != null)
            {
                return result;
            }

            var dealerRelationship = await _dealerRelationshipService.GetByDealerId(dealer.Id);
            if (dealerRelationship.Data != null)
            {
                await _dealerRelationshipService.Delete(dealerRelationship.Data);
            }

            await _context.Delete(dealer);
            return new SuccessResult(DealerMessage.Deleted);
        }

        public async Task<IDataResult<List<DealerDto>>> GetList()
        {
            return new SuccessDataResult<List<DealerDto>>(await _context.GetListDto());
        }

        public async Task<IDataResult<Dealer>> GetById(int id)
        {
            return new SuccessDataResult<Dealer>(await _context.Get(p => p.Id == id));
        }

        public async Task<IDataResult<DealerDto>> GetDtoById(int id)
        {
            return new SuccessDataResult<DealerDto>(await _context.GetDto(id));
        }

        public async Task<Dealer> GetByEmail(string email)
        {
            var result = await _context.Get(p => p.Email == email);
            return result;
        }

        private async Task<IResult> CheckIfEmailExists(string email)
        {
            var list = await GetByEmail(email);
            if (list != null)
            {
                return new ErrorResult("Bu mail adresi daha önce kullanılmış");
            }
            return new SuccessResult();
        }

        public async Task<IResult> CheckIfDealerOrderExist(int dealerId)
        {
            var result = await _orderService.GetListByDealerId(dealerId);
            if (result.Data.Count > 0)
            {
                return new ErrorResult("Siparişi bulunan müşteri kaydı silinemez!");
            }
            return new SuccessResult();
        }
    }
}
