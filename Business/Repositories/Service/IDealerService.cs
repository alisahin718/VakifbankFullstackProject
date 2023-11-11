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
    public interface IDealerService
    {
        Task<IResult> Add(DealerRegisterDto dealerRegisterDto);
        Task<IResult> Update(Dealer dealer);
        Task<IResult> Delete(Dealer dealer);
        Task<IDataResult<List<DealerDto>>> GetList();
        Task<IDataResult<Dealer>> GetById(int id);
        Task<IDataResult<DealerDto>> GetDtoById(int id);
        Task<Dealer> GetByEmail(string email);
        Task<IResult> CheckIfDealerOrderExist(int dealerId);
    }
}
