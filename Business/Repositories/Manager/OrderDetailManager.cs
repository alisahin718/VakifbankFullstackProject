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
    public class OrderDetailManager : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailManager(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<IResult> Add(OrderDetail orderDetail)
        {
            await _orderDetailRepository.Add(orderDetail);
            return new SuccessResult(OrderDetailMessages.Added);
        }

        public async Task<IResult> Update(OrderDetail orderDetail)
        {
            await _orderDetailRepository.Update(orderDetail);
            return new SuccessResult(OrderDetailMessages.Updated);
        }

        public async Task<IResult> Delete(OrderDetail orderDetail)
        {
            await _orderDetailRepository.Delete(orderDetail);
            return new SuccessResult(OrderDetailMessages.Deleted);
        }

        public async Task<IDataResult<List<OrderDetail>>> GetList(int orderId)
        {
            return new SuccessDataResult<List<OrderDetail>>(await _orderDetailRepository.GetAll(p => p.OrderId == orderId));
        }

        public async Task<IDataResult<List<OrderDetailDto>>> GetListDto(int orderId)
        {
            return new SuccessDataResult<List<OrderDetailDto>>(await _orderDetailRepository.GetListDto(orderId));
        }

        public async Task<IDataResult<OrderDetail>> GetById(int id)
        {
            return new SuccessDataResult<OrderDetail>(await _orderDetailRepository.Get(p => p.Id == id));
        }

        public async Task<List<OrderDetail>> GetListByProductId(int productId)
        {
            return await _orderDetailRepository.GetAll(p => p.ProductId == productId);
        }
    }
}
