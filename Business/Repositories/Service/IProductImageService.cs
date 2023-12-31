﻿using Core.Utilities.Result.Abstract;
using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.Service
{
    public interface IProductImageService
    {
        Task<IResult> Add(ProductImageAddDto productImageAddDto);
        Task<IResult> Update(ProductImageUpdateDto productImageUpdateDto);
        Task<IResult> SetMainImage(int id);
        Task<IResult> Delete(ProductImage productImage);
        Task<IDataResult<List<ProductImage>>> GetList();
        Task<IDataResult<List<ProductImage>>> GetListByProductId(int productId);
        Task<IDataResult<ProductImage>> GetById(int id);
    }
}
