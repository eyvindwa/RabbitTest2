using System;

using Newtonsoft.Json;

namespace RabbitTest2
{
	public class Item : ObservableObject
	{
		public string Id { get; set; }

		public string Text { get; set; }

		public string Description { get; set; }

		//[Field(6)]
		public string Kode { get; set; } = "FXVALR";

		//[Field(36)]
		public string SamtaleId { get; set; }

		//[Field(10)]
		public int PasientId { get; set; }

		//[Field(1)]
		public string Kjønn { get; set; }

		//[Field(10)]
		public string FødselsDato { get; set; }

		//[Field(10)]
		public string PrøveId { get; set; }

		//[Field(10)]
		public string PrøveIdLIS { get; set; }

		//[Field(20)]
		public string RegistrertTidspunkt { get; set; }

		//[Field(20)]
		public string SistResultatTidspunkt { get; set; }

		//[Field(50)]
		public string LagerPosisjon { get; set; }

		//[Field(1)]
		public string ErRerunBestillt { get; set; }

		//[Field(10)]
		public string SerumHemolyse { get; set; }

		//[Field(10)]
		public string SerumLipemi { get; set; }

		//[Field(10)]
		public string SerumIcterus { get; set; }

		[JsonIgnore]
		public string SerumIndex
		{
			get
			{
				return "Hemolyse:" + SerumHemolyse?.Trim() + " Lipemi:" + SerumLipemi?.Trim() + " Iicterus:" + SerumLipemi?.Trim() + " Saline:" + Saline?.ToString("0.##");
			}
		}

		//[Field(10)]
		public decimal? Saline { get; set; }

		//[Field(2)]
		public int AntHemolysePSvar { get; set; }
		//[Field(2)]
		public int AntLipemiPSvar { get; set; }
		//[Field(2)]
		public int AntIcterusPSvar { get; set; }
		//[Field(2)]
		public int AntSalinePSvar { get; set; }

		//[Field(20)]
		public string AnalysetidHemolyse { get; set; }

		//[Field(20)]
		public string AnalysetidIcterus { get; set; }

		//[Field(20)]
		public string AnalysetidLipemi { get; set; }

		//[Field(20)]
		public string AnalysetidSaline { get; set; }

		//[Field(15)]
		public string SisteSted { get; set; }
	}
}
