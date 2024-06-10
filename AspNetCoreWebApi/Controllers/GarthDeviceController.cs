using AspNetCoreWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GarthDeviceController : ControllerBase
    {
        WideWorldImportersContext context = new WideWorldImportersContext();


        [HttpGet("GetDeviceAuthentication")]
        //public async Task<string> GetDevice()
        public async Task<ActionResult<GarthDevice>> GetDeviceAuthentication()
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
        [HttpGet("GetDevice/{id}")]
        public async Task<ActionResult<GarthDevice>> GetDevice(int id)
        {


            var device = await context.GarthDevices.Where(device => device.Id == id).FirstOrDefaultAsync();
            if (device == null)
            {
                return NotFound();
            }
            return device;
        }

        [HttpGet("GetSearchResult")]
        public async Task<ActionResult<string>> GetSearchResult(string searchString)
        {
            var device = await context.GarthDevices.Where(device => device.Id == 1).FirstOrDefaultAsync();
            if (device == null)
            {
                return "Not Found";
            }
            return "Fake result";
        }

        [HttpPost("CreateDevice")]
        public async Task<ActionResult<MyGarthDevice>> PostGarthDevice([FromBody] MyGarthDevice myGarthDevice)
        {
            GarthDevice garthDevice = new GarthDevice();
            garthDevice.CompanyId = myGarthDevice.CompanyId;
            garthDevice.Module = myGarthDevice.Module;
            garthDevice.SerialNumber = myGarthDevice.SerialNumber;
            garthDevice.IpAddress = myGarthDevice.IpAddress;
            garthDevice.Username = myGarthDevice.Username;  
            garthDevice.Password = myGarthDevice.Password;
            garthDevice.PositionOfDevice = myGarthDevice.PositionOfDevice;
            context.GarthDevices.Add(garthDevice);
            await context.SaveChangesAsync();

            return await Task.FromResult(myGarthDevice);
        }
       
        // PUT api/<GarthDeviceController>/5
        [HttpPut("UpdateDevice/{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        //   api/<GarthDeviceController>/5
        [HttpDelete("DeleteDevice/{id}")]
      
        public async Task<bool> DeleteDevice(int id)
        {
            var device = await context.GarthDevices.FindAsync(id);
            if (device == null)
            {
                return false;
            }



            context.GarthDevices.Remove(device);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
