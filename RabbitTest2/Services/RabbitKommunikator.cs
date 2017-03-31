using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Xamarin.Forms;

namespace RabbitTest2
{
	public class RabbitKommunikator : IRabbitKommunikator
	{
		String kønavn = "RabbitTest2@BRAINSLUG";
		IModel channel;
		EventingBasicConsumer eventingBasicConsumer;

		public RabbitKommunikator()
		{
			var connectionFactory = new ConnectionFactory { VirtualHost = "PLATTFORM_TEST", HostName = "", UserName = "", Password = "" };

			connectionFactory.AutomaticRecoveryEnabled = true;
			var conn = connectionFactory.CreateConnection();

			channel =  conn.CreateModel();
			var arguments = new Dictionary<string, object>
			{
				{ "alternate-exchange", "LOST_MESSAGES"}
			};
			channel.ExchangeDeclare("PLATTFORM", "direct", true, false, arguments);


			channel.QueueDeclare(kønavn, true, false, true, null);
			channel.QueueBind(kønavn, "PLATTFORM", kønavn);

			eventingBasicConsumer = new EventingBasicConsumer(channel);
			eventingBasicConsumer.Received += LesIntern;
			channel.BasicConsume(kønavn, true, eventingBasicConsumer);
		}

		private void LesIntern(object sender, BasicDeliverEventArgs e)
		{
			if (e != null)
			{
				var props = e.BasicProperties;
				//var program = Encoding.UTF8.GetString((byte[])props.Headers["Program"]);
				//var server = Encoding.UTF8.GetString((byte[])props.Headers["Server"]);
				//var type = Encoding.UTF8.GetString((byte[])props.Headers["QualifiedTypeName"]);

				var data = Encoding.UTF8.GetString(e.Body);

				MessagingCenter.Send(this, "RabbitMeldingMottatt", data);
			}
		}

		public async Task Send(string program, string server, object message, string type, bool encrypted = false)
		{
			await Task.Run(() =>
		   {
			   var json = JsonConvert.SerializeObject(message);
			   var split = kønavn.Split('@');
			   var senderProgram = split[0];
			   var senderServer = split[1];
			   byte[] utf8Data = Encoding.UTF8.GetBytes(json);
			   var props = channel.CreateBasicProperties();
			   props.Headers = new Dictionary<string, object> { { "Program", senderProgram }, { "Server", senderServer }, { "QualifiedTypeName", type } };
			   var id = $"{program.Trim()}@{server.Trim()}";
			   channel.BasicPublish("PLATTFORM", id, props, utf8Data);
		   });
		}
	}
}
