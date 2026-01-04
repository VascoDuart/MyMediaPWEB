using RCLComum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLComum.Services {
    public class CarrinhoService {
        public List<CarrinhoItem> Itens { get; private set; } = new();

        public event Action? OnCarrinhoAlterado;

        public void Adicionar(ProdutoDTO produto) {
            var item = Itens.FirstOrDefault(i => i.ProdutoId == produto.ProdutoId);
            if (item == null) {
                Itens.Add(new CarrinhoItem {
                    ProdutoId = produto.ProdutoId,
                    Titulo = produto.Titulo,
                    Preco = produto.PrecoFinal,
                    Quantidade = 1
                });
            }
            else {
                item.Quantidade++;
            }
            OnCarrinhoAlterado?.Invoke();
        }

        public void Limpar() {
            Itens.Clear();
            OnCarrinhoAlterado?.Invoke();
        }

        public decimal ValorTotal => Itens.Sum(i => i.Total);
    }
}
