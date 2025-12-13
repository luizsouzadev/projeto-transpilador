# Documento Tecnico - Transpilador Portugol -> C#

## 1. Linguagens
- Origem: Portugol Studio
- Destino: C# 

## 2. Justificativa
Permitir que programas escritos para ensino (Portugol) sejam convertidos para C# sem reescrita manual, servindo como ponte de aprendizagem e aprofundamento de estudos.

## 3. Tokens suportados (léxico)
- Tipos: `inteiro`, `real`, `logico`, `caracter`, `cadeia`

- Palavras-chave (case-insensitive): `programa`, `funcao`, `retorne`, `escreva`, `leia`, `se`, `senao`, `enquanto`, `para`, `faca`, `escolha`, `caso`, `contrario`, `padrao`, `pare`

- Operadores lógicos: `e`, `ou`, `nao`

- Booleanos: `verdadeiro`, `falso` (também permitidos como identificadores)

- Operadores aritméticos: `+`, `-`, `*`, `/`, `%`, `++`, `--`

- Operadores relacionais: `==`, `!=`, `<`, `<=`, `>`, `>=`

- Atribuição: `=`

- Referência: `&` (para parâmetros por referência)

- Delimitadores: `(` `)` `{` `}` `[` `]`

- Pontuação: `,` `;` `.` `:`

- Literais: números inteiros/reais, strings em aspas duplas, caracteres em aspas simples

- Comentários: linha (`//`) e bloco (`/* ... */`)

## 4. Gramática (EBNF alinhada ao parser atual)
```
Programa ::= 'programa' '{' (DeclaracaoVar | FuncaoDecl)* '}'

Tipo ::= 'inteiro' | 'real' | 'logico' | 'caracter' | 'cadeia'

DeclaracaoVar ::= Tipo Declarador (',' Declarador)*

Declarador ::= Ident Indices? Inicializacao?

Indices ::= ('[' Expressao ']')+

Inicializacao ::= '=' (ListaInicializacao | Expressao)

ListaInicializacao ::= '{' ElementoInit (',' ElementoInit)* '}'

ElementoInit ::= ListaInicializacao | Expressao

FuncaoDecl ::= 'funcao' Tipo? Ident '(' Parametros? ')' Bloco

Parametros ::= Param (',' Param)*

Param ::= Tipo '&'? Ident

Bloco ::= '{' Instrucao* '}'

Instrucao ::= 
    | DeclaracaoVar ';'
    | Escreva ';'
    | Leia ';'
    | Retorne ';'
    | Pare ';'
    | AtribuicaoOuChamada ';'
    | Se
    | Enquanto
    | FacaEnquanto
    | Para
    | Escolha
    | Bloco
                  

Escreva ::= 'escreva' '(' ArgList ')'

Leia ::= 'leia' '(' Ident (',' Ident)* ')'

ArgList ::= Expressao (',' Expressao)*

Se ::= 'se' '(' Expressao ')' InstrucaoOuBloco ('senao' InstrucaoOuBloco)?

InstrucaoOuBloco ::= Instrucao | Bloco

Enquanto ::= 'enquanto' '(' Expressao ')' InstrucaoOuBloco

FacaEnquanto ::= 'faca' Bloco 'enquanto' '(' Expressao ')'

Para ::= 'para' '(' InicializacaoPara ';' Expressao ';' AtualizacaoPara ')' InstrucaoOuBloco

InicializacaoPara ::= DeclaracaoVar | AtribOuInc

AtualizacaoPara ::= AtribOuInc

Escolha ::= 'escolha' '(' Expressao ')' '{' (Caso | Padrao)* '}'

Caso ::= 'caso' Expressao ':' BlocoCasos

Padrao ::= 'caso' ('contrario' | 'padrao') ':' BlocoCasos

BlocoCasos ::= InstrucaoCasos*

Instrucao ::= 
    | DeclaracaoVar ';'
    | Escreva ';'
    | Leia ';'
    | Retorne ';'
    | Pare ';'
    | AtribuicaoOuChamada ';'
    | Se
    | Enquanto
    | FacaEnquanto
    | Para
    | Escolha
    | Bloco

Pare ::= 'pare'

Retorne ::= 'retorne' Expressao?

AtribuicaoOuChamada ::= 
    | Ident Indices? '=' Expressao
    | Ident '(' ArgList? ')'
    | Ident ('++' | '--')

AtribOuInc ::= 
    | Ident '=' Expressao
    | Ident ('++' | '--')

Expressao ::= ExprOu

ExprOu ::= ExprE ('ou' ExprE)*

ExprE ::= ExprRel ('e' ExprRel)*

ExprRel ::= ExprAdd (RelOp ExprAdd)*

RelOp ::= '==' | '!=' | '<' | '>' | '<=' | '>='

ExprAdd ::= ExprMul (('+' | '-') ExprMul)*

ExprMul ::= ExprUn (('*' | '/' | '%') ExprUn)*

ExprUn ::= 
    | '-' ExprUn
    | 'nao' ExprUn
    | '++' Ident
    | '--' Ident
    | Primaria

Primaria ::= 
    | '(' Expressao ')'
    | Numero
    | Caracter
    | String
    | ('verdadeiro' | 'falso')
    | Ident Sufixo?

Sufixo ::= 
    | Indices
    | '(' ArgList? ')'
    | ('++' | '--')

Ident ::= [A-Za-z_][A-Za-z0-9_]*

Numero ::= [0-9]+('.'[0-9]+)?

Caracter ::= '\'' (\\.|[^'\n]) '\''

String ::= '"' (\\.|[^"\n])* '"'
```

### Observações
- Para gerar um ponto de entrada, declare `funcao inicio() { ... }`; o emissor mapeia `inicio` para `static void Main(string[] args)` quando não há parâmetros.

- Arrays são suportados com sintaxe `tipo nome[tamanho1][tamanho2]...` com inicialização via `{...}`.

- Booleanos `verdadeiro` e `falso` podem ser usados como nomes de variáveis.

- Caracteres especiais em nomes de variáveis causarão erro (comportamento esperado).

## 5. Mapeamento de tipos (Portugol → C#)
| Portugol | C# | Padrão |
|----------|----|----|
| `inteiro` | `int` | 0 |
| `real` | `double` | 0.0 |
| `lógico` | `bool` | false |
| `cadeia` | `string` | "" |
| `caracter` | `string` | "" |
| `vazio` | `void` | N/A |

## 6. Transformações principais do emissor
- **Função `início()`** → `static void Main(string[] args)`
- **Operadores lógicos** → `e` → `&&`, `ou` → `||`, `nao` → `!`
- **Booleanos** → `verdadeiro` → `true`, `falso` → `false`
- **Arrays multidimensionais** → Sintaxe C# nativa `[,]` com índices separados por vírgula
- **Palavras-chave** → Escapadas com `@` quando necessário (ex: `@decimal`)

## 7. Exemplos de conversão
```portugol
//  Entrada: Portugol
programa {
  funcao inicio() {
    inteiro x = 10
    inteiro y = 20
    inteiro soma = x + y
    
    escreva("x = ", x, "\n")
    escreva("y = ", y, "\n")
    escreva("soma = ", soma, "\n")
  }
}
```

```csharp
// Saída: C#
using System;
class Programa {
    static void Main(string[] args)
    {
        int x = 10;
        int y = 20;
        int soma = (x + y);
        Console.Write("x = " + x + "\n");
        Console.Write("y = " + y + "\n");
        Console.Write("soma = " + soma + "\n");
    }
}
```
