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
programa        ::= 'programa' '{' (declaracao_var | funcao)* '}'

tipo            ::= 'inteiro' | 'real' | 'logico' | 'caracter' | 'cadeia'

funcao          ::= 'funcao' tipo? IDENT '(' parametros? ')' bloco
parametros      ::= (tipo '&'? IDENT) (',' tipo '&'? IDENT)*

bloco           ::= '{' instrucao* '}'
instrucao       ::= declaracao_var
                  | escreva
                  | leia
                  | se_senao
                  | enquanto
                  | para
                  | faca_enquanto
                  | escolha
                  | retorne
                  | atribuicao_ou_chamada
                  | pare
                  | bloco

declaracao_var  ::= tipo IDENT ('[' expressao ']')* ( '=' ( lista_inicializacao | expressao ) )? 
                    (',' IDENT ('[' expressao ']')* ( '=' expressao )? )*

lista_inicializacao ::= '{' elemento_init (',' elemento_init)* '}'
elemento_init   ::= lista_inicializacao | expressao

escreva         ::= 'escreva' '(' expressao (',' expressao)* ')'
leia            ::= 'leia' '(' IDENT (',' IDENT)* ')'
se_senao        ::= 'se' '(' expressao ')' instrucao ('senao' instrucao)?
enquanto        ::= 'enquanto' '(' expressao ')' instrucao
para            ::= 'para' '(' (declaracao_var | atrib_ou_inc) ';' expressao ';' atrib_ou_inc ')' instrucao
faca_enquanto   ::= 'faca' bloco 'enquanto' '(' expressao ')'
escolha         ::= 'escolha' '(' expressao ')' '{' (caso | padrao)* '}'
caso            ::= 'caso' expressao ':' bloco_casos
padrao          ::= 'caso' ('contrario' | 'padrao') ':' bloco_casos
bloco_casos     ::= instrucao_casos*
instrucao_casos ::= declaracao_var | escreva | leia | se_senao | enquanto | para | retorne | atribuicao_ou_chamada | bloco | pare

retorne         ::= 'retorne' expressao?
atribuicao_ou_chamada ::= IDENT ('[' expressao ']')* ( '=' expressao | '(' argumentos? ')' | '++' | '--' )
atrib_ou_inc    ::= IDENT ( '=' expressao | '++' | '--' )
pare            ::= 'pare'

expressao       ::= ou
ou              ::= e ('ou' e)*
e               ::= comparacao ('e' comparacao)*
comparacao      ::= aditiva (op_rel aditiva)*
op_rel          ::= '==' | '!=' | '<' | '<=' | '>' | '>='
aditiva         ::= multiplicativa (('+' | '-') multiplicativa)*
multiplicativa  ::= unaria (('*' | '/' | '%') unaria)*
unaria          ::= '-' unaria | 'nao' unaria | '++' IDENT | '--' IDENT | primaria
primaria        ::= NUM | STRING | CARACTER | 'verdadeiro' | 'falso'
                  | IDENT ('[' expressao ']')* ('(' argumentos? ')')? ('++' | '--')?
                  | '(' expressao ')'

argumentos      ::= expressao (',' expressao)*
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
- **Operadores lógicos** → `e` → `&&`, `ou` → `||`, `nao` → `!=` (XOR)
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
