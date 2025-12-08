using System;
using System.IO;
using Lexico;
using Transpilador;

class Programa
{
	static void Main(string[] args)
	{
		var caminho = args.Length > 0 ? args[0] : Path.Combine("testes", "lex_test.txt");
		
		if (!File.Exists(caminho))
		{
			Console.WriteLine($"Arquivo de teste não encontrado: {caminho}");
			return;
		}

		var texto = File.ReadAllText(caminho);
		var lexer = new AnalisadorLexico();
		var tokens = lexer.Tokenize(texto);

		Console.WriteLine("--- Tokens ---");
		foreach (var t in tokens)
		{
			Console.WriteLine(t);
		}

		// Transpile using simple parser
		var parser = new Sintatico.AnalisadorSintatico();
		var corpo = parser.Transpile(tokens);

		var emissor = new EmissorCSharp();
		var outPath = Path.Combine("testes","ProgramaTranspilado.cs");
		emissor.SalvarArquivo(outPath, corpo);
		Console.WriteLine($"\nArquivo C# gerado em: {outPath}");
	}
}
