using System;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ReceitasWebApi.Domain.Entities;
using ReceitasWebApi.Domain.Services;
using ReceitasWebApi.Infrastructure;
using ReceitasWebApi.Services;
using Xunit;

namespace ReceitasWebApiTests.Domain.Services
{
    public class ReceitaServiceTests
    {
        IReceitaService _service;
        Context _context;

        public ReceitaServiceTests()
        {
            var option = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new Context(option);
            _service = new ReceitaService(_context);
        }


        [Fact]
        public void Insert_DeveSalvarUmaReceitaComTodosAtributos()
        {
            var novaReceita = new Receita("Feijoada")
            {
                Descricao = "Crime ocorre nada acontece feijoada",
                Ingredientes = "Feijoada, linguiça, porco, koerano",
                MetodoDePreparo = "BRASILEIRO É PORCO",
                ImagemUrl = "https://i.redd.it/wws6jjswpq2z.jpg",
            };

            _service.Insert(novaReceita);

            _context.Receitas
                .Should()
                .HaveCount(1, "Ocorreu uma falha ao inserir os dados no banco");

            var receitaDoBanco = _context.Receitas.FirstOrDefault();
            receitaDoBanco.Should().Be(novaReceita, "Receita inserida pelo usuário deveria ser igual a receita inserida no banco de dados");            
        }

        [Fact]
        public void Insert_TituloTemMaisDe60Caracteres()
        {
            var novaReceita = new Receita("Crime ocorre nada acontece")
            {
                Descricao = "Feijoada deliciosa",
                ImagemUrl = "C:/Users/leoramon/imagem.png",
                MetodoDePreparo = "Muito bacon e feijão",
                Ingredientes = "Feijao, carne de porco e bacon"
            };
    
            _service.Insert(novaReceita);

            _context.Receitas
                .Should()
                .HaveCount(1);
        }        

        [Fact]
        public void Insert_DeveSalvarUmaReceitaComTituloInformado()
        {
            var novaReceita = new Receita("Feijoada");

            _service.Insert(novaReceita);

            _context.Receitas
                .Should()
                .HaveCount(1, "Não contém no banco de dados");

            var receitaDoBanco = _context.Receitas.FirstOrDefault();
            receitaDoBanco.Titulo.Should().Be(novaReceita.Titulo, "Titulo da Receita inserida pelo usuário deveria ser igual a receita inserida no banco de dados");
        }

        [Fact]
        public void Insert_TituloObrigatorio()
        {
            var novaReceita = new Receita("Feijoada")
            {
                Descricao = "Feijoada deliciosa",
                ImagemUrl = "C:/Users/leoramon/imagem.png",
                MetodoDePreparo = "Muito bacon e feijão",
                Ingredientes = "Feijao, carne de porco e bacon"
            };
            
            _service.Insert(novaReceita);

            _context.Receitas
                .Should()
                .HaveCount(1);
        }

        [Fact]
        public async void GetOne_RetornaUmaReceita()
        {
            var novaReceita = new Receita("Feijoada")
            {
                Descricao = "Feijoada deliciosa",
                ImagemUrl = "C:/Users/leoramon/imagem.png",
                MetodoDePreparo = "Muito bacon e feijão",
                Ingredientes = "Feijao, carne de porco e bacon"
            };
            
            await _service.Insert(novaReceita);
            _context.SaveChanges();

            var retorno = await _service.GetOne(novaReceita.Id);

            retorno.Title.Should().Be(novaReceita.Titulo); 
            retorno.ImageUrl.Should().Be(novaReceita.ImagemUrl); 
            retorno.Ingredients.Should().Be(novaReceita.Ingredientes); 
            retorno.Preparation.Should().Be(novaReceita.MetodoDePreparo); 
            retorno.Description.Should().Be(novaReceita.Descricao); 

            _context.Receitas
                .Should()
                .HaveCount(1);
        }       

        [Fact]
        public async void GetAll_RetornaTodasAsReceitas()
        {
            var feijoada = new Receita("Feijoada")
            {
                Descricao = "Feijoada deliciosa",
                ImagemUrl = "C:/Users/leoramon/imagem.png",
                MetodoDePreparo = "Muito bacon e feijão",
                Ingredientes = "Feijao, carne de porco e bacon"
            };
            
            var coxinha = new Receita("Coxinha")
            {
                Descricao = "Coxinha deliciosa",
                ImagemUrl = "C:/Users/leoramon/imagem.png",
                MetodoDePreparo = "Muito bacon e feijão",
                Ingredientes = "Feijao, carne de porco e bacon"
            };

            var pizza = new Receita("Pizza")
            {
                Descricao = "Pizza deliciosa",
                ImagemUrl = "C:/Users/leoramon/imagem.png",
                MetodoDePreparo = "Muito bacon e feijão",
                Ingredientes = "Feijao, carne de porco e bacon"
            };

            await _context.Receitas.AddRangeAsync(feijoada,coxinha,pizza);
            _context.SaveChanges();

            var retorno = await _service.GetAll();

            _context.Receitas
                .Should()
                .HaveCount(3);
                       
            var pizzaDoRetorno = retorno.FirstOrDefault(receita => receita.Title == pizza.Titulo);
            pizzaDoRetorno.Should().NotBeNull();

            pizzaDoRetorno = retorno.FirstOrDefault(receita => receita.Preparation == pizza.MetodoDePreparo);
            pizzaDoRetorno.Should().NotBeNull();
            
            pizzaDoRetorno = retorno.FirstOrDefault(receita => receita.Ingredients == pizza.Ingredientes);
            pizzaDoRetorno.Should().NotBeNull();

            pizzaDoRetorno = retorno.FirstOrDefault(receita => receita.ImageUrl == pizza.ImagemUrl);
            pizzaDoRetorno.Should().NotBeNull();

            pizzaDoRetorno = retorno.FirstOrDefault(receita => receita.Description == pizza.Descricao);
            pizzaDoRetorno.Should().NotBeNull();
        }

        // [Fact]
        // public async void Update_AtualizaUmaReceita()
        // {
        //     var feijoada = new Receita("Feijoada")
        //     {
        //         Descricao = "Feijoada deliciosa",
        //         ImagemUrl = "C:/Users/leoramon/imagem.png",
        //         MetodoDePreparo = "Muito bacon e feijão",
        //         Ingredientes = "Feijao, carne de porco e bacon"
        //     };

        //     await _context.Receitas.AddRangeAsync(feijoada);
        //     _context.SaveChanges();

        //     var retorno = await _service.GetOne(feijoada.Id);
        //     retorno.Description = "Feijoada Ruim";

        //     _service.Update(retorno);
        // }
    }
}