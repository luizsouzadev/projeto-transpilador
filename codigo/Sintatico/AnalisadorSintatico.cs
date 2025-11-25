using System;
using System.Collections.Generic;
using System.Text;
using Lexico;

namespace Sintatico
{
    public class AnalisadorSintatico
    {
        private List<Token> tokens;
        private int pos;
        private HashSet<string> declared = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, string> varTypes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private List<string> statements = new List<string>();

        public string Transpile(string source)
        {
            var lexer = new AnalisadorLexico();
            tokens = lexer.Tokenize(source);
            pos = 0;

            // skip optional header: algoritmo "name"
            if (MatchPalavra("algoritmo"))
            {
                Advance();
                if (CurrentTipo() == TipoToken.STRING) Advance();
            }

            // parse optional var declarations (many forms); we'll just collect identifiers after 'var'
            while (MatchPalavra("var"))
            {
                Advance();
                while (CurrentTipo() == TipoToken.IDENT)
                {
                    declared.Add(CurrentLexema());
                    Advance();
                    if (CurrentTipo() == TipoToken.VIRGULA) Advance();
                    else break;
                }
            }

            // find inicio
            while (!MatchPalavra("inicio") && !Match(TipoToken.EOF)) Advance();
            if (MatchPalavra("inicio")) Advance();

            // parse statements until 'fim'
            while (!MatchPalavra("fim") && !Match(TipoToken.EOF))
            {
                ParseStatement();
            }

            // prepare declarations based on inferred types
            var declLines = new List<string>();
            foreach (var name in declared)
            {
                string tipo;
                if (varTypes.TryGetValue(name, out tipo))
                {
                    if (tipo == "int") declLines.Add($"{tipo} {name};");
                    else if (tipo == "string") declLines.Add($"{tipo} {name};");
                    else declLines.Add($"object {name};");
                }
                else
                {
                    declLines.Add($"object {name};");
                }
            }

            // build body
            var sb = new StringBuilder();
            sb.AppendLine("    public class ProgramaTranspilado {");
            sb.AppendLine("        public static void Main() {");

            foreach (var d in declLines)
                sb.AppendLine("            " + d);

            foreach (var s in statements)
                sb.AppendLine("            " + s);

            sb.AppendLine("        }");
            sb.AppendLine("    }");

            return sb.ToString();
        }

        private void ParseStatement()
        {
            if (MatchPalavra("escreva"))
            {
                Advance(); // escreva
                Expect(TipoToken.PAREN_ESQ);
                Advance();
                var parts = new List<string>();
                while (!Match(TipoToken.PAREN_DIR) && !Match(TipoToken.EOF))
                {
                    if (Match(TipoToken.STRING)) { parts.Add(CurrentLexema()); Advance(); }
                    else if (Match(TipoToken.NUM)) { parts.Add(CurrentLexema()); Advance(); }
                    else if (Match(TipoToken.IDENT) || Match(TipoToken.PALAVRA_RESERVADA)) { parts.Add(CurrentLexema()); Advance(); }
                    if (Match(TipoToken.VIRGULA)) Advance();
                }
                Expect(TipoToken.PAREN_DIR);
                Advance();
                if (Match(TipoToken.PONTO_VIRGULA)) Advance();

                // join parts with + for C#
                var expr = string.Join(" + ", parts);
                statements.Add($"Console.WriteLine({expr});");
                return;
            }

            if (Match(TipoToken.IDENT))
            {
                var name = CurrentLexema();
                Advance();
                if (Match(TipoToken.ATRIBUICAO))
                {
                    Advance();
                    string rhs = "";
                    if (Match(TipoToken.NUM)) { rhs = CurrentLexema(); varTypes[name] = "int"; Advance(); }
                    else if (Match(TipoToken.STRING)) { rhs = CurrentLexema(); varTypes[name] = "string"; Advance(); }
                    else if (Match(TipoToken.IDENT)) { rhs = CurrentLexema(); Advance(); }

                    if (Match(TipoToken.PONTO_VIRGULA)) Advance();
                    statements.Add($"{name} = {rhs};");
                    return;
                }
            }

            if (MatchPalavra("se"))
            {
                Advance(); // se
                // parse simple condition: IDENT OP NUM
                var left = CurrentLexema(); Advance();
                var op = CurrentLexema(); Advance();
                var right = CurrentLexema(); Advance();
                // optionally 'entao' keyword
                if (MatchPalavra("entao")) Advance();

                statements.Add($"if ({left} {op} {right}) {{");

                // parse inner statements until 'fim'
                while (!MatchPalavra("fim") && !Match(TipoToken.EOF))
                {
                    ParseStatement();
                }

                // consume 'fim'
                if (MatchPalavra("fim")) Advance();
                statements.Add("}");
                return;
            }

            // unknown token: skip
            Advance();
        }

        private Token Current() => pos < tokens.Count ? tokens[pos] : tokens[tokens.Count - 1];
        private TipoToken CurrentTipo() => Current().Tipo;
        private string CurrentLexema() => Current().Lexema;
        private void Advance() { if (pos < tokens.Count) pos++; }
        private bool Match(TipoToken t) => CurrentTipo() == t;
        private bool MatchPalavra(string palavra) => CurrentTipo() == TipoToken.PALAVRA_RESERVADA && string.Equals(CurrentLexema(), palavra, StringComparison.OrdinalIgnoreCase);
        private void Expect(TipoToken t) { if (!Match(t)) { /* could throw syntax error, but skip for now */ } }
    }
}
