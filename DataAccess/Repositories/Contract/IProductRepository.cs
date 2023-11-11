﻿using Core.DataAccess;
using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Contract
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<List<ProductListDto>> GetList();
        Task<List<ProductListDto>> GetProductList(int dealerId);
    }
}
