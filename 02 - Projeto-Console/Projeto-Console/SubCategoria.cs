using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_CRUD
{
    public class SubCategoria
    {
        public string NomeSubCategoria { get; private set; }
        public string StatusSubCategoria { get; private set; }
        public DateTime DataInicialSubCategoria { get; private set; }
        public DateTime DataModificacaoSubCategoria { get; private set; }

        public SubCategoria(string nomeSubCategoria)
        {
            NomeSubCategoria = nomeSubCategoria;
            StatusSubCategoria = "Ativo";
            DataInicialSubCategoria = DateTime.Now;
        }
        // Cadastrando SubCategoria. 
        public static void CadastroSubCategoria(List<SubCategoria> listaSubCategoria)
        {
            Console.WriteLine(" -= Criando subcategoria =- ");
            Console.WriteLine();
            Console.Write("Digite o nome da subcategoria -> ");
            // Recebe o nome digitado
            string nomeSubCategoriaValidar = Console.ReadLine();

            // Valida se é apenas letras e menor de 128 caracteres. 
            if (Helper.ValidaNome(nomeSubCategoriaValidar))
            {
                var subcategoria = new SubCategoria(nomeSubCategoriaValidar);

                listaSubCategoria.Add(subcategoria);
                Console.WriteLine(" \n SubCategoria cadastrada:");
                Console.WriteLine(subcategoria.ToString());
                Helper.FinalMetodo();
            }
            else
            {
                Console.WriteLine("\n SubCategoria não cadastrada: \n  ## ERRO ## Só é permitido letras e até 128 caracteres.");
                Helper.FinalMetodo();
            }

        }

        public static void EditandoSubCategoria(List<SubCategoria> listaSubCategoria)
        {
            Console.WriteLine(" -= Editando subcategoria =-\n " +
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
                        // Edita nome SubCategoria.
                        Console.Write("Qual subcategoria deseja alterar o nome -> ");
                        string nomeSubCategoriaParaEditar = Console.ReadLine();

                        var buscaNomeSubCategoriaParaEditar = listaSubCategoria.Where(x => x.NomeSubCategoria.ToLower().Equals(nomeSubCategoriaParaEditar.ToLower()));
                        if (buscaNomeSubCategoriaParaEditar.Count() == 0)
                        {
                            Console.WriteLine("SubCategoria não encontrada\n");
                            Helper.FinalMetodo();
                        }
                        else
                        {
                            Console.Write("\nDigite o novo nome -> ");
                            var novoNomeValidar = Console.ReadLine();
                            if (Helper.ValidaNome(novoNomeValidar))
                            {
                                foreach (var subcategoria in buscaNomeSubCategoriaParaEditar)
                                {
                                    subcategoria.NomeSubCategoria = novoNomeValidar;
                                    subcategoria.DataModificacaoSubCategoria = DateTime.Now;
                                    Console.WriteLine(subcategoria.ToString());
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
                        // Altera Status SubCategoria. 
                        Console.WriteLine("Qual subcategoria deseja alterar o status: ");
                        string nomeSubCategoriaParaEditar = Console.ReadLine();

                        var buscaNomeSubCategoriaParaEditar = listaSubCategoria.Where(x => x.NomeSubCategoria.ToLower().Equals(nomeSubCategoriaParaEditar.ToLower()));
                        if (buscaNomeSubCategoriaParaEditar.Count() == 0)
                        {
                            Console.WriteLine("SubCategoria não encontrada\n");
                            Helper.FinalMetodo();
                        }
                        else
                        {
                            Console.Write("-> Ativar ou Inativar: a (ativar) i (inativar) ");
                            var novoStatusSubCategoria = Console.ReadLine();
                            if (novoStatusSubCategoria == "a")
                            {
                                novoStatusSubCategoria = "Ativo";
                                foreach (var subcategoria in buscaNomeSubCategoriaParaEditar)
                                {
                                    subcategoria.StatusSubCategoria = novoStatusSubCategoria;
                                    subcategoria.DataModificacaoSubCategoria = DateTime.Now;
                                    Console.WriteLine(subcategoria.ToString());
                                }
                                Console.WriteLine("\nSubCategoria ativada!");
                                Helper.FinalMetodo();
                            }
                            else
                            {
                                novoStatusSubCategoria = "Inativo";
                                foreach (var subcategoria in buscaNomeSubCategoriaParaEditar)
                                {
                                    subcategoria.StatusSubCategoria = novoStatusSubCategoria;
                                    subcategoria.DataModificacaoSubCategoria = DateTime.Now;
                                    Console.WriteLine(subcategoria.ToString());
                                }
                                Console.WriteLine("\nSubCategoria inativada!");
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
            if (DataModificacaoSubCategoria == DateTime.MinValue)
            {
                return "\n  Nome:................." + NomeSubCategoria +
                       "\n  Status:..............." + StatusSubCategoria +
                       "\n  Data da inclusão:....." + DataInicialSubCategoria;
            }
            return "\n  Nome:................." + NomeSubCategoria +
                   "\n  Status:..............." + StatusSubCategoria +
                   "\n  Data da inclusão:....." + DataInicialSubCategoria +
                   "\n  Data da modificação:.." + DataModificacaoSubCategoria;
        }


    }
}
