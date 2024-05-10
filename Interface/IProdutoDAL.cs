using EstoqueMVC.Models;

namespace EstoqueMVC.Interface
{
    public interface IProdutoDAL
    {
        List<Produto> listarProduto();
        bool inserirProduto(Produto produto);
        bool alterarProduto(Produto produto);
        List<Produto> pesqProdutoCodigo(int id);
        List<Produto> pesqProdutoNome(string nome);
        List<Produto> pesqProdutoPreco(decimal preco);
        bool excluirProduto(int id);
    }
}
