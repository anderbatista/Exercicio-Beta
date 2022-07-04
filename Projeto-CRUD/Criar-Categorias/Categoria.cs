using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_CRUD
{
    public class Categoria
    {
        // Variáveis da classe.
        public string NomeCategoria { get; private set; }
        public string StatusCategoria { get; private set; }
        public DateTime DataInicialCategoria { get; private set; }
        public DateTime DataModificacaoCategoria { get; private set; }

        //Construtor.
        public Categoria(string nomeCategoria)
        {
            NomeCategoria = nomeCategoria;
            StatusCategoria = "Ativo";
            DataInicialCategoria = DateTime.Now;
        }
        // Cadastrando Categoria. 
        public static void CadastroCategoria(List<Categoria> listaCategoria)
        {
            Console.WriteLine(" -= Criando categoria =- ");
            Console.WriteLine();
            Console.Write("Digite o nome da categoria -> ");
            // Recebe o nome digitado
            string nomeCategoriaValidar = Console.ReadLine();

            // Valida se é apenas letras e menor de 128 caracteres. 
            if (Helper.ValidaNome(nomeCategoriaValidar))
            {
                var categoria = new Categoria(nomeCategoriaValidar);

                listaCategoria.Add(categoria);
                Console.WriteLine(" \n Categoria cadastrada:");
                Console.WriteLine(categoria.ToString());
                Helper.FinalMetodo();
            }
            else
            {
                Console.WriteLine("\n Categoria não cadastrada: \n  ## ERRO ## Só é permitido letras e até 128 caracteres.");
                Helper.FinalMetodo();
            }

        }

        //Método editar.
        public static void EditandoCategoria(List<Categoria> listaCategoria)
        {
            Console.WriteLine(" -= Editando categoria =-\n " +
                              "Selecione a opção desejada:\n\n" +
                              "  1) Alterar nome\n" +
                              "  2) Alterar status\n" +
                              "  3) Voltar\n");
            Console.Write("Opção desejada -> ");
            try
            {
                int opcaoEditar = int.Parse(Console.ReadLine());
                Console.WriteLine("--------------------------");

                while (opcaoEditar != 0)
                {
                    if (opcaoEditar == 1)
                    {
                        // Edita nome Categoria.
                        Console.Write("Qual categoria deseja alterar o nome -> ");
                        string nomeCategoriaParaEditar = Console.ReadLine();

                        var buscaNomeCategoriaParaEditar = listaCategoria.Where(x => x.NomeCategoria.ToLower().Equals(nomeCategoriaParaEditar.ToLower()));
                        if (buscaNomeCategoriaParaEditar.Count() == 0)
                        {
                            Console.WriteLine("Categoria não encontrada\n");
                            Helper.FinalMetodo();
                        }
                        else
                        {
                            Console.Write("\nDigite o novo nome -> ");
                            var novoNomeValidar = Console.ReadLine();
                            if (Helper.ValidaNome(novoNomeValidar))
                            {
                                foreach (var categoria in buscaNomeCategoriaParaEditar)
                                {
                                    categoria.NomeCategoria = novoNomeValidar;
                                    categoria.DataModificacaoCategoria = DateTime.Now;
                                    Console.WriteLine(categoria.ToString());
                                    Helper.FinalMetodo();
                                }
                            }
                            else
                            {
                                Console.WriteLine("\n Não editado: \n  ## ERRO ## Só é permitido letras e até 128 caracteres.");
                                Helper.FinalMetodo();
                            }
                            break;
                        }

                    }
                    else if (opcaoEditar == 2)
                    {
                        // Altera Status Categoria. 
                        Console.WriteLine("Qual categoria deseja alterar o status: ");
                        string nomeCategoriaParaEditar = Console.ReadLine();

                        var buscaNomeCategoriaParaEditar = listaCategoria.Where(x => x.NomeCategoria.ToLower().Equals(nomeCategoriaParaEditar.ToLower()));
                        if (buscaNomeCategoriaParaEditar.Count() == 0)
                        {
                            Console.WriteLine("Categoria não encontrada\n");
                            Helper.FinalMetodo();
                        }
                        else
                        {
                            Console.Write("-> Ativar ou Inativar: a (ativar) i (inativar) ");
                            var novoStatusCategoria = Console.ReadLine();
                            if (novoStatusCategoria == "a")
                            {
                                novoStatusCategoria = "Ativo";
                                foreach (var categoria in buscaNomeCategoriaParaEditar)
                                {
                                    categoria.StatusCategoria = novoStatusCategoria;
                                    categoria.DataModificacaoCategoria = DateTime.Now;
                                    Console.WriteLine(categoria.ToString());
                                }
                                Console.WriteLine("\nCategoria ativada!");
                                Helper.FinalMetodo();
                            }
                            else
                            {
                                novoStatusCategoria = "Inativo";
                                foreach (var categoria in buscaNomeCategoriaParaEditar)
                                {
                                    categoria.StatusCategoria = novoStatusCategoria;
                                    categoria.DataModificacaoCategoria = DateTime.Now;
                                    Console.WriteLine(categoria.ToString());
                                }
                                Console.WriteLine("\nCategoria inativada!");
                                Helper.FinalMetodo();
                            }
                            break;
                        }
                    }
                    else if (opcaoEditar == 3)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Digite um numero válido");
                        Helper.FinalMetodo();
                        break;
                    }

                }
            }
            catch (FormatException)
            {
                string erro = "\nValor inválido\n";
                Console.WriteLine(erro);
                Console.ReadLine();
                Console.Clear();
            }

        }

        public override string ToString()
        {
            if (DataModificacaoCategoria == DateTime.MinValue)
            {
                return "\n  Nome:................." + NomeCategoria +
                       "\n  Status:..............." + StatusCategoria +
                       "\n  Data da inclusão:....." + DataInicialCategoria;
            }
            return "\n  Nome:................." + NomeCategoria +
                   "\n  Status:..............." + StatusCategoria +
                   "\n  Data da inclusão:....." + DataInicialCategoria +
                   "\n  Data da modificação:.." + DataModificacaoCategoria;
        }


    }


}
