/*  Descrição: 
 *  Este exemplo demonstra o uso de referências em Portugol Studio
 *  mostrando como passar parâmetros por referência em funções.
 */

programa {
  
  // Função que recebe parâmetros por valor (cópia)
  funcao modificar_por_valor(inteiro num, real valor, cadeia texto) {
    num = num * 2
    valor = valor + 10.0
    texto = "Modificado"
    escreva("Dentro da função por valor:\n")
    escreva("  num: ", num, "\n")
    escreva("  valor: ", valor, "\n")
    escreva("  texto: ", texto, "\n")
  }
  
  // Função que recebe parâmetros por referência
  funcao modificar_por_referencia(inteiro& num, real& valor, cadeia& texto) {
    num = num * 2
    valor = valor + 10.0
    texto = "Modificado por referência"
    escreva("Dentro da função por referência:\n")
    escreva("  num: ", num, "\n")
    escreva("  valor: ", valor, "\n")
    escreva("  texto: ", texto, "\n")
  }
  
  // Função que troca dois valores por referência
  funcao trocar_valores(inteiro& a, inteiro& b) {
    inteiro temp = a
    a = b
    b = temp
  }
  
  // Função que calcula área e perímetro de um retângulo por referência
  funcao calcular_retangulo(real largura, real altura, real& area, real& perimetro) {
    area = largura * altura
    perimetro = 2 * (largura + altura)
  }
  
  // Função que incrementa um contador por referência
  funcao incrementar_contador(inteiro& contador) {
    contador++
  }
  
  funcao inicio() {
    escreva("=== TESTE DE PASSAGEM POR VALOR ===\n")
    inteiro numero1 = 5
    real preco1 = 20.5
    cadeia nome1 = "Original"
    
    escreva("Antes da chamada por valor:\n")
    escreva("  numero1: ", numero1, "\n")
    escreva("  preco1: ", preco1, "\n")
    escreva("  nome1: ", nome1, "\n\n")
    
    modificar_por_valor(numero1, preco1, nome1)
    
    escreva("\nApós a chamada por valor:\n")
    escreva("  numero1: ", numero1, "\n")
    escreva("  preco1: ", preco1, "\n")
    escreva("  nome1: ", nome1, "\n")
    
    escreva("\n")
    para (inteiro i = 0; i < 50; i++) {
      escreva("=")
    }
    escreva("\n")
    
    escreva("=== TESTE DE PASSAGEM POR REFERÊNCIA ===\n")
    inteiro numero2 = 5
    real preco2 = 20.5
    cadeia nome2 = "Original"
    
    escreva("Antes da chamada por referência:\n")
    escreva("  numero2: ", numero2, "\n")
    escreva("  preco2: ", preco2, "\n")
    escreva("  nome2: ", nome2, "\n\n")
    
    modificar_por_referencia(numero2, preco2, nome2)
    
    escreva("\nApós a chamada por referência:\n")
    escreva("  numero2: ", numero2, "\n")
    escreva("  preco2: ", preco2, "\n")
    escreva("  nome2: ", nome2, "\n")
    
    escreva("\n")
    para (inteiro i = 0; i < 50; i++) {
      escreva("=")
    }
    escreva("\n")
    
    escreva("=== TESTE DE TROCA DE VALORES ===\n")
    inteiro x = 10
    inteiro y = 20
    
    escreva("Antes da troca:\n")
    escreva("  x: ", x, "\n")
    escreva("  y: ", y, "\n")
    
    trocar_valores(x, y)
    
    escreva("Após a troca:\n")
    escreva("  x: ", x, "\n")
    escreva("  y: ", y, "\n")
    
    para (inteiro i = 0; i < 50; i++) {
      escreva("=")
    }
    escreva("\n")
    
    escreva("=== TESTE DE MÚLTIPLOS RETORNOS ===\n")
    real largura = 5.0
    real altura = 3.0
    real area = 0.0
    real perimetro = 0.0
    
    escreva("Dimensões do retângulo:\n")
    escreva("  Largura: ", largura, "\n")
    escreva("  Altura: ", altura, "\n")
    
    calcular_retangulo(largura, altura, area, perimetro)
    
    escreva("Resultados calculados:\n")
    escreva("  Área: ", area, "\n")
    escreva("  Perímetro: ", perimetro, "\n")
    
    para (inteiro i = 0; i < 50; i++) {
      escreva("=")
    }
    escreva("\n")
    
    escreva("=== TESTE DE CONTADOR ===\n")
    inteiro contador = 0
    
    escreva("Contador inicial: ", contador, "\n")
    
    para (inteiro i = 0; i < 5; i++) {
      incrementar_contador(contador)
      escreva("Após incremento ", i + 1, ": ", contador, "\n")
    }
  }
}