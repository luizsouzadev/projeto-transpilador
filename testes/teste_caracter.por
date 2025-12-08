/*  Descrição: 
 *  Este exemplo demonstra o uso de variáveis do tipo caracter
 *  incluindo caracteres simples, strings e operações com texto.
 */

programa {
  funcao inicio() {
    // Declaração de variáveis do tipo caracter
    caracter letra = 'A'
    caracter simbolo = '@'
    caracter numero_char = '5'
    caracter espaco = ' '
    
    // Strings (sequências de caracteres)
    caracter nome = "João Silva"
    caracter sobrenome = "Santos"
    caracter frase = "Olá, mundo!"
    caracter texto_vazio = ""
    caracter texto_especial = "Acentuação: ção, ã, é"
    
    // Exibindo os valores
    escreva("Letra: ", letra, "\n")
    escreva("Símbolo: ", simbolo, "\n")
    escreva("Número como caracter: ", numero_char, "\n")
    escreva("Espaço: [", espaco, "]\n")
    
    escreva("\nStrings:\n")
    escreva("Nome: ", nome, "\n")
    escreva("Sobrenome: ", sobrenome, "\n")
    escreva("Frase: ", frase, "\n")
    escreva("Texto vazio: [", texto_vazio, "]\n")
    escreva("Texto com acentos: ", texto_especial, "\n")
    
    // Concatenação de strings
    caracter nome_completo = nome + " " + sobrenome
    caracter saudacao = "Bem-vindo, " + nome + "!"
    
    escreva("\nConcatenações:\n")
    escreva("Nome completo: ", nome_completo, "\n")
    escreva("Saudação: ", saudacao, "\n")
  }
}