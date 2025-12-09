using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Lexico
{

    public class AnalisadorLexico
    {
        private readonly Dictionary<string, TipoToken> palavrasReservadas = 
            new Dictionary<string, TipoToken>(StringComparer.OrdinalIgnoreCase)
        {
            { "programa", TipoToken.PALAVRA_RESERVADA },
            { "funcao", TipoToken.PALAVRA_RESERVADA },
            { "retorne", TipoToken.PALAVRA_RESERVADA },
            
            { "inteiro", TipoToken.TIPO_INTEIRO },
            { "real", TipoToken.TIPO_REAL },
            { "logico", TipoToken.TIPO_LOGICO },
            { "caracter", TipoToken.TIPO_CARACTER },
            
            { "escreva", TipoToken.PALAVRA_RESERVADA },
            { "leia", TipoToken.PALAVRA_RESERVADA },
            
            { "se", TipoToken.PALAVRA_RESERVADA },
            { "entao", TipoToken.PALAVRA_RESERVADA },
            { "então", TipoToken.PALAVRA_RESERVADA },
            { "senao", TipoToken.PALAVRA_RESERVADA },
            { "contrario", TipoToken.PALAVRA_RESERVADA },
            { "escolha", TipoToken.PALAVRA_RESERVADA },
            { "caso", TipoToken.PALAVRA_RESERVADA },
            { "padrao", TipoToken.PALAVRA_RESERVADA },
            
            { "enquanto", TipoToken.PALAVRA_RESERVADA },
            { "para", TipoToken.PALAVRA_RESERVADA },
            { "faca", TipoToken.PALAVRA_RESERVADA },
            { "pare", TipoToken.PALAVRA_RESERVADA },
            
            { "e", TipoToken.PALAVRA_RESERVADA },
            { "ou", TipoToken.PALAVRA_RESERVADA },
            { "nao", TipoToken.PALAVRA_RESERVADA },
            { "verdadeiro", TipoToken.PALAVRA_RESERVADA },
            { "falso", TipoToken.PALAVRA_RESERVADA }
        };

        private readonly List<Tuple<Regex, TipoToken, bool>> tokenDefs;

        public AnalisadorLexico()
        {
            tokenDefs = new List<Tuple<Regex, TipoToken, bool>>
            {
                Tuple.Create(new Regex("^/\\*[\\s\\S]*?\\*/", RegexOptions.Compiled), TipoToken.COMENTARIO, false),
                Tuple.Create(new Regex("^//.*", RegexOptions.Compiled), TipoToken.COMENTARIO, false),
                
                Tuple.Create(new Regex("^\"(\\\\.|[^\"])*\"", RegexOptions.Compiled), TipoToken.STRING, true),
                Tuple.Create(new Regex("^'(\\\\.|[^'])*'", RegexOptions.Compiled), TipoToken.CARACTER_LITERAL, true),
                Tuple.Create(new Regex("^[0-9]+(\\.[0-9]+)?", RegexOptions.Compiled), TipoToken.NUM, true),
                
                Tuple.Create(new Regex("^[A-Za-z_][A-Za-z0-9_]*", RegexOptions.Compiled), TipoToken.IDENT, true),
                
                Tuple.Create(new Regex("^\\+\\+", RegexOptions.Compiled), TipoToken.OP_INC, true),
                Tuple.Create(new Regex("^--", RegexOptions.Compiled), TipoToken.OP_DEC, true),
                Tuple.Create(new Regex("^==", RegexOptions.Compiled), TipoToken.OP_IGUAL, true),
                Tuple.Create(new Regex("^!=", RegexOptions.Compiled), TipoToken.OP_DIF, true),
                Tuple.Create(new Regex("^<=", RegexOptions.Compiled), TipoToken.OP_MENOR_IGUAL, true),
                Tuple.Create(new Regex("^>=", RegexOptions.Compiled), TipoToken.OP_MAIOR_IGUAL, true),
                
                Tuple.Create(new Regex("^\\+", RegexOptions.Compiled), TipoToken.OP_SOMA, true),
                Tuple.Create(new Regex("^-", RegexOptions.Compiled), TipoToken.OP_SUB, true),
                Tuple.Create(new Regex("^\\*", RegexOptions.Compiled), TipoToken.OP_MUL, true),
                Tuple.Create(new Regex("^/", RegexOptions.Compiled), TipoToken.OP_DIV, true),
                Tuple.Create(new Regex("^%", RegexOptions.Compiled), TipoToken.OP_MOD, true),
                
                Tuple.Create(new Regex("^<", RegexOptions.Compiled), TipoToken.OP_MENOR, true),
                Tuple.Create(new Regex("^>", RegexOptions.Compiled), TipoToken.OP_MAIOR, true),
                
                Tuple.Create(new Regex("^=", RegexOptions.Compiled), TipoToken.ATRIBUICAO, true),
                
                Tuple.Create(new Regex("^&", RegexOptions.Compiled), TipoToken.AMPERSAND, true),
                
                Tuple.Create(new Regex("^\\(", RegexOptions.Compiled), TipoToken.PAREN_ESQ, true),
                Tuple.Create(new Regex("^\\)", RegexOptions.Compiled), TipoToken.PAREN_DIR, true),
                Tuple.Create(new Regex("^\\{", RegexOptions.Compiled), TipoToken.CHAVE_ESQ, true),
                Tuple.Create(new Regex("^\\}", RegexOptions.Compiled), TipoToken.CHAVE_DIR, true),
                Tuple.Create(new Regex("^\\[", RegexOptions.Compiled), TipoToken.COLCHETE_ESQ, true),
                Tuple.Create(new Regex("^\\]", RegexOptions.Compiled), TipoToken.COLCHETE_DIR, true),
                Tuple.Create(new Regex("^:", RegexOptions.Compiled), TipoToken.DOIS_PONTOS, true),
                
                Tuple.Create(new Regex("^;", RegexOptions.Compiled), TipoToken.PONTO_VIRGULA, true),
                Tuple.Create(new Regex("^,", RegexOptions.Compiled), TipoToken.VIRGULA, true),
                Tuple.Create(new Regex("^\\.", RegexOptions.Compiled), TipoToken.PONTO, true),
                
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
                        var tipoToken = palavrasReservadas[lex];
                        tokens.Add(new Token(tipoToken, lex, linha, coluna));
                    }
                    else if (emite)
                    {
                        tokens.Add(new Token(tipo, lex, linha, coluna));
                    }

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
    }
}
