using System;
using System.Linq;

namespace EasyBus.Abstraction.Contracts
{
    public interface IPublisher
    {
        void Publish(IMessage message);

        IResponseMessage Request<TRequest, TResponse>(TRequest request, Action<TResponse> onResponse)
            where TRequest : class
            where TResponse : class;
        
    }
}