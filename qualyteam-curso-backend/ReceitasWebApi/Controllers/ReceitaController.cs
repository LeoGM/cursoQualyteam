using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReceitasWebApi.Domain.Entities;
using ReceitasWebApi.Domain.Services;
using ReceitasWebApi.Domain.ViewModel;

namespace ReceitasWebApi.Controllers
{
    [ApiController]
    [Route("food")]
    public class ReceitaController : ControllerBase
    {
        private readonly IReceitaService _service;
        public ReceitaController(IReceitaService service)
        { 
            _service = service;
        }

        [HttpGet]
        public async Task<ReceitaViewModel[]> GetAllAsync()
        {
            var result = await _service.GetAll();   
            return result;
        }

        [HttpPost]
        public async Task InsertAsync([FromBody] ReceitaViewModel request)
        { 
            var receita = new Receita(request.Title)
            {
                ImagemUrl = request.ImageUrl,
                Ingredientes = request.Ingredients,
                Descricao = request.Description,
                MetodoDePreparo = request.Preparation
            };
            await _service.Insert(receita);
        }

        [HttpPut("{id:guid}")]
        public async Task UpdateAsync()
        {
            
        }
    }

}
