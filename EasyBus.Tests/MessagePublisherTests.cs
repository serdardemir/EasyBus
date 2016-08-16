using EasyBus.Abstraction;
using EasyBus.Types.MessageTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyBus.Tests
{
    public class MessagePublisherTests : IClassFixture<MessagePublisherFixture>
    {
        MessagePublisherFixture fixture;
        public MessagePublisherTests(MessagePublisherFixture fixture)
        {
            this.fixture = fixture;
        }
        [Fact]
        public void Message_Emitter_Test()
        {
            var messageEmitter = fixture.Container.GetInstance<MessageEmitter>();

            messageEmitter.Emit(new LOGMessage()
             {
                 ErrorId = Guid.NewGuid().ToString(),
                 Source = "SomeCode",
                 SourceId = "SomeCode",
                 Type = "Extrange Exception",
                 Detail = "Error Detail",
                 Host = "google.com",
                 InfoUrl = "",
                 Message = "We have a problem..",
                 ServerVariables = new Dictionary<string, string> { { "something", "something" } },
                 StatusCode = "404",
                 Time = DateTime.Now,
                 User = "TheUser",
                 WebHostHtmlMessage = "Error Message"


             });



        }

    }
}
