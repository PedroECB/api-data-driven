using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ApiDataDriven.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiDataDriven.Data;

namespace ApiDataDriven.Controllers
{
    [ApiController]
    [Route("categories")]

    //https://localhost:5001/livro


    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> List()
        {
            List<Category> categories = new List<Category>();

            Category category = new Category("Produto", "Categoria de produtos", "00012231ACA");
            category.Id = 1;

            Category category2 = new Category("Serviços", "Categoria de serviços", "10012231AC3"); ;
            category2.Id = 2;

            Category category3 = new Category("Comunicação", "Categoria de comunicação", "21012211AA4"); ;
            category2.Id = 2;

            categories.Add(category);
            categories.Add(category2);
            categories.Add(category3);

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
        public async Task<ActionResult<Category>> GetById(int id)
        {
            Category category = new Category("Produto", "Categoria de produtos", "00012231ACA");
            category.Id = 1;

            return new JsonResult(category);
        }


        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<object>> Create([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            object objReturn = new { Message = "Categoria cadastrado com sucesso!", Category = category };
            return new JsonResult(objReturn);
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Put(int id, [FromBody] Category category)
        {
            if (id != category.Id)
                return BadRequest(new { Message = "Categoria não encontrada!" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return new JsonResult(category);
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<object>> Delete(int id)
        {
            return new JsonResult(new { Id = id, Message = "Categoria deletada com sucesso!" });
        }

    }
}