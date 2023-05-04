using System.Reflection.PortableExecutable;
using System.Resources;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FilesSystem.Controllers
{
    //[EnableCors(origins: "", headers: "", methods: "*")]
    [ApiController]
    [Route("api/[controller]")]

    public class FilesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok(ResourceReader.Read());
        }

        [HttpGet("{q}")]
        public ActionResult<List<Directory>> GetByPrefix(string q)
        {
            return Ok(ResourceReader.GetByPrefix(q));
        }
    }
}
