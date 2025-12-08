/*  Descrição: 
 *  Este exemplo demonstra o uso de variáveis do tipo lógico
 *  com valores verdadeiro/falso e operações lógicas.
 */

programa {
  funcao inicio() {
    // Declaração de variáveis lógicas
    logico verdadeiro = verdadeiro
    logico falso = falso
    logico esta_chovendo = falso
    logico tem_dinheiro = verdadeiro
    logico maior_idade = verdadeiro
    
    // Exibindo os valores
    escreva("Verdadeiro: ", verdadeiro, "\n")
    escreva("Falso: ", falso, "\n")
    escreva("Está chovendo: ", esta_chovendo, "\n")
    escreva("Tem dinheiro: ", tem_dinheiro, "\n")
    escreva("Maior de idade: ", maior_idade, "\n")
    
    // Operações lógicas
    logico pode_sair = nao esta_chovendo e tem_dinheiro
    logico pode_dirigir = maior_idade e tem_dinheiro
    logico situacao_ruim = esta_chovendo ou nao tem_dinheiro
    
    escreva("\nOperações lógicas:\n")
    escreva("Pode sair (não chove E tem dinheiro): ", pode_sair, "\n")
    escreva("Pode dirigir (maior idade E tem dinheiro): ", pode_dirigir, "\n")
    escreva("Situação ruim (chove OU não tem dinheiro): ", situacao_ruim, "\n")
    
    // Comparações que resultam em valores lógicos
    inteiro idade = 25
    inteiro limite_idade = 18
    logico eh_adulto = idade >= limite_idade
    
    escreva("\nComparações:\n")
    escreva("Idade: ", idade, "\n")
    escreva("É adulto (idade >= 18): ", eh_adulto, "\n")
  }
}