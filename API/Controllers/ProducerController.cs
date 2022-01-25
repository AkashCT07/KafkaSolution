using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private ProducerConfig _config;
        public ProducerController(ProducerConfig config)
        {
            this._config = config;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Get(string topic, [FromBody] Employee employee )
        {
            string serializeEmployee = JsonConvert.SerializeObject(employee);
            using (var producer = new ProducerBuilder<Null,string>(_config).Build())
            {
                await producer.ProduceAsync(topic, new Message<Null, string>
                {
                    Value = serializeEmployee
                });
                producer.Flush(TimeSpan.FromSeconds(10));
                return Ok(true);
            }
            
            
        }
    }
}
