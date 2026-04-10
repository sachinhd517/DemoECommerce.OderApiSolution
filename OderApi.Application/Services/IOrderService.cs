using OderApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OderApi.Application.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetOrdersByClientId(int clientId);

        Task<OrderDetailsDTO> GetOrderDetails(int orderId);
        Task<AppUserDTO> GetUser(int userId);
    }
}
