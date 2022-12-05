using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProducerApplication.Models;

namespace ProducerApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private ProducerConfig _configuration;
        private readonly IConfiguration _config;
        public CarsController(ProducerConfig configuration, IConfiguration config)
        {
            _configuration = configuration;
            _config = config;
        }
        [HttpPost("sendBookingDetails")]
        public async Task<ActionResult> Get([FromBody] CarDetails employee)
        {
            string serializedData = JsonConvert.SerializeObject(employee);

            var topic = _config.GetSection("TopicName").Value;

            using (var producer = new ProducerBuilder<Null, string>(_configuration).Build())
            {
                await producer.ProduceAsync(topic, new Message<Null, string> { Value = serializedData });
                producer.Flush(TimeSpan.FromSeconds(10));
                return Ok(true);
            }
        }
    }
}
