using OderApi.Application.DTOs;
using OderApi.Application.Interface;
using Polly;
using Polly.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace OderApi.Application.Services
{
    public class OderService(IOrder orderInterface, HttpClient httpClient, ResiliencePipelineProvider<string> resiliencePipeline) : IOrderService
    {
        // GET PRODUCT
        public async Task<ProductDTO> GetProduct(int productId)
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

        // GET USER

        public async Task<AppUserDTO> GetUser(int userId)
        {
            // call Product API ussing HttpClient and Polly for resilience
            // Rediect the call to Product API Geteway since product API is not reponse to outer world

            var getUser = await httpClient.GetAsync($"/api/Product/{userId}");
            if (!getUser.IsSuccessStatusCode)
            {
                return null!;
            }
            var product = await getUser.Content.ReadFromJsonAsync<AppUserDTO>();
            return product!;
        }

        // GET ORDER DETAILS BY ORDER ID
        public async Task<OrderDetailsDTO> GetOrderDetails(int orderId)
        {
            var order = await orderInterface.FindByIdAsync(orderId);
            if (order is null || order!.Id <= 0)
            {
                return null!;
            }
            // Get Retry pipeline from Polly registry
            var retryPipeline = resiliencePipeline.GetPipeline("my-retry-pipeline");

            // prepare Product
            var productDTO = await retryPipeline.ExecuteAsync(async token => await GetProduct(order.ProductId));

            //Prepare Client
            var appUserDTO = await retryPipeline.ExecuteAsync(async token => await GetUser(order.ClientId));

            // Populate order Details
            return new OrderDetailsDTO(
                order.Id,
                productDTO.Id,
                appUserDTO.Id,
                appUserDTO.Name,
                appUserDTO.Email,
                appUserDTO.Address,
                appUserDTO.TelephoneNumber,
                productDTO.Name,
                order.PurchaseQuantity,
                productDTO.Price,
                productDTO.Quantity * order.PurchaseQuantity,
                order.OrderedDate

                );
        
        }

        public Task<IEnumerable<OrderDTO>> GetOrdersByClientId(int clientId)
        {
            throw new NotImplementedException();
        }
    }
}
