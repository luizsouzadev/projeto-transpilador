using System;
using System.Collections.Generic;
using System.Text;
using Lexico;

namespace Sintatico
{
    public class AnalisadorSintatico
    {
        private List<Token> tokens = new List<Token>();
        private int pos = 0;
        private StringBuilder codigo = new StringBuilder();

        public string Transpile(List<Token> tokens)
        {
            this.tokens = tokens;
            this.pos = 0;
            this.codigo = new StringBuilder();

            ParsePrograma();
            return codigo.ToString();
        }

        private Token Atual() => pos < tokens.Count ? tokens[pos] : tokens[tokens.Count - 1];
        private Token Proximo() => pos + 1 < tokens.Count ? tokens[pos + 1] : tokens[tokens.Count - 1];
        private void Avancar() => pos++;

        private bool Verifica(string lexema)
        {
            return Atual().Lexema.Equals(lexema, StringComparison.OrdinalIgnoreCase);
        }

        private bool VerificaTipo(TipoToken tipo) => Atual().Tipo == tipo;

        private void Consome(string lexema)
        {
            if (!Verifica(lexema))
                throw new Exception($"Esperado '{lexema}', encontrado '{Atual().Lexema}' na linha {Atual().Linha}");
            Avancar();
        }

        private void ConsomeTipo(TipoToken tipo)
        {
            if (Atual().Tipo != tipo)
                throw new Exception($"Esperado token tipo {tipo}, encontrado {Atual().Tipo} na linha {Atual().Linha}");
            Avancar();
        }

        private void ParsePrograma()
        {
            Consome("algoritmo");
            // Pode ser identificador ou string
            if (VerificaTipo(TipoToken.IDENT))
                ConsomeTipo(TipoToken.IDENT);
            else if (VerificaTipo(TipoToken.STRING))
                ConsomeTipo(TipoToken.STRING);

            ParseDeclaracoes();
            Consome("inicio");
            ParseInstrucoes();
            Consome("fim");
        }

        private void ParseDeclaracoes()
        {
            while (Verifica("var"))
            {
                Consome("var");
                var nome = Atual().Lexema;
                ConsomeTipo(TipoToken.IDENT);
                Consome(";");
                codigo.AppendLine($"            int {nome} = 0;");
            }
        }

        private void ParseInstrucoes()
        {
            while (!Verifica("fim") && Atual().Tipo != TipoToken.EOF)
            {
                if (Verifica("escreva"))
                    ParseEscreva();
                else if (Verifica("leia"))
                    ParseLeia();
                else if (Verifica("se"))
                    ParseSe();
                else if (Verifica("enquanto"))
                    ParseEnquanto();
                else if (Verifica("funcao"))
                    ParseFuncao();
                else if (Verifica("retorne"))
                    ParseRetorno();
                else if (VerificaTipo(TipoToken.IDENT) && Proximo().Tipo == TipoToken.ATRIBUICAO)
                    ParseAtribuicao();
                else if (VerificaTipo(TipoToken.IDENT) && Proximo().Tipo == TipoToken.PAREN_ESQ)
                    ParseChamadaFuncao();
                else
                    Avancar();
            }
        }

        private void ParseEscreva()
        {
            Consome("escreva");
            Consome("(");

            var expr = ParseExpressao();
            codigo.Append($"            Console.WriteLine({expr}");

            while (Verifica(","))
            {
                Consome(",");
                expr = ParseExpressao();
                codigo.Append($" + \" \" + {expr}");
            }

            codigo.AppendLine(");");
            Consome(")");
            Consome(";");
        }

        private void ParseLeia()
        {
            Consome("leia");
            Consome("(");
            var nome = Atual().Lexema;
            ConsomeTipo(TipoToken.IDENT);
            codigo.AppendLine($"            if (int.TryParse(Console.ReadLine(), out {nome})) {{ }}");
            Consome(")");
            Consome(";");
        }

        private void ParseSe()
        {
            Consome("se");
            var cond = ParseExpressao();
            if (Verifica("entao") || Verifica("então"))
            {
                if (Verifica("entao")) Consome("entao");
                else Consome("então");
            }
            codigo.AppendLine($"            if ({cond})");
            codigo.AppendLine("            {");

            ParseInstrucoesBloco();

            codigo.AppendLine("            }");

            if (Verifica("senao"))
            {
                Consome("senao");
                codigo.AppendLine("            else");
                
                // Verifica se senao é seguido por outro se
                if (Verifica("se"))
                {
                    // Aninhamento de se/else: não adiciona chaves extras
                    ParseSe();
                }
                else
                {
                    codigo.AppendLine("            {");
                    ParseInstrucoesBloco();
                    codigo.AppendLine("            }");
                }
            }
            
            // Só consome fim se não foi consumido por um aninhamento
            if (Verifica("fim"))
                Consome("fim");
        }

        private void ParseEnquanto()
        {
            Consome("enquanto");
            Consome("(");
            var cond = ParseExpressao();
            Consome(")");
            if (Verifica("faca")) Consome("faca");
            
            codigo.AppendLine($"            while ({cond})");
            codigo.AppendLine("            {");
            ParseInstrucoesBloco();
            codigo.AppendLine("            }");
        }

        private void ParseFuncao()
        {
            Consome("funcao");
            var nomeFuncao = Atual().Lexema;
            ConsomeTipo(TipoToken.IDENT);

            Consome("(");
            var parametros = new List<string>();
            if (!Verifica(")"))
            {
                parametros.Add(Atual().Lexema);
                ConsomeTipo(TipoToken.IDENT);
                while (Verifica(","))
                {
                    Consome(",");
                    parametros.Add(Atual().Lexema);
                    ConsomeTipo(TipoToken.IDENT);
                }
            }
            Consome(")");

            var paramsStr = string.Join(", ", parametros.ConvertAll(p => $"int {p}"));
            codigo.AppendLine($"        private static int {nomeFuncao}({paramsStr})");
            codigo.AppendLine("        {");

            Consome("inicio");
            ParseInstrucoesBloco();
            Consome("fim");

            codigo.AppendLine("            return 0;");
            codigo.AppendLine("        }");
        }

        private void ParseRetorno()
        {
            Consome("retorne");
            var expr = ParseExpressao();
            codigo.AppendLine($"            return {expr};");
            Consome(";");
        }

        private void ParseAtribuicao()
        {
            var nome = Atual().Lexema;
            ConsomeTipo(TipoToken.IDENT);
            Consome("=");
            var expr = ParseExpressao();
            codigo.AppendLine($"            {nome} = {expr};");
            Consome(";");
        }

        private void ParseChamadaFuncao()
        {
            var nome = Atual().Lexema;
            ConsomeTipo(TipoToken.IDENT);
            Consome("(");
            var args = new List<string>();
            if (!Verifica(")"))
            {
                args.Add(ParseExpressao());
                while (Verifica(","))
                {
                    Consome(",");
                    args.Add(ParseExpressao());
                }
            }
            Consome(")");
            Consome(";");
            var argsStr = string.Join(", ", args);
            codigo.AppendLine($"            {nome}({argsStr});");
        }

        private void ParseInstrucoesBloco()
        {
            while (!Verifica("fim") && !Verifica("senao") && !Verifica("else") && Atual().Tipo != TipoToken.EOF)
            {
                if (Verifica("escreva"))
                    ParseEscreva();
                else if (Verifica("leia"))
                    ParseLeia();
                else if (Verifica("se"))
                    ParseSe();
                else if (Verifica("enquanto"))
                    ParseEnquanto();
                else if (Verifica("retorne"))
                    ParseRetorno();
                else if (VerificaTipo(TipoToken.IDENT) && Proximo().Tipo == TipoToken.ATRIBUICAO)
                    ParseAtribuicao();
                else if (VerificaTipo(TipoToken.IDENT) && Proximo().Tipo == TipoToken.PAREN_ESQ)
                    ParseChamadaFuncao();
                else
                    Avancar();
            }
        }

        // Precedência de expressões (do menor para o maior)
        private string ParseExpressao()
        {
            return ParseExprLogicaOu();
        }

        private string ParseExprLogicaOu()
        {
            var esq = ParseExprLogicaE();
            while (Verifica("ou") || Verifica("OU"))
            {
                var op = Atual().Lexema;
                Avancar();
                var dir = ParseExprLogicaE();
                esq = $"({esq} || {dir})";
            }
            return esq;
        }

        private string ParseExprLogicaE()
        {
            var esq = ParseExprRelacional();
            while (Verifica("e") || Verifica("E"))
            {
                var op = Atual().Lexema;
                Avancar();
                var dir = ParseExprRelacional();
                esq = $"({esq} && {dir})";
            }
            return esq;
        }

        private string ParseExprRelacional()
        {
            var esq = ParseAritmetica();
            if (Verifica("==") || Verifica("!=") || Verifica("<") || Verifica(">") || Verifica("<=") || Verifica(">="))
            {
                var op = Atual().Lexema;
                Avancar();
                var dir = ParseAritmetica();
                var opCSharp = op switch
                {
                    "==" => "==",
                    "!=" => "!=",
                    "<" => "<",
                    ">" => ">",
                    "<=" => "<=",
                    ">=" => ">=",
                    _ => "=="
                };
                return $"({esq} {opCSharp} {dir})";
            }
            return esq;
        }

        private string ParseAritmetica()
        {
            var esq = ParseTermo();
            while (Verifica("+") || Verifica("-"))
            {
                var op = Atual().Lexema;
                Avancar();
                var dir = ParseTermo();
                esq = $"({esq} {op} {dir})";
            }
            return esq;
        }

        private string ParseTermo()
        {
            var esq = ParseFator();
            while (Verifica("*") || Verifica("/"))
            {
                var op = Atual().Lexema;
                Avancar();
                var dir = ParseFator();
                esq = $"({esq} {op} {dir})";
            }
            return esq;
        }

        private string ParseFator()
        {
            if (Verifica("nao") || Verifica("NÃO") || Verifica("!"))
            {
                Avancar();
                var expr = ParseFator();
                return $"(!{expr})";
            }

            if (Verifica("("))
            {
                Consome("(");
                var expr = ParseExpressao();
                Consome(")");
                return $"({expr})";
            }

            if (VerificaTipo(TipoToken.NUM))
            {
                var num = Atual().Lexema;
                Avancar();
                return num;
            }

            if (VerificaTipo(TipoToken.STRING))
            {
                var str = Atual().Lexema;
                Avancar();
                return str;
            }

            if (VerificaTipo(TipoToken.IDENT))
            {
                var nome = Atual().Lexema;
                Avancar();
                if (Verifica("("))
                {
                    Consome("(");
                    var args = new List<string>();
                    if (!Verifica(")"))
                    {
                        args.Add(ParseExpressao());
                        while (Verifica(","))
                        {
                            Consome(",");
                            args.Add(ParseExpressao());
                        }
                    }
                    Consome(")");
                    var argsStr = string.Join(", ", args);
                    return $"{nome}({argsStr})";
                }
                return nome;
            }

            throw new Exception($"Esperado expressão, encontrado '{Atual().Lexema}' na linha {Atual().Linha}");
        }
    }
}
