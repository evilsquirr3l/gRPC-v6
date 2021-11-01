using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcService
{
    public class OrderService : Order.OrderBase
    {
        public override Task<Reply> OrderFood(Request request, ServerCallContext context)
        {
            File.AppendAllText("orders.txt", $"{request.Message}{Environment.NewLine}");
            
            return Task.FromResult(new Reply
            {
                Message = "You ordered:" + request.Message
            });
        }
    }
}