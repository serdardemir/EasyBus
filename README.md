# EasyBus
Lightweight Message Bus for .Net Easy integration with multiple messaging frameworks including RabbitMQ, ActiveMQ ,maybe MSMQ

[![Build status](https://ci.appveyor.com/api/projects/status/809jr48poq9op086?svg=true)](https://ci.appveyor.com/project/serdardemir/easybus)


![EasyBus Logo](https://github.com/serdardemir/EasyBus/blob/master/Content/images/integration.png)


#### Easy to install
EasyBus.RabbitMQIntegration is on NuGet. To install it, run the following command in the Package Manager Console
```csharp
//**Install v1**
Install-Package EasyBus.RabbitMQIntegration
```

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
![EasyBus Consumer](https://github.com/serdardemir/EasyBus/blob/master/Content/images/easybusconsumer.png)

**Specify how the policy should handle consumer faults**

```cs
<add key="RetryInterval" value="1" />
<add key="RetryCount" value="3" />
 
Policy.Execute<OperationResult>(() =>
{
	
}, message);

```
