Transpilador Portugol -> C#
==========================

Transpilador completo de Portugol Studio para C#, com suporte a lexer, parser recursivo em AST e emissor de código


Requisitos
----------
- .NET SDK (net10.0)
- Opcional: Mono/mcs para compilar e executar o C# gerado

Uso rápido
----------
```bash
# 1) Compilar o transpilador
make build

# 2) testar todos os exemplos
make run-examples 
ou
bash ./scripts/test_exemplos.sh
```

Makefile
--------
Disponíveis os seguintes comandos:
```bash
make build          # Compila o transpilador
make clean          # Remove arquivos gerados e compilados
make test-lex       # Executa teste léxico
make run-examples   # Executa script de testes
```

Scripts
-------
- `scripts/test_exemplos.sh` — transpila e executa todos os arquivos `.por` em `testes/` (usa dotnet + mcs/mono)

Estrutura
---------
- `codigo/Lexico/` — analisador léxico e definição de tokens
- `codigo/Sintatico/` e `codigo/AST/` — parser recursivo e nós da AST
- `codigo/Transpilador/` — emissor C# baseado em padrão visitor
- `testes/` — exemplos em Portugol (`*.por`) para validação
- `documentos/tecnico.md` — especificação técnica (idiomas, tokens, gramática)
- `scripts/` — scripts de teste e validação

Funcionalidades suportadas
---------------------------
✅ Tipos de dados: `inteiro`, `real`, `lógico`, `cadeia`, `caracter`

✅ Estruturas condicionais: `if`/`else-if`/`else`, `switch`/`case`

✅ Loops: `for`, `while`, `do-while` com controle de fluxo

✅ Funções com parâmetros por valor e referência (`&`)

✅ Arrays e matrizes (1D e multidimensional)

✅ Operadores: aritméticos, relacionais, lógicos


Documentação
------------
Detalhes completos de linguagem, tokens e gramática: veja `documentos/tecnico.md`.
