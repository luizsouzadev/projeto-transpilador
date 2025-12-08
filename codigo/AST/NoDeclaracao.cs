using System;

namespace AST
{
    public class NoDeclaracao : No
    {
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public No Inicializacao { get; set; }

        public NoDeclaracao(string nome, string tipo, No inicializacao = null)
        {
            Nome = nome;
            Tipo = tipo;
            Inicializacao = inicializacao;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
