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
    public interface IOrderService
    {
        Task<IResult> Add(int dealerId);
        Task<IResult> Update(Order order);
        Task<IResult> Delete(Order order);
        Task<IDataResult<List<Order>>> GetList();
        Task<IDataResult<List<OrderDto>>> GetListDto();
        Task<IDataResult<List<OrderDto>>> GetListByDealerIdDto(int dealerId);
        Task<IDataResult<List<Order>>> GetListByDealerId(int dealerId);
        Task<IDataResult<Order>> GetById(int id);
        Task<IDataResult<OrderDto>> GetByIdDto(int id);
    }
}
