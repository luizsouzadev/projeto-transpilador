using System;
using System.Collections.Generic;

namespace AST
{
    public class NoEscreva : No
    {
        public List<No> Argumentos { get; set; }

        public NoEscreva(List<No> argumentos)
        {
            Argumentos = argumentos;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
