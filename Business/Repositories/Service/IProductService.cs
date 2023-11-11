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
    public interface IProductService
    {
        Task<IResult> Add(Product product);
        Task<IResult> Update(Product product);
        Task<IResult> Delete(Product product);
        Task<IDataResult<List<ProductListDto>>> GetList();
        Task<IDataResult<List<ProductListDto>>> GetProductList(int dealerId);
        Task<IDataResult<Product>> GetById(int id);
    }
}
