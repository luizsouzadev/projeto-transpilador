/*  Descrição: 
 *  Este exemplo demonstra a precedência padrão de operadores
 *  no Portugol Studio e como ela afeta o resultado das expressões.
 */

programa {
  funcao inicio() {
    escreva("=== PRECEDÊNCIA DE OPERADORES ===\n\n")
    
    // Precedência: () > */ > +- > comparação > lógicos
    inteiro a = 10
    inteiro b = 5
    inteiro c = 2
    
    escreva("Valores: a = ", a, ", b = ", b, ", c = ", c, "\n\n")
    
    // 1. Precedência de operadores aritméticos
    escreva("1. OPERADORES ARITMÉTICOS:\n")
    inteiro resultado1 = a + b * c  // 5 * 2 primeiro = 10, depois 10 + 10 = 20
    escreva("a + b * c = ", a, " + ", b, " * ", c, " = ", resultado1, "\n")
    escreva("(sem parênteses: multiplicação primeiro)\n")
    
    inteiro resultado2 = (a + b) * c  // (10 + 5) primeiro = 15, depois 15 * 2 = 30
    escreva("(a + b) * c = (", a, " + ", b, ") * ", c, " = ", resultado2, "\n")
    escreva("(com parênteses: soma primeiro)\n\n")
    
    // 2. Precedência com divisão e resto
    escreva("2. DIVISÃO E RESTO:\n")
    inteiro resultado3 = a + b / c * 3  // 5/2=2, 2*3=6, 10+6=16
    escreva("a + b / c * 3 = ", a, " + ", b, " / ", c, " * 3 = ", resultado3, "\n")
    escreva("(ordem: divisão, multiplicação, soma)\n\n")
    
    // 3. Precedência com operadores relacionais
    escreva("3. OPERADORES RELACIONAIS:\n")
    logico resultado4 = a + b > c * 5  // 15 > 10 = verdadeiro
    escreva("a + b > c * 5 = ", a, " + ", b, " > ", c, " * 5 = ", resultado4, "\n")
    escreva("(aritmética primeiro, depois comparação)\n\n")
    
    // 4. Precedência com operadores lógicos
    escreva("4. OPERADORES LÓGICOS:\n")
    logico resultado5 = a > b e b > c ou c == 2
    escreva("a > b e b > c ou c == 2 = ")
    escreva(a, " > ", b, " e ", b, " > ", c, " ou ", c, " == 2 = ", resultado5, "\n")
    escreva("(comparações primeiro, depois E, depois OU)\n\n")
    
    // 5. Expressão complexa
    escreva("5. EXPRESSÃO COMPLEXA:\n")
    real x = 2.0
    real y = 3.0
    real z = 4.0
    real resultado6 = x + y * z / 2 - 1
    escreva("x + y * z / 2 - 1 = ", x, " + ", y, " * ", z, " / 2 - 1 = ", resultado6, "\n")
    escreva("Ordem: 3*4=12, 12/2=6, 2+6=8, 8-1=7\n\n")
    
    // 6. Comparação com e sem parênteses
    escreva("6. IMPORTÂNCIA DOS PARÊNTESES:\n")
    inteiro sem_parenteses = 10 + 5 * 2
    inteiro com_parenteses = (10 + 5) * 2
    escreva("Sem parênteses: 10 + 5 * 2 = ", sem_parenteses, "\n")
    escreva("Com parênteses: (10 + 5) * 2 = ", com_parenteses, "\n")
    escreva("Diferença: ", com_parenteses - sem_parenteses, "\n")
  }
}