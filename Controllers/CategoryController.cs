using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApiDataDriven.Models;
using ApiDataDriven.Data;
using Microsoft.AspNetCore.Authorization;

namespace ApiDataDriven.Controllers
{
    [ApiController]
    [Route("categories")]

    //https://localhost:5001/livro

    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Category>>> List([FromServices] DataContext context)
        {
            List<Category> categories = new List<Category>();

            try
            {
                categories = await context.Categories.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Falha ao listar categorias", ErrorMessage = ex.Message });
            }

            // Tipos de retorno
            /*
             * Ok(object);
             * NotFound();
             * BadRequest();
             * JsonResult(object)
             */

            return new JsonResult(categories);
        }


        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Category>> GetById(int id, [FromServices] DataContext context)
        {
            Category category;

            category = await context.Categories.Where(c => c.Id == id).AsNoTracking().FirstOrDefaultAsync();

            if (category == null)
                return NotFound(new { Message = "Categoria não encontrada!" });

            return Ok(category);
        }


        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<object>> Create([FromBody] Category category, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Add(category);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Não foi possível criar a categoria", ErrorExcepition = ex.Message });
            }

            object objReturn = new { Message = "Categoria cadastrada com sucesso!", Category = category };
            return new JsonResult(objReturn);
        }


        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<Category>> Put(int id, [FromBody] Category category, [FromServices] DataContext context)
        {
            if (id != category.Id)
                return BadRequest(new { Message = "Categoria não encontrada!" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Category>(category).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return category;
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Falha ao atualizar categoria", ErrorMessage = ex.Message });
            }
        }


        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<object>> Delete(int id, [FromServices] DataContext context)
        {
            Category category = context.Categories.Where(c => c.Id == id).FirstOrDefault();

            if (category == null)
                return NotFound(new { Message = "Categoria não encontrada" });

            try
            {
                context.Remove<Category>(category);
                await context.SaveChangesAsync();
                return new JsonResult(new { Id = id, Message = "Categoria deletada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Falha ao deletar a categoria!", ErrorMessage = ex.Message });
            }
        }


    }
}