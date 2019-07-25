using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json.Linq;

namespace ShutdownApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ActionsController : ControllerBase
    {
        private static Data.Actions ActionsData = new Data.Actions(1,0,0,0);
        string ActionsJson = JsonConvert.SerializeObject(ActionsData, Formatting.Indented);
        
        [HttpGet]
        [Route("getactions")]
        public JObject Get()
        {
            var json = JsonConvert.DeserializeObject(ActionsJson);
            return JObject.FromObject(json);
        }

        [HttpPost]
        [Route("postactions")]
        public JObject Post([FromBody] Data.Actions jsonResult)
        {
            string strjson = JsonConvert.SerializeObject(jsonResult, Formatting.Indented);
            var json = JsonConvert.DeserializeObject(strjson);
            return JObject.FromObject(json);
        }
    }
}
