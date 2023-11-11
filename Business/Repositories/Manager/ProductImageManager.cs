using Business.Repositories.Messages;
using Business.Repositories.Service;
using Business.Utilities.File;
using Core.Utilities.Business;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Model;
using DataAccess.Repositories.Contract;
using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.Manager
{
    public class ProductImageManager : IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IFileService _fileService;

        public ProductImageManager(IProductImageRepository productImageRepository, IFileService fileService)
        {
            _productImageRepository = productImageRepository;
            _fileService = fileService;
        }

        public async Task<IResult> Add(ProductImageAddDto productImageAddDto)
        {
            foreach (var image in productImageAddDto.Images)
            {
                IResult result = BusinessRules.Run(
                CheckIfImageExtesionsAllow(image.FileName),
                CheckIfImageSizeIsLessThanOneMb(image.Length));

                if (result == null)
                {
                    string fileName = _fileService.FileSaveToFtp(image);

                    ProductImage productImage = new()
                    {
                        Id = 0,
                        ImageUrl = fileName,
                        ProductId = productImageAddDto.ProductId,
                        IsMainImage = false
                    };

                    await _productImageRepository.Add(productImage);
                }
            }

            return new SuccessResult(ProductImageMessages.Added);
        }

        public async Task<IResult> Update(ProductImageUpdateDto productImageUpdateDto)
        {
            IResult result = BusinessRules.Run(
            CheckIfImageExtesionsAllow(productImageUpdateDto.Image.FileName),
            CheckIfImageSizeIsLessThanOneMb(productImageUpdateDto.Image.Length));

            if (result != null)
            {
                return result;
            }

            string path = @"./Content/img/" + productImageUpdateDto.ImageUrl;

            _fileService.FileDeleteToServer(path);

            string fileName = _fileService.FileSaveToServer(productImageUpdateDto.Image, "./Content/img/");

            ProductImage productImage = new()
            {
                Id = productImageUpdateDto.Id,
                ImageUrl = fileName,
                ProductId = productImageUpdateDto.ProductId,
                IsMainImage = productImageUpdateDto.IsMainImage
            };

            await _productImageRepository.Update(productImage);
            return new SuccessResult(ProductImageMessages.Updated);
        }

        public async Task<IResult> Delete(ProductImage productImage)
        {
            string path = productImage.ImageUrl;

            _fileService.FileDeleteToFtp(path);

            await _productImageRepository.Delete(productImage);
            return new SuccessResult(ProductImageMessages.Deleted);
        }

        public async Task<IDataResult<List<ProductImage>>> GetList()
        {
            return new SuccessDataResult<List<ProductImage>>(await _productImageRepository.GetAll());
        }

        public async Task<IDataResult<List<ProductImage>>> GetListByProductId(int productId)
        {
            return new SuccessDataResult<List<ProductImage>>(await _productImageRepository.GetAll(p => p.ProductId == productId));
        }

        public async Task<IDataResult<ProductImage>> GetById(int id)
        {
            return new SuccessDataResult<ProductImage>(await _productImageRepository.Get(p => p.Id == id));
        }

        private IResult CheckIfImageSizeIsLessThanOneMb(long imgSize)
        {
            decimal imgMbSize = Convert.ToDecimal(imgSize * 0.000010);
            if (imgMbSize > 5)
            {
                return new ErrorResult("Yüklediğiniz resmi boyutu en fazla 1mb olmalıdır");
            }
            return new SuccessResult();
        }

        private IResult CheckIfImageExtesionsAllow(string fileName)
        {
            var ext = fileName.Substring(fileName.LastIndexOf('.'));
            var extension = ext.ToLower();
            List<string> AllowFileExtensions = new List<string> { ".jpg", ".jpeg", ".gif", ".png" };
            if (!AllowFileExtensions.Contains(extension))
            {
                return new ErrorResult("Eklediğiniz resim .jpg, .jpeg, .gif, .png türlerinden biri olmalıdır!");
            }
            return new SuccessResult();
        }

        public async Task<IResult> SetMainImage(int id)
        {
            var productImage = await _productImageRepository.Get(p => p.Id == id);
            var productImages = await _productImageRepository.GetAll(p => p.ProductId == productImage.ProductId);
            foreach (var item in productImages)
            {
                item.IsMainImage = false;
                await _productImageRepository.Update(item);
            }

            productImage.IsMainImage = true;
            await _productImageRepository.Update(productImage);
            return new SuccessResult(ProductImageMessages.MainImageIsUpdated);
        }
    }
}
