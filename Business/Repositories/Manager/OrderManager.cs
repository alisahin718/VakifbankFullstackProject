using Business.Repositories.Messages;
using Business.Repositories.Service;
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
    public class OrderManager : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IBasketService _basketService;

        public OrderManager(IOrderRepository orderRepository, IOrderDetailService orderDetailService, IBasketService basketService)
        {
            _orderRepository = orderRepository;
            _orderDetailService = orderDetailService;
            _basketService = basketService;
        }

        public async Task<IResult> Add(int dealerId)
        {
            var baskets = await _basketService.GetListByDealerId(dealerId);

            string newOrderNumber = _orderRepository.GetOrderNumber();
            Order order = new()
            {
                Id = 0,
                DealerId = baskets.Data[0].DealerId,
                Date = DateTime.Now,
                OrderNumber = newOrderNumber,
                Status = "Onay Bekliyor"
            };
            await _orderRepository.Add(order);

            foreach (var basket in baskets.Data)
            {
                OrderDetail orderDetail = new()
                {
                    Id = 0,
                    OrderId = order.Id,
                    Price = basket.Price,
                    ProductId = basket.ProductId,
                    Quantity = basket.Quantity
                };
                await _orderDetailService.Add(orderDetail);

                Basket basketEntity = new()
                {
                    Id = basket.Id,
                    DealerId = basket.DealerId,
                    Price = basket.Price,
                    Quantity = basket.Quantity,
                    ProductId = basket.ProductId
                };
                await _basketService.Delete(basketEntity);
            }
            return new SuccessResult(OrderMessages.Added);
        }

        

        public async Task<IResult> Update(Order order)
        {
            await _orderRepository.Update(order);
            return new SuccessResult(OrderMessages.Updated);
        }

        public async Task<IResult> Delete(Order order)
        {
            var details = await _orderDetailService.GetList(order.Id);
            foreach (var detail in details.Data)
            {
                await _orderDetailService.Delete(detail);
            }

            await _orderRepository.Delete(order);
            return new SuccessResult(OrderMessages.Deleted);
        }

        public async Task<IDataResult<List<Order>>> GetList()
        {
            return new SuccessDataResult<List<Order>>(await _orderRepository.GetAll());
        }

        public async Task<IDataResult<List<OrderDto>>> GetListDto()
        {
            return new SuccessDataResult<List<OrderDto>>(await _orderRepository.GetListDto());
        }

        public async Task<IDataResult<List<OrderDto>>> GetListByDealerIdDto(int dealerId)
        {
            return new SuccessDataResult<List<OrderDto>>(await _orderRepository.GetListByDealerIdDto(dealerId));
        }

        public async Task<IDataResult<List<Order>>> GetListByDealerId(int dealerId)
        {
            return new SuccessDataResult<List<Order>>(await _orderRepository.GetAll(p => p.DealerId == dealerId));
        }

        public async Task<IDataResult<Order>> GetById(int id)
        {
            return new SuccessDataResult<Order>(await _orderRepository.Get(p => p.Id == id));
        }

        public async Task<IDataResult<OrderDto>> GetByIdDto(int id)
        {
            return new SuccessDataResult<OrderDto>(await _orderRepository.GetByIdDto(id));
        }

    }
}
