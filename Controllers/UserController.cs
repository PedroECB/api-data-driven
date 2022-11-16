using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApiDataDriven.Models;
using ApiDataDriven.Data;
using ApiDataDriven.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ApiDataDriven.Controllers
{
    [ApiController]
    [Route("users")]

    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<User>> Post([FromBody] User userModel, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.User.Add(userModel);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Falha ao criar usuário", ErrorMessage = ex.Message });
            }

            return new JsonResult(new { Message = "Usuário cadastrado com sucesso!", User = userModel });
        }


        [HttpPost]
        [Route("autenticar")]
        public async Task<ActionResult<object>> Autenticar([FromBody] User userModel, [FromServices] DataContext context)
        {
            var user = await context.User.Where(x => x.Name == userModel.Name && x.Email == userModel.Email).FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new { Message = "Usuário inexistente ou senha inválida", Code = 404 });

            var token = Services.TokenService.GenerateToken(user);

            return new JsonResult(new { User = user, Token = token });
        }


        [HttpGet]
        [Route("anonimo")]
        [AllowAnonymous]
        public object Anonimo()
        {
            var teste = HttpContext.User.Identity.Name;
            return new JsonResult(new { Id = teste });
        }

        [HttpGet]
        [Route("autenticado")]
        [Authorize]
        public object Autenticado()
        {
            var teste = HttpContext.User.Identity.Name;
            return new JsonResult(new { Id = int.Parse(teste) });
        }

        [HttpGet]
        [Route("gerente")]
        [Authorize(Roles = "ADMIN")]
        public string Admin() => "Admin";

        [HttpGet]
        [Route("cliente")]
        [Authorize(Roles = "CLIENTE")]
        public string Cliente() => "Cliente";
    }
}