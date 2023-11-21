using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class KafkaMessageDto
    {
        public Guid Id { get; set; }
        public string NameOperation { get; set; }
    }
}
