#!/bin/bash
set -euo pipefail

TRANSPILADOR="codigo/bin/Debug/net10.0/Transpilador"
EXEMPLOS_DIR="testes"

echo "================================"
echo "Testando todos os .por em $EXEMPLOS_DIR"
echo "================================"

TOTAL=0
PASSOU=0
FALHOU=0

for arquivo in "$EXEMPLOS_DIR"/*.por; do
	[ -f "$arquivo" ] || continue
	TOTAL=$((TOTAL + 1))
	nome=$(basename "$arquivo" .por)
	echo "\n[$TOTAL] $nome"

	# Transpila
	if ! dotnet "$TRANSPILADOR.dll" "$arquivo" > /dev/null 2>&1; then
		echo "   ❌ Falha na transpilacao"
		FALHOU=$((FALHOU + 1))
		continue
	fi

	if [ ! -f "$EXEMPLOS_DIR/ProgramaTranspilado.cs" ]; then
		echo "   ❌ Arquivo C# nao gerado"
		FALHOU=$((FALHOU + 1))
		continue
	fi

	exe="$EXEMPLOS_DIR/${nome}.exe"
	if mcs -out:"$exe" "$EXEMPLOS_DIR/ProgramaTranspilado.cs" 2>/dev/null; then
		saida=$(mono "$exe" 2>&1 | head -5)
		echo "   ✅ Passou"
		echo "   Saida: $(echo "$saida" | head -1)"
		PASSOU=$((PASSOU + 1))
		rm -f "$exe"
	else
		echo "   ❌ Falha na compilacao"
		FALHOU=$((FALHOU + 1))
	fi
done

echo "\nResumo: $PASSOU/$TOTAL passaram; Falhas: $FALHOU"
