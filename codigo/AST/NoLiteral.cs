using System;

namespace AST
{
    public class NoLiteral : No
    {
        public string Valor { get; set; }
        public string Tipo { get; set; } // inteiro, real, logico, cadeia

        public NoLiteral(string valor, string tipo)
        {
            Valor = valor;
            Tipo = tipo;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
