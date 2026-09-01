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
        Task<Veiculo?> ObterPorPlacaAsync(string placa);
        Task<IEnumerable<Veiculo?>> ListarAsync();
        Task AdicionarAsync(Veiculo veiculo);
        Task RemoverAsync(Guid id);
    }
}