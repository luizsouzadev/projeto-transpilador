/*  Descrição: 
 *  Este exemplo demonstra conceitos de sobrecarga de operadores
 *  e como diferentes tipos podem usar os mesmos operadores.
 *  Nota: Portugol Studio não permite definir sobrecarga personalizada,
 *  mas tem sobrecarga built-in para alguns operadores.
 */

programa {
  
  // Função auxiliar para mostrar operações
  funcao mostrar_operacao(real a, cadeia op, real b, real resultado) {
    escreva(a, " ", op, " ", b, " = ", resultado, "\n")
  }
  
  funcao inicio() {
    escreva("=== SOBRECARGA DE OPERADORES ===\n\n")
    
    escreva("O Portugol Studio tem sobrecarga automática para alguns operadores:\n\n")
    
    // 1. Operador + com diferentes tipos
    escreva("1. OPERADOR + COM DIFERENTES TIPOS:\n")
    
    // Soma de inteiros
    inteiro int1 = 10
    inteiro int2 = 20
    inteiro soma_int = int1 + int2
    escreva("Inteiros: ", int1, " + ", int2, " = ", soma_int, "\n")
    
    // Soma de reais
    real real1 = 5.5
    real real2 = 3.2
    real soma_real = real1 + real2
    escreva("Reais: ", real1, " + ", real2, " = ", soma_real, "\n")
    
    // Concatenação de strings
    cadeia str1 = "Olá"
    cadeia str2 = " Mundo"
    cadeia concat = str1 + str2
    escreva("Strings: \"", str1, "\" + \"", str2, "\" = \"", concat, "\"\n\n")
    
    // 2. Operador + com tipos mistos
    escreva("2. TIPOS MISTOS (conversão automática):\n")
    inteiro num_int = 10
    real num_real = 5.7
    real resultado_misto = num_int + num_real
    escreva("Inteiro + Real: ", num_int, " + ", num_real, " = ", resultado_misto, "\n")
    escreva("(inteiro é convertido para real automaticamente)\n\n")
    
    // 3. Operadores relacionais com diferentes tipos
    escreva("3. COMPARAÇÕES COM DIFERENTES TIPOS:\n")
    inteiro idade1 = 25
    inteiro idade2 = 30
    real altura1 = 1.75
    real altura2 = 1.80
    cadeia nome1 = "Ana"
    cadeia nome2 = "Bruno"
    
    escreva("Inteiros: ", idade1, " < ", idade2, " = ", (idade1 < idade2), "\n")
    escreva("Reais: ", altura1, " >= ", altura2, " = ", (altura1 >= altura2), "\n")
    escreva("Strings: \"", nome1, "\" == \"", nome2, "\" = ", (nome1 == nome2), "\n\n")
    
    // 4. Operador == com diferentes contextos
    escreva("4. OPERADOR == EM DIFERENTES CONTEXTOS:\n")
    inteiro numero = 5
    real decimal = 5.0
    cadeia texto_numero = "5"
    
    escreva("Número inteiro: ", numero, "\n")
    escreva("Número real: ", decimal, "\n")
    escreva("String: \"", texto_numero, "\"\n")
    escreva("inteiro == real: ", numero, " == ", decimal, " = ", (numero == decimal), "\n")
    escreva("(comparação numérica funciona)\n\n")
    
    // 5. Operadores lógicos - sempre com tipo lógico
    escreva("5. OPERADORES LÓGICOS:\n")
    logico condicao1 = verdadeiro
    logico condicao2 = falso
    inteiro valor1 = 10
    inteiro valor2 = 5
    
    escreva("Lógico E lógico: ", condicao1, " e ", condicao2, " = ", (condicao1 e condicao2), "\n")
    escreva("Comparação E comparação: (", valor1, " > ", valor2, ") e (", valor2, " > 0) = ", ((valor1 > valor2) e (valor2 > 0)), "\n\n")
    
    // 6. Demonstração prática - calculadora simples
    escreva("6. EXEMPLO PRÁTICO - CALCULADORA:\n")
    
    real num1 = 15.0
    real num2 = 4.0
    
    // Mesmo formato de saída, operadores diferentes
    mostrar_operacao(num1, "+", num2, num1 + num2)
    mostrar_operacao(num1, "-", num2, num1 - num2)
    mostrar_operacao(num1, "*", num2, num1 * num2)
    mostrar_operacao(num1, "/", num2, num1 / num2)
  }
}