using AspNetCoreWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Services;
using System.Collections.Generic;
using System.Security.Policy;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreWebApi.Controllers
{
    public class SearchResult
    {
        public string? ColumnName{get;set;}


        public string? ColumnValue { get; set;}
    }

    

    public class SearchResultExtend
    {
        public string? tableName { get; set; }

        public string? tableField { get; set; }

        public string? ColumnValue { get; set; }



        public   List<SearchResultExtend>  transformData(IQueryable<SearchResult> searchResult)
        {
            //if (searchResult.ColumnName != null)
            //{
            //    var ColumnNameSplit= searchResult.ColumnName.Split(".");
            //    tableName = $"{ColumnNameSplit[0]}+{ColumnNameSplit[1]}";
            //    tableColumn = ColumnNameSplit[2];
            //}
            List<SearchResultExtend> searchResultExtendList = new List<SearchResultExtend>();
            foreach (var item in searchResult)
            {
                if (item.ColumnName != null)
                {
                    SearchResultExtend searchResultExtend = new SearchResultExtend();
                    var ColumnNameSplit = item.ColumnName.Split(".");
                    searchResultExtend.tableName= ($"{ColumnNameSplit[0]}.{ColumnNameSplit[1]}");
                    searchResultExtend.tableField = (ColumnNameSplit[2]);
                    searchResultExtend.ColumnValue = item.ColumnValue;
                    searchResultExtendList.Add(searchResultExtend);
                }
            }
            return searchResultExtendList;
        }
    }


    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GarthDeviceController : ControllerBase
    {
        WideWorldImportersContext context = new WideWorldImportersContext();

        private readonly ILogger<GarthDeviceController> _logger;

        public GarthDeviceController(IConfiguration configuration,
        ILogger<GarthDeviceController> logger)
        {
            _logger= logger;
            _logger.LogInformation("GetEmployees");
            _logger.LogError("GetEmployees error testing");
        }

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
        public async Task<ActionResult<MyGarthDevice>> GetDevice(int id)
        {


            var device = await context.GarthDevices.Where(device => device.Id == id).FirstOrDefaultAsync();
            if (device == null)
            {
                return NotFound();
            }
            MyGarthDevice myGarthDevice = new MyGarthDevice();
            myGarthDevice.Id = id;
            myGarthDevice.Module= device.Module;
            var company = await context.GarthCompanies.Where(p => p.Id == device.CompanyId).FirstOrDefaultAsync();
            if (company == null)
            {
                throw new Exception($"CompanyName: {myGarthDevice.CompanyName} Not Found!");
            }
            myGarthDevice.CompanyName = company.Company;
            myGarthDevice.SerialNumber = device.SerialNumber;
            myGarthDevice.IpAddress = device.IpAddress;
            myGarthDevice.Username = device.Username;
            myGarthDevice.Password = device.Password;
            myGarthDevice.PositionOfDevice = device.PositionOfDevice;
            return await Task.FromResult(myGarthDevice);

        }

        [HttpGet("GetCompanyNameList")]
        public  async Task<ActionResult<List<string>>> GetCompanyNameList()
        {


            var companyNameList =  context.GarthCompanies.Select(p => p.Company );
            var companyIdList = context.GarthCompanies.Select(p => p.Id);
            var empty = new List<string>();
            if (companyNameList == null)
            {
                return empty;
            }
            await Task.Delay(1);
            return companyNameList.ToList();


        }
        [HttpGet("GetCompanyNameList1")]
        public async Task<ActionResult<List<GarthCompany>>> GetCompanyNameList1()
        {


            var companyList = context.GarthCompanies;
            var empty = new List<GarthCompany>();
            if (companyList == null)
            {
                return empty;
            }
            await Task.Delay(1);
            
            return companyList.ToList();


        }
        //[HttpGet("GetDevice/{id}")]
        //public async Task<ActionResult<GarthDevice>> GetDevice(int id)
        //{


        //    var device = await context.GarthDevices.Where(device => device.Id == id).FirstOrDefaultAsync();
        //    if (device == null)
        //    {
        //        return NotFound();
        //    }
        //    return device;
        //}



        [HttpGet("GetSearchResultExtend")]
        //public async Task<ActionResult<List<SearchResult>?>> GetSearchResult(string searchString)
        public async Task<ActionResult<List<SearchResultExtend>?>> GetSearchResultExtend(string searchString)
        {
            //var device = context.GarthDevices.FromSql($"EXEC [SearchAllTables] {searchString}");
            var device = context.Database.SqlQuery<SearchResult>($"EXEC [SearchAllTables] {searchString}");

            SearchResultExtend searchResultExtend = new SearchResultExtend();
            var SearchInfo=searchResultExtend.transformData(device);

            List<SearchResultExtend> empty = new List<SearchResultExtend>();
            if (device == null)
            {
                return empty;
            }
            await Task.Delay(1);
            //return device.ToList();
            return SearchInfo.ToList();

        }


        [HttpGet("GetSearchResult")]
        public async Task<ActionResult<List<SearchResult>?>> GetSearchResult(string searchString)
        
        {
            //var device = context.GarthDevices.FromSql($"EXEC [SearchAllTables] {searchString}");
            var device = context.Database.SqlQuery<SearchResult>($"EXEC [SearchAllTables] {searchString}");

            

            List<SearchResult> empty = new List<SearchResult>();
            if (device == null)
            {
                return empty;
            }
            await Task.Delay(1);
            return device.ToList();
            

        }
        [HttpPost("CreateDevice")]
        public async Task<ActionResult<MyGarthDevice>> PostGarthDevice([FromBody] MyGarthDevice myGarthDevice)
        {
            GarthDevice garthDevice = new GarthDevice();
            var company = await context.GarthCompanies.Where(p => p.Company == myGarthDevice.CompanyName).FirstOrDefaultAsync();
            if (company == null)
            {
                throw new Exception($"CompanyName: {myGarthDevice.CompanyName} Not Found!");
            }
            garthDevice.CompanyId = company.Id;
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
