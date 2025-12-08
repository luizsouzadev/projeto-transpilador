using System;

namespace AST
{
    public class NoAtribuicao : No
    {
        public string Nome { get; set; }
        public No Valor { get; set; }

        public NoAtribuicao(string nome, No valor)
        {
            Nome = nome;
            Valor = valor;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
