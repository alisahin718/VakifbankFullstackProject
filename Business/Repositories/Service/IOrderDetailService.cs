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
    public interface IOrderDetailService
    {
        Task<IResult> Add(OrderDetail orderDetail);
        Task<IResult> Update(OrderDetail orderDetail);
        Task<IResult> Delete(OrderDetail orderDetail);
        Task<IDataResult<List<OrderDetail>>> GetList(int orderId);
        Task<IDataResult<List<OrderDetailDto>>> GetListDto(int orderId);
        Task<List<OrderDetail>> GetListByProductId(int productId);
        Task<IDataResult<OrderDetail>> GetById(int id);
    }
}
