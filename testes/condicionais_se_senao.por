/*  Descrição: 
 *  Este exemplo demonstra o uso da estrutura condicional SE-SENAO
 *  com diferentes tipos de condições e comparações.
 */

programa {
  funcao inicio() {
    escreva("=== ESTRUTURA CONDICIONAL SE-SENAO ===\n\n")
    
    // 1. Condicional simples
    escreva("1. CONDICIONAL SIMPLES:\n")
    inteiro idade = 18
    escreva("Idade: ", idade, "\n")
    
    se (idade >= 18) {
      escreva("Resultado: Maior de idade\n")
    }
    escreva("\n")
    
    // 2. Condicional com SENAO
    escreva("2. CONDICIONAL COM SENAO:\n")
    inteiro numero = 7
    escreva("Número: ", numero, "\n")
    
    se (numero % 2 == 0) {
      escreva("Resultado: Número par\n")
    } senao {
      escreva("Resultado: Número ímpar\n")
    }
    escreva("\n")
    
    // 3. Condicionais aninhadas
    escreva("3. CONDICIONAIS ANINHADAS:\n")
    real nota = 8.5
    escreva("Nota: ", nota, "\n")
    
    se (nota >= 7.0) {
      se (nota >= 9.0) {
        escreva("Resultado: Excelente!\n")
      } senao {
        escreva("Resultado: Bom trabalho!\n")
      }
    } senao {
      se (nota >= 5.0) {
        escreva("Resultado: Precisa melhorar\n")
      } senao {
        escreva("Resultado: Insuficiente\n")
      }
    }
    escreva("\n")
    
    // 4. Múltiplas condições com operadores lógicos
    escreva("4. MÚLTIPLAS CONDIÇÕES:\n")
    inteiro temperatura = 25
    logico esta_sol = verdadeiro
    escreva("Temperatura: ", temperatura, "°C\n")
    escreva("Está sol: ", esta_sol, "\n")
    
    se (temperatura >= 20 e temperatura <= 30 e esta_sol) {
      escreva("Resultado: Perfeito para sair!\n")
    } senao se (temperatura > 30) {
      escreva("Resultado: Muito quente!\n")
    } senao se (temperatura < 15) {
      escreva("Resultado: Muito frio!\n")
    } senao {
      escreva("Resultado: Temperatura ok, mas sem sol\n")
    }
    escreva("\n")
    
    // 5. Validação de dados
    escreva("5. VALIDAÇÃO DE DADOS:\n")
    cadeia usuario = "admin"
    cadeia senha = "123456"
    escreva("Usuário: ", usuario, "\n")
    escreva("Senha: [oculta]\n")
    
    se (usuario == "admin" e senha == "123456") {
      escreva("Resultado: Login realizado com sucesso!\n")
    } senao se (usuario != "admin") {
      escreva("Resultado: Usuário inválido!\n")
    } senao {
      escreva("Resultado: Senha incorreta!\n")
    }
    escreva("\n")
    
    // 6. Comparação de strings
    escreva("6. COMPARAÇÃO DE STRINGS:\n")
    cadeia cor_favorita = "azul"
    escreva("Cor favorita: ", cor_favorita, "\n")
    
    se (cor_favorita == "azul") {
      escreva("Resultado: Cor do céu e do mar!\n")
    } senao se (cor_favorita == "verde") {
      escreva("Resultado: Cor da natureza!\n")
    } senao se (cor_favorita == "vermelho") {
      escreva("Resultado: Cor da paixão!\n")
    } senao {
      escreva("Resultado: Cor interessante!\n")
    }
    escreva("\n")
    
    // 7. Operador ternário simulado
    escreva("7. DECISÃO RÁPIDA:\n")
    inteiro pontos = 85
    cadeia resultado
    escreva("Pontos: ", pontos, "\n")
    
    se (pontos >= 70) {
      resultado = "Aprovado"
    } senao {
      resultado = "Reprovado"  
    }
    escreva("Resultado: ", resultado, "\n")
  }
}