using Estacionamento.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.Entities
{
    public class Vaga
    {
        public Guid Id { get; private set; }

        public int Numero { get; private set; }

        public TipoVeiculo Tipo { get; private set; }

        public StatusVaga Status { get; private set; }

        public Vaga(
            int numero, TipoVeiculo tipo
            ) 
        { 
            Id = Guid.NewGuid();
            Numero = numero;
            Tipo = tipo;
            Status = StatusVaga.Disponivel;
        }   

        public void Ocupar()
        {
            if (Status != StatusVaga.Disponivel)
            {
                throw new InvalidOperationException("A vaga não está disponível.");
            }

            Status = StatusVaga.Ocupada;
        }

        public void Liberar()
        {
            if (Status != StatusVaga.Ocupada)
            {
                throw new InvalidOperationException("A vaga não está ocupada.");
            }

            Status = StatusVaga.Disponivel;
        }
    }
}
