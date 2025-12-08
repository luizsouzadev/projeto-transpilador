using System;

namespace AST
{
    public class NoRetorne : No
    {
        public No Valor { get; set; }

        public NoRetorne(No valor = null)
        {
            Valor = valor;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
