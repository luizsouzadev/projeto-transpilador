#!/bin/bash

# Script para testar todos os exemplos de Portugol

EXEMPLO_DIR="testes"
TRANSPILADOR="codigo/bin/Debug/net10.0/Transpilador"

echo "================================"
echo "Testando Transpilador Portugol→C#"
echo "================================"

for exemplo in $EXEMPLO_DIR/exemplo*.txt; do
    if [ -f "$exemplo" ]; then
        nome=$(basename "$exemplo" .txt)
        echo ""
        echo "--- Testando: $nome ---"
        
        # Criar arquivo C# temporário
        csharp_file="${EXEMPLO_DIR}/${nome}.cs"
        
        # Usar o transpilador através de um comando direto
        dotnet "$TRANSPILADOR.dll" < "$exemplo" > /dev/null 2>&1
        
        # Compilar se o arquivo foi gerado
        if [ -f "$EXEMPLO_DIR/ProgramaTranspilado.cs" ]; then
            # Renomear para teste
            mv "$EXEMPLO_DIR/ProgramaTranspilado.cs" "$csharp_file"
            
            # Compilar com dotnet
            dotnet new console -n temp_test --force -q 2>/dev/null
            cp "$csharp_file" "temp_test/Program.cs"
            
            echo "Saída:"
            dotnet run --project temp_test -q 2>&1 | head -20
            
            rm -rf temp_test "$csharp_file"
        fi
    fi
done

echo ""
echo "Testes concluídos!"
