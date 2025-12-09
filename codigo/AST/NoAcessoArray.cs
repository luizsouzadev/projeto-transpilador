using System.Collections.Generic;

namespace AST
{
    public class NoAcessoArray : No
    {
        public string Nome { get; set; }
        public List<No> Indices { get; set; }  // [idx] para 1D, [idx1, idx2] para 2D

        public NoAcessoArray(string nome, List<No> indices)
        {
            Nome = nome;
            Indices = indices;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
