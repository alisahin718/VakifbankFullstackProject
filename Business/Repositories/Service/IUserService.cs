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
    public interface IUserService
    {
        Task Add(RegisterAuthDto authDto); //
        Task<IResult> Update(User user); //
        Task<IResult> Delete(User user); //
        Task<IDataResult<List<User>>> GetList(); //
        Task<User> GetByEmail(string email); //
        Task<List<OperationClaim>> GetUserOperationClaims(int userId); //
        Task<IDataResult<User>> GetById(int id); //
        Task<User> GetByIdForAuth(int id); //
    }
}
