using System;
using System.Collections.Generic;

namespace AST
{
    public class NoFuncao : No
    {
        public string Nome { get; set; }
        public string TipoRetorno { get; set; }
        public List<Tuple<string, string, bool>> Parametros { get; set; } // (tipo, nome, ehReferencia)
        public NoBloco Corpo { get; set; }

        public NoFuncao(string nome, string tipoRetorno, List<Tuple<string, string, bool>> parametros, NoBloco corpo)
        {
            Nome = nome;
            TipoRetorno = tipoRetorno;
            Parametros = parametros;
            Corpo = corpo;
        }

        public override void Aceitar(IVisitador visitador)
        {
            visitador.Visitar(this);
        }
    }
}
