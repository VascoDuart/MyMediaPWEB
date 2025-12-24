using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMedia.API.DTOs;
using MyMedia.Data;
using MyMedia.Data.Models;
using System.Security.Claims;

namespace MyMedia.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EncomendasController : ControllerBase {
        private readonly ApplicationDbContext _context;

        public EncomendasController(ApplicationDbContext context) {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "Cliente")]
        public async Task<ActionResult> Criar(EncomendaCreateDTO dto) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var encomenda = new Encomenda {
                DataEncomenda = DateTime.Now,
                ClienteId = userId,
                EstadoEncomenda = "Pendente", 
                Itens = new List<ItemEncomenda>(),
                ValorTotal = 0
            };

            foreach (var itemDto in dto.Itens) {
                var produto = await _context.Produtos.FindAsync(itemDto.ProdutoId);

                if (produto == null) return BadRequest($"Produto {itemDto.ProdutoId} não existe.");
                if (produto.Stock < itemDto.Quantidade) return BadRequest($"Stock insuficiente para {produto.Titulo}.");

                var item = new ItemEncomenda {
                    ProdutoId = itemDto.ProdutoId,
                    Quantidade = itemDto.Quantidade,
                    PrecoVendaUnitario = produto.PrecoFinal
                };

                encomenda.Itens.Add(item);
                encomenda.ValorTotal += (item.PrecoVendaUnitario * item.Quantidade);

                produto.Stock -= itemDto.Quantidade;
            }

            _context.Encomendas.Add(encomenda);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Encomenda criada!", id = encomenda.EncomendaId, total = encomenda.ValorTotal });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetEncomendas() {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdminOrFunc = User.IsInRole("Administrador") || User.IsInRole("Funcionario");

            var query = _context.Encomendas
                .Include(e => e.Cliente)
                .Include(e => e.Itens)
                .AsQueryable();

            if (!isAdminOrFunc) {
                query = query.Where(e => e.ClienteId == userId);
            }

            return await query.Select(e => new {
                e.EncomendaId,
                e.DataEncomenda,
                e.ValorTotal,
                e.EstadoEncomenda,
                Cliente = e.Cliente.NomeCompleto,
                QtdItens = e.Itens.Count
            }).ToListAsync();
        }

        [HttpPatch("{id}/estado")]
        [Authorize(Roles = "Administrador, Funcionario")]
        public async Task<IActionResult> MudarEstado(int id, [FromBody] string novoEstado) {
            var encomenda = await _context.Encomendas.FindAsync(id);
            if (encomenda == null) return NotFound();

            encomenda.EstadoEncomenda = novoEstado;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Estado atualizado com sucesso." });
        }
    }
}