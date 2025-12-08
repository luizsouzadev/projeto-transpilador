using System;
using System.Collections.Generic;

namespace AST
{
    public class NoLeia : No
    {
        public List<string> Variaveis { get; set; }

        public NoLeia(List<string> variaveis)
        {
            Variaveis = variaveis;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
