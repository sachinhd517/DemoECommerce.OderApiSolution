using eCommerce.SharedLibrary.Interface;
using OderApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OderApi.Application.Interface
{
    internal interface IOrder : IGenericInterface<Order>
    {
        Task<IEnumerable<Order>> GetOrdersAsync(Expression<Func<Order, bool>> predicate);
    }
}
