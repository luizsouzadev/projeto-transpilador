/*  Descrição: 
 *  Este exemplo demonstra o uso de matrizes (arrays bidimensionais)
 *  com diferentes tipos de dados e operações.
 */

programa {
  funcao inicio() {
    // Declaração de matrizes
    inteiro matriz_numeros[3][3] = {
      {1, 2, 3},
      {4, 5, 6},
      {7, 8, 9}
    }
    
    real notas_turma[2][4] = {
      {8.5, 9.0, 7.5, 6.8},
      {7.2, 8.8, 9.5, 8.0}
    }
    
    cadeia tabuleiro[3][3] = {
      {"X", "O", "X"},
      {"O", "X", "O"},
      {"X", "O", "X"}
    }
    
    logico matriz_booleana[2][2] = {
      {verdadeiro, falso},
      {falso, verdadeiro}
    }
    
    // Exibindo elementos das matrizes
    escreva("Matriz de inteiros 3x3:\n")
    para (inteiro i = 0; i < 3; i++) {
      para (inteiro j = 0; j < 3; j++) {
        escreva(matriz_numeros[i][j], " ")
      }
      escreva("\n")
    }
    
    escreva("\nMatriz de notas da turma (2 alunos, 4 provas):\n")
    para (inteiro i = 0; i < 2; i++) {
      escreva("Aluno ", i + 1, ": ")
      para (inteiro j = 0; j < 4; j++) {
        escreva(notas_turma[i][j], " ")
      }
      escreva("\n")
    }
    
    escreva("\nTabuleiro (Jogo da Velha):\n")
    para (inteiro i = 0; i < 3; i++) {
      para (inteiro j = 0; j < 3; j++) {
        escreva(tabuleiro[i][j], " ")
      }
      escreva("\n")
    }
    
    escreva("\nMatriz booleana 2x2:\n")
    para (inteiro i = 0; i < 2; i++) {
      para (inteiro j = 0; j < 2; j++) {
        escreva(matriz_booleana[i][j], " ")
      }
      escreva("\n")
    }
    
    // Operações com matrizes
    inteiro soma_diagonal = 0
    para (inteiro i = 0; i < 3; i++) {
      soma_diagonal = soma_diagonal + matriz_numeros[i][i]
    }
    
    real media_aluno1 = 0.0
    para (inteiro j = 0; j < 4; j++) {
      media_aluno1 = media_aluno1 + notas_turma[0][j]
    }
    media_aluno1 = media_aluno1 / 4.0
    
    inteiro count_x = 0
    para (inteiro i = 0; i < 3; i++) {
      para (inteiro j = 0; j < 3; j++) {
        se (tabuleiro[i][j] == "X") {
          count_x++
        }
      }
    }
    
    escreva("\nOperações:\n")
    escreva("Soma da diagonal principal: ", soma_diagonal, "\n")
    escreva("Média do aluno 1: ", media_aluno1, "\n")
    escreva("Quantidade de 'X' no tabuleiro: ", count_x, "\n")
  }
}