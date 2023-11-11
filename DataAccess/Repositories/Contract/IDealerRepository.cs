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
    public interface IDealerRepository : IRepositoryBase<Dealer>
    {
        Task<List<DealerDto>> GetListDto();
        Task<DealerDto> GetDto(int id);
    }
}
