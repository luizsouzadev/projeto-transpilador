using System;
using System.Collections.Generic;

namespace AST
{
    public class NoBloco : No
    {
        public List<No> Instrucoes { get; set; }

        public NoBloco(List<No> instrucoes = null)
        {
            Instrucoes = instrucoes ?? new List<No>();
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
