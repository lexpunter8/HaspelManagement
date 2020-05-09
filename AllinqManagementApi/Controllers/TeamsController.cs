using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllinqManagementApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AllinqManagementApi.Controllers
{
    [Route("api/[controller]")]
    public class TeamsController : Controller
    {
        private IFileService<string> myTeamService;

        public TeamsController(IFileService<string> teamService)
        {
            myTeamService = teamService;
        }
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var result = myTeamService.GetAllData();

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET api/<controller>/5
        [HttpGet("{team}")]
        public IActionResult Get(string team)
        {
            return NotFound();
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]string value)
        {
            myTeamService.Update(value);
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{team}")]
        public IActionResult Delete(string team)
        {
            myTeamService.Remove(team);
            return Ok();
        }
    }
}
