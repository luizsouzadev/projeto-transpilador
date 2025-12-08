#!/bin/bash

TRANSPILADOR="codigo/bin/Debug/net10.0/Transpilador"
EXEMPLO_DIR="testes"

echo "================================"
echo "Testando Transpilador Portugol→C#"
echo "================================"

for exemplo in $EXEMPLO_DIR/exemplo*.txt; do
    if [ -f "$exemplo" ]; then
        nome=$(basename "$exemplo" .txt)
        echo ""
        echo "--- $nome ---"
        echo "Entrada:"
        head -3 "$exemplo"
        echo "..."
        echo ""
        echo "Saída transpilada:"
        
        # Transpila e executa
        dotnet "$TRANSPILADOR.dll" < "$exemplo" > /dev/null 2>&1
        
        if [ -f "$EXEMPLO_DIR/ProgramaTranspilado.cs" ]; then
            # Compila e executa
            cd "$EXEMPLO_DIR"
            dotnet new console -n test_temp --force -q 2>/dev/null || true
            cp ProgramaTranspilado.cs test_temp/Program.cs
            echo "Resultado da execução:"
            dotnet run --project test_temp -q 2>&1
            rm -rf test_temp
            cd ..
        fi
    fi
done

echo ""
echo "✓ Testes concluídos!"
