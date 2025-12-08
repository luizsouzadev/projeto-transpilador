/*  Descrição: 
 *  Este exemplo demonstra o uso de vetores (arrays unidimensionais)
 *  com diferentes tipos de dados e operações.
 */

programa {
  funcao inicio() {
    // Declaração de vetores
    inteiro numeros[5] = {10, 20, 30, 40, 50}
    real notas[4] = {8.5, 9.0, 7.5, 6.8}
    cadeia nomes[3] = {"Ana", "Bruno", "Carlos"}
    logico respostas[4] = {verdadeiro, falso, verdadeiro, verdadeiro}
    
    // Exibindo elementos dos vetores
    escreva("Vetor de inteiros:\n")
    para (inteiro i = 0; i < 5; i++) {
      escreva("numeros[", i, "] = ", numeros[i], "\n")
    }
    
    escreva("\nVetor de reais (notas):\n")
    para (inteiro i = 0; i < 4; i++) {
      escreva("notas[", i, "] = ", notas[i], "\n")
    }
    
    escreva("\nVetor de caracteres (nomes):\n")
    para (inteiro i = 0; i < 3; i++) {
      escreva("nomes[", i, "] = ", nomes[i], "\n")
    }
    
    escreva("\nVetor de lógicos (respostas):\n")
    para (inteiro i = 0; i < 4; i++) {
      escreva("respostas[", i, "] = ", respostas[i], "\n")
    }
    
    // Operações com vetores
    inteiro soma = 0
    para (inteiro i = 0; i < 5; i++) {
      soma = soma + numeros[i]
    }
    
    real soma_notas = 0.0
    para (inteiro i = 0; i < 4; i++) {
      soma_notas = soma_notas + notas[i]
    }
    real media = soma_notas / 4.0
    
    inteiro respostas_corretas = 0
    para (inteiro i = 0; i < 4; i++) {
      se (respostas[i] == verdadeiro) {
        respostas_corretas++
      }
    }
    
    escreva("\nOperações:\n")
    escreva("Soma dos números: ", soma, "\n")
    escreva("Média das notas: ", media, "\n")
    escreva("Respostas corretas: ", respostas_corretas, "\n")
  }
}