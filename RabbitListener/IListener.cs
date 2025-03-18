namespace RabbitListener
{
	public interface IListener
	{
		public Task Recieve(string message);
	}
}
