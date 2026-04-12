using eCommerce.SharedLibrary.Responses;
using OderApi.Application.Interface;
using OderApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OderApi.Infrastructure.Repositories
{
    public class OrderRepository() : IOrder
    {
        public Task<Response> CreateAsync(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task<Response> DeleteAsync(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task<Order> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetByAsync(Expression<Func<Order, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetOrdersAsync(Expression<Func<Order, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateAsync(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
