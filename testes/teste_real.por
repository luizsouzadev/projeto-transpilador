/*  Descrição: 
 *  Este exemplo demonstra o uso de variáveis do tipo real
 *  com diferentes valores decimais e operações matemáticas.
 */

programa {
  funcao inicio() {
    // Declaração de variáveis reais
    real pi = 3.14159
    real temperatura = -5.7
    real altura = 1.75
    real peso = 70.5
    real zero_real = 0.0
    real numero_pequeno = 0.001
    
    // Exibindo os valores
    escreva("Pi: ", pi, "\n")
    escreva("Temperatura: ", temperatura, "°C\n")
    escreva("Altura: ", altura, "m\n")
    escreva("Peso: ", peso, "kg\n")
    escreva("Zero real: ", zero_real, "\n")
    escreva("Número pequeno: ", numero_pequeno, "\n")
    
    // Operações com reais
    real imc = peso / (altura * altura)
    real area_circulo = pi * 2.5 * 2.5
    real conversao_temp = (temperatura * 9.0 / 5.0) + 32.0
    
    escreva("\nOperações:\n")
    escreva("IMC (70.5 / (1.75²)): ", imc, "\n")
    escreva("Área do círculo (π * 2.5²): ", area_circulo, "\n")
    escreva("Temperatura em Fahrenheit: ", conversao_temp, "°F\n")
  }
}