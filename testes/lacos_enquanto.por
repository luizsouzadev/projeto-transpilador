/*  Descrição: 
 *  Este exemplo demonstra o uso do laço ENQUANTO
 *  com diferentes condições e aplicações práticas.
 */

programa {
  funcao inicio() {
    escreva("=== LAÇO DE REPETIÇÃO ENQUANTO ===\n\n")
    
    // 1. Laço básico com contador
    escreva("1. CONTADOR BÁSICO (0 a 4):\n")
    inteiro contador = 0
    enquanto (contador < 5) {
      escreva("contador = ", contador, "\n")
      contador++
    }
    escreva("\n")
    
    // 2. Laço com condição de parada
    escreva("2. SOMA ATÉ ULTRAPASSAR 50:\n")
    inteiro soma = 0
    inteiro numero = 1
    enquanto (soma <= 50) {
      soma = soma + numero
      escreva("Somando ", numero, ", total = ", soma, "\n")
      numero++
    }
    escreva("Soma final: ", soma, "\n\n")
    
    // 3. Validação de entrada (simulada)
    escreva("3. VALIDAÇÃO DE SENHA:\n")
    cadeia senha_correta = "123456"
    cadeia senha_digitada = "errada"  // Simulando entrada errada
    inteiro tentativas = 0
    inteiro max_tentativas = 3
    
    enquanto (senha_digitada != senha_correta e tentativas < max_tentativas) {
      tentativas++
      escreva("Tentativa ", tentativas, ": senha incorreta\n")
      
      // Simulando diferentes tentativas
      se (tentativas == 1) {
        senha_digitada = "654321"
      } senao se (tentativas == 2) {
        senha_digitada = "123456"  // Acerta na segunda tentativa
      }
    }
    
    se (senha_digitada == senha_correta) {
      escreva("Acesso autorizado!\n")
    } senao {
      escreva("Acesso negado! Muitas tentativas.\n")
    }
    escreva("\n")
    
    
    // 4. Menu simulado
    escreva("4. SIMULAÇÃO DE MENU:\n")
    inteiro opcao = 1
    
    enquanto (opcao != 0) {
      escreva("Opção ", opcao, " selecionada\n")
      
      escolha (opcao) {
        caso 1:
          escreva("Executando função 1...\n")
          pare
        caso 2:
          escreva("Executando função 2...\n")
          pare
        caso 3:
          escreva("Executando função 3...\n")
          pare
      }
      
      opcao++
      se (opcao > 3) opcao = 0  // Simula seleção de sair
    }
    escreva("Saindo do menu...\n")
  }
}