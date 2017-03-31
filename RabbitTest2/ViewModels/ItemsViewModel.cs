using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace RabbitTest2
{
	public class ItemsViewModel : BaseViewModel
	{
		public ObservableRangeCollection<Item> Items { get; set; }
		public Command SendRabbitCommand { get; set; }

		public ItemsViewModel()
		{
			Title = "Prøver til validering";
			Items = new ObservableRangeCollection<Item>();
			SendRabbitCommand = new Command(async () => await SendRabbitMqMessage());


			MessagingCenter.Subscribe<RabbitKommunikator, String>(this, "RabbitMeldingMottatt", (RabbitKommunikator kommunikator, string melding) =>
			{
				if(String.IsNullOrWhiteSpace(melding))
					return;

				Item i = JsonConvert.DeserializeAnonymousType(melding, new Item());

				if(i.Kode == "FXVALR")
					Items.Add(i);
				else if(i.Kode == "FXVALE")
					IsBusy = false;
			});
		}

		async Task SendRabbitMqMessage()
		{
			IsBusy=true;
			Items.Clear();
			var rabbitKomm = DependencyService.Get<IRabbitKommunikator>();
			await rabbitKomm.Send("FLXSRV", "MORBO", new { Kode = "FXVALQ", InstrumentgruppeId = "4", SamtaleId = Guid.NewGuid().ToString() }, "FXVALQ");
		}
	}
}
