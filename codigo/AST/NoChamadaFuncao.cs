using System;
using System.Collections.Generic;

namespace AST
{
    public class NoChamadaFuncao : No
    {
        public string Nome { get; set; }
        public List<No> Argumentos { get; set; }

        public NoChamadaFuncao(string nome, List<No> argumentos)
        {
            Nome = nome;
            Argumentos = argumentos;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
