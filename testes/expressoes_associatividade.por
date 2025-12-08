/*  Descrição: 
 *  Este exemplo demonstra a associatividade de operadores
 *  (esquerda para direita ou direita para esquerda).
 */

programa {
  funcao inicio() {
    escreva("=== ASSOCIATIVIDADE DE OPERADORES ===\n\n")
    
    // 1. Associatividade da esquerda para direita (maioria dos operadores)
    escreva("1. ASSOCIATIVIDADE ESQUERDA → DIREITA:\n")
    
    inteiro a = 20
    inteiro b = 10
    inteiro c = 5
    
    escreva("Valores: a = ", a, ", b = ", b, ", c = ", c, "\n\n")
    
    // Subtração (esquerda para direita)
    inteiro resultado1 = a - b - c  // (20 - 10) - 5 = 10 - 5 = 5
    escreva("a - b - c = ", a, " - ", b, " - ", c, " = ", resultado1, "\n")
    escreva("Avaliado como: (", a, " - ", b, ") - ", c, " = ", (a - b), " - ", c, " = ", resultado1, "\n\n")
    
    // Divisão (esquerda para direita)
    real x = 100.0
    real y = 5.0
    real z = 2.0
    real resultado2 = x / y / z  // (100 / 5) / 2 = 20 / 2 = 10
    escreva("x / y / z = ", x, " / ", y, " / ", z, " = ", resultado2, "\n")
    escreva("Avaliado como: (", x, " / ", y, ") / ", z, " = ", (x / y), " / ", z, " = ", resultado2, "\n\n")
    
    // Comparação da diferença se fosse direita para esquerda
    real hipotetico = x / (y / z)  // 100 / (5 / 2) = 100 / 2.5 = 40
    escreva("Se fosse direita→esquerda: x / (y / z) = ", x, " / (", y, " / ", z, ") = ", hipotetico, "\n\n")
    
    // 2. Operadores lógicos
    escreva("2. OPERADORES LÓGICOS (esquerda → direita):\n")
    logico p = verdadeiro
    logico q = falso
    logico r = verdadeiro
    
    escreva("Valores: p = ", p, ", q = ", q, ", r = ", r, "\n")
    
    logico resultado3 = p e q ou r  // (p e q) ou r = (verdadeiro e falso) ou verdadeiro = falso ou verdadeiro = verdadeiro
    escreva("p e q ou r = ", p, " e ", q, " ou ", r, " = ", resultado3, "\n")
    escreva("Avaliado como: (", p, " e ", q, ") ou ", r, " = ", (p e q), " ou ", r, " = ", resultado3, "\n\n")
    
    // 3. Concatenação de strings
    escreva("3. CONCATENAÇÃO DE STRINGS (esquerda → direita):\n")
    cadeia str1 = "A"
    cadeia str2 = "B"
    cadeia str3 = "C"
    
    cadeia resultado4 = str1 + str2 + str3  // ("A" + "B") + "C" = "AB" + "C" = "ABC"
    escreva("str1 + str2 + str3 = \"", str1, "\" + \"", str2, "\" + \"", str3, "\" = \"", resultado4, "\"\n")
    escreva("Avaliado como: (\"", str1, "\" + \"", str2, "\") + \"", str3, "\" = \"", (str1 + str2), "\" + \"", str3, "\" = \"", resultado4, "\"\n\n")
    
    // 4. Atribuição (direita para esquerda) - caso especial
    escreva("4. ATRIBUIÇÃO (direita → esquerda):\n")
    inteiro var1, var2, var3
    
    // Simulando atribuição múltipla (se suportada)
    var3 = 10
    var2 = var3
    var1 = var2
    
    escreva("var1 = var2 = var3 = 10\n")
    escreva("Resultado: var1 = ", var1, ", var2 = ", var2, ", var3 = ", var3, "\n")
    escreva("(atribuição acontece da direita para esquerda)\n\n")
    
    // 5. Exemplo prático da importância da associatividade
    escreva("5. EXEMPLO PRÁTICO:\n")
    real preco = 100.0
    real desconto1 = 0.1  // 10%
    real desconto2 = 0.05 // 5%
    
    real preco_final1 = preco * (1 - desconto1) * (1 - desconto2)  // Desconto sequencial
    real preco_final2 = preco * (1 - desconto1 - desconto2)        // Desconto somado
    
    escreva("Preço original: R$ ", preco, "\n")
    escreva("Descontos sequenciais: R$ ", preco_final1, "\n")
    escreva("Descontos somados: R$ ", preco_final2, "\n")
    escreva("Diferença: R$ ", preco_final2 - preco_final1, "\n")
  }
}