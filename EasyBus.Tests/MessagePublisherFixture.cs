using SimpleInjector;

namespace EasyBus.Tests
{
	public class MessagePublisherFixture
	{
		public Container Container;

		public MessagePublisherFixture()
		{
			this.Container = IocBootstrapper.Instance;
		}
	}
}