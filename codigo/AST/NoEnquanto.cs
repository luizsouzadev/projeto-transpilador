using System;

namespace AST
{
    public class NoEnquanto : No
    {
        public No Condicao { get; set; }
        public NoBloco Corpo { get; set; }

        public NoEnquanto(No condicao, NoBloco corpo)
        {
            Condicao = condicao;
            Corpo = corpo;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
