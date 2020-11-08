using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Services.Events
{
    public interface IEventPublisher
    {
        void Publish<T>(T message);
    }
}
