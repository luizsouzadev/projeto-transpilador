/*  Descrição: 
 *  Este exemplo demonstra o uso de FUNÇÕES no Portugol Studio
 *  - funções que retornam valores.
 */

programa {
  
  // Função que retorna a soma de dois números
  funcao inteiro somar(inteiro a, inteiro b) {
    retorne a + b
  }
  
  // Função que retorna o maior entre dois números
  funcao inteiro maior(inteiro x, inteiro y) {
    se (x > y) {
      retorne x
    } senao {
      retorne y
    }
  }
  
  // Função que calcula área do círculo
  funcao real area_circulo(real raio) {
    real pi = 3.14159
    retorne pi * raio * raio
  }
  
  // Função que verifica se número é par
  funcao logico eh_par(inteiro numero) {
    retorne (numero % 2 == 0)
  }
  
  // Função que retorna saudação
  funcao cadeia saudar(cadeia nome) {
    retorne "Olá, " + nome + "!"
  }
  
  // Função recursiva - fatorial
  funcao inteiro fatorial(inteiro n) {
    se (n <= 1) {
      retorne 1
    } senao {
      retorne n * fatorial(n - 1)
    }
  }
  
  funcao inicio() {
    escreva("=== FUNÇÕES (COM RETORNO) ===\n\n")
    
    // Testando função de soma
    inteiro resultado1 = somar(10, 5)
    escreva("somar(10, 5) = ", resultado1, "\n")
    
    // Testando função maior
    inteiro resultado2 = maior(8, 12)
    escreva("maior(8, 12) = ", resultado2, "\n")
    
    // Testando área do círculo
    real area = area_circulo(5.0)
    escreva("area_circulo(5.0) = ", area, "\n")
    
    // Testando função lógica
    logico par = eh_par(7)
    escreva("eh_par(7) = ", par, "\n")
    
    // Testando função de string
    cadeia saudacao = saudar("Maria")
    escreva("saudar(\"Maria\") = ", saudacao, "\n")
    
    // Testando função recursiva
    inteiro fat = fatorial(5)
    escreva("fatorial(5) = ", fat, "\n")
    
    escreva("\n=== CARACTERÍSTICAS DAS FUNÇÕES ===\n")
    escreva("• Têm tipo de retorno (inteiro, real, logico, cadeia)\n")
    escreva("• Usam a palavra RETORNE\n")
    escreva("• Podem receber parâmetros\n")
    escreva("• Retornam exatamente um valor\n")
    escreva("• Podem ser usadas em expressões\n")
  }
}