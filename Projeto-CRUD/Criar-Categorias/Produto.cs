using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_CRUD
{
    public class Produto
    {
        string NomeProduto { get; set; }
        string StatusProduto { get; set; }
        string DescricaoProduto { get; set; }
        double PesoProduto { get; set; }
        double AlturaProduto { get; set; }
        double LarguraProduto { get; set; }
        double ComprimentoProduto { get; set; }
        double ValorProduto { get; set; }
        int EstoqueProduto { get; set; }
        string CentroDistribuicaoProduto { get; set; }
        public DateTime DataInicialProduto { get; private set; }
        public DateTime DataModificacaoProduto { get; private set; }


        public Produto(string nomeProduto/*, string descricaoProduto, double pesoProduto, double alturaProduto, double larguraProduto, double comprimentoProduto, double valorProduto, int estoqueProduto, string centroDistribuicaoProduto*/)
        {
            NomeProduto = nomeProduto;
            StatusProduto = "Ativo";
            //DescricaoProduto = descricaoProduto;
            //PesoProduto = pesoProduto;
            //AlturaProduto = alturaProduto;
            //LarguraProduto = larguraProduto;
            //ComprimentoProduto = comprimentoProduto;
            //ValorProduto = valorProduto;
            //EstoqueProduto = estoqueProduto;
            //CentroDistribuicaoProduto = centroDistribuicaoProduto;
            DataInicialProduto = DateTime.Now;
        }

        public static void CadastroProduto(List<Produto> listaProduto)
        {
            Console.WriteLine(" -= Cadastrando produto =- ");
            Console.WriteLine();
            Console.Write("Digite o nome do produto -> ");
            // Recebe o nome digitado
            string nomeProdutoValidar = Console.ReadLine();

            // Valida se é apenas letras e menor de 128 caracteres. 
            if (Helper.ValidaNome(nomeProdutoValidar))
            {
                var produto = new Produto(nomeProdutoValidar);

                listaProduto.Add(produto);
                Console.WriteLine(" \n Produto cadastrado:");
                Console.WriteLine(produto.ToString());
                Helper.FinalMetodo();
            }
            else
            {
                Console.WriteLine("\n Produto não cadastrado: \n  ## ERRO ## Só é permitido letras e até 128 caracteres.");
                Helper.FinalMetodo();
            }
        }

        public override string ToString()
        {
            if (true)
            {
                return "\n  Nome:................." + NomeProduto +
                       "\n  Status:..............." + StatusProduto +
                       "\n  Data da inclusão:....." + DataInicialProduto;
            }
            //return "\n  Nome:................." + NomeCategoria +
            //       "\n  Status:..............." + StatusCategoria +
            //       "\n  Data da inclusão:....." + DataInicialCategoria +
            //       "\n  Data da modificação:.." + DataModificacaoCategoria;
        }

    }
}
