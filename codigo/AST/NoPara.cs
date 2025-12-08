using System;

namespace AST
{
    public class NoPara : No
    {
        public string VariavelInit { get; set; }
        public No ValorInit { get; set; }
        public No Condicao { get; set; }
        public string VariavelIncr { get; set; }
        public No ExpressaoIncr { get; set; }
        public NoBloco Corpo { get; set; }

        public NoPara(string varInit, No valorInit, No condicao, string varIncr, No exprIncr, NoBloco corpo)
        {
            VariavelInit = varInit;
            ValorInit = valorInit;
            Condicao = condicao;
            VariavelIncr = varIncr;
            ExpressaoIncr = exprIncr;
            Corpo = corpo;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
