using System;

namespace AST
{
    public class NoPare : No
    {
        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
