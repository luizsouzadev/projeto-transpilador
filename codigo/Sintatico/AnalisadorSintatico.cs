using System;
using System.Collections.Generic;
using System.Linq;
using Lexico;
using AST;

namespace Sintatico
{
    public class AnalisadorSintatico
    {
        private List<Token> tokens;
        private int pos = 0;
        private Token atual;

        public AnalisadorSintatico(List<Token> tokens)
        {
            this.tokens = tokens;
            this.atual = tokens[0];
        }

        private void Avancar()
        {
            if (pos < tokens.Count - 1)
            {
                pos++;
                atual = tokens[pos];
            }
        }

        private void Esperar(TipoToken tipo)
        {
            if (atual.Tipo != tipo)
            {
                throw new Exception($"Esperado {tipo}, mas encontrado {atual.Tipo} em [{atual.Linha},{atual.Coluna}]: '{atual.Lexema}'");
            }
            Avancar();
        }

        private string EsperarIdentificador()
        {
            // Em Portugol, verdadeiro/falso podem ser usados como nomes de variáveis
            if (atual.Tipo == TipoToken.IDENT || 
                (atual.Tipo == TipoToken.PALAVRA_RESERVADA && 
                 (atual.Lexema == "verdadeiro" || atual.Lexema == "falso")))
            {
                var nome = atual.Lexema;
                Avancar();
                return nome;
            }
            throw new Exception($"Esperado identificador, mas encontrado {atual.Tipo} em [{atual.Linha},{atual.Coluna}]: '{atual.Lexema}'");
        }

        private bool EhTipo()
        {
            return atual.Tipo == TipoToken.TIPO_INTEIRO ||
                   atual.Tipo == TipoToken.TIPO_REAL ||
                   atual.Tipo == TipoToken.TIPO_LOGICO ||
                   atual.Tipo == TipoToken.TIPO_CARACTER ||
                   (atual.Tipo == TipoToken.IDENT && atual.Lexema.Equals("cadeia", StringComparison.OrdinalIgnoreCase));
        }

        private string ObterTipo()
        {
            if (atual.Tipo == TipoToken.TIPO_INTEIRO)
            {
                Avancar();
                return "inteiro";
            }
            if (atual.Tipo == TipoToken.TIPO_REAL)
            {
                Avancar();
                return "real";
            }
            if (atual.Tipo == TipoToken.TIPO_LOGICO)
            {
                Avancar();
                return "logico";
            }
            if (atual.Tipo == TipoToken.TIPO_CARACTER)
            {
                Avancar();
                return "caracter";
            }
            if (atual.Tipo == TipoToken.IDENT && atual.Lexema.Equals("cadeia", StringComparison.OrdinalIgnoreCase))
            {
                Avancar();
                return "cadeia";
            }
            throw new Exception($"Tipo de dados esperado em [{atual.Linha},{atual.Coluna}]");
        }

        public NoBloco Analisar()
        {
            if (atual.Tipo != TipoToken.PALAVRA_RESERVADA || !atual.Lexema.Equals("programa", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Programa deve começar com 'programa'");
            }
            Avancar();
            Esperar(TipoToken.CHAVE_ESQ);

            var declaracoes = new List<No>();

            // Processa declarações de variáveis globais e funções
            while (atual.Tipo != TipoToken.CHAVE_DIR && atual.Tipo != TipoToken.EOF)
            {
                // Variáveis globais
                if (EhTipo())
                {
                    declaracoes.AddRange(ParseDeclaracaoVar());
                }
                // Funções
                else if (atual.Tipo == TipoToken.PALAVRA_RESERVADA && atual.Lexema.Equals("funcao", StringComparison.OrdinalIgnoreCase))
                {
                    declaracoes.Add(ParseFuncao());
                }
                else
                {
                    throw new Exception($"Esperado tipo ou 'funcao' em [{atual.Linha},{atual.Coluna}]");
                }
            }

            Esperar(TipoToken.CHAVE_DIR);
            return new NoBloco(declaracoes);
        }

        private No ParseFuncao()
        {
            Esperar(TipoToken.PALAVRA_RESERVADA); // 'funcao'
            
            string tipoRetorno = "vazio";
            if (EhTipo())
            {
                tipoRetorno = ObterTipo();
            }

            string nomeFuncao = EsperarIdentificador();

            Esperar(TipoToken.PAREN_ESQ);
            var parametros = ParseParametros();
            Esperar(TipoToken.PAREN_DIR);

            Esperar(TipoToken.CHAVE_ESQ);
            var corpo = ParseBloco();
            Esperar(TipoToken.CHAVE_DIR);

            return new NoFuncao(nomeFuncao, tipoRetorno, parametros, corpo);
        }

        private List<Tuple<string, string, bool>> ParseParametros()
        {
            var parametros = new List<Tuple<string, string, bool>>();
            
            while (EhTipo())
            {
                string tipo = ObterTipo();
                bool ehReferencia = false;
                
                if (atual.Tipo == TipoToken.AMPERSAND)
                {
                    ehReferencia = true;
                    Avancar();
                }
                
                string nome = EsperarIdentificador();
                
                parametros.Add(Tuple.Create(tipo, nome, ehReferencia));

                if (atual.Tipo == TipoToken.VIRGULA)
                {
                    Avancar();
                }
                else
                {
                    break;
                }
            }

            return parametros;
        }

        private NoBloco ParseBloco()
        {
            var instrucoes = new List<No>();

            while (atual.Tipo != TipoToken.CHAVE_DIR && atual.Tipo != TipoToken.EOF)
            {
                instrucoes.Add(ParseInstrucao());
            }

            return new NoBloco(instrucoes);
        }

        private No ParseInstrucaoSimples()
        {
            return ParseInstrucao();
        }

        private No ParseInstrucao()
        {
            if (EhTipo())
            {
                var declaracoes = ParseDeclaracaoVar();
                if (declaracoes.Count == 1)
                    return declaracoes[0];
                return new NoBloco(declaracoes);
            }
            
            if (atual.Tipo == TipoToken.PALAVRA_RESERVADA || atual.Tipo == TipoToken.IDENT)
            {
                string lex = atual.Lexema.ToLower();
                
                if (lex == "escreva")
                {
                    return ParseEscreva();
                }
                else if (lex == "leia")
                {
                    return ParseLeia();
                }
                else if (lex == "se")
                {
                    return ParseSe();
                }
                else if (lex == "enquanto")
                {
                    return ParseEnquanto();
                }
                else if (lex == "faca")
                {
                    return ParseFacaEnquanto();
                }
                else if (lex == "para")
                {
                    return ParsePara();
                }
                else if (lex == "escolha")
                {
                    return ParseEscolha();
                }
                else if (lex == "retorne")
                {
                    return ParseRetorne();
                }
                else if (lex == "pare")
                {
                    Avancar();
                    return new NoPare();
                }
                else
                {
                    return ParseAtribuicaoOuChamada();
                }
            }
            else if (atual.Tipo == TipoToken.CHAVE_ESQ)
            {
                Avancar();
                var bloco = ParseBloco();
                Esperar(TipoToken.CHAVE_DIR);
                return bloco;
            }
            else
            {
                throw new Exception($"Instrução inesperada em [{atual.Linha},{atual.Coluna}]: {atual.Lexema}");
            }
        }

        private List<No> ParseDeclaracaoVar()
        {
            string tipo = ObterTipo();
            var declaracoes = new List<No>();

            while (true)
            {
                string nome = EsperarIdentificador();

                List<No> tamanhosArray = null;
                if (atual.Tipo == TipoToken.COLCHETE_ESQ)
                {
                    tamanhosArray = new List<No>();
                    while (atual.Tipo == TipoToken.COLCHETE_ESQ)
                    {
                        Avancar(); // [
                        tamanhosArray.Add(ParseExpressao());
                        Esperar(TipoToken.COLCHETE_DIR); // ]
                    }
                }

                No inicializacao = null;
                if (atual.Tipo == TipoToken.ATRIBUICAO)
                {
                    Avancar();
                    if (atual.Tipo == TipoToken.CHAVE_ESQ)
                    {
                        inicializacao = ParseListaInicializacao();
                    }
                    else
                    {
                        inicializacao = ParseExpressao();
                    }
                }

                declaracoes.Add(new NoDeclaracao(nome, tipo, inicializacao, tamanhosArray));

                if (atual.Tipo == TipoToken.VIRGULA)
                {
                    Avancar();
                }
                else
                {
                    break;
                }
            }

            return declaracoes;
        }

        private No ParseListaInicializacao()
        {
            Esperar(TipoToken.CHAVE_ESQ);
            var elementos = new List<No>();

            if (atual.Tipo != TipoToken.CHAVE_DIR)
            {
                if (atual.Tipo == TipoToken.CHAVE_ESQ)
                {
                    elementos.Add(ParseListaInicializacao());
                }
                else
                {
                    elementos.Add(ParseExpressao());
                }

                while (atual.Tipo == TipoToken.VIRGULA)
                {
                    Avancar();
                    if (atual.Tipo == TipoToken.CHAVE_ESQ)
                    {
                        elementos.Add(ParseListaInicializacao());
                    }
                    else
                    {
                        elementos.Add(ParseExpressao());
                    }
                }
            }

            Esperar(TipoToken.CHAVE_DIR);
            return new NoListaInicializacao(elementos);
        }

        private No ParseEscreva()
        {
            Esperar(TipoToken.PALAVRA_RESERVADA);
            Esperar(TipoToken.PAREN_ESQ);
            
            var argumentos = new List<No>();
            argumentos.Add(ParseExpressao());

            while (atual.Tipo == TipoToken.VIRGULA)
            {
                Avancar();
                argumentos.Add(ParseExpressao());
            }

            Esperar(TipoToken.PAREN_DIR);
            return new NoEscreva(argumentos);
        }

        private No ParseLeia()
        {
            Esperar(TipoToken.PALAVRA_RESERVADA);
            Esperar(TipoToken.PAREN_ESQ);
            
            var variaveis = new List<string>();
            variaveis.Add(EsperarIdentificador());

            while (atual.Tipo == TipoToken.VIRGULA)
            {
                Avancar();
                variaveis.Add(EsperarIdentificador());
            }

            Esperar(TipoToken.PAREN_DIR);
            return new NoLeia(variaveis);
        }

        private No ParseSe()
        {
            Esperar(TipoToken.PALAVRA_RESERVADA);
            Esperar(TipoToken.PAREN_ESQ);
            var condicao = ParseExpressao();
            Esperar(TipoToken.PAREN_DIR);

            NoBloco blocoSe;
            if (atual.Tipo == TipoToken.CHAVE_ESQ)
            {
                Avancar();
                blocoSe = ParseBloco();
                Esperar(TipoToken.CHAVE_DIR);
            }
            else
            {
                var instrucao = ParseInstrucaoSimples();
                blocoSe = new NoBloco(new List<No> { instrucao });
            }

            NoBloco blocoSenao = null;
            if (atual.Tipo == TipoToken.PALAVRA_RESERVADA && atual.Lexema.Equals("senao", StringComparison.OrdinalIgnoreCase))
            {
                Avancar();
                
                if (atual.Tipo == TipoToken.PALAVRA_RESERVADA && atual.Lexema.Equals("se", StringComparison.OrdinalIgnoreCase))
                {
                    var elseIfNode = ParseSe();
                    blocoSenao = new NoBloco(new List<No> { elseIfNode });
                }
                else if (atual.Tipo == TipoToken.CHAVE_ESQ)
                {
                    Avancar();
                    blocoSenao = ParseBloco();
                    Esperar(TipoToken.CHAVE_DIR);
                }
                else
                {
                    var instrucao = ParseInstrucaoSimples();
                    blocoSenao = new NoBloco(new List<No> { instrucao });
                }
            }

            return new NoSe(condicao, blocoSe, blocoSenao);
        }

        private No ParseEnquanto()
        {
            Esperar(TipoToken.PALAVRA_RESERVADA);
            Esperar(TipoToken.PAREN_ESQ);
            var condicao = ParseExpressao();
            Esperar(TipoToken.PAREN_DIR);

            NoBloco bloco;
            if (atual.Tipo == TipoToken.CHAVE_ESQ)
            {
                Avancar();
                bloco = ParseBloco();
                Esperar(TipoToken.CHAVE_DIR);
            }
            else
            {
                var instrucao = ParseInstrucaoSimples();
                bloco = new NoBloco(new List<No> { instrucao });
            }

            return new NoEnquanto(condicao, bloco);
        }

        private No ParseFacaEnquanto()
        {
            Esperar(TipoToken.PALAVRA_RESERVADA);
            Esperar(TipoToken.CHAVE_ESQ);
            var bloco = ParseBloco();
            Esperar(TipoToken.CHAVE_DIR);
            if (!(atual.Tipo == TipoToken.PALAVRA_RESERVADA && atual.Lexema.Equals("enquanto", StringComparison.OrdinalIgnoreCase)))
                throw new Exception("Esperado 'enquanto' apos bloco do faca");
            Avancar();
            Esperar(TipoToken.PAREN_ESQ);
            var cond = ParseExpressao();
            Esperar(TipoToken.PAREN_DIR);
            return new NoFacaEnquanto(bloco, cond);
        }

        private No ParseEscolha()
        {
            Esperar(TipoToken.PALAVRA_RESERVADA);
            Esperar(TipoToken.PAREN_ESQ);
            var expr = ParseExpressao();
            Esperar(TipoToken.PAREN_DIR);
            Esperar(TipoToken.CHAVE_ESQ);

            var casos = new List<NoCaso>();
            NoBloco padrao = null;

            while (atual.Tipo != TipoToken.CHAVE_DIR && atual.Tipo != TipoToken.EOF)
            {
                if (atual.Tipo == TipoToken.PALAVRA_RESERVADA && atual.Lexema.Equals("caso", StringComparison.OrdinalIgnoreCase))
                {
                    Avancar();
                    
                    if (atual.Tipo == TipoToken.PALAVRA_RESERVADA && (atual.Lexema.Equals("contrario", StringComparison.OrdinalIgnoreCase) || atual.Lexema.Equals("padrao", StringComparison.OrdinalIgnoreCase)))
                    {
                        Avancar();
                        Esperar(TipoToken.DOIS_PONTOS);
                        padrao = ParseBlocoCasos();
                        continue;
                    }
                    
                    var valorCaso = ParseExpressao();
                    Esperar(TipoToken.DOIS_PONTOS);
                    var corpo = ParseBlocoCasos();
                    casos.Add(new NoCaso(valorCaso, corpo));
                    continue;
                }

                if (atual.Tipo == TipoToken.PALAVRA_RESERVADA && (atual.Lexema.Equals("contrario", StringComparison.OrdinalIgnoreCase) || atual.Lexema.Equals("padrao", StringComparison.OrdinalIgnoreCase)))
                {
                    Avancar();
                    Esperar(TipoToken.DOIS_PONTOS);
                    padrao = ParseBlocoCasos();
                    continue;
                }

                break;
            }

            Esperar(TipoToken.CHAVE_DIR);
            return new NoEscolha(expr, casos, padrao);
        }

        private NoBloco ParseBlocoCasos()
        {
            var instrucoes = new List<No>();
            while (!(atual.Tipo == TipoToken.PALAVRA_RESERVADA && (atual.Lexema.Equals("caso", StringComparison.OrdinalIgnoreCase) || atual.Lexema.Equals("contrario", StringComparison.OrdinalIgnoreCase) || atual.Lexema.Equals("padrao", StringComparison.OrdinalIgnoreCase) || atual.Lexema.Equals("pare", StringComparison.OrdinalIgnoreCase)))
                   && atual.Tipo != TipoToken.CHAVE_DIR)
            {
                if (EhTipo())
                {
                    instrucoes.AddRange(ParseDeclaracaoVar());
                }
                else if (atual.Tipo == TipoToken.PALAVRA_RESERVADA || atual.Tipo == TipoToken.IDENT)
                {
                    string lex = atual.Lexema.ToLower();
                    if (lex == "escreva") instrucoes.Add(ParseEscreva());
                    else if (lex == "leia") instrucoes.Add(ParseLeia());
                    else if (lex == "se") instrucoes.Add(ParseSe());
                    else if (lex == "enquanto") instrucoes.Add(ParseEnquanto());
                    else if (lex == "faca") instrucoes.Add(ParseFacaEnquanto());
                    else if (lex == "para") instrucoes.Add(ParsePara());
                    else if (lex == "retorne") instrucoes.Add(ParseRetorne());
                    else instrucoes.Add(ParseAtribuicaoOuChamada());
                }
                else if (atual.Tipo == TipoToken.CHAVE_ESQ)
                {
                    Avancar();
                    instrucoes.AddRange(ParseBloco().Instrucoes);
                    Esperar(TipoToken.CHAVE_DIR);
                }
                else
                {
                    break;
                }
            }
            if (atual.Tipo == TipoToken.PALAVRA_RESERVADA && atual.Lexema.Equals("pare", StringComparison.OrdinalIgnoreCase))
            {
                Avancar();
            }
            return new NoBloco(instrucoes);
        }

        private No ParsePara()
        {
            Esperar(TipoToken.PALAVRA_RESERVADA);
            Esperar(TipoToken.PAREN_ESQ);

            No inicializacao;
            if (EhTipo())
            {
                var tipo = ObterTipo();
                var nome = EsperarIdentificador();
                Esperar(TipoToken.ATRIBUICAO);
                var valor = ParseExpressao();
                inicializacao = new NoDeclaracao(nome, tipo, valor);
            }
            else
            {
                inicializacao = ParseAtribOuInc();
            }

            Esperar(TipoToken.PONTO_VIRGULA);

            var condicao = ParseExpressao();
            Esperar(TipoToken.PONTO_VIRGULA);

            var atualizacao = ParseAtribOuInc();

            Esperar(TipoToken.PAREN_DIR);

            NoBloco bloco;
            if (atual.Tipo == TipoToken.CHAVE_ESQ)
            {
                Avancar();
                bloco = ParseBloco();
                Esperar(TipoToken.CHAVE_DIR);
            }
            else
            {
                var instrucao = ParseInstrucaoSimples();
                bloco = new NoBloco(new List<No> { instrucao });
            }

            return new NoPara(inicializacao, condicao, atualizacao, bloco);
        }

        private No ParseRetorne()
        {
            Esperar(TipoToken.PALAVRA_RESERVADA);
            
            No valor = null;
            if (atual.Tipo != TipoToken.CHAVE_DIR && 
                !(atual.Tipo == TipoToken.PALAVRA_RESERVADA && atual.Lexema.Equals("senao", StringComparison.OrdinalIgnoreCase)))
            {
                valor = ParseExpressao();
            }

            return new NoRetorne(valor);
        }

        private No ParseAtribuicaoOuChamada()
        {
            var nome = EsperarIdentificador();
            
            if (atual.Tipo == TipoToken.COLCHETE_ESQ)
            {
                var indices = new List<No>();
                while (atual.Tipo == TipoToken.COLCHETE_ESQ)
                {
                    Avancar();
                    indices.Add(ParseExpressao());
                    Esperar(TipoToken.COLCHETE_DIR);
                }
                
                Esperar(TipoToken.ATRIBUICAO);
                var valor = ParseExpressao();
                return new NoAtribuicaoArray(nome, indices, valor);
            }
            
            if (atual.Tipo == TipoToken.PAREN_ESQ)
            {
                Avancar();
                var argumentos = new List<No>();
                
                if (atual.Tipo != TipoToken.PAREN_DIR)
                {
                    argumentos.Add(ParseExpressao());
                    while (atual.Tipo == TipoToken.VIRGULA)
                    {
                        Avancar();
                        argumentos.Add(ParseExpressao());
                    }
                }

                Esperar(TipoToken.PAREN_DIR);
                return new NoChamadaFuncao(nome, argumentos);
            }
            else if (atual.Tipo == TipoToken.OP_INC || atual.Tipo == TipoToken.OP_DEC)
            {
                var delta = atual.Tipo == TipoToken.OP_INC ? 1 : -1;
                Avancar();
                return new NoIncremento(nome, delta);
            }
            else
            {
                Esperar(TipoToken.ATRIBUICAO);
                var valor = ParseExpressao();
                return new NoAtribuicao(nome, valor);
            }
        }

        private No ParseAtribOuInc()
        {
            var nome = EsperarIdentificador();

            if (atual.Tipo == TipoToken.OP_INC || atual.Tipo == TipoToken.OP_DEC)
            {
                var delta = atual.Tipo == TipoToken.OP_INC ? 1 : -1;
                Avancar();
                return new NoIncremento(nome, delta);
            }

            Esperar(TipoToken.ATRIBUICAO);
            var valor = ParseExpressao();
            return new NoAtribuicao(nome, valor);
        }

        private No ParseExpressao()
        {
            return ParseOu();
        }

        private No ParseOu()
        {
            var esquerda = ParseE();

            while (atual.Tipo == TipoToken.PALAVRA_RESERVADA && atual.Lexema.Equals("ou", StringComparison.OrdinalIgnoreCase))
            {
                Avancar();
                var direita = ParseE();
                esquerda = new NoExpressaoBinaria(esquerda, "ou", direita);
            }

            return esquerda;
        }

        private No ParseE()
        {
            var esquerda = ParseComparacao();

            while (atual.Tipo == TipoToken.PALAVRA_RESERVADA && atual.Lexema.Equals("e", StringComparison.OrdinalIgnoreCase))
            {
                Avancar();
                var direita = ParseComparacao();
                esquerda = new NoExpressaoBinaria(esquerda, "e", direita);
            }

            return esquerda;
        }

        private No ParseComparacao()
        {
            var esquerda = ParseAditiva();

            while (atual.Tipo == TipoToken.OP_IGUAL ||
                   atual.Tipo == TipoToken.OP_DIF ||
                   atual.Tipo == TipoToken.OP_MAIOR ||
                   atual.Tipo == TipoToken.OP_MAIOR_IGUAL ||
                   atual.Tipo == TipoToken.OP_MENOR ||
                   atual.Tipo == TipoToken.OP_MENOR_IGUAL)
            {
                string op = atual.Lexema;
                Avancar();
                var direita = ParseAditiva();
                esquerda = new NoExpressaoBinaria(esquerda, op, direita);
            }

            return esquerda;
        }

        private No ParseAditiva()
        {
            var esquerda = ParseMultiplicativa();

            while (atual.Tipo == TipoToken.OP_SOMA || atual.Tipo == TipoToken.OP_SUB)
            {
                string op = atual.Lexema;
                Avancar();
                var direita = ParseMultiplicativa();
                esquerda = new NoExpressaoBinaria(esquerda, op, direita);
            }

            return esquerda;
        }

        private No ParseMultiplicativa()
        {
            var esquerda = ParseUnaria();

            while (atual.Tipo == TipoToken.OP_MUL || 
                   atual.Tipo == TipoToken.OP_DIV ||
                   atual.Tipo == TipoToken.OP_MOD)
            {
                string op = atual.Lexema;
                Avancar();
                var direita = ParseUnaria();
                esquerda = new NoExpressaoBinaria(esquerda, op, direita);
            }

            return esquerda;
        }

        private No ParseUnaria()
        {
            if (atual.Tipo == TipoToken.OP_SUB)
            {
                Avancar();
                var operando = ParseUnaria();
                return new NoExpressaoBinaria(new NoLiteral("0", "inteiro"), "-", operando);
            }

            if (atual.Tipo == TipoToken.OP_INC || atual.Tipo == TipoToken.OP_DEC)
            {
                var delta = atual.Tipo == TipoToken.OP_INC ? 1 : -1;
                Avancar();
                if (atual.Tipo != TipoToken.IDENT)
                    throw new Exception($"Esperado identificador apos operador de incremento em [{atual.Linha},{atual.Coluna}]");
                var nome = atual.Lexema;
                Esperar(TipoToken.IDENT);
                return new NoIncremento(nome, delta);
            }

            if (atual.Tipo == TipoToken.PALAVRA_RESERVADA && atual.Lexema.Equals("nao", StringComparison.OrdinalIgnoreCase))
            {
                Avancar();
                var operando = ParseUnaria();
                return new NoExpressaoBinaria(new NoLiteral("verdadeiro", "logico"), "xor", operando);
            }

            return ParsePrimaria();
        }

        private No ParsePrimaria()
        {
            if (atual.Tipo == TipoToken.PAREN_ESQ)
            {
                Avancar();
                var expr = ParseExpressao();
                Esperar(TipoToken.PAREN_DIR);
                return expr;
            }

            if (atual.Tipo == TipoToken.NUM)
            {
                var valor = atual.Lexema;
                Avancar();
                var tipo = valor.Contains(".") ? "real" : "inteiro";
                return new NoLiteral(valor, tipo);
            }

            if (atual.Tipo == TipoToken.CARACTER_LITERAL)
            {
                var valor = atual.Lexema;
                Avancar();
                return new NoLiteral(valor, "caracter");
            }

            if (atual.Tipo == TipoToken.STRING)
            {
                var valor = atual.Lexema;
                Avancar();
                return new NoLiteral(valor, "cadeia");
            }

            if (atual.Tipo == TipoToken.PALAVRA_RESERVADA || atual.Tipo == TipoToken.IDENT)
            {
                string lex = atual.Lexema.ToLower();
                
                if (lex == "verdadeiro" || lex == "falso")
                {
                    var valor = lex;
                    Avancar();
                    return new NoLiteral(valor, "logico");
                }

                if (lex == "contrario" || lex == "padrao" || lex == "pare" || lex == "caso")
                {
                    throw new Exception($"Palavra-chave '{atual.Lexema}' não pode ser usada como valor em [{atual.Linha},{atual.Coluna}]");
                }

                var nome = atual.Lexema;
                Avancar();

                if (atual.Tipo == TipoToken.COLCHETE_ESQ)
                {
                    var indices = new List<No>();
                    while (atual.Tipo == TipoToken.COLCHETE_ESQ)
                    {
                        Avancar();
                        indices.Add(ParseExpressao());
                        Esperar(TipoToken.COLCHETE_DIR);
                    }
                    return new NoAcessoArray(nome, indices);
                }

                if (atual.Tipo == TipoToken.OP_INC || atual.Tipo == TipoToken.OP_DEC)
                {
                    var delta = atual.Tipo == TipoToken.OP_INC ? 1 : -1;
                    Avancar();
                    return new NoIncremento(nome, delta);
                }

                if (atual.Tipo == TipoToken.PAREN_ESQ)
                {
                    Avancar();
                    var argumentos = new List<No>();
                    
                    if (atual.Tipo != TipoToken.PAREN_DIR)
                    {
                        argumentos.Add(ParseExpressao());
                        while (atual.Tipo == TipoToken.VIRGULA)
                        {
                            Avancar();
                            argumentos.Add(ParseExpressao());
                        }
                    }

                    Esperar(TipoToken.PAREN_DIR);
                    return new NoChamadaFuncao(nome, argumentos);
                }
                else
                {
                    return new NoExpressao(nome);
                }
            }

            throw new Exception($"Expressão primária esperada em [{atual.Linha},{atual.Coluna}]: {atual.Lexema}");
        }
    }
}
