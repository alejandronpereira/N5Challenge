using Application.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IKafkaProducerService
    {
        Task ProduceMessageAsync(string topic, string operation); 
    }
}
