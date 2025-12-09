using System.Collections.Generic;

namespace AST
{
    public class NoListaInicializacao : No
    {
        public List<No> Elementos { get; set; }

        public NoListaInicializacao(List<No> elementos)
        {
            Elementos = elementos;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
