using OderApi.Application.DTOs;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace OderApi.Application.Services
{
    public class OderService(HttpClient httpClient, ResiliencePipeline<string> resiliencePipeline) : IOrderService
    {
        // GET PRODUCT
        public async Task<ProductDTO> GetOrders(int productId)
        {
            // call Product API ussing HttpClient and Polly for resilience
            // Rediect the call to Product API Geteway since product API is not reponse to outer world
    
            var getProduct = await httpClient.GetAsync($"/api/Product/{productId}");
            if(!getProduct.IsSuccessStatusCode)
            {
                return null!;
            }
            var product = await getProduct.Content.ReadFromJsonAsync<ProductDTO>();
            return product!;
        }
        public Task<OrderDetailsDTO> GetOrderDetails(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderDTO>> GetOrdersByClientId(int clientId)
        {
            throw new NotImplementedException();
        }
    }
}
