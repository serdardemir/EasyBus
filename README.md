# EasyBus
Lightweight Message Bus for .Net Easy integration with multiple messaging frameworks including RabbitMQ, ActiveMQ ,maybe MSMQ


How to use?
========================
**Naming Convention for error strategy** 
......






**Pub / Sub Sample**
```cs
// Find subsriber. (RabbitMQ, ActiveMQ etc.)
var subscriber = container.GetInstance<ISubscriber>();

// Find all message handlers in running process
var messageHandlers = container.GetAllInstances<IMessageHandler>();

console.WriteLine(messageHandlers.Count() + " subscriber found.", ConsoleColor.Green);

//Start to subscribe (async) for all message handlers
foreach (var item in messageHandlers)
{                    
		subscriber.Subscribe(item);
	
}


```
**Specify how the policy should handle consumer faults**
```cs
<add key="RetryInterval" value="1" />
<add key="RetryCount" value="3" />
 
Policy.Execute<OperationResult>(() =>
{
	
}, message);

```
