using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ApiDataDriven.Models;
using System.Collections.Generic;

namespace ApiDataDriven.Controllers
{
    [ApiController]
    [Route("categories")]

    //https://localhost:5001/livro
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public List<Category> List()
        {
            List<Category> categories = new List<Category>();

            Category category = new Category("Produto", "Categoria de produtos", "00012231ACA");
            category.Id = 1;

            Category category2 = new Category("Serviços", "Categoria de serviços", "10012231AC3"); ;
            category2.Id = 2;

            categories.Add(category);
            categories.Add(category2);

            return categories;
        }

        [HttpGet]
        [Route("{id:int}")]
        public Category GetById(int id)
        {
            Category category = new Category("Produto", "Categoria de produtos", "00012231ACA");
            category.Id = 1;

            return category;
        }

        [HttpPost]
        [Route("create")]

        public object Create(Category category)
        {
            var objReturn = new { Message = "Produto cadastrado com sucesso!", Category = category };
            return objReturn;
        }

    }
}