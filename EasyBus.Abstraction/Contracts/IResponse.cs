using System;
using System.Linq;

namespace EasyBus.Abstraction.Contracts
{
    public interface IResponse
    {
        void Response<TRequest, TResponse>(Func<TRequest, TResponse> onResponse, IResponse responder);

        void StartRespond(ISubscriber subscriber);

        void CreateResponse(IRequestMessage message);
    }
}
