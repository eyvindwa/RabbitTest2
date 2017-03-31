using System;
using System.Threading.Tasks;

namespace RabbitTest2
{
	public interface IRabbitKommunikator
	{
		Task Send(string program, string server, object message, string type, bool encrypted = false);
	}
}
