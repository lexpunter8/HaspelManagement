using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllinqManagementApi.Adapters;
using AllinqManagementApi.Interfaces;
using DataModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AllinqManagementApi.Controllers
{
    [Route("api/[controller]")]
    public class HaspelController : Controller
    {
        public IFileService<Haspel> myFileService { get; }

        public HaspelController(IFileService<Haspel> fileService)
        {
            myFileService = fileService;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Haspel> Get()
        {
            return myFileService.GetAllData();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var result = myFileService.GetByBarcode(id);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Haspel value)
        {
            myFileService.Update(value);
        }
        
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
