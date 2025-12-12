using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyMedia.Data.Models {
    public class ApplicationUser : IdentityUser {
        public string NomeCompleto { get; set; }

        public bool IsAtivo { get; set; } = false;

        // Adicionar outras propriedades (ex: NIF, Morada, etc.)
    }
}
