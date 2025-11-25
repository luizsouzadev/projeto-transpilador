test-lex: build
PROJECT_DIR = codigo

.PHONY: all build run clean mcs run-mono test-lex

all: build

build:
	@if command -v dotnet >/dev/null 2>&1; then \
		echo "dotnet found: building with dotnet..."; \
		dotnet build $(PROJECT_DIR); \
	else \
		echo "dotnet not found: skipping dotnet build (use 'make mcs' to compile with mcs)."; \
	fi

run:
	@if command -v dotnet >/dev/null 2>&1; then \
		echo "running with dotnet..."; \
		dotnet run --project $(PROJECT_DIR); \
	else \
		$(MAKE) run-mono; \
	fi

run-nobuild:
	@if command -v dotnet >/dev/null 2>&1; then \
		echo "running (no build) with dotnet..."; \
		dotnet run --project $(PROJECT_DIR) --no-build; \
	else \
		echo "dotnet not found: use 'make run-mono' after running 'make mcs'"; \
	fi


clean:
	@if command -v dotnet >/dev/null 2>&1; then \
		dotnet clean $(PROJECT_DIR); \
	fi
	rm -f testes/ProgramaTranspilado.cs programa.exe testes/ProgramaTranspilado.exe

mcs:
	@echo "Compilando com mcs (Mono) ...";
	mcs -out:programa.exe codigo/*.cs codigo/Lexico/*.cs codigo/Sintatico/*.cs codigo/Transpilador/*.cs

run-mono: mcs
	@echo "Executando com mono ...";
	mono programa.exe

# tokenization test: reads testes/lex_test.txt and prints tokens
test-lex: build
	@if command -v dotnet >/dev/null 2>&1; then \
		dotnet run --project $(PROJECT_DIR); \
	else \
		$(MAKE) run-mono; \
	fi

# compile and run the last generated transpilado C# (testes/ProgramaTranspilado.cs)
run-generated:
	@if [ -f testes/ProgramaTranspilado.cs ]; then \
		if command -v mcs >/dev/null 2>&1; then \
			mcs -out:testes/ProgramaTranspilado.exe testes/ProgramaTranspilado.cs && mono testes/ProgramaTranspilado.exe; \
		else \
			echo "mcs/mono not found. Install Mono or run 'make run' (dotnet)"; exit 1; \
		fi \
	else \
		echo "Arquivo 'testes/ProgramaTranspilado.cs' não encontrado. Gere com 'make run' primeiro."; exit 1; \
	fi

run-examples:
	@./scripts/run_examples.sh
