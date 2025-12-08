/*  Descrição: 
 *  Este exemplo demonstra o uso do laço FACA-ENQUANTO
 *  e comandos de controle PARE e CONTINUE.
 */

programa {
  funcao inicio() {
    escreva("=== LAÇO FAÇA-ENQUANTO E CONTROLES ===\n\n")
    
    // 1. Diferença entre ENQUANTO e FACA-ENQUANTO
    escreva("1. COMPARANDO ENQUANTO vs FAÇA-ENQUANTO:\n")
    
    escreva("ENQUANTO (condição falsa):\n")
    inteiro i = 10
    enquanto (i < 5) {
      escreva("Este código nunca executa\n")
      i++
    }
    escreva("Não executou nenhuma vez\n\n")
    
    escreva("FAÇA-ENQUANTO (condição falsa):\n")
    inteiro j = 10
    faca {
      escreva("Este código executa pelo menos uma vez, j = ", j, "\n")
      j++
    } enquanto (j < 5)
    escreva("Executou uma vez mesmo com condição falsa\n\n")
    
    // 2. Menu com FACA-ENQUANTO
    escreva("2. SIMULAÇÃO DE MENU:\n")
    inteiro opcao = 0
    inteiro contador_menu = 0
    
    faca {
      contador_menu++
      escreva("=== MENU (iteração ", contador_menu, ") ===\n")
      escreva("1 - Opção A\n")
      escreva("2 - Opção B\n")
      escreva("3 - Opção C\n")
      escreva("0 - Sair\n")
      
      // Simulando escolhas do usuário
      se (contador_menu == 1) opcao = 1
      senao se (contador_menu == 2) opcao = 2
      senao se (contador_menu == 3) opcao = 0
      
      escreva("Opção escolhida: ", opcao, "\n")
      
      escolha (opcao) {
        caso 1:
          escreva("Executando Opção A...\n")
          pare
        caso 2:
          escreva("Executando Opção B...\n")
          pare
        caso 3:
          escreva("Executando Opção C...\n")
          pare
        caso 0:
          escreva("Saindo...\n")
          pare
        caso contrario:
          escreva("Opção inválida!\n")
      }
      escreva("\n")
      
    } enquanto (opcao != 0)
    
    // 3. Simulando CONTINUE (pulando números pares)
    escreva("3. SIMULANDO CONTINUE (pulando números pares):\n")
    para (inteiro k = 1; k <= 10; k++) {
      se (k % 2 != 0) {  // Só executa se for ímpar
        escreva("Número ímpar: ", k, "\n")
      }
      // Números pares são "pulados" pela condição
    }
    escreva("\n")  

  }
}