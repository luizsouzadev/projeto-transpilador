/*  Descrição: 
 *  Este exemplo demonstra ERRO PROPOSITAL usando caracteres especiais
 *  em nomes de variáveis. O caractere 'ç' causará erro de compilação.
 */

programa {
  funcao inicio() {
    // Variáveis válidas
    cadeia nome = "João"
    inteiro idade = 25
    
    // ERRO PROPOSITAL: variável com caractere especial 'ç'
    // Esta linha causará erro de compilação!
    cadeia atribuição = "Pedro"
    
    // Esta linha também não será executada devido ao erro acima
    escreva("Nome: ", nome, "\n")
    escreva("Idade: ", idade, "\n")
    escreva("Nome com ç: ", atribuição, "\n")
  }
}