using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Services.Events
{
    public class SubscriberService : ISubscriberService
    {
        public IList<IConsumer<T>> GetSubscriptions<T>()
        {
            // Find all classes that inherit IConsumer<T>
            throw new NotImplementedException();
        }
    }
}
