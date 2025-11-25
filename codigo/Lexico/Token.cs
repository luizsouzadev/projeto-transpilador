using System;

namespace Lexico
{
	public class Token
	{
		public TipoToken Tipo { get; }
		public string Lexema { get; }
		public int Linha { get; }
		public int Coluna { get; }

		public Token(TipoToken tipo, string lexema, int linha, int coluna)
		{
			Tipo = tipo;
			Lexema = lexema;
			Linha = linha;
			Coluna = coluna;
		}

		public override string ToString()
		{
			return $"[{Linha},{Coluna}] {Tipo}: '{Lexema}'";
		}
	}
}
