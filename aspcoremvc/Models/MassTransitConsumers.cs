using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class SendMessageConsumer : IConsumer<Message>
    {
        //private readonly Int1 service1;
        public SendMessageConsumer()//Int1 parservice)
        {
            //service1 = parservice;
        }
        public Task Consume(ConsumeContext<Message> context)
        {
            //service1.WriteMsg($"Receive message value: {context.Message.Value}");
            Console.WriteLine($"Receive message value: {context.Message.Value}");
            return Task.CompletedTask;
        }
    }
    public class Message
    {
        public string Value { get; set; }
    }
}
