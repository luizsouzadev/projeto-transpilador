#!/bin/bash

echo "================================"
echo "Testando Transpilador Portugol→C#"
echo "================================"

# Test 1
echo ""
echo "--- Teste 1: Aritmética ---"
dotnet codigo/bin/Debug/net10.0/Transpilador.dll testes/exemplo1_aritmetica.txt 2>&1 | grep -E "Arquivo C#|ERROR" && \
mcs -out:testes/ex1.exe testes/ProgramaTranspilado.cs 2>/dev/null && \
mono testes/ex1.exe && \
rm testes/ex1.exe

# Test 2
echo ""
echo "--- Teste 2: Operadores Lógicos ---"
dotnet codigo/bin/Debug/net10.0/Transpilador.dll testes/exemplo2_operadores_logicos.txt 2>&1 | grep -E "Arquivo C#|ERROR" && \
mcs -out:testes/ex2.exe testes/ProgramaTranspilado.cs 2>/dev/null && \
mono testes/ex2.exe && \
rm testes/ex2.exe

# Test 3
echo ""
echo "--- Teste 3: Loop Enquanto ---"
dotnet codigo/bin/Debug/net10.0/Transpilador.dll testes/exemplo3_enquanto.txt 2>&1 | grep -E "Arquivo C#|ERROR" && \
mcs -out:testes/ex3.exe testes/ProgramaTranspilado.cs 2>/dev/null && \
mono testes/ex3.exe && \
rm testes/ex3.exe

# Test 4
echo ""
echo "--- Teste 4: Condicional Aninhado ---"
dotnet codigo/bin/Debug/net10.0/Transpilador.dll testes/exemplo4_condicional_aninhado.txt 2>&1 | grep -E "Arquivo C#|ERROR" && \
mcs -out:testes/ex4.exe testes/ProgramaTranspilado.cs 2>/dev/null && \
mono testes/ex4.exe && \
rm testes/ex4.exe

# Test 5
echo ""
echo "--- Teste 5: Expressões Complexas ---"
dotnet codigo/bin/Debug/net10.0/Transpilador.dll testes/exemplo5_expressoes_complexas.txt 2>&1 | grep -E "Arquivo C#|ERROR" && \
mcs -out:testes/ex5.exe testes/ProgramaTranspilado.cs 2>/dev/null && \
mono testes/ex5.exe && \
rm testes/ex5.exe

echo ""
echo "✓ Todos os testes concluídos!"
