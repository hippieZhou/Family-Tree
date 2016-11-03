using Microsoft.Practices.Prism.PubSubEvents;
using System;
namespace ZQ.Event
{
    public class Events
    { 
    }
    public class LoadModuleEvent : PubSubEvent<Type> { }
}
