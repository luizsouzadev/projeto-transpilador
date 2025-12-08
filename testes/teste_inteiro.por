/*  Descrição: 
 *  Este exemplo demonstra o uso de variáveis do tipo inteiro
 *  com diferentes valores positivos, negativos e zero.
 */

programa {
  funcao inicio() {
    // Declaração de variáveis inteiras
    inteiro numero_positivo = 42
    inteiro numero_negativo = -15
    inteiro zero = 0
    inteiro numero_grande = 1000000
    
    // Exibindo os valores
    escreva("Número positivo: ", numero_positivo, "\n")
    escreva("Número negativo: ", numero_negativo, "\n")
    escreva("Zero: ", zero, "\n")
    escreva("Número grande: ", numero_grande, "\n")
    
    // Operações com inteiros
    inteiro soma = numero_positivo + numero_negativo
    inteiro produto = numero_positivo * 2
    inteiro divisao = numero_grande / 100
    
    escreva("\nOperações:\n")
    escreva("Soma (42 + (-15)): ", soma, "\n")
    escreva("Produto (42 * 2): ", produto, "\n")
    escreva("Divisão (1000000 / 100): ", divisao, "\n")
  }
}