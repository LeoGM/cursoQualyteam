using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReceitasWebApi.Domain.Entities;
using ReceitasWebApi.Domain.Services;
using ReceitasWebApi.Domain.ViewModel;
using ReceitasWebApi.Infrastructure;

namespace ReceitasWebApi.Services
{
    public class ReceitaService : IReceitaService
    {
        private readonly Context _context;
        public ReceitaService(Context context)
        {
            _context = context;
        }

        public async Task<ReceitaViewModel[]> GetAll()
        {            
            return await _context.Receitas.Select(receita => new ReceitaViewModel(){
                Title = receita.Titulo,
                Description = receita.Descricao,
                Id = receita.Id,
                ImageUrl = receita.ImagemUrl,
                Ingredients = receita.Ingredientes,
                Preparation = receita.MetodoDePreparo
            }).ToArrayAsync();
        }

        public async Task<ReceitaViewModel> GetOne(Guid id)
        {
            return await _context.Receitas.Where(receita => receita.Id == id).Select(receita => new ReceitaViewModel(){
                Title = receita.Titulo,
                Description = receita.Descricao,
                Id = receita.Id,
                ImageUrl = receita.ImagemUrl,
                Ingredients = receita.Ingredientes,
                Preparation = receita.MetodoDePreparo
            }).FirstOrDefaultAsync();

            // var receitaRetorno = await _context.Receitas.Where(receita => receita.Id == id).FirstOrDefaultAsync();
             
            // return new ReceitaViewModel(){
            //     Title = receitaRetorno.Titulo,
            //     Description = receitaRetorno.Descricao,
            //     Id = receitaRetorno.Id,
            //     ImageUrl = receitaRetorno.ImagemUrl,
            //     Ingredients = receitaRetorno.Ingredientes,
            //     Preparation = receitaRetorno.MetodoDePreparo
            // };
        }

        public async Task Insert(Receita receita)
        {
            _context.Receitas.Add(receita);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Receita receita)
        {
            var receitaBanco = await _context.Receitas.FindAsync(receita.Id);
           
            if ( receitaBanco!=null )
            {
                throw new Exception("Receita informada não existe");
            }           

            _context.Receitas.Update(receita);
            await _context.SaveChangesAsync();
        }
    }
}