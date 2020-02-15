using System;

namespace ReceitasWebApi.Domain.Entities
{
    public class Entity
    {
        public Guid Id { get; }
        public DateTime DataCriacao { get; }

        public Entity()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
        }
    }
}