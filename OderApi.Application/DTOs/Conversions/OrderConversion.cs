using OderApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OderApi.Application.DTOs.NewFolder
{
    public static class OrderConversion
    {
        public static Order ToEntity(OrderDTO order) => new()
        {
            Id = order.Id,
            ClientId = order.ClientId,
            ProductId = order.ProductId,
            OrderedDate = order.OrderDate,
            PurchaseQuantity = order.PurchaseQuantity,

        };


        public static (OrderDTO?, IEnumerable<OrderDTO>?) FromEntity(Order? order, IEnumerable<Order?> orders)
        {
            // return single order 

            if(order is not null || orders is null)
            {
                var singleOrder = new OrderDTO
                    (
                        order!.Id,
                        order.ClientId,
                        order.ProductId,
                        order.PurchaseQuantity,
                        order.OrderedDate
                    );
                return (singleOrder, null);
            }

            // return all orders list 

            if(orders is not null || order is null)
            {
                var _orders = orders!.Select
                    (
                        o => new OrderDTO
                        (
                            o!.Id,
                            o.ClientId,
                            o.ProductId,
                            o.PurchaseQuantity,
                            o.OrderedDate
                        )
                    );
                return (null, _orders);
            }
            return (null, null);
        }
    }
}
