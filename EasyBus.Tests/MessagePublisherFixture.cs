using EasyBus.Abstraction;
using EasyBus.Abstraction.Contracts;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

