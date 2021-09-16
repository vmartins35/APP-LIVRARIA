using System;

namespace livraria.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						Listarlivro();
						break;
					case "2":
						Inserirlivro();
						break;
					case "3":
						Atualizarlivro();
						break;
					case "4":
						Excluirlivro();
						break;
					case "5":
						Visualizarlivro();
						break;
					case "C":
						Console.Clear();
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}

			Console.WriteLine("A livraria vinicius agradece.");
			Console.ReadLine();
        }

        private static void Excluirlivro()
		{
			Console.Write("Digite o id do livro: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			repositorio.Exclui(indiceSerie);
		}

        private static void Visualizarlivro()
		{
			Console.Write("Digite o id do livro: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			var serie = repositorio.RetornaPorId(indiceSerie);

			Console.WriteLine(serie);
		}

        private static void Atualizarlivro()
		{
			Console.Write("Digite o id do livro: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in Enum.GetValues(typeof(Tipos_livros)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Tipos_livros), i));
			}
			Console.Write("Digite o tipo de livro entre as opções acima: ");
			int entradaTipos_livros = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título do livro: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de publicação do livro: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição do livro: ");
			string entradaDescricao = Console.ReadLine();

			Serie atualizaSerie = new Serie(id: indiceSerie,
										tipos_livros: (Tipos_livros)entradaTipos_livros,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Atualiza(indiceSerie, atualizaSerie);
		}
        private static void Listarlivro()
		{
			Console.WriteLine("Listar livros");

			var lista = repositorio.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhuma livro cadastrado.");
				return;
			}

			foreach (var serie in lista)
			{
                var excluido = serie.retornaExcluido();
                
				Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
			}
		}

        private static void Inserirlivro()
		{
			Console.WriteLine("Inserir novo livro");

			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in Enum.GetValues(typeof(Tipos_livros)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Tipos_livros), i));
			}
			Console.Write("Digite o tipo do livro entre as opções acima: ");
			int entradaTipos_livros = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título do livro: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de publicação do livro: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição do livro: ");
			string entradaDescricao = Console.ReadLine();

			Serie novaSerie = new Serie(id: repositorio.ProximoId(),
										tipos_livros: (Tipos_livros)entradaTipos_livros,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Insere(novaSerie);
		}

        private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("Código vinicius a sua disposição!!!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar os livros cadastrados");
			Console.WriteLine("2- Inserir um novo livro");
			Console.WriteLine("3- Atualizar um livro");
			Console.WriteLine("4- Excluir um livro");
			Console.WriteLine("5- Visualizar os livros");
			Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
    }
}
