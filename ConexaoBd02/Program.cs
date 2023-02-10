using Dapper;
using Microsoft.VisualBasic.FileIO;
using System.Data.SqlClient;
using System.Net.Mail;

namespace ConexaoBd02
{
    public class Categoria
    {
        public int Id { get; set; }
        public string? Descricao { get; set; }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            int opcao;

            ExibirMenu(false);
            Console.WriteLine(new String('-', 80));
            Console.Write("Escolha sua opção: ");
            opcao = int.Parse(Console.ReadLine());
            Console.WriteLine(new String('-', 80));

            while (opcao != 5)
            {
                if (opcao == 1)
                {
                    IncluirCategoria();
                }
                else if (opcao == 2)
                {
                    ListarCategoria();
                }
                else if (opcao == 3)
                {
                    AlterarCategoria();
                }
                else if (opcao == 4)
                {
                    ExcluirCategoria();
                }
                else if(opcao == 5)
                {
                    Console.WriteLine("Programa encerrado!");
                    break;
                }
                else
                {
                    Console.WriteLine("Opção Inválida, selecione uma opção dentre as apresentadas");
                }

                ExibirMenu(true);
                Console.WriteLine(new String('-', 80));
                Console.Write("Escolha sua opção: ");
                opcao = int.Parse(Console.ReadLine());
                Console.WriteLine(new String('-', 80));
            }
        }

        static string conexao = @"Data Source=(localdb)\MSSQLLocalDB; 
                                  Initial Catalog=DbProdutos;
                                  Integrated Security=True";


        static void ExibirMenu(bool clearConsole)
        {
            if (clearConsole == false)
            {
                Console.WriteLine("Opções Disponíveis:");
                Console.WriteLine("1 - Incluir");
                Console.WriteLine("2 - Listar");
                Console.WriteLine("3 - Alterar");
                Console.WriteLine("4 - Excluir");
                Console.WriteLine("5 - Sair");
            }
            else
            {
                Console.WriteLine("Pressione uma tecla pra continuar...");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Opções Disponíveis:");
                Console.WriteLine("1 - Incluir");
                Console.WriteLine("2 - Listar");
                Console.WriteLine("3 - Alterar");
                Console.WriteLine("4 - Excluir");
                Console.WriteLine("5 - Sair");
            }
            
        }


        // Opção 1
        static void IncluirCategoria()
        {
            string resposta;
            do
            {
                Console.Write("Informe a categoria: ");
                string categoria = Console.ReadLine();

                using (var conn = new SqlConnection(conexao))
                {
                    var registros = conn.Execute("Insert into TBCategorias (Descricao) values (@Descricao)",
                    new { Descricao = categoria });
                    Console.WriteLine("Registros inseridos: " + registros);
                    Console.Write("Continuar a inserir? (S/N): ");
                }
                resposta = Console.ReadLine();
            } while (resposta == "s" || resposta == "S");

            
        }


        // Opção 2
        static void ListarCategoria()
        {
            using (var conn = new SqlConnection(conexao))
            {
                var categorias = conn.Query<Categoria>("Select * from TBCategorias");
                foreach (var item in categorias)
                {
                    Console.WriteLine("Id: " + item.Id);
                    Console.WriteLine("Descrição: " + item.Descricao);
                    Console.WriteLine("------------------------------------");
                }

            }
        }


        // obs: o where é necessário pq se não ele iria alterar todos os nomes da tabelas, não o que do id que a gente quer mudar
        // ex: update NomeDaTabela SET nome=@nome, idade=@idade where Id=@id
        // update NomeDaTabela SET Descricao=@Descricao where Id=@id

        // Opção 3
        static void AlterarCategoria()
        {
            Console.Write("Informe o código do Id: ");
            int codigo = int.Parse(Console.ReadLine());

            Console.Write("Informe a nova categoria: ");
            string categoria = Console.ReadLine();

            // o using é um recurso que diz que vai executar essa sequencia de comando
            // e depois de terminar vai desalocar a memoria usada nele
            // é quase uma declaração de classe dentro da classe

            using (var conn = new SqlConnection(conexao))
            {
                var registros = conn.Execute("Update TBCategorias set Descricao=@Descricao where Id=@id",
                                              new { Id = codigo, Descricao = categoria }
                    );

                Console.WriteLine("Registros alterados: " + registros);
            }
        }


        // Opção 4
        static void ExcluirCategoria()
        {
            Console.Write("Informe o código do Id: ");
            int codigo = int.Parse(Console.ReadLine());

            using (var conn = new SqlConnection(conexao))
            {
                var registros = conn.Execute("Delete from TBCategorias where Id=@Id",
                                              new { Id = codigo}
                                              );
                Console.WriteLine($"Registro do Id = {codigo} removido");
            }
        }

    }
}