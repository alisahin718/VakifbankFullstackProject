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
    public interface IOrderDetailRepository : IRepositoryBase<OrderDetail>
    {
        Task<List<OrderDetailDto>> GetListDto(int orderId);
    }
}
