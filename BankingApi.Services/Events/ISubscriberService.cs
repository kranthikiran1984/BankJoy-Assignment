using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Services.Events
{
    public interface ISubscriberService
    {
        IList<IConsumer<T>> GetSubscriptions<T>();
    }
}
