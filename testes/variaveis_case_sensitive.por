/*  Descrição: 
 *  Este exemplo demonstra que o Portugol diferencia maiúsculas de minúsculas
 *  nas variáveis (case-sensitive).
 */

programa {
  funcao inicio() {
    // Declarando variáveis com nomes similares mas diferentes casos
    inteiro a = 10
    inteiro A = 20
    cadeia nome = "joão"
    cadeia Nome = "Maria"
    cadeia NOME = "PEDRO"
    real valor = 5.5
    real Valor = 10.5
    real VALOR = 15.5
    
    escreva("=== DEMONSTRAÇÃO: CASE-SENSITIVE ===\n")
    escreva("Variáveis minúsculas:\n")
    escreva("  a = ", a, "\n")
    escreva("  nome = ", nome, "\n")
    escreva("  valor = ", valor, "\n")
    
    escreva("\nVariáveis com primeira letra maiúscula:\n")
    escreva("  A = ", A, "\n")
    escreva("  Nome = ", Nome, "\n")
    escreva("  Valor = ", Valor, "\n")
    
    escreva("\nVariáveis totalmente maiúsculas:\n")
    escreva("  NOME = ", NOME, "\n")
    escreva("  VALOR = ", VALOR, "\n")
    
    escreva("\n=== CONCLUSÃO ===\n")
    escreva("Como pode ver, 'a' e 'A' são variáveis diferentes!\n")
    escreva("O Portugol Studio diferencia maiúsculas de minúsculas.\n")
  }
}