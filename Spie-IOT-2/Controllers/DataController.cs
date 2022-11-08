using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using InfluxDB.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Spie_IOT_2;

namespace Spie_IOT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        public DataController()
        {

        }

        [HttpPost(Name = "SendMeasure")]
        public IActionResult SendMeasure([FromBody] SendMeasureBodyModel bodyModel)
        {
            try
            {
                var value = Convert.ToInt32(bodyModel.Data.Substring(2), 16)/10;
                var code = bodyModel.Data.Remove(2);

                var influxDBClient = InfluxDBClientFactory.Create("http://localhost:8086", "FAWjcsemACi9nLiSo2Pw0edaZGkuwD8XYb2VAeRytUDVNtP1Q_0po1DghOqXbIffk0GDInhNLPlV2M-6XJdRAA==");

                using (var writeApi = influxDBClient.GetWriteApi())
                {
                    var point = PointData.Measurement("temperature")
                        .Tag("code", code)
                        .Field("value", value);

                    writeApi.WritePoint(point, "Test4", "Test");
                }

                influxDBClient.Dispose();

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
