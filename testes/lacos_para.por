/*  Descrição: 
 *  Este exemplo demonstra o uso do laço PARA
 *  com diferentes configurações e aplicações práticas.
 */

programa {
  funcao inicio() {
    escreva("=== LAÇO DE REPETIÇÃO PARA ===\n\n")
    
    // 1. Laço básico crescente
    escreva("1. LAÇO BÁSICO (0 a 4):\n")
    para (inteiro i = 0; i < 5; i++) {
      escreva("i = ", i, "\n")
    }
    escreva("\n")
    
    // 2. Laço decrescente
    escreva("2. LAÇO DECRESCENTE (10 a 6):\n")
    para (inteiro i = 10; i >= 6; i--) {
      escreva("i = ", i, "\n")
    }
    escreva("\n")
    
    // 3. Laço com incremento diferente
    escreva("3. LAÇO COM INCREMENTO DE 2:\n")
    para (inteiro i = 0; i <= 10; i = i + 2) {
      escreva("i = ", i, "\n")
    }
    escreva("\n")
    
    // 4. Soma de números
    escreva("4. SOMA DE 1 A 10:\n")
    inteiro soma = 0
    para (inteiro i = 1; i <= 10; i++) {
      soma = soma + i
      escreva("Somando ", i, ", total = ", soma, "\n")
    }
    escreva("Soma final: ", soma, "\n\n")
    
    
    // 5. Fatorial
    escreva("5. FATORIAL DE 5:\n")
    inteiro n = 5
    inteiro fatorial = 1
    escreva("Calculando ", n, "!:\n")
    
    para (inteiro i = 1; i <= n; i++) {
      fatorial = fatorial * i
      escreva("Passo ", i, ": ", fatorial, "\n")
    }
    escreva("Resultado: ", n, "! = ", fatorial, "\n\n")

  }
}