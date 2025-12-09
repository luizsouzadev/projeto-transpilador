using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AST;

namespace Transpilador
{

    public class EmissorCSharp : IVisitador
    {
        private StringBuilder codigo;
        private int indentacao = 0;
        private Dictionary<string, List<bool>> funcoesComRef = new Dictionary<string, List<bool>>();

        public string GerarCodigo(NoBloco raiz)
        {
            codigo = new StringBuilder();
            
            ColetarAssinaturasFuncoes(raiz);
            
            codigo.AppendLine("using System;");
            codigo.AppendLine();
            codigo.AppendLine("class Programa");
            codigo.AppendLine("{");
            
            raiz.Aceitar(this);
            
            codigo.AppendLine("}");
            
            return codigo.ToString();
        }

        public void SalvarArquivo(string caminho, NoBloco raiz)
        {
            var codigoGerado = GerarCodigo(raiz);
            File.WriteAllText(caminho, codigoGerado);
        }

        private void Indentar()
        {
            codigo.Append(new string(' ', indentacao * 4));
        }

        private void ColetarAssinaturasFuncoes(NoBloco raiz)
        {
            foreach (var instrucao in raiz.Instrucoes)
            {
                if (instrucao is NoFuncao funcao)
                {
                    var refFlags = new List<bool>();
                    foreach (var param in funcao.Parametros)
                    {
                        refFlags.Add(param.Item3);
                    }
                    funcoesComRef[funcao.Nome.ToLower()] = refFlags;
                }
            }
        }

        private string MapearTipo(string tipoPortugol)
        {
            return tipoPortugol.ToLower() switch
            {
                "inteiro" => "int",
                "real" => "double",
                "logico" => "bool",
                "cadeia" => "string",
                "caracter" => "string",
                "vazio" => "void",
                _ => "object"
            };
        }

        private string ObterValorPadrao(string tipo)
        {
            return tipo switch
            {
                "int" => "0",
                "double" => "0.0",
                "bool" => "false",
                "string" => "\"\"",
                _ => "null"
            };
        }

        private string EscaparIdentificador(string nome)
        {
            var palavrasChaveCSharp = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char",
                "checked", "class", "const", "continue", "decimal", "default", "delegate",
                "do", "double", "else", "enum", "event", "explicit", "extern", "false",
                "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit",
                "in", "int", "interface", "internal", "is", "lock", "long", "namespace",
                "new", "null", "object", "operator", "out", "override", "params", "private",
                "protected", "public", "readonly", "ref", "return", "sbyte", "sealed",
                "short", "sizeof", "stackalloc", "static", "string", "struct", "switch",
                "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked",
                "unsafe", "ushort", "using", "virtual", "void", "volatile", "while"
            };

            return palavrasChaveCSharp.Contains(nome) ? "@" + nome : nome;
        }

        public void Visitar(NoBloco no)
        {
            foreach (var instrucao in no.Instrucoes)
            {
                EmitInstrucao(instrucao);
            }
        }

        public void Visitar(NoFuncao no)
        {
            indentacao++;
            Indentar();
            
            var tipoRetorno = MapearTipo(no.TipoRetorno);
            var modificador = no.Nome.Equals("inicio", StringComparison.OrdinalIgnoreCase) ? "static void Main" : $"static {tipoRetorno} {no.Nome}";
            
            codigo.Append(modificador);
            codigo.Append("(");
            
            var parametros = new List<string>();
            foreach (var param in no.Parametros)
            {
                var tipoCSharp = MapearTipo(param.Item1);
                var nomeParam = EscaparIdentificador(param.Item2);
                var refModifier = param.Item3 ? "ref " : "";
                parametros.Add($"{refModifier}{tipoCSharp} {nomeParam}");
            }
            
            if (no.Nome.Equals("inicio", StringComparison.OrdinalIgnoreCase) && parametros.Count == 0)
            {
                codigo.Append("string[] args");
            }
            else
            {
                codigo.Append(string.Join(", ", parametros));
            }
            
            codigo.AppendLine(")");
            Indentar();
            codigo.AppendLine("{");
            
            indentacao++;
            no.Corpo.Aceitar(this);
            indentacao--;
            
            Indentar();
            codigo.AppendLine("}");
            codigo.AppendLine();
            indentacao--;
        }

        public void Visitar(NoDeclaracao no)
        {
            Indentar();
            var tipoCSharp = MapearTipo(no.Tipo);
            var nomeEscapado = EscaparIdentificador(no.Nome);
            
            if (no.TamanhosArray != null && no.TamanhosArray.Count > 0)
            {
                if (no.TamanhosArray.Count == 1)
                {
                    codigo.Append($"{tipoCSharp}[] ");
                }
                else
                {
                    codigo.Append(tipoCSharp);
                    codigo.Append("[");
                    for (int i = 1; i < no.TamanhosArray.Count; i++)
                        codigo.Append(",");
                    codigo.Append("] ");
                }
                codigo.Append(nomeEscapado);
                
                if (no.Inicializacao != null)
                {
                    codigo.Append(" = ");
                    no.Inicializacao.Aceitar(this);
                }
                else
                {
                    codigo.Append(" = new ");
                    codigo.Append(tipoCSharp);
                    foreach (var tamanho in no.TamanhosArray)
                    {
                        codigo.Append("[");
                        tamanho.Aceitar(this);
                        codigo.Append("]");
                    }
                }
            }
            else
            {
                codigo.Append($"{tipoCSharp} {nomeEscapado}");
                
                if (no.Inicializacao != null)
                {
                    codigo.Append(" = ");
                    no.Inicializacao.Aceitar(this);
                }
                else
                {
                    codigo.Append($" = {ObterValorPadrao(tipoCSharp)}");
                }
            }
            
            codigo.AppendLine(";");
        }

        public void Visitar(NoAtribuicao no)
        {
            Indentar();
            var nomeEscapado = EscaparIdentificador(no.Nome);
            codigo.Append($"{nomeEscapado} = ");
            no.Valor.Aceitar(this);
            codigo.AppendLine(";");
        }

        public void Visitar(NoLiteral no)
        {
            if (no.Tipo == "cadeia")
            {
                codigo.Append(no.Valor);
            }
            else if (no.Tipo == "caracter")
            {
                if (no.Valor.StartsWith("'") && no.Valor.EndsWith("'"))
                {
                    var charValue = no.Valor.Substring(1, no.Valor.Length - 2);
                    codigo.Append($"\"{charValue}\"");
                }
                else
                {
                    codigo.Append(no.Valor);
                }
            }
            else if (no.Tipo == "logico")
            {
                codigo.Append(no.Valor == "verdadeiro" ? "true" : "false");
            }
            else
            {
                codigo.Append(no.Valor);
            }
        }

        public void Visitar(NoExpressao no)
        {
            var nomeEscapado = EscaparIdentificador(no.Identificador);
            codigo.Append(nomeEscapado);
        }

        public void Visitar(NoExpressaoBinaria no)
        {
            codigo.Append("(");
            no.Esquerda.Aceitar(this);
            
            var opCSharp = no.Operador.ToLower() switch
            {
                "e" => " && ",
                "ou" => " || ",
                "xor" => " != ",
                _ => $" {no.Operador} "
            };
            
            codigo.Append(opCSharp);
            no.Direita.Aceitar(this);
            codigo.Append(")");
        }

        public void Visitar(NoEscreva no)
        {
            Indentar();
            codigo.Append("Console.Write(");
            
            for (int i = 0; i < no.Argumentos.Count; i++)
            {
                no.Argumentos[i].Aceitar(this);
                if (i < no.Argumentos.Count - 1)
                {
                    codigo.Append(" + ");
                }
            }
            
            codigo.AppendLine(");");
        }

        public void Visitar(NoLeia no)
        {
            foreach (var variavel in no.Variaveis)
            {
                Indentar();
                codigo.AppendLine($"{variavel} = int.Parse(Console.ReadLine());");
            }
        }

        public void Visitar(NoSe no)
        {
            Indentar();
            codigo.Append("if (");
            no.Condicao.Aceitar(this);
            codigo.AppendLine(")");
            
            Indentar();
            codigo.AppendLine("{");
            indentacao++;
            no.BlocoSe.Aceitar(this);
            indentacao--;
            Indentar();
            codigo.AppendLine("}");
            
            if (no.BlocoSenao != null)
            {
                Indentar();
                codigo.AppendLine("else");
                Indentar();
                codigo.AppendLine("{");
                indentacao++;
                no.BlocoSenao.Aceitar(this);
                indentacao--;
                Indentar();
                codigo.AppendLine("}");
            }
        }

        public void Visitar(NoEnquanto no)
        {
            Indentar();
            codigo.Append("while (");
            if (no.Condicao != null)
            {
                no.Condicao.Aceitar(this);
            }
            else
            {
                codigo.Append("true");
            }
            codigo.AppendLine(")");
            
            Indentar();
            codigo.AppendLine("{");
            indentacao++;
            no.Corpo.Aceitar(this);
            indentacao--;
            Indentar();
            codigo.AppendLine("}");
        }

        public void Visitar(NoPara no)
        {
            Indentar();
            codigo.AppendLine("{");
            indentacao++;
            
            if (no.Inicializacao != null)
            {
                EmitInstrucao(no.Inicializacao);
            }

            Indentar();
            codigo.Append("while (");
            no.Condicao.Aceitar(this);
            codigo.AppendLine(")");
            Indentar();
            codigo.AppendLine("{");
            indentacao++;
            no.Corpo.Aceitar(this);
            if (no.Atualizacao != null)
            {
                EmitInstrucao(no.Atualizacao);
            }
            indentacao--;
            Indentar();
            codigo.AppendLine("}");
            
            indentacao--;
            Indentar();
            codigo.AppendLine("}");
        }

        public void Visitar(NoRetorne no)
        {
            Indentar();
            codigo.Append("return");
            if (no.Valor != null)
            {
                codigo.Append(" ");
                no.Valor.Aceitar(this);
            }
            codigo.AppendLine(";");
        }

        public void Visitar(NoChamadaFuncao no)
        {
            codigo.Append($"{no.Nome}(");
            
            List<bool> refParams = null;
            if (funcoesComRef.TryGetValue(no.Nome.ToLower(), out refParams))
            {
                for (int i = 0; i < no.Argumentos.Count; i++)
                {
                    if (i < refParams.Count && refParams[i])
                    {
                        codigo.Append("ref ");
                    }
                    no.Argumentos[i].Aceitar(this);
                    if (i < no.Argumentos.Count - 1)
                    {
                        codigo.Append(", ");
                    }
                }
            }
            else
            {
                for (int i = 0; i < no.Argumentos.Count; i++)
                {
                    no.Argumentos[i].Aceitar(this);
                    if (i < no.Argumentos.Count - 1)
                    {
                        codigo.Append(", ");
                    }
                }
            }
            
            codigo.Append(")");
        }

        public void Visitar(NoIncremento no)
        {
            var nomeEscapado = EscaparIdentificador(no.Nome);
            codigo.Append(nomeEscapado);
            codigo.Append(no.Delta > 0 ? "++" : "--");
        }

        public void Visitar(NoEscolha no)
        {
            Indentar();
            codigo.Append("switch (");
            no.Expressao.Aceitar(this);
            codigo.AppendLine(")");
            Indentar();
            codigo.AppendLine("{");
            indentacao++;
            foreach (var caso in no.Casos)
            {
                Indentar();
                codigo.Append("case ");
                caso.Valor.Aceitar(this);
                codigo.AppendLine(":");
                indentacao++;
                caso.Corpo.Aceitar(this);
                Indentar();
                codigo.AppendLine("break;");
                indentacao--;
            }

            if (no.CasoPadrao != null)
            {
                Indentar();
                codigo.AppendLine("default:");
                indentacao++;
                no.CasoPadrao.Aceitar(this);
                Indentar();
                codigo.AppendLine("break;");
                indentacao--;
            }

            indentacao--;
            Indentar();
            codigo.AppendLine("}");
        }

        public void Visitar(NoCaso no)
        {
        }

        public void Visitar(NoPare no)
        {
        }

        public void Visitar(NoFacaEnquanto no)
        {
            Indentar();
            codigo.AppendLine("do");
            Indentar();
            codigo.AppendLine("{");
            indentacao++;
            no.Corpo.Aceitar(this);
            indentacao--;
            Indentar();
            codigo.Append("} while (");
            no.Condicao.Aceitar(this);
            codigo.AppendLine(");");
        }

        private void EmitInstrucao(No no)
        {
            switch (no)
            {
                case NoChamadaFuncao:
                case NoIncremento:
                    Indentar();
                    no.Aceitar(this);
                    codigo.AppendLine(";");
                    break;
                case NoAtribuicao:
                case NoAtribuicaoArray:
                case NoDeclaracao:
                case NoEscreva:
                case NoLeia:
                case NoPare:
                    no.Aceitar(this);
                    break;
                default:
                    no.Aceitar(this);
                    break;
            }
        }

        public void Visitar(NoAcessoArray no)
        {
            var nomeEscapado = EscaparIdentificador(no.Nome);
            codigo.Append($"{nomeEscapado}[");
            for (int i = 0; i < no.Indices.Count; i++)
            {
                no.Indices[i].Aceitar(this);
                if (i < no.Indices.Count - 1)
                    codigo.Append(", ");
            }
            codigo.Append("]");
        }

        public void Visitar(NoListaInicializacao no)
        {
            codigo.Append("{ ");
            for (int i = 0; i < no.Elementos.Count; i++)
            {
                no.Elementos[i].Aceitar(this);
                if (i < no.Elementos.Count - 1)
                    codigo.Append(", ");
            }
            codigo.Append(" }");
        }

        public void Visitar(NoAtribuicaoArray no)
        {
            Indentar();
            var nomeEscapado = EscaparIdentificador(no.Nome);
            codigo.Append($"{nomeEscapado}[");
            for (int i = 0; i < no.Indices.Count; i++)
            {
                no.Indices[i].Aceitar(this);
                if (i < no.Indices.Count - 1)
                    codigo.Append(", ");
            }
            codigo.Append("] = ");
            no.Valor.Aceitar(this);
            codigo.AppendLine(";");
        }
    }
}
