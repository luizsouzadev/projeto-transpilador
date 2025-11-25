using System.IO;

namespace Transpilador
{
	public class EmissorCSharp
	{
		public string Emitir(string corpo)
		{
			var header = CabecalhoCSharp.Obter();
			var footer = CabecalhoCSharp.FecharNamespace();
			return header + corpo + footer;
		}

		public void SalvarArquivo(string caminho, string corpo)
		{
			File.WriteAllText(caminho, Emitir(corpo));
		}
	}
}
