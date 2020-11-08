using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Services.Events
{
    public interface IConsumer<T>
    {
        void HandleEvent(T eventData);
    }
}
