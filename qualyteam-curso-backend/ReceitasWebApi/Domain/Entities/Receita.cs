using System;

namespace ReceitasWebApi.Domain.Entities
{
    public class Receita : Entity
    {
        public string Titulo { get; private set; }
        public string ImagemUrl { get; set; }
        public string Ingredientes { get; set; }
        public string Descricao { get; set; }
        public string MetodoDePreparo { get; set; }
        protected Receita()
        {

        }
        public Receita(string titulo)
        {
            if (String.IsNullOrEmpty(titulo))
            {
                throw new Exception("Titulo da receita não pode ser nula.");
            }

            if(titulo.Length >= 60)
            {
                throw new Exception("O título não pode conter mais de 60 caracteres.");
            }            
            Titulo = titulo;
        }
    }
}