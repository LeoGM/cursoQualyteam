using System;
using System.Threading.Tasks;
using ReceitasWebApi.Domain.Entities;
using ReceitasWebApi.Domain.ViewModel;

namespace ReceitasWebApi.Domain.Services
{
    public interface IReceitaService
    {
        Task Insert(Receita receita);
        Task Update(Receita receita);
        Task<ReceitaViewModel[]> GetAll();
        Task<ReceitaViewModel> GetOne(Guid id);
    }
}