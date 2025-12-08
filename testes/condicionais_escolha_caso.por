/*  Descrição: 
 *  Este exemplo demonstra o uso da estrutura ESCOLHA-CASO
 *  para múltiplas opções baseadas em valores específicos.
 */

programa {
  funcao inicio() {
    escreva("=== ESTRUTURA ESCOLHA-CASO ===\n\n")
    
    // 1. Escolha básica com números
    escreva("1. ESCOLHA COM NÚMEROS:\n")
    inteiro dia_semana = 3
    escreva("Dia da semana (1-7): ", dia_semana, "\n")
    
    escolha (dia_semana) {
      caso 1:
        escreva("Resultado: Domingo\n")
        pare
      caso 2:
        escreva("Resultado: Segunda-feira\n")
        pare
      caso 3:
        escreva("Resultado: Terça-feira\n")
        pare
      caso 4:
        escreva("Resultado: Quarta-feira\n")
        pare
      caso 5:
        escreva("Resultado: Quinta-feira\n")
        pare
      caso 6:
        escreva("Resultado: Sexta-feira\n")
        pare
      caso 7:
        escreva("Resultado: Sábado\n")
        pare
      caso contrario:
        escreva("Resultado: Dia inválido!\n")
    }
    escreva("\n")
    
    // 2. Escolha com caracteres
    escreva("2. ESCOLHA COM CARACTERES:\n")
    caracter opcao = 'B'
    escreva("Opção escolhida: ", opcao, "\n")
    
    escolha (opcao) {
      caso 'A':
        escreva("Resultado: Opção A - Cadastrar usuário\n")
        pare
      caso 'B':
        escreva("Resultado: Opção B - Listar usuários\n")
        pare
      caso 'C':
        escreva("Resultado: Opção C - Excluir usuário\n")
        pare
      caso 'S':
        escreva("Resultado: Opção S - Sair do sistema\n")
        pare
      caso contrario:
        escreva("Resultado: Opção inválida!\n")
    }
    escreva("\n")
    
    // 3. Calculadora simples
    escreva("3. CALCULADORA COM ESCOLHA:\n")
    real num1 = 10.0
    real num2 = 3.0
    caracter operacao = '/'
    escreva("Operação: ", num1, " ", operacao, " ", num2, "\n")
    
    escolha (operacao) {
      caso '+':
        escreva("Resultado: ", (num1 + num2), "\n")
        pare
      caso '-':
        escreva("Resultado: ", (num1 - num2), "\n")
        pare
      caso '*':
        escreva("Resultado: ", (num1 * num2), "\n")
        pare
      caso '/':
        se (num2 != 0) {
          escreva("Resultado: ", (num1 / num2), "\n")
        } senao {
          escreva("Erro: Divisão por zero!\n")
        }
        pare
      caso contrario:
        escreva("Erro: Operação inválida!\n")
    }
    escreva("\n")
    
    // 4. Sistema de notas
    escreva("4. SISTEMA DE NOTAS:\n")
    caracter conceito = 'A'
    escreva("Conceito: ", conceito, "\n")
    
    escolha (conceito) {
      caso 'A':
        escreva("Excelente! Nota entre 9.0 e 10.0\n")
        pare
      caso 'B':
        escreva("Muito bom! Nota entre 8.0 e 8.9\n")
        pare
      caso 'C':
        escreva("Bom! Nota entre 7.0 e 7.9\n")
        pare
      caso 'D':
        escreva("Regular! Nota entre 6.0 e 6.9\n")
        pare
      caso 'F':
        escreva("Insuficiente! Nota abaixo de 6.0\n")
        pare
      caso contrario:
        escreva("Conceito inválido!\n")
    }
    escreva("\n")

  }
}