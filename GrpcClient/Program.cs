using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcService;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5000", new GrpcChannelOptions()
            {
                HttpHandler = GetHttpClientHandler()
            });

            // создаем клиента
            var client = new Order.OrderClient(channel);
            while (true)
            {
                Console.Write("Закажите блюдо: ");
                var message = Console.ReadLine();

                // обмениваемся сообщениями с сервером
                var reply = await client.OrderFoodAsync(new Request {Message = message});
                Console.WriteLine(reply.Message);
            }
        }

        private static HttpClientHandler GetHttpClientHandler()
        {
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            return httpHandler;
        }
    }
}