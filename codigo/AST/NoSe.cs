using System;

namespace AST
{
    public class NoSe : No
    {
        public No Condicao { get; set; }
        public NoBloco BlocoSe { get; set; }
        public NoBloco BlocoSenao { get; set; }

        public NoSe(No condicao, NoBloco blocoSe, NoBloco blocoSenao = null)
        {
            Condicao = condicao;
            BlocoSe = blocoSe;
            BlocoSenao = blocoSenao;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
