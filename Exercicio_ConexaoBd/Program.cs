using Dapper;
using Microsoft.Win32;
using System.Data.SqlClient;

namespace Exercicio_ConexaoBd
{
    internal class Program
    {

        // 1) Adicionar no NuGet:
        // - System.Data.SqlClient
        // - Dapper
        // - Dapper.Contrib

        public class Estados
        {
            public int Id { get; set; }
            public string? Estado { get; set; }
        }

        public class Times
        {
            public int Id { get; set; }
            public string? IdEstado { get; set; }
            public string? Nome { get; set; }
            public string? Cidade { get; set; }
        }


        static void Main(string[] args)
        {
            int opcao;

            ExibirMenu(false);
            Console.WriteLine(new String('-', 80));
            Console.Write("Escolha sua opção: ");
            opcao = int.Parse(Console.ReadLine());
            Console.WriteLine(new String('-', 80));

            while (opcao != 6)
            {
                if (opcao == 1)
                {
                    IncluirTimeBrasil();
                }
                else if (opcao == 2)
                {
                    ListarTimesBrasil();
                }
                else if (opcao == 3)
                {
                    AlterarTimeBrasil();
                }
                else if (opcao == 4)
                {
                    ExcluirTimeBrasilPorId();
                }
                else if (opcao == 5)
                {
                    ExcluirTimeBrasilPorNome();
                }
                else if (opcao == 6)
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
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch(Exception)
                {
                    opcao = 0;
                }
                Console.WriteLine(new String('-', 80));
            }
        }


        static void ExibirMenu(bool clearConsole)
        {
            if (clearConsole == false)
            {
                Console.WriteLine("Opções Disponíveis:");
                Console.WriteLine("1 - Incluir Time");
                Console.WriteLine("2 - Listar Times");
                Console.WriteLine("3 - Alterar Time");
                Console.WriteLine("4 - Excluir Time por Id");
                Console.WriteLine("5 - Excluir Time por Nome");
                Console.WriteLine("6 - Sair");
            }
            else
            {
                Console.WriteLine("Pressione uma tecla pra continuar...");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Opções Disponíveis:");
                Console.WriteLine("1 - Incluir Time");
                Console.WriteLine("2 - Listar Times");
                Console.WriteLine("3 - Alterar Time");
                Console.WriteLine("4 - Excluir Time por Id");
                Console.WriteLine("5 - Excluir Time por Nome");
                Console.WriteLine("6 - Sair");
            }
        }


        // String para criar conexao com o banco
        static string conexao = @"Data Source=(localdb)\MSSQLLocalDB; 
                                Initial Catalog=DbTimesFutebol;
                                Integrated Security=True";


        #region Métodos da Tabela TBTimesBrasil

        // Métodos referentes à tabela TBTimesBrasil do DbTimesFutebol
        static void IncluirEstado()
        {
            Console.Write("Insira o nome do Estado que deseja adicionar ao banco de dados: ");
            string estado = Console.ReadLine();

            using (var conn = new SqlConnection(conexao))
            {
                var registros = conn.Execute("Insert into TBTimesBrasil (Estado) values (@Estado)",
                new { Estado = estado });
                Console.WriteLine("Registros inseridos: " + registros);
            }
        }

        static void ListarEstados()
        {
            using (var conn = new SqlConnection(conexao))
            {
                var estados = conn.Query<Estados>("Select * from TBTimesBrasil");
                foreach (var item in estados)
                {
                    Console.WriteLine("Id: " + item.Id);
                    Console.WriteLine("Estado: " + item.Estado);
                    Console.WriteLine("------------------------------------");
                }
            }
        }

        #endregion


        #region Métodos da Tabela TBTimes

        // -----------------------------------------------------
        // Métodos referentes à tabela TBTimes do DbTimesFutebol
        // -----------------------------------------------------

        // Opção 1
        static void IncluirTimeBrasil()
        {
            Console.Write("Insira o nome do time que deseja adicionar ao banco de dados: ");
            string nomeTime = Console.ReadLine();
            Console.Write("Insira o Id do estado do time que deseja adicionar ao banco de dados: ");
            string idEstado = Console.ReadLine();
            Console.Write("Insira a cidade do time que deseja adicionar ao banco de dados: ");
            string cidade = Console.ReadLine();

            if (nomeTime == "" || idEstado == "" || cidade == "")
            {
                Console.WriteLine("Inclusão não realizada, verifique o preenchimento das informações e tente novamente.");
            }
            else
            {
                using (var conn = new SqlConnection(conexao))
                {
                    var registros = conn.Execute("Insert into TBTimes (IdEstado, Nome, Cidade) values (@IdEstado, @Nome, @Cidade)",
                    new { Nome = nomeTime, IdEstado = idEstado, Cidade = cidade });
                    Console.WriteLine($"-> Time {nomeTime} incluido no banco de dados.");
                }
            }
        }

        // Opção 2
        static void ListarTimesBrasil()
        {
            using (var conn = new SqlConnection(conexao))
            {
                var times = conn.Query<Times>("Select * from TBTimes");
                foreach (var item in times)
                {
                    Console.WriteLine("Id: " + item.Id);
                    Console.WriteLine("Id do Estado na tabela principal: " + item.IdEstado);
                    Console.WriteLine("Nome do time: " + item.Nome);
                    Console.WriteLine("Cidade do time: " + item.Cidade);
                    Console.WriteLine("------------------------------------");
                }
            }
        }

        // Opção 3
        static void AlterarTimeBrasil()
        {
            Console.Write("Insira o Id que deseja modificar do banco de dados: ");
            string id = Console.ReadLine();
            Console.Write("Insira o novo nome do time que deseja adicionar ao banco de dados: ");
            string nomeTime = Console.ReadLine();
            Console.Write("Insira o novo Id do estado do time que deseja adicionar ao banco de dados: ");
            string idEstado = Console.ReadLine();
            Console.Write("Insira a nova cidade do time que deseja adicionar ao banco de dados: ");
            string cidade = Console.ReadLine();

            using (var conn = new SqlConnection(conexao))
            {
                if (nomeTime == "" || idEstado == "" || cidade == "")
                {
                    Console.WriteLine("Alteração não realizada, verifique o preenchimento das informações e tente novamente.");
                }
                else
                {
                    var registros = conn.Execute(
                                        "Update TBTimes set IdEstado=@IdEstado, Nome=@Nome, Cidade=@Cidade where Id=@Id",
                                         new { Id = id, IdEstado = idEstado, Nome = nomeTime, Cidade = cidade }
                                        );
                    if (registros == 0)
                    {
                        Console.WriteLine("-> Id informado não encontrado no banco de dados. Tente novamente.");
                    }
                    else
                    {
                        Console.WriteLine($"-> Time {nomeTime} incluido na alteração");
                    }
                }
            }
        }

        // Opção 4
        static void ExcluirTimeBrasilPorId()
        {
            Console.Write("Digite o Id do time que deseja excluir do banco de dados: ");
            try
            {
                int id = int.Parse(Console.ReadLine());

                using (var conn = new SqlConnection(conexao))
                {
                    var registro = conn.Execute(
                                        "Delete from TBTimes where Id=@Id",
                                         new { Id = id }
                                         );
                    if (registro == 0)      // Caso a operação 'Execute' não realize nada (retorne zero), ou seja, a informação nao foi encontrada no banco de dados 
                    {
                        Console.WriteLine("-> Id informado não encontrado no banco de dados. Tente novamente.");
                    }
                    else
                    {
                        Console.WriteLine($"-> Exclusão realizada.");
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Não foi informado o Id do time. Tente novamente.");  // Caso o valor de id == null (apenas apertar enter sem escrever nada)
            }            
        }

        // Opção 5
        // obs: Pode haver mais um time com o mesmo nome, então este não é um bom método de deleção, sendo melhor utilziar o id ou 'Registro'
        static void ExcluirTimeBrasilPorNome()
        {
            Console.Write("Digite o nome do time que deseja excluir do banco de dados: ");
            string nome = Console.ReadLine();

            using (var conn = new SqlConnection(conexao))
            {
                var registro = conn.Execute(
                    "Delete from TBTimes where Nome=@Nome",
                     new { Nome = nome }
                     );
                
                if (registro == 0)
                {
                    Console.WriteLine("-> Id informado não encontrado no banco de dados. Tente novamente.");

                }
                else
                {
                    Console.WriteLine($"-> Exclusão realizada.");
                }
            }
        }

        #endregion

    }
}