/*  Descrição: 
 *  Este exemplo demonstra o uso de PROCEDIMENTOS no Portugol Studio
 *  - procedimentos que não retornam valores.
 */

programa {
  
  // Procedimento simples sem parâmetros
  funcao exibir_titulo() {
    escreva("===============================\n")
    escreva("    SISTEMA DE PROCEDIMENTOS   \n")
    escreva("===============================\n")
  }
  
  // Procedimento com parâmetros
  funcao exibir_dados(cadeia nome, inteiro idade) {
    escreva("Nome: ", nome, "\n")
    escreva("Idade: ", idade, " anos\n")
  }
  
  // Procedimento que modifica variáveis globais por referência
  funcao trocar_valores(inteiro& a, inteiro& b) {
    inteiro temp = a
    a = b
    b = temp
  }

  
  funcao inicio() {
    escreva("=== PROCEDIMENTOS (SEM RETORNO) ===\n\n")
    
    // Chamando procedimento sem parâmetros
    exibir_titulo()
    
    // Chamando procedimento com parâmetros
    escreva("1. EXIBINDO DADOS:\n")
    exibir_dados("João", 25)
    escreva("\n")
    
    // Testando troca de valores
    escreva("2. TROCANDO VALORES:\n")
    inteiro x = 10
    inteiro y = 20
    escreva("Antes: x = ", x, ", y = ", y, "\n")
    trocar_valores(x, y)
    escreva("Depois: x = ", x, ", y = ", y, "\n\n")
    
  }
}