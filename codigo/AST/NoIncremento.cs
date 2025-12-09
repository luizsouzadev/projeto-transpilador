using System;

namespace AST
{
    public class NoIncremento : No
    {
        public string Nome { get; }
        public int Delta { get; }

        public NoIncremento(string nome, int delta)
        {
            Nome = nome;
            Delta = delta;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
