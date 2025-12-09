# Documento Tecnico - Transpilador Portugol -> C#

## 1. Linguagens
- Origem: Portugol (dialeto estilo Portugol Studio)
- Destino: C# (geracao de `ProgramaTranspilado.cs` com classe `Programa`)

## 2. Justificativa
Permitir que programas escritos para ensino (Portugol) sejam convertidos para C# sem reescrita manual, servindo como ponte de aprendizagem e como vitrine simples de pipeline compilador (lexico + parser + emissor).

## 3. Tokens suportados (léxico)
- Tipos: `inteiro`, `real`, `lógico`, `caracter`, `cadeia`
- Palavras-chave: `programa`, `função`, `retorne`, `escreva`, `leia`, `se`, `senão`, `enquanto`, `para`, `faça`, `escolha`, `caso`, `contrário`, `pare`
- Operadores lógicos: `e`, `ou`, `não`
- Booleanos: `verdadeiro`, `falso` (também permitidos como identificadores)
- Operadores aritméticos: `+`, `-`, `*`, `/`, `%`, `++`, `--`
- Operadores relacionais: `==`, `!=`, `<`, `<=`, `>`, `>=`
- Atribuição: `=`
- Referência: `&` (para parâmetros por referência)
- Delimitadores: `(` `)` `{` `}` `[` `]`
- Pontuação: `,` `;` `.` `:`
- Literais: números inteiros/reais, strings em aspas duplas, caracteres em aspas simples
- Comentários: linha (`//`) e bloco (`/* ... */`)

## 4. Gramatica (EBNF alinhada ao parser atual)
```
programa        ::= 'programa' '{' (declaracao_var | funcao)* '}'
funcao          ::= 'funcao' tipo? IDENT '(' parametros? ')' '{' bloco '}'
parametros      ::= (tipo IDENT) (',' tipo IDENT)*

declaracao_var  ::= tipo IDENT ('=' expressao)? (',' IDENT ('=' expressao)? )*
tipo            ::= 'inteiro' | 'real' | 'logico' | 'cadeia'

bloco           ::= instrucao*
instrucao       ::= declaracao_var
                  | escreva
                  | leia
                  | se
                  | enquanto
                  | para
                  | retorne
                  | atribuicao
                  | chamada_funcao
                  | '{' bloco '}'

escreva         ::= 'escreva' '(' expressao (',' expressao)* ')'
leia            ::= 'leia' '(' IDENT (',' IDENT)* ')'
se              ::= 'se' '(' expressao ')' '{' bloco '}' ('senao' '{' bloco '}')?
enquanto        ::= 'enquanto' '(' expressao ')' '{' bloco '}'
para            ::= 'para' '(' IDENT '=' expressao ';' expressao ';' IDENT '=' expressao ')' '{' bloco '}'
retorne         ::= 'retorne' expressao?
atribuicao      ::= IDENT '=' expressao
chamada_funcao  ::= IDENT '(' expressao? (',' expressao)* ')'

expressao       ::= ou
ou              ::= e ('ou' e)*
e               ::= comparacao ('e' comparacao)*
comparacao      ::= aditiva (op_rel aditiva)*
op_rel          ::= '==' | '!=' | '<' | '<=' | '>' | '>='
aditiva         ::= multiplicativa (('+' | '-') multiplicativa)*
multiplicativa  ::= unaria (('*' | '/' | '%') unaria)*
unaria          ::= '-' unaria | 'nao' unaria | primaria
primaria        ::= NUM | STRING | 'verdadeiro' | 'falso'
                  | IDENT | chamada_funcao | '(' expressao ')'
```

### Observacoes
- Para gerar um ponto de entrada, declare `funcao inicio { ... }`; o emissor mapeia `inicio` para `static void Main(string[] args)`.
- Arrays/colchetes e literais de caractere sao apenas tokenizados; ainda nao ha suporte sintatico.
- `cadeia` e aceita como tipo mesmo sendo tokenizada como IDENT.
