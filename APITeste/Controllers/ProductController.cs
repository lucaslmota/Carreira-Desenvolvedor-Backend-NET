using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APITeste.Data;
using APITeste.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APITeste.Controllers
{
    [Route("product")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContexte contexte)
        {
            var product = await contexte.Products.Include(x => x.Category).AsNoTracking().ToListAsync();
            return product;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> GetId(int id, [FromServices] DataContexte contexte)
        {
            var product = await contexte.Products.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        [HttpGet]
        [Route("categoris/{id:int}")]
        public async Task<ActionResult<List<Product>>> GetByCategory([FromServices] DataContexte contexte, int id)
        {
            var product = await contexte.Products.Include(x => x.CategoryId).AsNoTracking().Where(x => x.Id == id).ToListAsync();
            return product;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post([FromBody] Product product, [FromServices] DataContexte contexte)
        {
            if (ModelState.IsValid)
            {
                contexte.Products.Add(product);
                await contexte.SaveChangesAsync();
                return product;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }

}