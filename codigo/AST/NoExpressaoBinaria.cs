using System;

namespace AST
{
    public class NoExpressaoBinaria : No
    {
        public No Esquerda { get; set; }
        public string Operador { get; set; }
        public No Direita { get; set; }

        public NoExpressaoBinaria(No esquerda, string operador, No direita)
        {
            Esquerda = esquerda;
            Operador = operador;
            Direita = direita;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
