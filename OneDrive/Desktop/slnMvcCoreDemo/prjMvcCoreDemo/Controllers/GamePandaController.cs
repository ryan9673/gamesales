using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace prjMvcCoreDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamePandaController : ControllerBase
    {
        // GET: api/GamePandaController
        [HttpGet]
        public IEnumerable<TProduct> Get()//Web API好處用網址驅動,詳情見slnGamePandaApp
        {
            DbDemoContext db = new DbDemoContext();
            var datas = from p in db.TProducts
                        select p;
            foreach (var items in datas)
            {
                items.FCost = 0;
                if (items.FQty>1000)
                    items.FQty= 1000;
                
            }
            return datas;
        }

        // GET api/<GamePandaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GamePandaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GamePandaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GamePandaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
