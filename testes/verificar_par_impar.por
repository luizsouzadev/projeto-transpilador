// Este programa verifica se um número é par ou ímpar
programa {
  funcao inicio() {
    inteiro numero = 7 // número a ser verificado

    // verifica se o número é par
    se (numero % 2 == 0) {
      escreva("Número par")
    } senao {
      escreva("Número ímpar")
    }
  }
}