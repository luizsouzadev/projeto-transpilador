using System;
using System.Collections.Generic;

namespace AST
{
    public class NoDeclaracao : No
    {
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public No Inicializacao { get; set; }
        public List<No> TamanhosArray { get; set; }

        public NoDeclaracao(string nome, string tipo, No inicializacao = null, List<No> tamanhosArray = null)
        {
            Nome = nome;
            Tipo = tipo;
            Inicializacao = inicializacao;
            TamanhosArray = tamanhosArray;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
