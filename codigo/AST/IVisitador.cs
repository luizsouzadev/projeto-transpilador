using System;

namespace AST
{
    /// <summary>
    /// Interface para o padrão Visitor na AST
    /// </summary>
    public interface IVisitador
    {
        void Visitar(NoBloco no);
        void Visitar(NoFuncao no);
        void Visitar(NoDeclaracao no);
        void Visitar(NoAtribuicao no);
        void Visitar(NoLiteral no);
        void Visitar(NoExpressao no);
        void Visitar(NoExpressaoBinaria no);
        void Visitar(NoEscreva no);
        void Visitar(NoLeia no);
        void Visitar(NoSe no);
        void Visitar(NoEnquanto no);
        void Visitar(NoPara no);
        void Visitar(NoRetorne no);
        void Visitar(NoChamadaFuncao no);
        void Visitar(NoIncremento no);
        void Visitar(NoEscolha no);
        void Visitar(NoCaso no);
        void Visitar(NoPare no);
        void Visitar(NoFacaEnquanto no);
        void Visitar(NoAcessoArray no);
        void Visitar(NoListaInicializacao no);
        void Visitar(NoAtribuicaoArray no);
    }
}
