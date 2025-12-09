using System;

namespace AST
{
    public class NoFacaEnquanto : No
    {
        public NoBloco Corpo { get; }
        public No Condicao { get; }

        public NoFacaEnquanto(NoBloco corpo, No condicao)
        {
            Corpo = corpo;
            Condicao = condicao;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
