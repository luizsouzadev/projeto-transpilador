using System.Collections.Generic;

namespace AST
{
    public class NoAtribuicaoArray : No
    {
        public string Nome { get; set; }
        public List<No> Indices { get; set; }
        public No Valor { get; set; }

        public NoAtribuicaoArray(string nome, List<No> indices, No valor)
        {
            Nome = nome;
            Indices = indices;
            Valor = valor;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
