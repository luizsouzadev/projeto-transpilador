/*  Descrição: 
 *  Tratamento manual de erros no Portugol Studio (não há try-catch).
 */

programa {
  
  // Divisão segura - evita divisão por zero
  funcao real divisao_segura(real a, real b) {
    se (b == 0.0) {
      escreva("ERRO: Divisão por zero!\n")
      retorne -1.0  // Código de erro
    }
    retorne a / b
  }
  
  // Validação de idade
  funcao logico idade_valida(inteiro idade) {
    se (idade < 0 ou idade > 120) {
      escreva("ERRO: Idade inválida!\n")
      retorne falso
    }
    retorne verdadeiro
  }
  
  funcao inicio() {
    escreva("=== TRATAMENTO DE ERROS ===\n\n")
    
    // 1. Testando divisões
    escreva("1. DIVISÃO SEGURA:\n")
    real resultado1 = divisao_segura(10.0, 2.0)
    se (resultado1 != -1.0) {
      escreva("10 / 2 = ", resultado1, "\n")
    }
    
    real resultado2 = divisao_segura(8.0, 0.0)  // Erro!
    escreva("\n")
    
    // 2. Testando idades
    escreva("2. VALIDAÇÃO DE IDADE:\n")
    inteiro idades[3] = {25, -5, 150}
    
    para (inteiro i = 0; i < 3; i++) {
      escreva("Idade ", idades[i], ": ")
      se (idade_valida(idades[i])) {
        escreva("OK\n")
      } senao {
        escreva("Inválida\n")
      }
    }

  }
}