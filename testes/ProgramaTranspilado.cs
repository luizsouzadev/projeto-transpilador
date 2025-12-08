using System;
using System.Collections.Generic;

namespace Transpilado
{
    public class ProgramaTranspilado
    {
        public static void Main()
        {
            int numero = 0;
            int resultado = 0;
            numero = 10;
            resultado = (((numero * 2) + 5) - 3);
            if (((resultado > 20)))
            {
            Console.WriteLine("Resultado maior que 20: " + " " + resultado);
            }
            else
            {
            Console.WriteLine("Resultado nao excede 20: " + " " + resultado);
            }
        }
    }
}
