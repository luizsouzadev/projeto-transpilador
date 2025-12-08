using System;

namespace AST
{
    public class NoExpressao : No
    {
        public string Identificador { get; set; }

        public NoExpressao(string identificador)
        {
            Identificador = identificador;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
