using System;

namespace AST
{
    /// <summary>
    /// Classe base para todos os nós da Árvore Sintática Abstrata
    /// </summary>
    public abstract class No
    {
        public abstract void Aceitar(IVisitador visitador);
    }
}
