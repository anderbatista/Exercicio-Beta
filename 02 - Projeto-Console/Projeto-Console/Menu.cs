using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Projeto_CRUD
{
    public class Menu
    {
        public static List<Categoria> listaCategorias = new List<Categoria>();
        public static List<Produto> listaProduto = new List<Produto>();
        public static List<SubCategoria> listaSubCategoria = new List<SubCategoria>();

        public static void ExibirMenu()
        {
            Console.WriteLine("======= Bem vindo =======\n" +
                              " 1) Criar categoria     -\n" +
                              " 2) Editar categoria    -\n" +
                              " 3) Criar SubCategoria  -\n" +
                              " 4) Editar SubCategoria -\n" +
                              " 5) Pesquisar           -\n" +
                              " 6) Criar Produto       -\n" +
                              " 0) Sair                -\n" +
                              "-------------------------");
            Console.Write("Opção desejada -> ");
            string OpcaoEscolhida = Console.ReadLine();
            Console.WriteLine("-------------------------");


            switch (OpcaoEscolhida)
            {
                case "1":
                    Categoria.CadastroCategoria(listaCategorias);

                    ExibirMenu();
                    break;

                case "2":
                    Categoria.EditandoCategoria(listaCategorias);

                    ExibirMenu();
                    break;

                case "3":
                    SubCategoria.CadastroSubCategoria(listaSubCategoria);

                    ExibirMenu();
                    break;

                case "4":
                    SubCategoria.EditandoSubCategoria(listaSubCategoria);

                    ExibirMenu();
                    break;

                case "5":
                    Console.WriteLine("Pesquisar\n" +
                                      "1) Categoria\n" +
                                      "2) SubCategoria\n" +
                                      "0) Voltar\n");
                    Console.Write("Opção desejada: ");
                    var OpcaoPesquisar = Console.ReadLine();
                    while (OpcaoPesquisar != "0")
                    {
                        if (OpcaoPesquisar == "1")
                        {
                            foreach (var categoria in listaCategorias)
                            {
                                Console.WriteLine("--Categoria--");
                                Console.WriteLine(categoria.ToString() + "\n");
                            }
                            Helper.FinalMetodo();
                            break;
                        }
                        else if (OpcaoPesquisar == "2")
                        {
                            foreach (var subcategoria in listaSubCategoria)
                            {
                                Console.WriteLine("--SubCategoria--");
                                Console.WriteLine(subcategoria.ToString() + "\n");
                            }
                            Helper.FinalMetodo();
                            break;
                        }
                        else if (OpcaoPesquisar == "0")
                        {
                            break;
                        }
                    }

                    ExibirMenu();
                    break;

                case "6":
                    Produto.CadastroProduto(listaProduto);

                    ExibirMenu();
                    break;

                case "0":
                    Console.WriteLine("Até logo...");
                    Console.WriteLine();

                    break;

                default:
                    Console.WriteLine("Digite um numero válido");
                    Console.WriteLine();

                    ExibirMenu();
                    break;
            }
        }
    }
}


