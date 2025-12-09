using System;

namespace AST
{
    public class NoCaso : No
    {
        public No Valor { get; }
        public NoBloco Corpo { get; }

        public NoCaso(No valor, NoBloco corpo)
        {
            Valor = valor;
            Corpo = corpo;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
