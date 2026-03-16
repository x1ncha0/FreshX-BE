using Freshx_API.Dtos;
using Freshx_API.Dtos.Auth.Role;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Freshx_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestlistController : ControllerBase
    {
        // GET: api/<TestlistController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TestlistController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestlistController>
        [HttpPost]
        public void Post([FromBody] List<RoleResponse> medical)
        {
            Console.WriteLine(medical);
        }

        // PUT api/<TestlistController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestlistController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
