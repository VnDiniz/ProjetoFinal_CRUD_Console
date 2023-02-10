using System;
using System.Data.SqlClient;

namespace ConexaoBD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // providers - provedores

            // Data Source=(localdb)\MSSQLLocalDB  (usaremos)
            // Initial Catalog=master  (usaremos)
            // Integrated Security=True  (usaremos)

            // obs: Usa apenas uma vez por projeto
            // Uma maneira de criar conexão com o banco
            // String para criar conexao com o banco
            string conexao = @"Data Source=(localdb)\MSSQLLocalDB; 
                               Initial Catalog=DbProdutos;
                               Integrated Security=True";

            // Criar a conexão
            var cn = new SqlConnection();
            cn.ConnectionString = conexao;

            // Abrir a conexão
            cn.Open();


            // ORM - Object Relational Mapping
            // Dapper ou Entity (ambos transformam uma classe em uma tabela do banco de dados, no caso o bdAula10)
            // Dapper é mais leve e mais rápido, mas o Entity é mais usado no mercado (sabendo um também sabe o outro)
            // Ambos fazem parte de uma pacote externo (precisamos pro NuGet e using pra importa-los)


            // Para fechar a conexão
            cn.Close();



        }
    }
}