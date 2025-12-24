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
    public class ProdutosController : ControllerBase {
        private readonly ApplicationDbContext _context;

        public ProdutosController(ApplicationDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutos() {
            return await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Fornecedor)
                .Where(p => p.Estado == EstadoProduto.Ativo) 
                .Select(p => new ProdutoDTO {
                    ProdutoId = p.ProdutoId,
                    Titulo = p.Titulo,
                    Descricao = p.Descricao,
                    PrecoFinal = p.PrecoFinal,
                    CategoriaNome = p.Categoria.Nome,
                    FornecedorNome = p.Fornecedor.NomeCompleto,
                    Estado = p.Estado.ToString()
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDTO>> GetProduto(int id) {
            var p = await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(x => x.ProdutoId == id);

            if (p == null) return NotFound();

            return new ProdutoDTO {
                ProdutoId = p.ProdutoId,
                Titulo = p.Titulo,
                Descricao = p.Descricao,
                PrecoFinal = p.PrecoFinal,
                CategoriaNome = p.Categoria.Nome,
                FornecedorNome = p.Fornecedor.NomeCompleto,
                Estado = p.Estado.ToString()
            };
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Fornecedor")]
        public async Task<ActionResult<Produto>> PostProduto(ProdutoCreateDTO dto) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var produto = new Produto {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                PrecoBase = dto.PrecoBase,
                PrecoFinal = dto.PrecoBase, 
                Stock = dto.Stock,
                CategoriaId = dto.CategoriaId,
                FornecedorId = userId,
                Estado = EstadoProduto.Pendente, 
                ParaVenda = true
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduto), new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador, Fornecedor")]
        public async Task<IActionResult> PutProduto(int id, ProdutoCreateDTO dto) {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Administrador");
            if (!isAdmin && produto.FornecedorId != userId) return Forbid();

            produto.Titulo = dto.Titulo;
            produto.Descricao = dto.Descricao;
            produto.PrecoBase = dto.PrecoBase;
            produto.PrecoFinal = dto.PrecoBase; 
            produto.Stock = dto.Stock;
            produto.CategoriaId = dto.CategoriaId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}/ativar")]
        [Authorize(Roles = "Administrador, Funcionario")]
        public async Task<IActionResult> AtivarProduto(int id) {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return NotFound();

            produto.Estado = EstadoProduto.Ativo;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Produto ativado com sucesso e já está visível na loja pública." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteProduto(int id) {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return NotFound();

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}