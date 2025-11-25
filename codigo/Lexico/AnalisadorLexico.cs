using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Lexico
{
	public class AnalisadorLexico
	{
			private readonly Dictionary<string, TipoToken> palavrasReservadas = new Dictionary<string, TipoToken>(StringComparer.OrdinalIgnoreCase)
		{
			{ "escreva", TipoToken.PALAVRA_RESERVADA },
			{ "se", TipoToken.PALAVRA_RESERVADA },
			{ "senao", TipoToken.PALAVRA_RESERVADA },
			{ "entao", TipoToken.PALAVRA_RESERVADA },
			{ "enquanto", TipoToken.PALAVRA_RESERVADA },
			{ "inicio", TipoToken.PALAVRA_RESERVADA },
			{ "fim", TipoToken.PALAVRA_RESERVADA },
			{ "var", TipoToken.PALAVRA_RESERVADA },
			{ "algoritmo", TipoToken.PALAVRA_RESERVADA }
		};

		private readonly List<Tuple<Regex, TipoToken, bool>> tokenDefs;

		public AnalisadorLexico()
		{
			tokenDefs = new List<Tuple<Regex, TipoToken, bool>>
			{
				Tuple.Create(new Regex("^/\\*[\\s\\S]*?\\*/", RegexOptions.Compiled), TipoToken.COMENTARIO, false),
				Tuple.Create(new Regex("^//.*", RegexOptions.Compiled), TipoToken.COMENTARIO, false),
				Tuple.Create(new Regex("^\"(\\\\.|[^\"])*\"", RegexOptions.Compiled), TipoToken.STRING, true),
				Tuple.Create(new Regex("^[0-9]+(\\.[0-9]+)?", RegexOptions.Compiled), TipoToken.NUM, true),
				Tuple.Create(new Regex("^[A-Za-z_][A-Za-z0-9_]*", RegexOptions.Compiled), TipoToken.IDENT, true),
				Tuple.Create(new Regex("^==|^!=|^<=|^>=", RegexOptions.Compiled), TipoToken.OP_IGUAL, true),
				Tuple.Create(new Regex("^[+\\-]", RegexOptions.Compiled), TipoToken.OP_SOMA, true),
				Tuple.Create(new Regex("^[*/]", RegexOptions.Compiled), TipoToken.OP_MUL, true),
				Tuple.Create(new Regex("^=", RegexOptions.Compiled), TipoToken.ATRIBUICAO, true),
				Tuple.Create(new Regex("^[<>]", RegexOptions.Compiled), TipoToken.OP_MAIOR, true),
				Tuple.Create(new Regex("^\\(", RegexOptions.Compiled), TipoToken.PAREN_ESQ, true),
				Tuple.Create(new Regex("^\\)", RegexOptions.Compiled), TipoToken.PAREN_DIR, true),
				Tuple.Create(new Regex("^\\{", RegexOptions.Compiled), TipoToken.CHAVE_ESQ, true),
				Tuple.Create(new Regex("^\\}", RegexOptions.Compiled), TipoToken.CHAVE_DIR, true),
				Tuple.Create(new Regex("^;", RegexOptions.Compiled), TipoToken.PONTO_VIRGULA, true),
				Tuple.Create(new Regex("^,", RegexOptions.Compiled), TipoToken.VIRGULA, true),
				Tuple.Create(new Regex("^\\s+", RegexOptions.Compiled), TipoToken.WHITESPACE, false)
			};
		}

		public List<Token> Tokenize(string texto)
		{
			var pos = 0;
			var linha = 1;
			var coluna = 1;
			var tokens = new List<Token>();

			while (pos < texto.Length)
			{
				var sub = texto.Substring(pos);
				bool casou = false;

				foreach (var entry in tokenDefs)
				{
					var regex = entry.Item1;
					var tipo = entry.Item2;
					var emite = entry.Item3;

					var m = regex.Match(sub);
					if (!m.Success || m.Index != 0) continue;

					var lex = m.Value;

					if (tipo == TipoToken.IDENT && palavrasReservadas.ContainsKey(lex))
					{
						tokens.Add(new Token(TipoToken.PALAVRA_RESERVADA, lex, linha, coluna));
					}
					else if (emite)
					{
						// Map some generic token types into more specific ones when needed
						var t = MapTokenTipo(tipo, lex);
						tokens.Add(new Token(t, lex, linha, coluna));
					}

					// atualizar posição, linha e coluna
					var linhas = CountLines(lex);
					if (linhas > 0)
					{
						linha += linhas;
						var lastNewline = lex.LastIndexOf('\n');
						coluna = lex.Length - lastNewline;
					}
					else
					{
						coluna += lex.Length;
					}

					pos += lex.Length;
					casou = true;
					break;
				}

				if (!casou)
				{
					tokens.Add(new Token(TipoToken.DESCONHECIDO, texto[pos].ToString(), linha, coluna));
					if (texto[pos] == '\n')
					{
						linha++;
						coluna = 1;
					}
					else
					{
						coluna++;
					}
					pos++;
				}
			}

			tokens.Add(new Token(TipoToken.EOF, string.Empty, linha, coluna));
			return tokens;
		}

		private int CountLines(string s)
		{
			int count = 0;
			foreach (var c in s) if (c == '\n') count++;
			return count;
		}

		private TipoToken MapTokenTipo(TipoToken tipo, string lexema)
		{
			switch (tipo)
			{
				case TipoToken.OP_SOMA:
					if (lexema == "+") return TipoToken.OP_SOMA;
					if (lexema == "-") return TipoToken.OP_SUB;
					return TipoToken.OP_SOMA;
				case TipoToken.OP_MUL:
					if (lexema == "*") return TipoToken.OP_MUL;
					if (lexema == "/") return TipoToken.OP_DIV;
					return TipoToken.OP_MUL;
				case TipoToken.OP_IGUAL:
					if (lexema == "==") return TipoToken.OP_IGUAL;
					if (lexema == "!=") return TipoToken.OP_DIF;
					if (lexema == "<=") return TipoToken.OP_MENOR_IGUAL;
					if (lexema == ">=") return TipoToken.OP_MAIOR_IGUAL;
					return TipoToken.OP_IGUAL;
				case TipoToken.OP_MAIOR:
					if (lexema == ">") return TipoToken.OP_MAIOR;
					if (lexema == "<") return TipoToken.OP_MENOR;
					return tipo;
				case TipoToken.PAREN_ESQ:
					if (lexema == "(") return TipoToken.PAREN_ESQ;
					return TipoToken.PAREN_DIR;
				case TipoToken.CHAVE_ESQ:
					if (lexema == "{") return TipoToken.CHAVE_ESQ;
					return TipoToken.CHAVE_DIR;
				default:
					return tipo;
			}
		}
	}
}
