namespace Transpilador
{
    public static class CabecalhoCSharp
    {
        public static string Obter()
        {
            return "using System;\nusing System.Collections.Generic;\n\nnamespace Transpilado\n{\n";
        }

        public static string FecharNamespace()
        {
            return "\n}\n";
        }
    }
}
