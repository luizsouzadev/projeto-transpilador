using System;
using System.IO;
using Lexico;
using Sintatico;
using Transpilador;

class Programa
{
	static void Main(string[] args)
	{
		var caminho = args.Length > 0 ? args[0] : Path.Combine("testes", "teste_inteiro.por");
		
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

		var parser = new AnalisadorSintatico(tokens);
		var ast = parser.Analisar();

		var emissor = new EmissorCSharp();
		var inputDir = Path.GetDirectoryName(caminho);
		var inputFile = Path.GetFileNameWithoutExtension(caminho);
		var outPath = Path.Combine(inputDir ?? ".", $"{inputFile}.cs");
		emissor.SalvarArquivo(outPath, ast);
		Console.WriteLine($"\nArquivo C# gerado em: {outPath}");
	}
}
