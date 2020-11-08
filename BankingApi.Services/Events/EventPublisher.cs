using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankingApi.Services.Events
{
    public class EventPublisher : IEventPublisher
    {
        public ISubscriberService _subscriberService;

        public EventPublisher(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        public void Publish<T>(T eventData)
        {
            var subscriptions = _subscriberService.GetSubscriptions<T>();
            subscriptions.ToList().ForEach(consumer =>
            {
                try
                {
                    consumer.HandleEvent(eventData);
                }
                catch(Exception exHandleEvent)
                {
                    //Log exeption
                }
            });
        }
    }
}
