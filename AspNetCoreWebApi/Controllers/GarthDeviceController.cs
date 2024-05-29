using AspNetCoreWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GarthDeviceController : ControllerBase
    {
        WideWorldImportersContext context = new WideWorldImportersContext();


        [HttpGet("GetDevice")]
        //public async Task<string> GetDevice()
        public async Task<ActionResult<GarthDevice>> GetDevice()
        {
            string username = HttpContext.User.Identity.Name;

            var device=await context.GarthDevices.Where(device => device.Username ==username).FirstOrDefaultAsync();
            if (device == null)
            {
                return NotFound();
            }
            return device;
        }


        // GET: api/<GarthDeviceController>
        [HttpGet]
        public IEnumerable<GarthDevice> Get()
        {
            //using (var context = new WideWorldImportersContext())
            
            return context.GarthDevices.ToList();
            
        }

        // GET api/<GarthDeviceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GarthDeviceController>
        [HttpPost("CreateDevice")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GarthDeviceController>/5
        [HttpPut("UpdateDevice/{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        //   api/<GarthDeviceController>/5
        [HttpDelete("DeleteDevice/{id}")]
        public void Delete(int id)
        {
        }
    }
}
