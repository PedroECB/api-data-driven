using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ApiDataDriven.Models;
using ApiDataDriven.Data;

namespace ApiDataDriven.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {
            List<Product> products = new List<Product>();
            try
            {
                products = await context.Product
                .Include(x => x.Category)
                .AsNoTracking()
                .ToListAsync();

                return new JsonResult(products);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Falha ao listar produtos", ErrorMessage = ex.Message });
            }
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> GetById(int id, [FromServices] DataContext context)
        {
            try
            {
                Product product = await context.Product.FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                    return BadRequest(new { Message = "Produto não encontrado" });

                return product;
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Falha ao buscar produto!", ErrorMessage = ex.Message });
            }
        }


        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Product>> Create([FromBody] Product product, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { ErrorMessage = ModelState });

            context.Product.Add(product);
            await context.SaveChangesAsync();

            return Ok(new { Message = "Produto criado com sucesso!", Product = product });
        }


        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> Update(int id, [FromBody] Product product, [FromServices] DataContext context)
        {
            Product productUpdate = await context.Product.FirstOrDefaultAsync(x => x.Id == id);

            if (productUpdate == null || id != product.Id)
                return BadRequest(new { ErrorMessage = "Produto não encontrado" });

            if (!ModelState.IsValid)
                return BadRequest(new { ErrorMessage = ModelState });

            context.Entry<Product>(product);
            await context.SaveChangesAsync();

            return Ok(new { Message = "Produto criado com sucesso!", Product = product });
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> Delete(int id, [FromServices] DataContext context)
        {
            Product productUpdate = await context.Product.FirstOrDefaultAsync(x => x.Id == id);

            if (productUpdate == null)
                return BadRequest(new { ErrorMessage = "Produto não encontrado" });

            context.Product.Remove(productUpdate);
            await context.SaveChangesAsync();

            return Ok(new { Message = "Produto deletado com sucesso!" });
        }




    }
}