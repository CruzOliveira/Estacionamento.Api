using Estacionamento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.Interfaces
{
    public interface IVeiculoRepository
    {
        Task<Veiculo?> ObterPorIdAsync(Guid id);
        Task<Veiculo?> ObterPorPlacaAsynk(string placa);
        Task<IEnumerable<Veiculo?>> ListarAsynk();
        Task AdicionarAsync(Veiculo veiculo);
        Task RemoverAsync(Guid id);
    }
}