using Core.DataAccess;
using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Contract
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        string GetOrderNumber();
        Task<List<OrderDto>> GetListDto();
        Task<List<OrderDto>> GetListByDealerIdDto(int dealerId);
        Task<OrderDto> GetByIdDto(int id);
    }
}
