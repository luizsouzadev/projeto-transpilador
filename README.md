Projeto Transpilador (Portugol -> C#)
===================================

Resumo
------
Este repositório contém um transpilador simples que converte um subconjunto de Portugol (estrutura estilo Portugol Studio) para código C#.

Estrutura principal
- `codigo/` — código-fonte do transpilador (léxico, sintático, emissor).
- `codigo/Lexico` — analisador léxico (regex-based).
- `codigo/Sintatico` — parser simples e gerador de código (AST-lite).
- `codigo/Transpilador` — emissor e cabeçalho C#.
- `testes/` — exemplos e arquivos gerados.

Requisitos
----------

----

Como usar (Makefile)
---------------------
O repositório inclui um `Makefile` com alvos úteis:

- `make build` — tenta `dotnet build codigo` (se `dotnet` existir). Caso contrário, apenas informa e sugere `make mcs`.
- `make run` — executa o runner (usa `dotnet run` quando disponível, caso contrário usa `mono` fallback).
- `make mcs` — compila todos os `.cs` com `mcs` (Mono) gerando `programa.exe`.
- `make run-mono` — compila com `mcs` e executa com `mono`.
- `make test-lex` — executa o runner que tokeniza `testes/lex_test.txt` e gera o transpilado.
- `make run-generated` — compila e executa o arquivo `testes/ProgramaTranspilado.cs` (usa `mcs`/`mono`).

Exemplo rápido
--------------
Para tokenizar o exemplo e gerar o código C# transpilado:

```bash
make run
# ou, se preferir Mono:
make run-mono

# depois, para compilar/rodar o transpilado gerado:
make run-generated
```

Comando único (script)
----------------------

Existe um script `scripts/run_examples.sh` que executa os passos principais (compilar/rodar o transpilador e depois compilar/rodar o transpilado). Use:

```bash
./scripts/run_examples.sh
```

Ou via Makefile:

```bash
make run-examples
```

O arquivo de entrada de exemplo está em `testes/lex_test.txt` e o transpilado é salvo em `testes/ProgramaTranspilado.cs`.

O que foi implementado
----------------------
- Léxico: reconhecimento de identificadores, números, strings, operadores básicos, pontuação, comentários e palavras reservadas (`escreva`, `se`, `entao`, `senao`, `enquanto`, `inicio`, `fim`, `var`, `algoritmo`).
- Parser simples: reconhece `algoritmo`, `var` declarações, bloco `inicio`/`fim`, `ident = literal;`, `escreva(...)`, e `se ... entao ... fim`.
- Emissor: gera um arquivo C# com `namespace Transpilado` e uma classe `ProgramaTranspilado` contendo `Main()` que executa as instruções.

Limitações e próximos passos
---------------------------
- Parser e emissor cobrem apenas um subconjunto simples; não há suporte a funções, expressões complexas, tipos completos ou diagnósticos robustos.
- Próximos passos sugeridos: suportar `senao`, `então` com acento, expressões compostas, funções/procedures, testes unitários e melhor inferência de tipos.

Contribuições
-------------
Abra uma issue ou envie um patch. Posso continuar implementando recursos — diga qual prioridade prefere (ex.: `senao`/`então`, mais operadores, funções, testes).
