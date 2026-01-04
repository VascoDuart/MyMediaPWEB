using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMedia.Data;
using MyMedia.Data.Models;

namespace MyMedia.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase {
        private readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias() {
            return await _context.Categorias.OrderBy(c => c.Nome).ToListAsync();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Funcionario")]
        public async Task<ActionResult<Categoria>> PostCategoria([FromBody] string nome) {
            if (string.IsNullOrWhiteSpace(nome)) {
                return BadRequest("O nome da categoria não pode estar vazio.");
            }

            var novaCategoria = new Categoria { Nome = nome };
            _context.Categorias.Add(novaCategoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategorias), new { id = novaCategoria.CategoriaId }, novaCategoria);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador,Funcionario")]
        public async Task<IActionResult> DeleteCategoria(int id) {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) {
                return NotFound();
            }

            var temProdutos = await _context.Produtos.AnyAsync(p => p.CategoriaId == id);
            if (temProdutos) {
                return BadRequest("Não é possível apagar a categoria porque existem produtos associados a ela.");
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Categoria eliminada com sucesso." });
        }
    }
}