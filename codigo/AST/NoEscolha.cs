using System;
using System.Collections.Generic;

namespace AST
{
    public class NoEscolha : No
    {
        public No Expressao { get; }
        public List<NoCaso> Casos { get; }
        public NoBloco CasoPadrao { get; }

        public NoEscolha(No expressao, List<NoCaso> casos, NoBloco casoPadrao = null)
        {
            Expressao = expressao;
            Casos = casos;
            CasoPadrao = casoPadrao;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
