using System;

namespace AST
{
    public class NoPara : No
    {
        public No Inicializacao { get; }
        public No Condicao { get; }
        public No Atualizacao { get; }
        public NoBloco Corpo { get; }

        public NoPara(No inicializacao, No condicao, No atualizacao, NoBloco corpo)
        {
            Inicializacao = inicializacao;
            Condicao = condicao;
            Atualizacao = atualizacao;
            Corpo = corpo;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
