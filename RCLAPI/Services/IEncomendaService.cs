using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCLComum.Models;

namespace RCLAPI.Services {
    public interface IEncomendaService {
        Task<bool> FinalizarEncomendaAsync(EncomendaCreateDTO encomenda);

        Task<List<EncomendaListaDTO>> GetEncomendasClienteAsync();
    }
}
