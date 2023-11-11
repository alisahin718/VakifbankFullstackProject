using Business.Repositories.Messages;
using Business.Repositories.Service;
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
    public class ProductManager : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImageService _productImageService;
        private readonly IPriceListDetailService _priceListDetailService;
        private readonly IBasketService _basketService;
        private readonly IOrderDetailService _orderDetailService;

        public ProductManager(IProductRepository productRepository, 
            IProductImageService productImageService, 
            IPriceListDetailService priceListDetailService, 
            IBasketService basketService, IOrderDetailService orderDetailService)
        {
            _productRepository = productRepository;
            _productImageService = productImageService;
            _priceListDetailService = priceListDetailService;
            _basketService = basketService;
            _orderDetailService = orderDetailService;
        }

        //[SecuredAspect("admin,prdocut.add")]

        public async Task<IResult> Add(Product product)
        {
            await _productRepository.Add(product);
            return new SuccessResult(ProductMessages.Added);
        }

        // [SecuredAspect("admin,prdocut.update")]

        public async Task<IResult> Update(Product product)
        {
            await _productRepository.Update(product);
            return new SuccessResult(ProductMessages.Updated);
        }

        //[SecuredAspect("admin,prdocut.delete")]

        public async Task<IResult> Delete(Product product)
        {
            IResult result = BusinessRules.Run(
                await CheckIfProductExistToBaskets(product.Id),
                await CheckIfProductExistToOrderDetails(product.Id)
                );

            if (result != null)
            {
                return result;
            }

            var images = await _productImageService.GetListByProductId(product.Id);
            foreach (var image in images.Data)
            {
                await _productImageService.Delete(image);
            }

            var priceListProducts = await _priceListDetailService.GetListByProductId(product.Id);
            foreach (var item in priceListProducts)
            {
                await _priceListDetailService.Delete(item);
            }

            await _productRepository.Delete(product);
            return new SuccessResult(ProductMessages.Deleted);
        }

        // [SecuredAspect("admin,prdocut.get")]
        public async Task<IDataResult<List<ProductListDto>>> GetList()
        {
            return new SuccessDataResult<List<ProductListDto>>(await _productRepository.GetList());
        }

        //[SecuredAspect("admin,prdocut.get")]
        public async Task<IDataResult<List<ProductListDto>>> GetProductList(int dealerId)
        {
            return new SuccessDataResult<List<ProductListDto>>(await _productRepository.GetProductList(dealerId));
        }

        // [SecuredAspect("admin,prdocut.get")]
        public async Task<IDataResult<Product>> GetById(int id)
        {
            return new SuccessDataResult<Product>(await _productRepository.Get(p => p.Id == id));
        }

        public async Task<IResult> CheckIfProductExistToBaskets(int productId)
        {
            var result = await _basketService.GetListByProductId(productId);
            if (result.Count() > 0)
            {
                return new ErrorResult("Silmeye çalıştığınız ürün sepette bulunuyor!");
            }
            return new SuccessResult();
        }

        public async Task<IResult> CheckIfProductExistToOrderDetails(int productId)
        {
            var result = await _orderDetailService.GetListByProductId(productId);
            if (result.Count() > 0)
            {
                return new ErrorResult("Silmeye çalıştığınız ürünün siparişi var!");
            }
            return new SuccessResult();
        }

    }
}
