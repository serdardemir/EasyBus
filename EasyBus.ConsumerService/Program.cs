using System;
using System.Linq;
using Topshelf;

namespace EasyBus.ConsumerService
{
    class Program
    {
        static void Main(string[] args)
        {

           var res= IocBootstrapper.Instance;
            HostFactory.Run(x =>
            {
                x.Service(() => new ConsumerService());
                x.RunAsLocalSystem();
                x.SetDescription("EasyBus Consumer Service");
                x.SetDisplayName(typeof(ConsumerService).Namespace);
                x.SetServiceName(typeof(ConsumerService).Namespace);
                x.UseNLog();
                x.StartAutomatically();
            });

            return;
        }
    }
}